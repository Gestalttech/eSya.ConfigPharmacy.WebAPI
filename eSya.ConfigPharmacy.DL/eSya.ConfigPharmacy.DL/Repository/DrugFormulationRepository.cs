using eSya.ConfigPharmacy.DL.Entities;
using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DL.Repository
{
    public class DrugFormulationRepository: IDrugFormulationRepository
    {
        private readonly IStringLocalizer<DrugFormulationRepository> _localizer;
        public DrugFormulationRepository(IStringLocalizer<DrugFormulationRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Drug Formulation

       
        public async Task<List<DO_Composition>> GetActiveCompositionforTreeview(string prefix)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var ds = db.GtEphdrcs.Where(x=>x.ActiveStatus && x.DrugCompDesc.ToUpper().StartsWith(prefix.ToUpper()) || prefix == "")
                    .Select(r => new DO_Composition
                    {
                        CompositionId = r.CompositionId,
                        DrugCompDesc = r.DrugCompDesc,
                    }).OrderBy(o => o.DrugCompDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_DrugFormulation>> GetDrugFormulationInfobyCompositionID(int composId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEphdfrs
                   .Join(db.GtEcapcds,
                     g => g.DrugForm,
                     c => c.ApplicationCode,
                     (g, c) => new { g, c }
                     )
                   .Join(db.GtEcapcds,
                     gc => gc.g.MethodOfAdministration,
                     p => p.ApplicationCode,
                     (gc, p) => new { gc, p } )
                   .Where(w => (w.gc.g.CompositionId == composId))
                   .Select(r => new DO_DrugFormulation
                   {
                       CompositionId = r.gc.g.CompositionId,
                       FormulationId = r.gc.g.FormulationId,
                       FormulationDesc = r.gc.g.FormulationDesc,
                       Volume = r.gc.g.Volume,
                       DrugForm = r.gc.g.DrugForm,
                       DrugFormDesc = r.gc.c.CodeDesc,
                       MethodOfAdministration = r.gc.g.MethodOfAdministration,
                       MethodOfAdministrationDesc = r.p.CodeDesc,
                       ActiveStatus = r.gc.g.ActiveStatus,
                   }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_DrugFormulation> GetDrugFormulationParamsbyFormulationID(int composId, int formulationId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEphdfrs.Where(h => h.CompositionId == composId && h.FormulationId == formulationId)
                         .Select(r => new DO_DrugFormulation
                         {
                             FormulationId = r.FormulationId,
                             CompositionId = r.CompositionId,
                             lst_formulationparams = db.GtEphdfps.Where(h => h.CompositionId == composId && h.FormulationId == formulationId)
                            .Select(p => new DO_eSyaParameter
                            {
                                ParameterID = p.ParameterId,
                                ParmPerct=p.ParmPerc,
                                ParmAction = p.ParmAction,
                                ParmDesc=p.ParmDesc,
                                ParmValue=p.ParmValue
                            }).ToList()

                         }).FirstOrDefaultAsync();
                        return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDrugFormulation(DO_DrugFormulation obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtEphdfr _formula = db.GtEphdfrs.Where(x => x.CompositionId == obj.CompositionId && x.FormulationId==obj.FormulationId).FirstOrDefault();
                        if (_formula == null)
                        {
                            int maxformulationId = db.GtEphdfrs.Select(c => c.FormulationId).DefaultIfEmpty().Max() + 1;
                            _formula = new GtEphdfr
                            {
                                CompositionId = obj.CompositionId,
                                FormulationId = maxformulationId,
                                FormulationDesc = obj.FormulationDesc,
                                DrugForm = obj.DrugForm,
                                Volume = obj.Volume,
                                MethodOfAdministration = obj.MethodOfAdministration,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEphdfrs.Add(_formula);

                            foreach (DO_eSyaParameter ip in obj.lst_formulationparams)
                            {

                                var _parm = new GtEphdfp
                                {
                                    CompositionId = obj.CompositionId,
                                    FormulationId = maxformulationId,
                                    ParameterId = ip.ParameterID,
                                    ParmPerc = ip.ParmPerct,
                                    ParmDesc = ip.ParmDesc,
                                    ParmValue = ip.ParmValue,
                                    ParmAction = ip.ParmAction,
                                    ActiveStatus = ip.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,
                                };
                                db.GtEphdfps.Add(_parm);
                            }

                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0216", Message = string.Format(_localizer[name: "W0216"]) };
                        }
                    }

                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;

                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateDrugFormulation(DO_DrugFormulation obj)
        {

            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdfr _formulation = db.GtEphdfrs.Where(x => x.CompositionId == obj.CompositionId && x.FormulationId==obj.FormulationId).FirstOrDefault();
                        if (_formulation != null)
                        {

                            _formulation.FormulationDesc = obj.FormulationDesc;
                            _formulation.DrugForm = obj.DrugForm;
                            _formulation.Volume = obj.Volume;
                            _formulation.MethodOfAdministration = obj.MethodOfAdministration;
                            _formulation.ActiveStatus = obj.ActiveStatus;
                            _formulation.ModifiedBy = obj.UserID;
                            _formulation.ModifiedOn = System.DateTime.Now;
                            _formulation.ModifiedTerminal = obj.TerminalID;

                            foreach (DO_eSyaParameter ip in obj.lst_formulationparams)
                            {
                                GtEphdfp sPar = db.GtEphdfps.Where(x => x.CompositionId == obj.CompositionId && x.FormulationId==obj.FormulationId && x.ParameterId == ip.ParameterID).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmPerc = ip.ParmPerct;
                                    sPar.ParmDesc = ip.ParmDesc;
                                    sPar.ParmValue = ip.ParmValue;
                                    sPar.ParmAction = ip.ParmAction;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                }
                                else
                                {
                                    var _parm = new GtEphdfp
                                    {
                                        FormulationId=obj.FormulationId,
                                        CompositionId = obj.CompositionId,
                                        ParameterId = ip.ParameterID,
                                        ParmPerc = ip.ParmPerct,
                                        ParmDesc = ip.ParmDesc,
                                        ParmValue = ip.ParmValue,
                                        ParmAction = ip.ParmAction,
                                        ActiveStatus = ip.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,
                                    };
                                    db.GtEphdfps.Add(_parm);
                                }
                            }
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0217", Message = string.Format(_localizer[name: "W0217"]) };

                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region Map Formulation to Manufacturer
        public async Task<List<DO_DrugFormulation>> GetActiveFormulations()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var ds = db.GtEphdfrs.Where(x => x.ActiveStatus)
                    .Select(f => new DO_DrugFormulation
                    {
                        FormulationId = f.FormulationId,
                        FormulationDesc = f.FormulationDesc,
                    }).OrderBy(o => o.FormulationDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_Composition> GetCompositionbyFormulationID(int formulationId)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var ds = db.GtEphdfrs
                    .Join(db.GtEphdrcs,
                      g => g.CompositionId,
                      c => c.CompositionId,
                      (g, c) => new { g, c })
                    .Where(w => (w.g.FormulationId==formulationId))
                    .Select(f => new DO_Composition
                    {
                        CompositionId = f.g.CompositionId,
                        DrugCompDesc = f.c.DrugCompDesc,
                    }).FirstOrDefaultAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_MapFormulationManufacturer>> GetLinkedManufacturerwithFormulation(int formulationId,int compositionId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEphmnfs.Where(x => x.ActiveStatus == true)
                   .GroupJoin(db.GtEphdfms.Where(w => w.FormulationId == formulationId && w.CompositionId==compositionId),
                     d => d.ManufacturerId,
                     l => l.ManufacturerId,
                    (f, m) => new { f, m })
                   .SelectMany(z => z.m.DefaultIfEmpty(),
                    (a, b) => new DO_MapFormulationManufacturer
                    {
                        ManufacturerId =a.f.ManufacturerId,
                        ManufacturerName=a.f.ManufacturerName,
                        FormulationId = formulationId,
                        CompositionId =compositionId,
                        ActiveStatus = b == null ? false : b.ActiveStatus
                    }).ToListAsync();
                    var distManufact = ds.GroupBy(x => new { x.ManufacturerId, x.FormulationId, x.CompositionId })
                       .Select(y => y.First());
                    return distManufact.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateManufacturerLinkwithFormulation(DO_MapFormulationManufacturer obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {


                        foreach (var f in obj.manfctlist)
                        {
                            var manfc = db.GtEphdfms.Where(x => x.FormulationId ==f.FormulationId && x.CompositionId==f.CompositionId && x.ManufacturerId==f.ManufacturerId).FirstOrDefault();
                            if (manfc != null)
                            {

                                manfc.ActiveStatus = f.ActiveStatus;
                                manfc.ModifiedBy = f.UserID;
                                manfc.ModifiedOn = System.DateTime.Now;
                                manfc.ModifiedTerminal = f.TerminalID;

                            }
                            else
                            {

                                var mapmanufct = new GtEphdfm
                                {
                                    FormulationId=f.FormulationId,
                                    CompositionId=f.CompositionId,
                                    ManufacturerId = f.ManufacturerId,
                                    ActiveStatus = f.ActiveStatus,
                                    CreatedBy = obj.UserID,
                                    FormId = obj.FormID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtEphdfms.Add(mapmanufct);


                            }
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        #endregion
    }
}

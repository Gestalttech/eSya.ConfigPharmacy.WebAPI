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
    public class CompositionRepository: ICompositionRepository
    {
        private readonly IStringLocalizer<CompositionRepository> _localizer;
        public CompositionRepository(IStringLocalizer<CompositionRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Drug Composition
        public async Task<List<DO_Composition>> GetCompositionByPrefix(string prefix)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {

                    var ds = db.GtEphdrcs
                    .GroupJoin(db.GtEcapcds,
                      g =>new { g.DrugClass },
                      c =>new { DrugClass = c.ApplicationCode },
                      (g, c) => new { g, c = c.FirstOrDefault() }
                      )
                    .GroupJoin(db.GtEcapcds,
                      gc =>new { gc.g.TherapueticClass },
                      p =>new { TherapueticClass = p.ApplicationCode },
                      (gc, p) => new { gc, p = p.FirstOrDefault() }
                      )
                    .GroupJoin(db.GtEcapcds,
                      gcc => new {gcc.gc.g.PharmacyGroup },
                      pc => new { PharmacyGroup = pc.ApplicationCode },
                      (gcc, pc) => new { gcc, pc = pc.FirstOrDefault() }
                      )
                    .Where(w => (w.gcc.gc.g.DrugCompDesc.ToUpper().StartsWith(prefix.ToUpper()) || prefix == "All"))
                    .Select(r => new DO_Composition
                    {
                        CompositionId = r.gcc.gc.g.CompositionId,
                        IsCombination = r.gcc.gc.g.IsCombination,
                        DrugCompDesc = r.gcc.gc.g.DrugCompDesc,
                        DrugClass = r.gcc.gc.g.DrugClass,
                        AvailableAsGeneric = r.gcc.gc.g.AvailableAsGeneric,
                        DrugSchedule = r.gcc.gc.g.DrugSchedule,
                        TherapueticClass = r.gcc.gc.g.TherapueticClass,
                        PharmacyGroup= r.gcc.gc.g.PharmacyGroup,
                        ActiveStatus = r.gcc.gc.g.ActiveStatus,
                        DrugClassDesc = r.gcc.gc.c.CodeDesc,
                        TherapueticClassDesc = r.gcc.p.CodeDesc,
                        PharmacyGroupDesc=r.pc.CodeDesc
                    }).OrderBy(o => o.DrugCompDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_Composition> GetCompositionInfo(int composId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = db.GtEphdrcs
                   .GroupJoin(db.GtEcapcds,
                     g => new { g.DrugClass },
                     c => new { DrugClass = c.ApplicationCode },
                     (g, c) => new { g, c = c.FirstOrDefault() }
                     )
                   .GroupJoin(db.GtEcapcds,
                     gc => new { gc.g.TherapueticClass },
                     p => new { TherapueticClass = p.ApplicationCode },
                     (gc, p) => new { gc, p = p.FirstOrDefault() }
                     )
                   .GroupJoin(db.GtEcapcds,
                     gcc => new { gcc.gc.g.PharmacyGroup },
                     pc => new { PharmacyGroup = pc.ApplicationCode },
                     (gcc, pc) => new { gcc, pc = pc.FirstOrDefault() }
                     )
                   .Where(w => (w.gcc.gc.g.CompositionId== composId))
                   .Select(r => new DO_Composition
                   {
                       CompositionId = r.gcc.gc.g.CompositionId,
                       IsCombination = r.gcc.gc.g.IsCombination,
                       DrugCompDesc = r.gcc.gc.g.DrugCompDesc,
                       DrugClass = r.gcc.gc.g.DrugClass,
                       AvailableAsGeneric = r.gcc.gc.g.AvailableAsGeneric,
                       DrugSchedule = r.gcc.gc.g.DrugSchedule,
                       TherapueticClass = r.gcc.gc.g.TherapueticClass,
                       PharmacyGroup = r.gcc.gc.g.PharmacyGroup,
                       ActiveStatus = r.gcc.gc.g.ActiveStatus,
                       DrugClassDesc = r.gcc.gc.c.CodeDesc,
                       TherapueticClassDesc = r.gcc.p.CodeDesc,
                       PharmacyGroupDesc = r.pc.CodeDesc,
                       l_composionparams = db.GtEphdcps.Where(h => h.CompositionId == composId)
                       .Select(p => new DO_eSyaParameter
                        {
                            ParameterID = p.ParameterId,
                            ParmAction = p.ParmAction,
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

        public async Task<DO_ReturnParameter> InsertComposition(DO_Composition obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdrc is_compositionExists = db.GtEphdrcs.FirstOrDefault(u => u.DrugCompDesc.ToUpper().Replace(" ", "") == obj.DrugCompDesc.ToUpper().Replace(" ", ""));
                        if (is_compositionExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0211", Message = string.Format(_localizer[name: "W0211"]) };
                        }
                        GtEphdrc _comp = db.GtEphdrcs.Where(x => x.CompositionId == obj.CompositionId ).FirstOrDefault();
                        if (_comp == null)
                        {
                            int maxcompId = db.GtEphdrcs.Select(c => c.CompositionId).DefaultIfEmpty().Max()+1;
                            _comp = new GtEphdrc
                            {
                                CompositionId=maxcompId,
                                IsCombination = obj.IsCombination,
                                DrugCompDesc = obj.DrugCompDesc,
                                DrugClass=obj.DrugClass,
                                TherapueticClass=obj.TherapueticClass,
                                AvailableAsGeneric=obj.AvailableAsGeneric,
                                DrugSchedule=obj.DrugSchedule,
                                PharmacyGroup=obj.PharmacyGroup,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEphdrcs.Add(_comp);

                            foreach (DO_eSyaParameter ip in obj.l_composionparams)
                            {

                                var _parm = new GtEphdcp
                                {
                                    CompositionId = obj.CompositionId,
                                    ParameterId = ip.ParameterID,
                                    ParmPerc = 0,
                                    ParmDesc = null,
                                    ParmValue = 0,
                                    ParmAction = ip.ParmAction,
                                    ActiveStatus = ip.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,
                                };
                                db.GtEphdcps.Add(_parm);
                            }

                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0210", Message = string.Format(_localizer[name: "W0210"]) };
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

        public async Task<DO_ReturnParameter> UpdateComposition(DO_Composition obj)
        {

            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdrc _comp = db.GtEphdrcs.Where(x => x.CompositionId == obj.CompositionId).FirstOrDefault();

                        GtEphdrc is_compositionExists = db.GtEphdrcs.FirstOrDefault(u => u.DrugCompDesc.ToUpper().Replace(" ", "") == obj.DrugCompDesc.ToUpper().Replace(" ", "") && u.CompositionId != obj.CompositionId);
                        if (is_compositionExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0211", Message = string.Format(_localizer[name: "W0211"]) };
                        }
                        if (_comp != null)
                        {

                            _comp.IsCombination = obj.IsCombination;
                            _comp.DrugCompDesc = obj.DrugCompDesc;
                            _comp.DrugClass = obj.DrugClass;
                            _comp.TherapueticClass = obj.TherapueticClass;
                            _comp.AvailableAsGeneric = obj.AvailableAsGeneric;
                            _comp.DrugSchedule = obj.DrugSchedule;
                            _comp.PharmacyGroup = obj.PharmacyGroup;
                            _comp.ActiveStatus = obj.ActiveStatus;
                            _comp.ModifiedBy = obj.UserID;
                            _comp.ModifiedOn = System.DateTime.Now;
                            _comp.ModifiedTerminal = obj.TerminalID;

                            foreach (DO_eSyaParameter ip in obj.l_composionparams)
                            {
                                GtEphdcp sPar = db.GtEphdcps.Where(x => x.CompositionId == obj.CompositionId && x.ParameterId == ip.ParameterID).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmPerc = 0;
                                    sPar.ParmDesc = null;
                                    sPar.ParmValue = 0;
                                    sPar.ParmAction = ip.ParmAction;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                }
                                else
                                {
                                    var _parm = new GtEphdcp
                                    {
                                        CompositionId = obj.CompositionId,
                                        ParameterId = ip.ParameterID,
                                        ParmPerc = 0,
                                        ParmDesc = null,
                                        ParmValue = 0,
                                        ParmAction = ip.ParmAction,
                                        ActiveStatus = ip.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,
                                    };
                                    db.GtEphdcps.Add(_parm);
                                }
                            }
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0209", Message = string.Format(_localizer[name: "W0209"]) };

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
    }
}

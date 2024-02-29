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
    public class DrugClassRepository: IDrugClassRepository
    {
        private readonly IStringLocalizer<DrugClassRepository> _localizer;
        public DrugClassRepository(IStringLocalizer<DrugClassRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Drug Class
        public async Task<List<DO_DrugClass>> GetAllDrugClass()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var ds = await db.GtEphdcos
                        .Select(d => new DO_DrugClass
                        {
                            DrugClass = d.DrugClass,
                            DrugClassDesc = d.DrugClassDesc,
                            ActiveStatus = d.ActiveStatus
                        }).OrderBy(o => o.DrugClassDesc).ToListAsync();

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDrugClass(DO_DrugClass obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdco is_drugclassExists = db.GtEphdcos.FirstOrDefault(u => u.DrugClassDesc.ToUpper().Replace(" ", "") == obj.DrugClassDesc.ToUpper().Replace(" ", ""));
                        if (is_drugclassExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0212", Message = string.Format(_localizer[name: "W0212"]) };
                        }
                        
                        int _drugclassID = db.GtEphdcos.Select(c => c.DrugClass).DefaultIfEmpty().Max();
                        _drugclassID = _drugclassID + 1;

                        var drugclass = new GtEphdco
                        {
                            DrugClass = _drugclassID,
                            DrugClassDesc = obj.DrugClassDesc,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtEphdcos.Add(drugclass);
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> UpdateIntoDrugClass(DO_DrugClass obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdco is_drugclassExists = db.GtEphdcos.FirstOrDefault(u => u.DrugClassDesc.ToUpper().Replace(" ", "") == obj.DrugClassDesc.ToUpper().Replace(" ", "") && u.DrugClass != obj.DrugClass);
                        if (is_drugclassExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0212", Message = string.Format(_localizer[name: "W0212"]) };
                        }

                        GtEphdco drugclass = db.GtEphdcos.Where(m => m.DrugClass == obj.DrugClass).FirstOrDefault();
                        if (drugclass != null)
                        {
                            drugclass.DrugClassDesc = obj.DrugClassDesc;
                            drugclass.ActiveStatus = obj.ActiveStatus;
                            drugclass.ModifiedBy = obj.UserID;
                            drugclass.ModifiedOn = System.DateTime.Now;
                            drugclass.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0213", Message = string.Format(_localizer[name: "W0213"]) };

                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveDrugClass(bool status, int drugclassId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdco drugclass = db.GtEphdcos.Where(w => w.DrugClass == drugclassId).FirstOrDefault();
                        if (drugclass == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0213", Message = string.Format(_localizer[name: "W0213"]) };
                        }

                        drugclass.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

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

        #region Drug Therapeutic
        public async Task<List<DO_DrugTherapeutic>> GetAllDrugTherapeutics()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var ds = await db.GtEphdtcs
                        .Select(t => new DO_DrugTherapeutic
                        {
                            DrugTherapeutic = t.DrugTherapeutic,
                            DrugTherapeuticDesc = t.DrugTherapeuticDesc,
                            ActiveStatus = t.ActiveStatus
                        }).OrderBy(o => o.DrugTherapeuticDesc).ToListAsync();

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDrugTherapeutic(DO_DrugTherapeutic obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        //GtEphdtc is_therapfExists = db.GtEphdtcs.FirstOrDefault(u => u.DrugTherapeuticDesc.ToUpper().Replace(" ", "") == obj.DrugTherapeuticDesc.ToUpper().Replace(" ", ""));
                        //if (is_therapfExists != null)
                        //{
                        //    return new DO_ReturnParameter() { Status = false, StatusCode = "W0214", Message = string.Format(_localizer[name: "W0214"]) };
                        //}
                        
                        int _therapID = db.GtEphdtcs.Select(c => c.DrugTherapeutic).DefaultIfEmpty().Max();
                        _therapID = _therapID + 1;

                        var therapeutic = new GtEphdtc
                        {
                            DrugTherapeutic = _therapID,
                            DrugTherapeuticDesc = obj.DrugTherapeuticDesc,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtEphdtcs.Add(therapeutic);
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> UpdateDrugTherapeutic(DO_DrugTherapeutic obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        //GtEphdtc is_threupExists = db.GtEphdtcs.FirstOrDefault(u => u.DrugTherapeuticDesc.ToUpper().Replace(" ", "") == obj.DrugTherapeuticDesc.ToUpper().Replace(" ", "") && u.DrugTherapeutic != obj.DrugTherapeutic);
                        //if (is_threupExists != null)
                        //{
                        //    return new DO_ReturnParameter() { Status = false, StatusCode = "W0214", Message = string.Format(_localizer[name: "W0214"]) };
                        //}

                        GtEphdtc threp = db.GtEphdtcs.Where(m => m.DrugTherapeutic == obj.DrugTherapeutic).FirstOrDefault();
                        if (threp != null)
                        {
                            threp.DrugTherapeuticDesc = obj.DrugTherapeuticDesc;
                            threp.ActiveStatus = obj.ActiveStatus;
                            threp.ModifiedBy = obj.UserID;
                            threp.ModifiedOn = System.DateTime.Now;
                            threp.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0215", Message = string.Format(_localizer[name: "W0215"]) };

                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveDrugTherapeutic(bool status, int drugthrapId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphdtc thrap = db.GtEphdtcs.Where(w => w.DrugTherapeutic == drugthrapId).FirstOrDefault();
                        if (thrap == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0215", Message = string.Format(_localizer[name: "W0215"]) };
                        }

                        thrap.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

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

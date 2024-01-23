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
    
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IStringLocalizer<ManufacturerRepository> _localizer;
        public ManufacturerRepository(IStringLocalizer<ManufacturerRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Manufacturer
        public async Task<List<DO_Manufacturer>> GetManufacturerListByNamePrefix(string manufacturerNamePrefix)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var ds =await db.GtEphmnfs
                        .Where(w => w.ManufacturerName.StartsWith(manufacturerNamePrefix != "All" ? manufacturerNamePrefix : ""))
                        .Select(r => new DO_Manufacturer
                        {
                            ManufacturerId = r.ManufacturerId,
                            ManfShortName = r.ManfShortName,
                            ManufacturerName = r.ManufacturerName,
                            ActiveStatus = r.ActiveStatus
                        }).OrderBy(o => o.ManufacturerName).ToListAsync();
                    
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertManufacturer(DO_Manufacturer obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphmnf is_manfExists = db.GtEphmnfs.FirstOrDefault(u => u.ManufacturerName.ToUpper().Replace(" ", "") == obj.ManufacturerName.ToUpper().Replace(" ", ""));
                        if (is_manfExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0201", Message = string.Format(_localizer[name: "W0201"]) };
                        }
                        GtEphmnf is_shortdescExists = db.GtEphmnfs.FirstOrDefault(u => u.ManfShortName.ToUpper().Replace(" ", "") == obj.ManfShortName.ToUpper().Replace(" ", ""));
                        if (is_shortdescExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0202", Message = string.Format(_localizer[name: "W0202"]) };
                        }
                        int _manfID = db.GtEphmnfs.Select(c => c.ManufacturerId).DefaultIfEmpty().Max();
                        _manfID = _manfID + 1;

                        var manufact = new GtEphmnf
                        {
                            ManufacturerId = _manfID,
                            ManufacturerName = obj.ManufacturerName,
                            ManfShortName = obj.ManfShortName,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormId,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtEphmnfs.Add(manufact);
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
        public async Task<DO_ReturnParameter> UpdateManufacturer(DO_Manufacturer obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphmnf is_manfExists = db.GtEphmnfs.FirstOrDefault(u => u.ManufacturerName.ToUpper().Replace(" ", "") == obj.ManufacturerName.ToUpper().Replace(" ", "") && u.ManufacturerId!=obj.ManufacturerId);
                        if (is_manfExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0201", Message = string.Format(_localizer[name: "W0201"]) };
                        }
                        GtEphmnf is_shortdescExists = db.GtEphmnfs.FirstOrDefault(u => u.ManfShortName.ToUpper().Replace(" ", "") == obj.ManfShortName.ToUpper().Replace(" ", "") && u.ManufacturerId != obj.ManufacturerId);
                        if (is_shortdescExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0202", Message = string.Format(_localizer[name: "W0202"]) };
                        }


                        GtEphmnf manf = db.GtEphmnfs.Where(m => m.ManufacturerId == obj.ManufacturerId).FirstOrDefault();
                        if (manf != null)
                        {
                            manf.ManufacturerName = obj.ManufacturerName;
                            manf.ManfShortName = obj.ManfShortName;
                            manf.ActiveStatus = obj.ActiveStatus;
                            manf.ModifiedBy = obj.UserID;
                            manf.ModifiedOn = System.DateTime.Now;
                            manf.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0203", Message = string.Format(_localizer[name: "W0203"]) };

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
        public async Task<DO_ReturnParameter> ActiveOrDeActiveManufacturer(bool status, int manufId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEphmnf manu = db.GtEphmnfs.Where(w => w.ManufacturerId == manufId).FirstOrDefault();
                        if (manu == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0203", Message = string.Format(_localizer[name: "W0203"]) };
                        }

                        manu.ActiveStatus = status;
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

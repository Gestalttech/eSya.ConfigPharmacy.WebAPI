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
    public class VendorRepository: IVendorRepository
    {
        private readonly IStringLocalizer<VendorRepository> _localizer;
        public VendorRepository(IStringLocalizer<VendorRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Map Manufacturer to Vendor
        public async Task<List<DO_ManufacturerVendorLink>> GetActiveManufacturerList()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtEphmnfs.Where(x => x.ActiveStatus == true)
                      .Select(x => new DO_ManufacturerVendorLink
                      {
                          ManufacturerId = x.ManufacturerId,
                          ManufacturerName = x.ManufacturerName,
                         
                      }).OrderBy(x=>x.ManufacturerName).ToListAsync();
                    return ds;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ManufacturerVendorLink>> GetMappedVendorListbyManufacturer(int manufId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEavncds.Where(x => x.ActiveStatus == true)
                   .GroupJoin(db.GtEphmvls.Where(w => w.ManufacturerId == manufId),
                     d => d.VendorId,
                     l => l.VendorId,
                    (man, vend) => new { man, vend })
                   .SelectMany(z => z.vend.DefaultIfEmpty(),
                    (a, b) => new DO_ManufacturerVendorLink
                    {
                        ManufacturerId = manufId,
                        VendorId = a.man.VendorId,
                        VendorName = a.man.VendorName,
                        ActiveStatus = b == null ? false : b.ActiveStatus
                    }).ToListAsync();

                    var Distinctvendors = ds.GroupBy(x => x.VendorId).Select(y => y.First());
                    return Distinctvendors.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateManufacturer(DO_ManufacturerVendorLink obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var manlist = db.GtEphmvls.Where(w => w.ManufacturerId == obj.ManufacturerId).ToList();
                        foreach (var m in manlist)
                        {
                           var maf= db.GtEphmvls.Where(x => x.ManufacturerId == m.ManufacturerId).FirstOrDefault();
                            if (maf != null)
                            {
                                db.Remove(maf);
                                db.SaveChanges();
                            }
                        }

                        foreach (var mv in obj.vendorlist)
                        {
                           var vendor= db.GtEphmvls.Where(x => x.ManufacturerId == mv.ManufacturerId).FirstOrDefault();
                            if (vendor != null)
                            {

                                vendor.ActiveStatus = mv.ActiveStatus;
                                vendor.ModifiedBy = mv.UserID;
                                vendor.ModifiedOn = System.DateTime.Now;
                                vendor.ModifiedTerminal = mv.TerminalID;
                                
                            }
                            else
                            {
                                
                                    var mapvendor = new GtEphmvl
                                    {
                                        ManufacturerId = obj.ManufacturerId,
                                        VendorId = mv.VendorId,
                                        ActiveStatus = mv.ActiveStatus,
                                        CreatedBy = obj.UserID,
                                        FormId=obj.FormId,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };
                                    db.GtEphmvls.Add(mapvendor);
                                

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

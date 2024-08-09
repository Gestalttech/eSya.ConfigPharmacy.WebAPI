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
    public class GSTRepository: IGSTRepository
    {
        private readonly IStringLocalizer<GSTRepository> _localizer;
        public GSTRepository(IStringLocalizer<GSTRepository> localizer)
        {
            _localizer = localizer;
        }
        #region GST Pharmacy Percentage
        public async Task<List<DO_GST>> GetHSNCodes()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEphdfps
                        .Where(w => w.ActiveStatus && w.ParmValue!=Convert.ToInt32(0.0))
                        .Select(r => new DO_GST
                        {
                            HSNVal = Convert.ToInt32(r.ParmValue)
                        }).OrderBy(o => o.HSNVal)
                          .Distinct()
                          .ToListAsync();
                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_GST>> GetPharmacyGSTPercentages()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEphgsts
                        .Select(r => new DO_GST
                        {
                            Hsncode = r.Hsncode,
                            Gstperc=r.Gstperc,
                            EffectiveFrom=r.EffectiveFrom,
                            EffectiveTill=r.EffectiveTill,
                            ActiveStatus= r.ActiveStatus
                        }).OrderBy(o => o.Hsncode)
                          .ToListAsync();
                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdatePharmacyGSTPercentage(DO_GST obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Check if a record already exists with the same keys and an open period (EffectiveTill is null)
                        var GstExist = db.GtEphgsts
                            .Where(w => w.Hsncode ==Convert.ToInt32(obj.Hsncode) &&
                                        //w.Gstperc == obj.Gstperc &&
                                        w.EffectiveTill == null)
                            .FirstOrDefault();

                        if (GstExist != null)
                        {
                            if (obj.EffectiveFrom != GstExist.EffectiveFrom)
                            {
                                if (obj.EffectiveFrom < GstExist.EffectiveFrom)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0218", Message = string.Format(_localizer[name: "W0218"]) };
                                }

                                // Close the existing record by setting the EffectiveTill date
                                GstExist.EffectiveTill = obj.EffectiveFrom.AddDays(-1);
                                GstExist.ModifiedBy = obj.UserID;
                                GstExist.ModifiedOn = DateTime.Now;
                                GstExist.ModifiedTerminal = obj.TerminalID;
                                GstExist.ActiveStatus = false;

                                // Insert the new record
                                var Gst = new GtEphgst
                                {
                                    Hsncode = Convert.ToInt32(obj.Hsncode),
                                    Gstperc = obj.Gstperc,
                                    EffectiveFrom = obj.EffectiveFrom,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtEphgsts.Add(Gst);
                            }
                            else
                            {
                                // Update the existing record
                                GstExist.ActiveStatus = obj.ActiveStatus;
                                GstExist.ModifiedBy = obj.UserID;
                                GstExist.ModifiedOn = DateTime.Now;
                                GstExist.ModifiedTerminal = obj.TerminalID;
                            }
                        }
                        else
                        {
                            // No existing record found, so insert the new record
                           
                                var Gstnew = new GtEphgst
                                {
                                    Hsncode = Convert.ToInt32(obj.Hsncode),
                                    Gstperc = obj.Gstperc,
                                    EffectiveFrom = obj.EffectiveFrom,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtEphgsts.Add(Gstnew);
                            
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

        //Active only records inserted into table code
        //public async Task<DO_ReturnParameter> InsertSpecialtyDoctorLinkList(List<DO_SpecialtyDoctorLink> obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (DO_SpecialtyDoctorLink sm in obj)
        //                {
        //                    GtEsdosp spDl = db.GtEsdosp.Where(x => x.BusinessKey == sm.BusinessKey && x.SpecialtyId == sm.SpecialtyID && x.DoctorId == sm.DoctorID).FirstOrDefault();
        //                    if (spDl != null)
        //                    {
        //                        spDl.ActiveStatus = sm.ActiveStatus;
        //                        spDl.ModifiedBy = sm.UserID;
        //                        spDl.ModifiedOn = System.DateTime.Now;
        //                        spDl.ModifiedTerminal = sm.TerminalID;
        //                    }
        //                    else if (sm.ActiveStatus)
        //                    {
        //                        var sMaster = new GtEsdosp
        //                        {
        //                            BusinessKey = sm.BusinessKey,
        //                            SpecialtyId = sm.SpecialtyID,
        //                            DoctorId = sm.DoctorID,
        //                            ActiveStatus = sm.ActiveStatus,
        //                            FormId = sm.FormId,
        //                            CreatedBy = sm.UserID,
        //                            CreatedOn = System.DateTime.Now,
        //                            CreatedTerminal = sm.TerminalID,

        //                        };
        //                        db.GtEsdosp.Add(sMaster);
        //                    }
        //                }

        //                await db.SaveChangesAsync();
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, Message = "Doctor Name Linked with Specialty Successfully." };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}

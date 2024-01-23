using eSya.ConfigPharmacy.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.IF
{
    public interface IVendorRepository
    {
        #region Map Vendors to Manufacturer
        Task<List<DO_ManufacturerVendorLink>> GetActiveManufacturerList();
        Task<List<DO_ManufacturerVendorLink>> GetMappedVendorListbyManufacturer(int manufId);
        Task<DO_ReturnParameter> InsertOrUpdateManufacturer(DO_ManufacturerVendorLink obj);
        #endregion
    }
}

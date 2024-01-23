using eSya.ConfigPharmacy.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.IF
{
    public interface IManufacturerRepository
    {
        #region Manufacturer
        Task<List<DO_Manufacturer>> GetManufacturerListByNamePrefix(string manufacturerNamePrefix);
        Task<DO_ReturnParameter> InsertManufacturer(DO_Manufacturer obj);
        Task<DO_ReturnParameter> UpdateManufacturer(DO_Manufacturer obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveManufacturer(bool status, int manufId);
        #endregion
    }
}

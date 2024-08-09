using eSya.ConfigPharmacy.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.IF
{
    public interface IGSTRepository
    {
        #region GST Pharmacy Percentage
        Task<List<DO_GST>> GetHSNCodes();
        Task<List<DO_GST>> GetPharmacyGSTPercentages();
        Task<DO_ReturnParameter> InsertOrUpdatePharmacyGSTPercentage(DO_GST obj);
        #endregion
    }
}

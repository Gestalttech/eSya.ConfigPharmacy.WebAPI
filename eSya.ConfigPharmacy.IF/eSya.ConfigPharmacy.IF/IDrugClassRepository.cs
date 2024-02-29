using eSya.ConfigPharmacy.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.IF
{
    public interface IDrugClassRepository
    {
        #region Drug Class
        Task<List<DO_DrugClass>> GetAllDrugClass();
        Task<DO_ReturnParameter> InsertIntoDrugClass(DO_DrugClass obj);
        Task<DO_ReturnParameter> UpdateIntoDrugClass(DO_DrugClass obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDrugClass(bool status, int drugclassId);
        #endregion
        #region Drug Therapeutic
        Task<List<DO_DrugTherapeutic>> GetAllDrugTherapeutics();
        Task<DO_ReturnParameter> InsertIntoDrugTherapeutic(DO_DrugTherapeutic obj);
        Task<DO_ReturnParameter> UpdateDrugTherapeutic(DO_DrugTherapeutic obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDrugTherapeutic(bool status, int drugthrapId);
        #endregion
    }
}

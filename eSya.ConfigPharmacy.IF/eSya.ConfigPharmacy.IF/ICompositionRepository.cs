using eSya.ConfigPharmacy.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.IF
{
    public interface ICompositionRepository
    {
        #region Drug Composition
        Task<List<DO_DrugClass>> GetActiveDrugClass();
        Task<List<DO_DrugTherapeutic>> GetActiveDrugTherapeutics();
        Task<List<DO_Composition>> GetCompositionByPrefix(string prefix);
        Task<DO_Composition> GetCompositionInfo(int composId);
        Task<DO_ReturnParameter> InsertComposition(DO_Composition obj);
        Task<DO_ReturnParameter> UpdateComposition(DO_Composition obj);
        #endregion
    }
}

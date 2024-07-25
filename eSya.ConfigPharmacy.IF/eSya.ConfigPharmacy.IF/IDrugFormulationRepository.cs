using eSya.ConfigPharmacy.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.IF
{
    public interface IDrugFormulationRepository
    {
        #region Drug formulation
        Task<List<DO_Composition>> GetActiveCompositionforTreeview(string prefix);
        Task<List<DO_DrugFormulation>> GetDrugFormulationInfobyCompositionID(int composId);
        Task<DO_DrugFormulation> GetDrugFormulationParamsbyFormulationID(int composId, int formulationId);
        Task<DO_ReturnParameter> InsertIntoDrugFormulation(DO_DrugFormulation obj);
        Task<DO_ReturnParameter> UpdateDrugFormulation(DO_DrugFormulation obj);
        #endregion

        #region Map Formulation to Manufacturer
        Task<List<DO_DrugFormulation>> GetActiveFormulations(string prefix);
        Task<DO_Composition> GetCompositionbyFormulationID(int formulationId);
        Task<List<DO_MapFormulationManufacturer>> GetLinkedManufacturerwithFormulation(int formulationId, int compositionId);
        Task<DO_ReturnParameter> InsertOrUpdateManufacturerLinkwithFormulation(DO_MapFormulationManufacturer obj);
        #endregion
    }
}

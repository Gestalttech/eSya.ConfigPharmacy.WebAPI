using eSya.ConfigPharmacy.DL.Repository;
using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigPharmacy.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrugFormulationController : ControllerBase
    {
        private readonly IDrugFormulationRepository _drugFormulationRepository;

        public DrugFormulationController(IDrugFormulationRepository drugFormulationRepository)
        {
            _drugFormulationRepository = drugFormulationRepository;
        }

        #region Drug Formulation
        /// <summary>
        /// Get Active Composition for Tree view.
        /// UI Reffered - Drug Formulation, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveCompositionforTreeview(string prefix)
        {
            var comps = await _drugFormulationRepository.GetActiveCompositionforTreeview(prefix);
            return Ok(comps);
        }
        /// <summary>
        /// Get Drug Formulation for Grid view.
        /// UI Reffered - Drug Formulation, 
        /// </summary>
        /// <returns>composId</returns>
        [HttpGet]
        public async Task<IActionResult> GetDrugFormulationInfobyCompositionID(int composId)
        {
            var formulations = await _drugFormulationRepository.GetDrugFormulationInfobyCompositionID(composId);
            return Ok(formulations);
        }
        /// <summary>
        /// Get Drug Formulation Parameters
        /// UI Reffered - Drug Formulation, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDrugFormulationParamsbyFormulationID(int composId, int formulationId)
        {
            var parms = await _drugFormulationRepository.GetDrugFormulationParamsbyFormulationID(composId, formulationId);
            return Ok(parms);
        }

        /// <summary>
        /// Insert into Drug Formulation Table
        /// UI Reffered - Drug Formulation,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDrugFormulation(DO_DrugFormulation obj)
        {
            var msg = await _drugFormulationRepository.InsertIntoDrugFormulation(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Drug Formulation Table
        /// UI Reffered - Drug Formulation,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDrugFormulation(DO_DrugFormulation obj)
        {
            var msg = await _drugFormulationRepository.UpdateDrugFormulation(obj);
            return Ok(msg);
        }


        #endregion

        #region Map Formulation to Manufacturer
        /// <summary>
        /// Get Active Formulation for drop down.
        /// UI Reffered -  Map Formulation to Manufacturer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveFormulations()
        {
            var formu = await _drugFormulationRepository.GetActiveFormulations();
            return Ok(formu);
        }
        /// <summary>
        /// Get Composition by Formulation ID.
        /// UI Reffered - Map Formulation to Manufacturer
        /// </summary>
        /// <returns>composId</returns>
        [HttpGet]
        public async Task<IActionResult> GetCompositionbyFormulationID(int formulationId)
        {
            var comp = await _drugFormulationRepository.GetCompositionbyFormulationID(formulationId);
            return Ok(comp);
        }
        /// <summary>
        /// Get Drug Manufacturer with Formulation
        /// UI Reffered - Map Formulation to Manufacturer, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLinkedManufacturerwithFormulation(int formulationId, int compositionId)
        {
            var mapfm = await _drugFormulationRepository.GetLinkedManufacturerwithFormulation(formulationId, compositionId);
            return Ok(mapfm);
        }

        /// <summary>
        /// Insert into Manufacturer with Formulation Table
        /// UI Reffered - Map Formulation to Manufacturer,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateManufacturerLinkwithFormulation(DO_MapFormulationManufacturer obj)
        {
            var msg = await _drugFormulationRepository.InsertOrUpdateManufacturerLinkwithFormulation(obj);
            return Ok(msg);
        }
        #endregion
    }
}

using eSya.ConfigPharmacy.DL.Repository;
using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigPharmacy.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrugClassController : ControllerBase
    {
        private readonly IDrugClassRepository _drugClassRepository;

        public DrugClassController(IDrugClassRepository drugClassRepository)
        {
            _drugClassRepository = drugClassRepository;
        }

        #region Drug Class
        /// <summary>
        /// Get Drug Class List for Grid.
        /// UI Reffered - Drug Class, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDrugClass()
        {
            var mfs = await _drugClassRepository.GetAllDrugClass();
            return Ok(mfs);
        }

        /// <summary>
        /// Insert into Drug Class Table
        /// UI Reffered - Drug Class,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDrugClass(DO_DrugClass obj)
        {
            var msg = await _drugClassRepository.InsertIntoDrugClass(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Drug Class Table
        /// UI Reffered - Drug Class,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateIntoDrugClass(DO_DrugClass obj)
        {
            var msg = await _drugClassRepository.UpdateIntoDrugClass(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Active Or De Active Drug Class.
        /// UI Reffered - Drug Class
        /// </summary>
        /// <param name="status-drugclassId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveDrugClass(bool status, int drugclassId)
        {
            var ac = await _drugClassRepository.ActiveOrDeActiveDrugClass(status, drugclassId);
            return Ok(ac);
        }
        #endregion

        #region Drug Therapeutic
        /// <summary>
        /// Get Drug Therapeutic List for Grid.
        /// UI Reffered - Drug Therapeutic, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDrugTherapeutics()
        {
            var mfs = await _drugClassRepository.GetAllDrugTherapeutics();
            return Ok(mfs);
        }

        /// <summary>
        /// Insert into Drug Therapeutic Table
        /// UI Reffered - Drug Class,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDrugTherapeutic(DO_DrugTherapeutic obj)
        {
            var msg = await _drugClassRepository.InsertIntoDrugTherapeutic(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Drug Therapeutic Table
        /// UI Reffered - Drug Therapeutic,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDrugTherapeutic(DO_DrugTherapeutic obj)
        {
            var msg = await _drugClassRepository.UpdateDrugTherapeutic(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Active Or De Active Drug Therapeutic.
        /// UI Reffered - Drug Therapeutic
        /// </summary>
        /// <param name="status-drugclassId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveDrugTherapeutic(bool status, int drugthrapId)
        {
            var ac = await _drugClassRepository.ActiveOrDeActiveDrugTherapeutic(status, drugthrapId);
            return Ok(ac);
        }
        #endregion
    }
}

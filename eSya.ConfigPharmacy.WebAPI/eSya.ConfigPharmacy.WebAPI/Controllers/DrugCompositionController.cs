using eSya.ConfigPharmacy.DL.Repository;
using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigPharmacy.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrugCompositionController : ControllerBase
    {
        private readonly ICompositionRepository _compositionRepository;

        public DrugCompositionController(ICompositionRepository compositionRepository)
        {
            _compositionRepository = compositionRepository;
        }
        #region Drug Composition
        /// <summary>
        /// Get Drug Composition for Grid & JsTree.
        /// UI Reffered - Composition, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCompositionByPrefix(string prefix)
        {
            var mfs = await _compositionRepository.GetCompositionByPrefix(prefix);
            return Ok(mfs);
        }
        /// <summary>
        /// Get Drug Composition by Composition ID.
        /// UI Reffered - Composition, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCompositionInfo(int composId)
        {
            var mfs = await _compositionRepository.GetCompositionInfo(composId);
            return Ok(mfs);
        }


        /// <summary>
        /// Insert into Composition Table
        /// UI Reffered - Drug Composition,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertComposition(DO_Composition obj)
        {
            var msg = await _compositionRepository.InsertComposition(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Composition Table
        /// UI Reffered - Drug Composition,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateComposition(DO_Composition obj)
        {
            var msg = await _compositionRepository.UpdateComposition(obj);
            return Ok(msg);
        }

       
        #endregion
    }
}

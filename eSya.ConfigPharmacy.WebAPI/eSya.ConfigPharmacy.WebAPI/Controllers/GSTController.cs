using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigPharmacy.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSTController : ControllerBase
    {
        private readonly IGSTRepository _GstRepository;

        public GSTController(IGSTRepository GstRepository)
        {
            _GstRepository = GstRepository;
        }
        #region GST Pharmacy Percentage
        /// <summary>
        /// Get HSN Codes for Drop down.
        /// UI Reffered - GST, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetHSNCodes()
        {
            var ds = await _GstRepository.GetHSNCodes();
            return Ok(ds);
        }
        /// <summary>
        /// Get Pharmacy GST Percentages for Drop Grid.
        /// UI Reffered - GST, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPharmacyGSTPercentages()
        {
            var ds = await _GstRepository.GetPharmacyGSTPercentages();
            return Ok(ds);
        }

        /// <summary>
        /// Insert Or Update Pharmacy GST Percentages
        /// UI Reffered - GST,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePharmacyGSTPercentage(DO_GST obj)
        {
            var msg = await _GstRepository.InsertOrUpdatePharmacyGSTPercentage(obj);
            return Ok(msg);
        }

        #endregion
    }
}

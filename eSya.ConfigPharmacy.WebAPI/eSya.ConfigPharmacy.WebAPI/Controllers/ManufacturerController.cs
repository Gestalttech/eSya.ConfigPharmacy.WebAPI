using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigPharmacy.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }
        #region Manufacturer
        /// <summary>
        /// Get Manufacturer List for Grid.
        /// UI Reffered - Manufacturer, 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetManufacturerListByNamePrefix(string manufacturerNamePrefix)
        {
            var mfs = await _manufacturerRepository.GetManufacturerListByNamePrefix(manufacturerNamePrefix);
            return Ok(mfs);
        }

        /// <summary>
        /// Insert into Manufacturer Table
        /// UI Reffered - Manufacturer,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertManufacturer(DO_Manufacturer obj)
        {
            var msg = await _manufacturerRepository.InsertManufacturer(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Manufacturer Table
        /// UI Reffered - Manufacturer,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateManufacturer(DO_Manufacturer obj)
        {
            var msg = await _manufacturerRepository.UpdateManufacturer(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Active Or De Active Manufacturer.
        /// UI Reffered - Manufacturer
        /// </summary>
        /// <param name="status-code_type"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveManufacturer(bool status, int manufId)
        {
            var ac = await _manufacturerRepository.ActiveOrDeActiveManufacturer(status, manufId);
            return Ok(ac);
        }
        #endregion
    }
}

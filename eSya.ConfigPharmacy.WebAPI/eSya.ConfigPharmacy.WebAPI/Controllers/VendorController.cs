using eSya.ConfigPharmacy.DO;
using eSya.ConfigPharmacy.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ConfigPharmacy.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorController(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        #region Map Vendors to Manufacturer
        /// <summary>
        /// Getting Active Manufacturer List
        /// UI Reffered - Map Vendor Manufacturer List -> for Tree
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetActiveManufacturerList()
        {
            var ds = await _vendorRepository.GetActiveManufacturerList();
            return Ok(ds);
        }

        /// <summary>
        /// Getting Mapped Vendors List with Manufacturer .
        /// UI Reffered - Map Vendor Manufacturer -> Vendors Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMappedVendorListbyManufacturer(int manufId)
        {
            var ds = await _vendorRepository.GetMappedVendorListbyManufacturer(manufId);
            return Ok(ds);
        }

        /// <summary>
        /// Insert Or Update Manufacturer-Vendors Links .
        /// UI Reffered - Map Vendor Manufacturer
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateManufacturer(DO_ManufacturerVendorLink obj)
        {
            var msg = await _vendorRepository.InsertOrUpdateManufacturer(obj);
            return Ok(msg);

        }
        #endregion
    }
}

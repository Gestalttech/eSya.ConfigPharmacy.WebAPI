using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DO
{
    public class DO_ManufacturerVendorLink
    {
        public int ManufacturerId { get; set; }
        public int VendorId { get; set; }
        public bool ActiveStatus { get; set; }
        public string? VendorName { get; set; }
        public string? ManufacturerName { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public List<DO_ManufacturerVendorLink>? vendorlist { get; set; }
    }
   
}

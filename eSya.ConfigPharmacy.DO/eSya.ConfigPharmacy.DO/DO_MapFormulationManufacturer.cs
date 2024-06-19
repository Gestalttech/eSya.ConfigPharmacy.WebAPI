using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DO
{
    public class DO_MapFormulationManufacturer
    {
        public int CompositionId { get; set; }
        public int FormulationId { get; set; }
        public int ManufacturerId { get; set; }
        public string? ManufacturerName { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public List<DO_MapFormulationManufacturer>? manfctlist { get; set; }

    }
}

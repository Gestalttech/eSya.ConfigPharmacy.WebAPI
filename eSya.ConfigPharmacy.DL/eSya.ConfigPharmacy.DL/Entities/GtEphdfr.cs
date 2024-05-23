using System;
using System.Collections.Generic;

namespace eSya.ConfigPharmacy.DL.Entities
{
    public partial class GtEphdfr
    {
        public int CompositionId { get; set; }
        public int FormulationId { get; set; }
        public string FormulationDesc { get; set; } = null!;
        public int DrugForm { get; set; }
        public string? Volume { get; set; }
        public int MethodOfAdministration { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}

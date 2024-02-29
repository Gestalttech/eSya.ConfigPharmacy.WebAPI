using System;
using System.Collections.Generic;

namespace eSya.ConfigPharmacy.DL.Entities
{
    public partial class GtEphdtc
    {
        public int DrugTherapeutic { get; set; }
        public string DrugTherapeuticDesc { get; set; } = null!;
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

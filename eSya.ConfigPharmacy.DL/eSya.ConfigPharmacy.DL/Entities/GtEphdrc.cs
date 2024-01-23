using System;
using System.Collections.Generic;

namespace eSya.ConfigPharmacy.DL.Entities
{
    public partial class GtEphdrc
    {
        public int CompositionId { get; set; }
        public bool IsCombination { get; set; }
        public string DrugCompDesc { get; set; } = null!;
        public int DrugClass { get; set; }
        public int TherapueticClass { get; set; }
        public bool AvailableAsGeneric { get; set; }
        public string DrugSchedule { get; set; } = null!;
        public int PharmacyGroup { get; set; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DO
{
    public class DO_Composition
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
    }
    public class DO_eSyaParameter
    {
        public int ParameterID { get; set; }
        public bool ParmAction { get; set; }
        public decimal ParmValue { get; set; }
        public decimal ParmPerct { get; set; }
        public string? ParmDesc { get; set; }
        public bool ActiveStatus { get; set; }
        public int CompositionId { get; set; }
    }
}

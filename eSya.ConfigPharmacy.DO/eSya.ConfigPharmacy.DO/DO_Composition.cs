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
        public string DrugCompDesc { get; set; } 
        public int DrugClass { get; set; }
        public int TherapueticClass { get; set; }
        public int PharmacyGroup { get; set; }
        public bool ActiveStatus { get; set; }
        public string? DrugClassDesc { get; set; }
        public string? TherapueticClassDesc { get; set; }
        public string? PharmacyGroupDesc { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public List<DO_eSyaParameter> l_composionparams { get; set; }
    }
    public class DO_eSyaParameter
    {
        public int ParameterID { get; set; }
        public bool ParmAction { get; set; }
        public decimal ParmValue { get; set; }
        public decimal ParmPerct { get; set; }
        public string? ParmDesc { get; set; }
        public bool ActiveStatus { get; set; }

    }
}

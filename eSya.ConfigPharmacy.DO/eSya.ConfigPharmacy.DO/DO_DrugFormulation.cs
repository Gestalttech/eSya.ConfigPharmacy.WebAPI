using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DO
{
    public class DO_DrugFormulation
    {
        public int CompositionId { get; set; }
        public int FormulationId { get; set; }
        public string FormulationDesc { get; set; } = null!;
        public int DrugForm { get; set; }
        public string? Volume { get; set; }
        public int MethodOfAdministration { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public List<DO_eSyaParameter> lst_formulationparams { get; set; }
        public string? DrugFormDesc { get; set; }
        public string? MethodOfAdministrationDesc { get; set; }
    }
}

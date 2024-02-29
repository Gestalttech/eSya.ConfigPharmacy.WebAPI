using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DO
{
    public class DO_DrugClass
    {
        public int DrugClass { get; set; }
        public string DrugClassDesc { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
    public class DO_DrugTherapeutic
    {
        public int DrugTherapeutic { get; set; }
        public string DrugTherapeuticDesc { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}

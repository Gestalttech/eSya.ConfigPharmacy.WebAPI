using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConfigPharmacy.DO
{
    public class DO_ReturnParameter
    {
        public bool Status { get; set; }
        public string StatusCode { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public decimal ID { get; set; }
        public string Key { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
   public class RequestViewModel
    {
        public int IdRequest { get; set; }
        public string Sname_Client { get; set; }
        public string Fname { get; set; }
        public string Address { get; set; }
        public string Driver { get; set; }
        public string Car { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
   public class Driver :DriverSql
    {
        public int IdDriver { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string Birth { get; set; }
        public int Y_drive { get; set; }
        public string P_number { get; set; }
    }
}

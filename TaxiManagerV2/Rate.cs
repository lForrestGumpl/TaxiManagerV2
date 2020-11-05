using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class Rate : RateSql
    {
        public int IdRate { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Range { get; set; }
    }
}

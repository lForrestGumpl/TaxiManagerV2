using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
   public class Car: CarSql
    {
        public int IdCar { get; set; }
        public string MarkCar { get; set; }
        public string Bodywork { get; set; }
        public string ColorCar { get; set; }
        public string IdDriver { get; set; }
        public string NumberCar { get; set; }
        public string Status { get; set; }
    }
}

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
        public void CreateRate()
        {
            IdRate = GetNextId("rate_table");
            CreateNewRate(Name, Price, Range);
        }
        public void Delete()
        {
            DeleteRate(IdRate);
        }
        public void Update()
        {
            UpdateRate(Name, Price, Range, IdRate);
        }
    }
}

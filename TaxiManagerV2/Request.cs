using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class Request : RequestSql
    {
        public int IdRequest { get; set; }
        public string Sname { get; set; }
        public string Fname { get; set; }
        public string Address { get; set; }
        public string Driver { get; set; }
        //public int DateCreate { get; set; }
        public string NumberCar { get; set; }

        public void CreateRequest()
        {
            IdRequest = GetNextId("request_table");
            CreateNewRequest(Sname, Fname, Address, Driver, NumberCar);
        }
        public void Delete()
        {
            DeleteRequest(IdRequest);
        }
        public void Update()
        {
            UpdateRequest(Sname, Fname, Address, Driver, NumberCar, IdRequest);
        }
    }
}

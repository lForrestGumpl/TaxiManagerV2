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
        public string Sname_Client { get; set; }
        public string Fname { get; set; }
        public string Address { get; set; }
        public int IdDriver { get; set; }
        public int IdCar { get; set; }

        public void CreateRequest()
        {
            IdRequest = GetNextId("request_table");
            CreateNewRequest(Sname_Client, Fname, Address, IdDriver, IdCar);
        }
        public void Delete()
        {
            DeleteRequest(IdRequest);
        }
        public void Update()
        {
            UpdateRequest(Sname_Client, Fname, Address, IdDriver, IdCar, IdRequest);
        }
    }
}

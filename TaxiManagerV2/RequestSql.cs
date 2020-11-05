using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class RequestSql : DB
    {
        internal static List<Request> GetRequests()
        {
            List<Request> result = new List<Request>();
            string sql = "SELECT id_request, sname, fname, address, driver, car_number  FROM request_table"; //id_request, sname, fname, address, driver, car_number
            Request last = null;
            //int requestid = 0;
            Dictionary<int, Request> requests = new Dictionary<int, Request>();
            if (OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (last != null && last.IdRequest != dr.GetInt32("id_request"))
                                last = null;
                            if (last == null)
                            {
                                last = new Request();
                                last.IdRequest = dr.GetInt32("id_request");
                                last.Sname = dr.GetString("sname");
                                last.Fname = dr.GetString("fname");
                                last.Address = dr.GetString("address");
                                last.Driver = dr.GetString("driver");
                                last.NumberCar = dr.GetString("car_number");
                                // Driver driver = new Driver();
                                //driver.Fname = dr.GetString("fname");
                                result.Add(last);
                            }
                        }
                    }
                }
                CloseConnection();
            }
            return result;
        }
        protected static bool CreateNewRequest(string Sname, string Fname, string Address, string Driver, string NumberCar)
        {
            string sql = "INSERT INTO request_table VALUES (0, '"+ Sname+"', '"+ Fname+ "','" + Address + "','" + Driver + "','" + NumberCar + "')";
            return RunSQL(sql);
        }
    }
}

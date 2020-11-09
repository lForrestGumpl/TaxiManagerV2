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
            string sql = "SELECT id_request, sname, fname, address, id_driver, id_car FROM request_table"; //id_request, sname, fname, address, driver, car_number
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
                                last.Sname_Client = dr.GetString("sname");
                                last.Fname = dr.GetString("fname");
                                last.Address = dr.GetString("address");
                                last.IdDriver = dr.GetInt32("id_driver");
                                last.IdCar = dr.GetInt32("id_car");
                                result.Add(last);
                            }
                        }
                    }
                }
                CloseConnection();
            }
            return result;
        }
        protected static bool CreateNewRequest(string Sname_Client, string Fname, string Address, int IdDriver, int IdCar)
        {
            string sql = "INSERT INTO request_table VALUES (0, '"+ Sname_Client + "', '"+ Fname+ "','" + Address + "','" + IdDriver + "','" + IdCar + "')";
            return RunSQL(sql);
        }
        protected static bool DeleteRequest(int id)
        {
            string sql = "DELETE FROM request_table WHERE id_request = " + id;
            return RunSQL(sql);
        }
        protected static bool UpdateRequest(string Sname_Client, string Fname, string Address, int IdDriver, int IdCar, int IdRequest)
        {
            string sql = "UPDATE request_table SET sname = '" + Sname_Client + "', fname = '" + Fname + "', address'" + Address + "', id_driver'" + IdDriver + "', id_car = '" + IdCar + "' WHERE id_request =" + IdRequest;
            return RunSQL(sql);
        }
        internal static Request GetRequestById(int IdRequest)
        {
            Request request = null;
            string sql = "SELECT id_request, sname, fname, address, id_driver, id_car FROM request_table WHERE id_request=" + IdRequest;
            if (OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            request = new Request
                            {
                                IdRequest = dr.GetInt32("id_request"),
                                Sname_Client = dr.GetString("sname"),
                                Fname = dr.GetString("fname"),
                                Address = dr.GetString("address"),
                                IdDriver = dr.GetInt32("id_driver"),
                                IdCar = dr.GetInt32("id_car"),
                            };
                        }
                    }
                }
                CloseConnection();
            }
            return request;

        }
    }
}

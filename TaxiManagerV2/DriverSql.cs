using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class DriverSql : DB
    {
        internal static List<Driver> GetDrivers()
        {
            List<Driver> result = new List<Driver>();
            string sql = " SELECT id_driver, fname, sname, birth, y_drive as year, p_number  FROM driver_table ";
            Driver last = null;
            {
                if (OpenConnection())
                {
                    using (var mc = new MySqlCommand(sql, connection))
                    {
                        using (var dr = mc.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (last != null && last.IdDriver != dr.GetInt32("id_driver"))
                                    last = null;
                                if (last == null)
                                {
                                    last = new Driver();
                                    last.IdDriver = dr.GetInt32("id_driver");
                                    last.Fname = dr.GetString("fname");
                                    last.Sname = dr.GetString("sname");
                                    last.Birth = dr.GetString("birth");
                                    last.Y_drive = dr.GetInt32("year");
                                    last.P_number = dr.GetString("p_number");
                                    result.Add(last);
                                }
                            }
                        }
                    }
                    CloseConnection();
                }
                return result;
            }
        }
    }
}

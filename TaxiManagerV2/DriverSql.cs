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
            string sql = " SELECT idDriver, fname, sname, birth, y_drive as year, p_number  FROM driver_table ";
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
                                if (last != null && last.Id_Driver != dr.GetInt32("idDriver"))
                                    last = null;
                                if (last == null)
                                {
                                    last = new Driver();
                                    last.Id_Driver = dr.GetInt32("idDriver");
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
        internal static bool CreateNewDriver(string Fname,  string Sname, string Birth, int Y_drive, string P_number)
        {
            string sql = "INSERT INTO driver_table VALUE(0, '"+Fname+ "', '" + Sname + "','" + Birth + "','" + Y_drive + "','" + P_number + "')";
            return RunSQL(sql);

        }
        internal static bool DeleteDriver(int idDriver)
        {
            string sql = "DELETE FROM driver_table WHERE idDriver =" + idDriver;
            return RunSQL(sql);
        }
        internal static bool UpdateDriver(string Fname, string Sname, string Birth, int Y_drive, string P_number, int Id_Driver)
        {
            string sql = "UPDATE driver_tabe SET fname='" + Fname + "', sname='" + Sname + "', birth='" + Birth + "', y_drive='" + Y_drive + "',p_number='" + P_number + "' WHERE idDriver = " + Id_Driver;
            return RunSQL(sql);
        }
    }
}

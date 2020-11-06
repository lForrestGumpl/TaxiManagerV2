using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class ManagerSql : DB
    {
        internal static List<Manager> GetManagers()
        {
            List<Manager> result = new List<Manager>();
            string sql = "SELECT id_manager, fname, sname, redirection_code as code, status FROM manager_table";
            Manager last = null;
            if (OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (last != null && last.IdManager != dr.GetInt32("id_manager"))
                                last = null;
                            if (last == null)
                            {
                                last = new Manager();
                                last.IdManager = dr.GetInt32("id_manager");
                                last.Fname = dr.GetString("fname");
                                last.Sname = dr.GetString("sname");
                                last.RedirectionCode = dr.GetString("code");
                                last.Status = dr.GetString("status");
                                result.Add(last);
                            }
                        }
                    }
                }
                CloseConnection();
            }
            return result;
        }
        
        internal static bool CreateNewManager(string Fname, string Sname, string RedirectionCode, string Status)
        {
            string sql = "INSERT INTO manager_table VALUE (0, '"+Fname+ "', '" + Sname + "','" + RedirectionCode + "','" + Status + "')";
            return RunSQL(sql);
        }
        internal static bool DeleteManager(int idManager)
        {
            string sql = "DELETE FROM manager_table WHERE id_manager ="+ idManager;
            return RunSQL(sql);
        }
        internal static bool UpdateManager(string Fname, string Sname, string RedirectionCode, string Status, int IdManager)
        {
            string sql = "UPDATE manager_table SET fname='" + Fname + "', sname='" + Sname + "', redirection_code='" + RedirectionCode + "', status='" + Status + "' WHERE id_manager= " + IdManager;
            return RunSQL(sql);
        }
        
    }
}

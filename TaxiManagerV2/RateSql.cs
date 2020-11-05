using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class RateSql: DB
    {
        internal static List<Rate> GetRates()
        {
            List<Rate> result = new List<Rate>();
            string sql = " SELECT id_rate, name, price, distance FROM rate_table";
            Rate last = null;
            {
                if (OpenConnection())
                {
                    using (var mc = new MySqlCommand(sql, connection))
                    {
                        using (var dr = mc.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (last != null && last.IdRate != dr.GetInt32("id_rate"))
                                    last = null;
                                if (last == null)
                                {
                                    last = new Rate();
                                    last.IdRate = dr.GetInt32("id_rate");
                                    last.Name = dr.GetString("name");
                                    last.Price = dr.GetInt32("price");
                                    last.Range = dr.GetString("distance");
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

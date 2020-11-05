using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
    public class CarSql: DB
    {
        internal static List<Car> GetCars()
        {
            List<Car> result = new List<Car>();
            string sql = " SELECT id_car, car_mark, bodywork, car_color, driver, car_number, status  FROM car_table ";
            Car last = null;
            {
                if (OpenConnection())
                {
                    using (var mc = new MySqlCommand(sql, connection))
                    {
                        using (var dr = mc.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (last != null && last.IdCar != dr.GetInt32("id_car"))
                                    last = null;
                                if (last == null)
                                {
                                    last = new Car();
                                    last.IdCar = dr.GetInt32("id_car");
                                    last.MarkCar = dr.GetString("car_mark");
                                    last.Bodywork = dr.GetString("bodywork");
                                    last.ColorCar = dr.GetString("car_color");
                                    last.IdDriver = dr.GetString("driver");
                                    last.NumberCar = dr.GetString("car_number");
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
        }
    }
}

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
        internal static bool CreateNewCar(string MarkCar, string Bodywork, string ColorCar, string IdDriver, string NumberCar, string Status)
        {
            string sql = "INSERT INTO car_table VALUES(0 , '"+ MarkCar + "','" + Bodywork + "','" + ColorCar + "','" + IdDriver + "','" + NumberCar + "','" + Status + "')";
            return RunSQL(sql);
        }
        internal static bool DeleteCar(int idCar)
        {
            string sql = "DELETE FROM car_table WHERE id_car = " + idCar;
            return RunSQL(sql);

        }
        internal static bool UpdateCar(string MarkCar, string Bodywork, string ColorCar, string IdDriver, string NumberCar, string Status, int IdCar)
        {
            string sql = "UPDATE car_table SET car_mark = '" + MarkCar + "',bodywork = '" +Bodywork + "', car_color = '" + ColorCar + "',driver = '" + IdDriver + "',car_number = '" + NumberCar + "',status = '" + Status +"' WHERE id_car = " + IdCar;
            return RunSQL(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagerV2
{
   public class Car: CarSql
    {
        public int Id_Car { get; set; }
        public string MarkCar { get; set; }
        public string Bodywork { get; set; }
        public string ColorCar { get; set; }
        public int IdDriver { get; set; }
        public string NumberCar { get; set; }
        public string Status { get; set; }

        public void CreateCar()
        {
            Id_Car = GetNextId("car_table");
            CreateNewCar(MarkCar, Bodywork,ColorCar,IdDriver, NumberCar, Status);
        }
        public void Delete()
        {
            DeleteCar(Id_Car);
        }
        public void Update()
        {
            UpdateCar(MarkCar, Bodywork, ColorCar, IdDriver, NumberCar, Status, Id_Car);
        }

    }
}

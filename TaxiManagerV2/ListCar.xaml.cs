using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaxiManagerV2
{
    /// <summary>
    /// Логика взаимодействия для ListCar.xaml
    /// </summary>
    public partial class ListCar : Window, INotifyPropertyChanged
    {
        public Car selectedCar;
        public List<CarViewModel> CarsVM { get; set; }
        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                SignalChanged();
            }
        }
        public ListCar()
        {
            InitializeComponent();
            DB dB = new DB();
            var Cars = CarSql.GetCars();
            var Drivers = DriverSql.GetDrivers();
            var query =
                from car in Cars
                from driver in Drivers
                where car.IdDriver == driver.Id_Driver
                select new CarViewModel
                {
                    IdCar = car.Id_Car,
                    MarkCar = car.MarkCar,
                    Bodywork = car.Bodywork,
                    ColorCar = car.ColorCar,
                    NumberCar = car.NumberCar,
                    Status = car.Status,
                    Driver = driver.Sname
                };
            //DataContext = this;
            CarsVM = new List<CarViewModel>(query);
            carGrid.ItemsSource = CarsVM;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar();
            if (addCar.ShowDialog() == true)
            {
                var Drivers = DriverSql.GetDrivers();

                CarViewModel carViewModel = new CarViewModel
                {
                    IdCar = addCar.edit.Id_Car,
                    MarkCar = addCar.edit.MarkCar,
                    Bodywork = addCar.edit.Bodywork,
                    ColorCar = addCar.edit.ColorCar,
                    NumberCar = addCar.edit.NumberCar,
                    Driver = Drivers.FirstOrDefault(x => x.Id_Driver == addCar.edit.IdDriver).Sname
                };

                CarsVM.Add(carViewModel);
                
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (carGrid.SelectedIndex == -1)
                return;
            CarViewModel carViewModel = (CarViewModel)carGrid.SelectedItem;
            Car car = CarSql.GetCarById(carViewModel.IdCar);
            AddCar addCar = new AddCar(car);
            addCar.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (carGrid.SelectedIndex == -1)
                return;

            CarViewModel carViewModel = (CarViewModel)carGrid.SelectedItem;
            Car car = CarSql.GetCarById(carViewModel.IdCar);
            car.Delete();
            CarsVM.Remove(carViewModel);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var Cars = CarSql.GetCars();
            var Drivers = DriverSql.GetDrivers();
            var query =
                from car in Cars
                from driver in Drivers
                where car.IdDriver == driver.Id_Driver
                select new CarViewModel
                {
                    IdCar = car.Id_Car,
                    MarkCar = car.MarkCar,
                    Bodywork = car.Bodywork,
                    ColorCar = car.ColorCar,
                    NumberCar = car.NumberCar,
                    Status = car.Status,
                    Driver = driver.Sname
                };
            //DataContext = this;
            CarsVM = new List<CarViewModel>(query);
            carGrid.ItemsSource = CarsVM;
        }
    }
}

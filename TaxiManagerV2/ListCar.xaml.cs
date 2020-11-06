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
        public List<Car> Cars { get; set; }
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
            Cars = CarSql.GetCars();
            DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddCar().ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (carGrid.SelectedIndex == -1)
                return;
            Car car = (Car)carGrid.SelectedItem;
            AddCar addCar = new AddCar(car);
            addCar.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (carGrid.SelectedIndex == -1)
                return;
            Car car = (Car)carGrid.SelectedItem;
            car.Delete();
            Cars.Remove(car);
        }
    }
}

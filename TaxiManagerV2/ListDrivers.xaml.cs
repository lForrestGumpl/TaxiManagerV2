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
    /// Логика взаимодействия для ListDrivers.xaml
    /// </summary>
    public partial class ListDrivers : Window, INotifyPropertyChanged
    {
        private Driver selectedDriver;

        public List<Driver> Drivers { get; set; }
        public Driver SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                SignalChanged();
            }
        }
        public ListDrivers()
        {
            InitializeComponent();
            DB dB = new DB();
            Drivers = DriverSql.GetDrivers();
            DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string name = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddDriver().ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (driversGrid.SelectedIndex == -1)
                return;
            Driver driver = (Driver)driversGrid.SelectedItem;
            AddDriver addDriver = new AddDriver(driver);
            addDriver.ShowDialog();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (driversGrid.SelectedIndex == -1)
                return;
            Driver driver = (Driver)driversGrid.SelectedItem;
            driver.Delete();
            Drivers.Remove(driver);
        }
    }
}

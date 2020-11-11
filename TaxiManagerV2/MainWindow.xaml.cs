using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TaxiManagerV2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private RequestViewModel selectedRequest;
        public List<RequestViewModel> RequestsVM { get; set; }
        public RequestViewModel SelectedRequest
        {
            get => selectedRequest;
            set
            {
                selectedRequest = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IdRequest"));
                SignalChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DB dB = new DB();
            var Requests = RequestSql.GetRequests();
            var Drivers = DriverSql.GetDrivers();
            var Cars = CarSql.GetCars();
            var query =
                from request in Requests
                from driver in Drivers
                from car in Cars
                where request.IdDriver == driver.Id_Driver
                where request.IdCar == car.Id_Car
                select new RequestViewModel
                {
                    IdRequest = request.IdRequest,
                    Sname_Client = request.Sname_Client,
                    Fname = request.Fname,
                    Address = request.Address,
                    Driver = driver.Sname,
                    Car = car.NumberCar
                };
            //DataContext = this;
            RequestsVM = new List<RequestViewModel>(query);
            requestsGrid.ItemsSource = RequestsVM;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ListCar listCar = new ListCar();
            listCar.Show();
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            ListDrivers listDrivers = new ListDrivers();
            listDrivers.Show();
        }

        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            ListManager listManager = new ListManager();
            listManager.Show();
        }

        private void MenuItem_Click3(object sender, RoutedEventArgs e)
        {
            ListRate listRate = new ListRate();
            listRate.Show();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddRequest addRequest = new AddRequest();
            if (addRequest.ShowDialog() == true)
            {
                var Drivers = DriverSql.GetDrivers();
                var Cars = CarSql.GetCars();

                RequestViewModel requestViewModel = new RequestViewModel
                {
                    IdRequest = addRequest.edit.IdRequest,
                    Sname_Client = addRequest.edit.Sname_Client,
                    Fname = addRequest.edit.Fname,
                    Address = addRequest.edit.Address,
                    Driver = Drivers.FirstOrDefault(x => x.Id_Driver == addRequest.edit.IdDriver).Sname,
                    Car = Cars.FirstOrDefault(x => x.Id_Car == addRequest.edit.IdCar).NumberCar
                };
                RequestsVM.Add(requestViewModel);

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (requestsGrid.SelectedIndex == -1)
                return;
            RequestViewModel requestViewModel = (RequestViewModel)requestsGrid.SelectedItem;
            Request request = RequestSql.GetRequestById(requestViewModel.IdRequest);
            request.Delete();
            RequestsVM.Remove(requestViewModel);


        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var Requests = RequestSql.GetRequests();
            var Drivers = DriverSql.GetDrivers();
            var Cars = CarSql.GetCars();
            var query =
                from request in Requests
                from driver in Drivers
                from car in Cars
                where request.IdDriver == driver.Id_Driver
                where request.IdCar == car.Id_Car
                select new RequestViewModel
                {
                    IdRequest = request.IdRequest,
                    Sname_Client = request.Sname_Client,
                    Fname = request.Fname,
                    Address = request.Address,
                    Driver = driver.Sname,
                    Car = car.NumberCar
                };
            requestsGrid.ItemsSource = query;

        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                int pageMargin = 6;
                Size pageSize = new Size(printDialog.PrintableAreaWidth - pageMargin * 2,
                printDialog.PrintableAreaHeight - 20);
                requestsGrid.Measure(pageSize);
                requestsGrid.Arrange(new Rect(0, 0, 0, 0));
                requestsGrid.Measure(pageSize);
                printDialog.PrintVisual(requestsGrid, "DataGrid");
                requestsGrid.LayoutTransform = null;
            }
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            string constring = "server=" + Properties.Settings.Default.server + ";database=" + Properties.Settings.Default.database + ";user=" + Properties.Settings.Default.login + ";pwd=" + Properties.Settings.Default.password + ";";
            constring += "charset=utf8;convertzerodatetime=true;";
            string file = "backup.sql";
            using (MySqlConnection connect = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = connect;
                        connect.Open();
                        mb.ExportToFile(file);
                        connect.Close();
                        MessageBox.Show("Резервная копия базы данных создана" + file);
                    }
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var textItemBox = comboSearch.SelectionBoxItem;
            var TextSearch = textSearch.Text;
            var Requests = RequestSql.GetRequests();
            var Drivers = DriverSql.GetDrivers();
            var Cars = CarSql.GetCars();
            if (textItemBox.ToString() == "Введите Имя клиента")
            {
                if (TextSearch.Length == 0)
                {
                    MessageBox.Show("Сначала введите данные в поиск");
                }
                else
                {
                    var serRequest = Requests.Where(x => x.Fname.ToLower().Contains(TextSearch.ToLower()));
                    if (serRequest.Count() == 0)
                    {
                        MessageBox.Show($"{TextSearch} не существует");
                    }
                    else
                    {
                        var query =
                           from request in Requests
                           from driver in Drivers
                           from car in Cars
                           where request.IdDriver == driver.Id_Driver
                           where request.IdCar == car.Id_Car
                           select new RequestViewModel
                           {
                               IdRequest = request.IdRequest,
                               Sname_Client = request.Sname_Client,
                               Fname = request.Fname,
                               Address = request.Address,
                               Driver = driver.Sname,
                               Car = car.NumberCar
                           };
                        requestsGrid.ItemsSource = query;
                        MessageBox.Show($"Клиенты с именем {TextSearch}");
                    }
                }
              
            }
            if (textItemBox.ToString() == "Введите фамилию клиента")
            {
                if (TextSearch.Length == 0)
                {
                    MessageBox.Show("Сначала введите данные в поиск");
                }
                else
                {
                    var serRequest = Requests.Where(x => x.Sname_Client.ToLower().Contains(TextSearch.ToLower()));
                    if (serRequest.Count() == 0)
                    {
                        MessageBox.Show($"{TextSearch} не существует");
                    }
                    else
                    {
                        var query =
                           from request in Requests
                           from driver in Drivers
                           from car in Cars
                           where request.IdDriver == driver.Id_Driver
                           where request.IdCar == car.Id_Car
                           select new RequestViewModel
                           {
                               IdRequest = request.IdRequest,
                               Sname_Client = request.Sname_Client,
                               Fname = request.Fname,
                               Address = request.Address,
                               Driver = driver.Sname,
                               Car = car.NumberCar
                           };
                        requestsGrid.ItemsSource = query;
                        MessageBox.Show($"Клиенты с фамиилей {TextSearch}");
                    }
                }
            }
            if (textItemBox.ToString() == "Введите водитель")
            {
                if (TextSearch.Length == 0)
                {
                    MessageBox.Show("Сначала введите данные в поиск");
                }
                else
                {
                    var serRequest = Drivers.Where(x => x.Sname.ToLower().Contains(TextSearch.ToLower()));
                    if (serRequest.Count() == 0)
                    {
                        MessageBox.Show($"{TextSearch} не существует");
                    }
                    else
                    {
                        var query =
                           from request in Requests
                           from driver in Drivers
                           from car in Cars
                           where request.IdDriver == driver.Id_Driver
                           where request.IdCar == car.Id_Car
                           select new RequestViewModel
                           {
                               IdRequest = request.IdRequest,
                               Sname_Client = request.Sname_Client,
                               Fname = request.Fname,
                               Address = request.Address,
                               Driver = driver.Sname,
                               Car = car.NumberCar
                           };
                        requestsGrid.ItemsSource = query;
                        MessageBox.Show($"Заявки, выполненные водителем - {TextSearch}");
                    }
                }
            }
        }
    }
}

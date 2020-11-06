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
        private Request selectedRequest;
        public List<Request> Requests { get; set; }
        public Request SelectedRequest
        {
            get => selectedRequest;
            set
            {
                selectedRequest = value;
                SignalChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DB dB = new DB();
            Requests = RequestSql.GetRequests();
            DataContext = this;
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
                Requests.Add(addRequest.edit);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (requestsGrid.SelectedIndex == -1)
                return;
            Request request = (Request)requestsGrid.SelectedItem;
            request.Delete();
            Requests.Remove(request);
            

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}

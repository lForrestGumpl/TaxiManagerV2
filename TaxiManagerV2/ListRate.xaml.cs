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
    /// Логика взаимодействия для ListRate.xaml
    /// </summary>
    public partial class ListRate : Window, INotifyPropertyChanged
    {
        private Rate selectedRate;
        public List<Rate> Rates { get; set; }
        public Rate SelectedRate
        {
            get => selectedRate;
            set
            {
                selectedRate = value;
                SignalChanged();
            }
        }

        public ListRate()
        {
            InitializeComponent();
            DB dB = new DB();
            Rates = RateSql.GetRates();
            DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string name = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddRate().ShowDialog();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (rateGrid.SelectedIndex == -1)
                return;
            Rate rate = (Rate)rateGrid.SelectedItem;
            AddRate addRate = new AddRate(rate);
            addRate.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (rateGrid.SelectedIndex == -1)
                return;
            Rate rate = (Rate)rateGrid.SelectedItem;
            rate.Delete();
            Rates.Remove(rate);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var Rates = RateSql.GetRates();
            var query =
                  from rate in Rates
                  select new Rate { 
                      IdRate = rate.IdRate,
                      Name =  rate.Name,
                      Price = rate.Price,
                      Range = rate.Range
                  };
            rateGrid.ItemsSource = query;
        }
    }
}

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

    }
}

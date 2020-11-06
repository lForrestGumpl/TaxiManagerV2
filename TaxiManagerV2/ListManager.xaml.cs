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
    /// Логика взаимодействия для ListManager.xaml
    /// </summary>
    public partial class ListManager : Window, INotifyPropertyChanged
    {
        private Manager selectedManager;
        public List<Manager> Managers { get; set; }
        public Manager SelectedManager
        {
            get => selectedManager;
            set
            {
                selectedManager = value;
                SignalChanged();
            }
        }
        public ListManager()
        {
            InitializeComponent();
            DB dB = new DB();
            Managers = ManagerSql.GetManagers();
            DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string name = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddManager().ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (managerGrid.SelectedIndex == -1)
                return;
            Manager manager = (Manager)managerGrid.SelectedItem;
            AddManager addManager = new AddManager(manager);
            addManager.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (managerGrid.SelectedIndex == -1)
                return;
            Manager manager = (Manager)managerGrid.SelectedItem;
            manager.Delete();
            Managers.Remove(manager);
        }
    }
}

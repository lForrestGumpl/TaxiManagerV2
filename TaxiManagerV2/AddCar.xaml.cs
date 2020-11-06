using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        public Car edit;
        public AddCar()
        {
            InitializeComponent();
            edit = new Car();
            DataContext = edit;
        }
        public AddCar(Car edit)
        {
            InitializeComponent();
            this.edit = edit;
            DataContext = edit;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (edit.IdCar == 0)
                edit.CreateCar();
            else
                edit.Update();

        }
    }
}

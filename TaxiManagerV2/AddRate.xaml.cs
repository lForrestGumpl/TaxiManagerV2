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
    /// Логика взаимодействия для AddRate.xaml
    /// </summary>
    public partial class AddRate : Window
    {
        public Rate edit;
        public AddRate()
        {
            InitializeComponent();
            edit = new Rate();
            DataContext = edit;
        }
        public AddRate(Rate edit)
        {
            InitializeComponent();
            this.edit = edit;
            DataContext = edit;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (edit.IdRate == 0)
                edit.CreateRate();
            else
                edit.Update();

        }
    }
}

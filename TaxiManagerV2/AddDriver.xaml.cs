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
    /// Логика взаимодействия для AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        public Driver edit;
        public AddDriver()
        {
            InitializeComponent();
            edit = new Driver();
            DataContext = edit;
        }
        public AddDriver(Driver edit)
        {
            InitializeComponent();
            this.edit = edit;
            DataContext = edit;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (edit.IdDriver == 0)
                edit.CreateDriver();
            else
                edit.Update();

        }
    }
}

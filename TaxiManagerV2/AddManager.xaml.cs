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
    /// Логика взаимодействия для AddManager.xaml
    /// </summary>
    public partial class AddManager : Window
    {
        public Manager edit;
        public AddManager()
        {
            InitializeComponent();
            edit = new Manager();
            DataContext = edit;
        }
        public AddManager(Manager edit)
        {
            InitializeComponent();
            this.edit = edit;
            DataContext = edit;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (edit.IdManager == 0)
                edit.CreateManager();
            else
                edit.Update();

        }
    }
}

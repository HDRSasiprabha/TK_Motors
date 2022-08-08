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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for EmployeeHandling.xaml
    /// </summary>
    public partial class EmployeeHandling : Page
    {
        public EmployeeHandling()
        {
            InitializeComponent();
        }

        private void Btn_vemp(object sender, RoutedEventArgs e)
        {

            Employeesignupwindow a = new Employeesignupwindow();
            a.Show();
           
        }

        private void Btn_remp(object sender, RoutedEventArgs e)
        {

            empframe.Navigate(new System.Uri("RemoveEmployee.xaml",
               UriKind.RelativeOrAbsolute));
        }

        private void Btn_uemp(object sender, RoutedEventArgs e)
        {

            empframe.Navigate(new System.Uri("UpdateEmployee.xaml",
               UriKind.RelativeOrAbsolute));
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            empframe.Navigate(new System.Uri("Manager_dashboard.xaml",
               UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            empframe.Navigate(new System.Uri("Manager_dashboard.xaml",
               UriKind.RelativeOrAbsolute));
        }
    }
}

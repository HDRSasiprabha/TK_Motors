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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void btn_menu_Click_1(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("Manager_dashboard.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void btn_jobOverview_Click(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("JobOverview.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void btn_inventoryOverview_Click(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("customerDetailsReport.xaml",
                UriKind.RelativeOrAbsolute));

        }

        private void btn_annualalesReport_Click(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("SalesReport.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                         UriKind.RelativeOrAbsolute));
        }

        private void btn_empdetails_Click(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("empDetails.xaml",
                         UriKind.RelativeOrAbsolute));
        }

        private void btn_stock_Click(object sender, RoutedEventArgs e)
        {
            repframe.Navigate(new System.Uri("ViewParts.xaml",
                         UriKind.RelativeOrAbsolute));
        }
    }
}

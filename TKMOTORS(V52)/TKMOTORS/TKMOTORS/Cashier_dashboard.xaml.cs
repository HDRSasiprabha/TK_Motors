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
    /// Interaction logic for Cashier_dashboard.xaml
    /// </summary>
    public partial class Cashier_dashboard : Page
    {
        public Cashier_dashboard()
        {
            InitializeComponent();
        }

        private void btn_handleBill_Click(object sender, RoutedEventArgs e)
        {
           cashierframe.Navigate(new System.Uri("invoicePage.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
            if (main != null)
            {
                // main.Exitbt_PreviewKeyDown(main.Exitbt, e);
                main.Close();
            }
        }

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
            if (main != null)
            {
                // main.Exitbt_PreviewKeyDown(main.Exitbt, e);
                main.Close();
            }
        }
    }
}

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
    /// Interaction logic for Mechanic_dashboard.xaml
    /// </summary>
    public partial class Mechanic_dashboard : Page
    {
        public Mechanic_dashboard()
        {
            InitializeComponent();
        }

       
       

        private void bn_NewJob_Click_2(object sender, RoutedEventArgs e)
        {
            mecframe.Navigate(new System.Uri("Newjob.xaml",
                  UriKind.RelativeOrAbsolute));

        }

        private void btn_Ongoing_Click(object sender, RoutedEventArgs e)
        {
            mecframe.Navigate(new System.Uri("Ongoing.xaml",
                  UriKind.RelativeOrAbsolute));
        }

       

        private void btn_Pending_Click(object sender, RoutedEventArgs e)
        {
            mecframe.Navigate(new System.Uri("PendingJob.xaml",
                  UriKind.RelativeOrAbsolute));
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            /* Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
             if (main != null)
             {
                 // main.Exitbt_PreviewKeyDown(main.Exitbt, e);
                 main.Close();
             }
 */
            QR s = new QR();
            s.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
            if (main != null)
            {
                // main.Exitbt_PreviewKeyDown(main.Exitbt, e);
                main.Close();
            }*/
            QR s = new QR();
            s.Show();
        }
    }
}

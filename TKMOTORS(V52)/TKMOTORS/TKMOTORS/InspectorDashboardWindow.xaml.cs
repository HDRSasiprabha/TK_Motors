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

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for InspectorDashboardWindow.xaml
    /// </summary>
    public partial class InspectorDashboardWindow : Window
    {
        public InspectorDashboardWindow()
        {
            InitializeComponent();
        }

       

        private void btn_register_vehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle_registration a = new Vehicle_registration();
            this.Content = a;
        
        }

        private void btn_registerCus_Click(object sender, RoutedEventArgs e)
        {
            Customerregisterwindow a = new Customerregisterwindow();
            a.Show();
        }

        private void btn_Ajob_Click(object sender, RoutedEventArgs e)
        {
            Accident_page a = new Accident_page();
            this.Content = a;
        }

        private void btn_Pjob_Click(object sender, RoutedEventArgs e)
        {
            Painter_page a = new Painter_page();
            this.Content = a;
        }

        private void btn_Mjob_Click(object sender, RoutedEventArgs e)
        {
            Mechanic_page a = new Mechanic_page();
            this.Content = a;
        }


        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            /*  Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
              if (main != null)
              {
                  // main.Exitbt_PreviewKeyDown(main.Exitbt, e);
                  main.Close();
              }*/
            QR s = new QR();
            s.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*  Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
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

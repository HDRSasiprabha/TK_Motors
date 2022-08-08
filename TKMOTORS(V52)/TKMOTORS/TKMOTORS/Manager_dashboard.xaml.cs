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
using System.Windows.Threading;
namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Manager_dashboard.xaml
    /// </summary>
    public partial class Manager_dashboard : Page
    {
        public Manager_dashboard()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        ConnectDB db = new ConnectDB();
        private void UpdateTimer_Tick(object sender,EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();
            
        }
        private void Btn_close(object sender, RoutedEventArgs e)
        {
            Managerlogininwindow main = Application.Current.Windows[2] as Managerlogininwindow;
            if (main != null)
            {
                // main.Exitbt_PreviewKeyDown(main.Exitbt, e);
                main.Close();
            }
            
        }

        private void Btn_Emp(object sender, RoutedEventArgs e)
        {     
            
            MainFrame.Navigate(new System.Uri("EmployeeHandling.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void Btn_Invenover(object sender, RoutedEventArgs e)
        {

            MainFrame.Navigate(new System.Uri("InventoryHandling.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void Btn_Repover(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("Reports.xaml",
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

        private void btn_changePswrd_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("ChangePassword.xaml",
           UriKind.RelativeOrAbsolute));
        }

        private void btn_apointment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("viewAppoinment.xaml",
           UriKind.RelativeOrAbsolute));
        }
    }
}

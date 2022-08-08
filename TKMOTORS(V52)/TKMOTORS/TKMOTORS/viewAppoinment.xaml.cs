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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Threading;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for viewAppoinment.xaml
    /// </summary>
    public partial class viewAppoinment : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public viewAppoinment()
        {
            InitializeComponent();
            viewall();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        void viewall()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                da = new SqlDataAdapter("Select * from Appointment", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datagrid_view.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            vempframe.Navigate(new System.Uri("Manager_dashboard.xaml",
              UriKind.RelativeOrAbsolute));
        }
    }
}

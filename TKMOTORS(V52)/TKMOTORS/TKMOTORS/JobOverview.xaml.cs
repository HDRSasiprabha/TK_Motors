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
using LiveCharts;
using LiveCharts.Wpf;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for JobOverview.xaml
    /// </summary>
    public partial class JobOverview : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public JobOverview()
        {
            InitializeComponent();
            completed_jobs();
            pending_jobs();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        public void completed_jobs()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select Job_ID,documentation,color,Job_type,Mechanic_ID,Veh_ID from job where Job_status = 'Finished'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datgrid_Cjobs.ItemsSource = dt.DefaultView;
            con.Close();
        }
        public void pending_jobs()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select Job_ID,documentation,color,Job_type,Mechanic_ID,Veh_ID from job where Job_status='Pending'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datgrid_Pjobs.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            jobframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            jobframe.Navigate(new System.Uri("Reports.xaml",
                UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            jobframe.Navigate(new System.Uri("Reports.xaml",
               UriKind.RelativeOrAbsolute));
        }
    }
}

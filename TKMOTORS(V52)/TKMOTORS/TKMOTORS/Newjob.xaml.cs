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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Threading;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Newjob.xaml
    /// </summary>
    public partial class Newjob : Page
    {

        SqlConnection con;
        SqlDataAdapter da;
        SqlDataAdapter ba;


        public Newjob()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("SELECT [Job_ID],[documentation] as Documentation,[color] as Colour,[Job_type] from job WHERE Job_Status='New Job'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dg_NewJobs.ItemsSource = dt.DefaultView;
            con.Close();

            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            ba = new SqlDataAdapter("SELECT [Job_ID],[Damage_pts] as Damaged_Part from Damage_Part WHERE Checked=1 order by Job_ID", con);
            DataTable data = new DataTable();
            ba.Fill(data);
            Dg_damagepts.ItemsSource = data.DefaultView;
            con.Close();

        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            NJframe.Navigate(new System.Uri("Mechanic_dashboard.xaml",
                 UriKind.RelativeOrAbsolute));
        }

        private void btn_quotation_Click(object sender, RoutedEventArgs e)
        {
            NJframe.Navigate(new System.Uri("Quotation.xaml",
            UriKind.RelativeOrAbsolute));
        }
    }
}

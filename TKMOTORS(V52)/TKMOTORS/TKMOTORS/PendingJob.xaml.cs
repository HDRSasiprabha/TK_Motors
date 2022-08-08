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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for PendingJob.xaml
    /// </summary>
    public partial class PendingJob : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        
        public PendingJob()
        {
            InitializeComponent();
            fillcmb();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("SELECT [Job_ID],[documentation] as Documentation,[color] as Colour,[Job_type] from job WHERE Job_Status='Pending'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dg_pending.ItemsSource = dt.DefaultView;
            con.Close();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        void fillcmb()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Job_ID]FROM [dbo].[Job] where Job_status='Pending'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string jid = dr.GetString(0);
                    cmb_jid.Items.Add(jid);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            penframe.Navigate(new System.Uri("Mechanic_dashboard.xaml",
                 UriKind.RelativeOrAbsolute));
        }

       

       

        private void btn_Start_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmb_jid.SelectedItem == null)

                MessageBox.Show("Please select job ID", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                ConnectDB.openConnection();
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.sql = "UPDATE [dbo].[Job]SET[Job_status] = 'Ongoing' WHERE Job_ID =@jd";

                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.cmd.Parameters.Clear();
                ConnectDB.cmd.Parameters.AddWithValue("@jd", cmb_jid.SelectedItem);
                ConnectDB.cmd.ExecuteNonQuery();

                MessageBox.Show("Job started", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Information);
                penframe.Navigate(new System.Uri("PendingJob.xaml",
                                 UriKind.RelativeOrAbsolute));
            }
        }

       
    }
}

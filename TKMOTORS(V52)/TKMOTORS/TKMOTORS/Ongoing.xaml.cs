using Microsoft.Win32;
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
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using System.IO;
using System.Windows.Threading; 

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Ongoing.xaml
    /// </summary>
    public partial class Ongoing : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
      //  SqlCommand cmd;

        public Ongoing()
        {
            InitializeComponent();
            fillcmb();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("SELECT [Job_ID],[documentation] as Documentation,[color] as Colour,[Job_type] from job  WHERE Job_Status='Ongoing'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dg_ongoing.ItemsSource = dt.DefaultView;
            con.Close();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        void fillcmb()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Job_ID]FROM [dbo].[Job] where Job_status='Ongoing'", con);
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
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            ongoingframe.Navigate(new System.Uri("Mechanic_dashboard.xaml",
                  UriKind.RelativeOrAbsolute));
        }

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                String filepath = openFileDialog.FileName;
                //MessageBox.Show(filepath);
                sendMail(filepath, "jude8899@ymail.com");

            }
        }

        public void sendMail(String path, string toMail)
        {

            //Mail
            try
            {
                MailMessage newMail = new MailMessage();
                newMail.From = new MailAddress("milindascania@gmail.com"); //email vari
                newMail.To.Add(new MailAddress(toMail));
                newMail.Subject = "Test Subject";
                newMail.IsBodyHtml = true;

                var inlineLogo = new LinkedResource(path, "image/png");
                inlineLogo.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                string body = string.Format(@"
                 <img src=""cid:{0}"" />
            <p>Current Progress of vehicle</p>
        ", inlineLogo.ContentId);

                var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                view.LinkedResources.Add(inlineLogo);
                newMail.AlternateViews.Add(view);

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("milindascania@gmail.com", "scania678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(newMail);
                //attachment.Dispose();

                MessageBox.Show("Mail Sent successfully", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Mail not sent!", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Start_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmb_jid.SelectedItem == null)

                MessageBox.Show("Please select job ID", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                ConnectDB.openConnection();
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.sql = "UPDATE [dbo].[Job]SET[Job_status] = 'Finished' WHERE Job_ID =@jd";

                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.cmd.Parameters.Clear();
                ConnectDB.cmd.Parameters.AddWithValue("@jd", cmb_jid.SelectedItem);
                ConnectDB.cmd.ExecuteNonQuery();

                MessageBox.Show("Job Finished", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Information);
                ongoingframe.Navigate(new System.Uri("Ongoing.xaml",
                                 UriKind.RelativeOrAbsolute));
            }
        }

       
    }
}

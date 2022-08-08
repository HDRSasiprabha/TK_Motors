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
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Windows.Threading;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Quotation.xaml
    /// </summary>
    public partial class Quotation : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        string VehicleId;
        string mail;
        string cusname;
        string date;
        string cusadd;
        string cusid;


        public Quotation()
        {

            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            fillcmb();


            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("SELECT [part_ID] AS 'Part ID',[part_name],[price],[quantity_in_hand],'' AS Qty from part", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dg_parts.ItemsSource = dt.DefaultView;
            con.Close();


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader srn = null;
            cmd.Connection = conn;
            cmd.CommandText = "SELECT TOP(1)[Quotation_ID] FROM [dbo].[Quotation] ORDER BY [Quotation_ID] DESC ";
            conn.Open();
            srn = cmd.ExecuteReader();


            if (srn.Read())
            {
                string str = srn.GetValue(0).ToString();


                string digits = new string(str.Where(char.IsDigit).ToArray());
                string letters = new string(str.Where(char.IsLetter).ToArray());

                int number;
                if (!int.TryParse(digits, out number)) //int.Parse would do the job since only digits are selected
                {
                    Console.WriteLine("Something weired happened");
                }

                string newStr = letters + (++number).ToString("D3");
                txt_qid.Text = newStr.ToString();


            }
            conn.Close();


        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
           date = DateTime.Now.ToString();

        }
        void fillcmb()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Job_ID]FROM [dbo].[Job] where Job_status='New job'", con);
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
        private void btn_send_Click(object sender, RoutedEventArgs e)
        {

           if (cmb_jid.SelectedItem == null)

                MessageBox.Show("Please select job ID", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

          

           else
            {

                ConnectDB.sql = "SELECT [Job_ID],[documentation],[color],[Job_type],[Job_status],[Mechanic_ID],[Veh_ID],[Cus_ID]FROM[dbo].[Job] where Job_ID='"+cmb_jid.SelectedItem+"'";
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.da = new SqlDataAdapter(ConnectDB.cmd);
                ConnectDB.dt = new DataTable();
                ConnectDB.da.Fill(ConnectDB.dt);

                if (ConnectDB.dt.Rows.Count > 0)
                {
                    
                    VehicleId = ConnectDB.dt.Rows[0]["Veh_ID"].ToString();
                    cusid = ConnectDB.dt.Rows[0]["Cus_ID"].ToString();


                }

                ConnectDB.sql = "SELECT [customer_ID],[cname],[address],[Email]FROM[dbo].[Customer] where customer_ID='"+ cusid + "'";
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.da = new SqlDataAdapter(ConnectDB.cmd);
                ConnectDB.dt = new DataTable();
                ConnectDB.da.Fill(ConnectDB.dt);

                if (ConnectDB.dt.Rows.Count > 0)
                {

                    mail = ConnectDB.dt.Rows[0]["Email"].ToString();
                    cusname= ConnectDB.dt.Rows[0]["cname"].ToString();
                    cusadd= ConnectDB.dt.Rows[0]["address"].ToString();

                }



                sendMail(mail);
                SaveData();
                QFrame.Navigate(new System.Uri("Quotation.xaml",
                UriKind.RelativeOrAbsolute));






            }
            
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        public void sendMail(string toMail)
        {

            //Mail
            try
            {
                MailMessage newMail = new MailMessage();
                newMail.From = new MailAddress("milindascania@gmail.com"); //email vari
                newMail.To.Add(new MailAddress(toMail));
                newMail.Subject = "Test Subject";
                newMail.IsBodyHtml = true;

                var inlineLogo = new LinkedResource(@"C:\ThePealKitchen\test.jpg", "image/png");
                inlineLogo.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                string body = string.Format(@"<div id='project'>" +
        "<div><span> CLIENT </span> "+ cusname + " </div>" +
        "<div><span> ADDRESS </span> " + cusadd + " </div>" +
        "<div><span> EMAIL </span> " + mail + "</div>" +
        "<div><span> DATE </span> "+date+" </div>" +
        
      "</div>", inlineLogo.ContentId);


                body = body + string.Format(@"<table>
        <thead>
          <tr>
            <th>PART ID</th>
            <th>PART NAME</th>
            <th>PRICE</th>
            <th>QTY</th>
            <th>TOTAL</th>
          </tr>
        </thead>
        <tbody>");

                var rows = GetDataGridRows(Dg_parts);
                decimal total = 0;
                foreach (DataGridRow row in rows)
                {

                    DataRowView rowView = (DataRowView)row.Item;
                    CheckBox chk = Dg_parts.Columns[0].GetCellContent(row) as CheckBox;
                    if ((bool)chk.IsChecked)
                    {
                        body = body + string.Format(@"<tr>");
                        //foreach (DataGridColumn column in Dg_parts.Columns)
                        //{
                        //if (column.GetCellContent(row) is TextBlock)
                        //{
                        //TextBlock cellContent = column.GetCellContent(row) as TextBlock;
                        //MessageBox.Show(cellContent.Text);
                        body = body + string.Format(@"<td>");
                        body = body + string.Format(@"" + rowView.Row.ItemArray[0].ToString()); //Part Id
                        body = body + string.Format(@"</td>");

                        body = body + string.Format(@"<td>");
                        body = body + string.Format(@"" + rowView.Row.ItemArray[1].ToString()); //Part Name
                        body = body + string.Format(@"</td>");

                        body = body + string.Format(@"<td>");
                        body = body + string.Format(@"" + rowView.Row.ItemArray[2].ToString()); //Price
                        body = body + string.Format(@"</td>");

                        body = body + string.Format(@"<td>");
                        body = body + string.Format(@"" + rowView.Row.ItemArray[4].ToString()); //Qty
                        body = body + string.Format(@"</td>");

                        body = body + string.Format(@"<td>");
                        body = body + string.Format(@"" + Convert.ToDecimal(rowView.Row.ItemArray[2].ToString()) * Convert.ToDecimal(rowView.Row.ItemArray[4].ToString()) + ""); //Total
                        body = body + string.Format(@"</td>");
                        //}
                        //}

                        body = body + string.Format(@"</tr>");

                        total = total + (Convert.ToDecimal(rowView.Row.ItemArray[2].ToString()) * Convert.ToDecimal(rowView.Row.ItemArray[4].ToString()));
                    }
                }

                body = body + string.Format(@"
          <tr>
            <td colspan = '4' class='grand total'>GRAND TOTAL</td>
            <td class='grand total'>Rs. " + total.ToString());
            body = body + string.Format(@" </td>
          </tr>
        </tbody>
      </table>");

                var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                //view.LinkedResources.Add(inlineLogo);
                newMail.AlternateViews.Add(view);

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("milindascania@gmail.com", "scania678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(newMail);
                //attachment.Dispose();

                MessageBox.Show("Part Quotation send to customer email and data saved", "TK Motors", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void SaveData()
        {

            try
            {

                //Quotation id save to database here
                ConnectDB.openConnection();
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.sql = "INSERT INTO [dbo].[Quotation]([Quotation_ID],[Mec_ID],[v_ID],[Job_ID])VALUES(@QID,@MID,@VID,@JOBID)";

                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.cmd.Parameters.Clear();
                ConnectDB.cmd.Parameters.AddWithValue("@QID", txt_qid.Text);
                ConnectDB.cmd.Parameters.AddWithValue("@MID", "E0002");
                ConnectDB.cmd.Parameters.AddWithValue("@VID", VehicleId );
                ConnectDB.cmd.Parameters.AddWithValue("@JOBID", cmb_jid.SelectedItem);
                ConnectDB.cmd.ExecuteNonQuery();


                //data save to vehicle_Job_Quotation
                

                
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.sql = "INSERT INTO [dbo].[Vehicle_Job_Quotation]([VID],[JID],[QID])VALUES(@v,@j,@q)";

                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.cmd.Parameters.Clear();
                ConnectDB.cmd.Parameters.AddWithValue("@q", txt_qid.Text);
                ConnectDB.cmd.Parameters.AddWithValue("@j", cmb_jid.SelectedItem);
                ConnectDB.cmd.Parameters.AddWithValue("@v", VehicleId);
                ConnectDB.cmd.ExecuteNonQuery();


                
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.sql = "UPDATE [dbo].[Job]SET[Job_status] = 'Pending' WHERE Job_ID =@jd";

                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.cmd.Parameters.Clear();
                ConnectDB.cmd.Parameters.AddWithValue("@jd", cmb_jid.SelectedItem);
                ConnectDB.cmd.ExecuteNonQuery();


                var rows = GetDataGridRows(Dg_parts);
                //decimal total = 0;
                
                foreach (DataGridRow row in rows)
                {

                    DataRowView rowView = (DataRowView)row.Item;
                    CheckBox chk = Dg_parts.Columns[0].GetCellContent(row) as CheckBox;
                    if ((bool)chk.IsChecked)
                    {


                        ConnectDB.openConnection();
                        ConnectDB.cmd.CommandType = CommandType.Text;
                        ConnectDB.sql = "INSERT INTO [dbo].[part_quotation] ([QID],[PID],[Quantity]) VALUES (@QID,@PID,@qty)";

                        ConnectDB.cmd.CommandText = ConnectDB.sql;
                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@QID",txt_qid.Text); 
                        ConnectDB.cmd.Parameters.AddWithValue("@PID", rowView.Row.ItemArray[0].ToString());
                        ConnectDB.cmd.Parameters.AddWithValue("@qty", rowView.Row.ItemArray[4].ToString());
                        ConnectDB.cmd.ExecuteNonQuery();




                        //TO DO - Complete Insert Query








                        
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            QFrame.Navigate(new System.Uri("Mechanic_dashboard.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            QFrame.Navigate(new System.Uri("Newjob.xaml",
               UriKind.RelativeOrAbsolute));
        }

       
    }
}

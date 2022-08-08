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
    /// Interaction logic for invoicePage.xaml
    /// </summary>
    public partial class invoicePage : Page
    {

        SqlDataAdapter daa;

        public invoicePage()
        {
            InitializeComponent();
            string cashid = "E0003";
            txt_cashid.Text = cashid.ToString();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader srn = null;
            cmd.Connection = conn;
            cmd.CommandText = "Select top(1) Bill_ID from Bill order by  Bill_ID  desc ";
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
                txt_bid.Text = newStr.ToString();

            }
            conn.Close();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        public static SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        public static SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        public static SqlCommand cmd = new SqlCommand("", con);
        ConnectDB db = new ConnectDB();
        private void btn_dash_Click(object sender, RoutedEventArgs e)
        {
            invframe.Navigate(new System.Uri("Cashier_dashboard.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void txt_qid_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (con1.State == ConnectionState.Closed)
                {
                    con1.Open();
                }
                if (con2.State == ConnectionState.Closed)
                {
                    con2.Open();
                }
                if (txt_qid.Text != "")
                {
                    {
                        //SqlCommand cmd1 = new SqlCommand("SELECT  sum(part.price * part_quotation.Quantity) as net_tot FROM part_quotation, part where part_quotation.QID = '" + txt_qid.Text + "' and part_quotation.PID = part.part_ID; ", con);
                        //SqlDataReader da1 = cmd1.ExecuteReader();
                        DataTable dt1 = db.getData("SELECT  sum(part.price * part_quotation.Quantity) as net_tot FROM part_quotation, part where part_quotation.QID = '" + txt_qid.Text + "' and part_quotation.PID = part.part_ID; ");
                        if (dt1.Rows.Count > 0)
                        {
                            txt_sub_tot.Text = dt1.Rows[0][0].ToString();

                        }

                        //da1.Close();

                    }
                    {

                        //SqlCommand cmd2 = new SqlCommand("select v_ID from quotation where Quotation_ID='" + txt_qid.Text + "'", con1);
                        //SqlDataReader da2 = cmd2.ExecuteReader();
                        DataTable dt2 = db.getData("select v_ID from quotation where Quotation_ID='" + txt_qid.Text + "'");

                        if (dt2.Rows.Count > 0)
                        {
                            txt_vid.Text = dt2.Rows[0][0].ToString();

                        }
                        //da2.Close();
                    }

                    daa = new SqlDataAdapter("SELECT part_quotation.PID, part.part_name, part.price, part_quotation.Quantity,part.price*part_quotation.Quantity as Total FROM part_quotation, part where part_quotation.QID = '" + txt_qid.Text + "' and part_quotation.PID = part.part_ID; ", con2);
                    DataTable dt = new DataTable();
                    daa.Fill(dt);
                    dg_invoice.ItemsSource = dt.DefaultView;
                    daa.Dispose();



                    con.Close();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void txt_tot_TextChanged(object sender, TextChangedEventArgs e)
        {



        }

        private void btn_paid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_qid.Text))
                {
                    MessageBox.Show("Quotation ID cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_cashid.Text))
                {
                    MessageBox.Show("Cashier ID cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_cusid.Text))
                {
                    MessageBox.Show("Customer ID cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_vid.Text))
                {
                    MessageBox.Show("Vehicle ID cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_dis.Text))
                {
                    MessageBox.Show("Discount cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_Scharge.Text))
                {
                    MessageBox.Show("Service Charge cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_tot.Text))
                {
                    MessageBox.Show("Net. Total cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_sub_tot.Text))
                {
                    MessageBox.Show("Sub Total cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_bid.Text))
                {
                    MessageBox.Show("Bill ID cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    string query = "Insert into bill values ('" + txt_bid.Text + "', '" + txt_sub_tot.Text + "', '" + txt_dis.Text + "',  '" + txt_Scharge.Text + "','2020.02.13','" + txt_cashid.Text + "','" + txt_cusid.Text + "','" + txt_qid.Text + "','" + txt_vid.Text + "') ";
                    int i = db.add_update_remove(query);
                    if (i == 1)
                    {
                        MessageBox.Show("Bill made Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_bid.Clear();
                        txt_qid.Clear();
                        txt_dis.Clear();
                        txt_Scharge.Clear();
                        txt_tot.Clear();
                        txt_sub_tot.Clear();
                        invframe.Navigate(new System.Uri("invoicePage.xaml",
                 UriKind.RelativeOrAbsolute));

                    }
                    else
                        MessageBox.Show("Bill Cannot make", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_cal_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_dis.Text))
            {
                MessageBox.Show("Discount cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            else if (string.IsNullOrEmpty(txt_Scharge.Text))
            {
                MessageBox.Show("Service Charge cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (string.IsNullOrEmpty(txt_sub_tot.Text))
            {
                MessageBox.Show("Sub Total cannot be blank.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                double sub_tot, net_tot, dis, scharge;
                sub_tot = Convert.ToDouble(txt_sub_tot.Text);
                dis = Convert.ToDouble(txt_dis.Text);
                scharge = Convert.ToDouble(txt_Scharge.Text);
                net_tot = ((scharge + sub_tot) - ((scharge + sub_tot) * dis));
                txt_tot.Text = net_tot.ToString();
            }

        }
    }
}
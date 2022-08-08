using System;
using System.Collections.Generic;
using System.Data;
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
using System.Configuration;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Accident_page.xaml
    /// </summary>
    public partial class Accident_page : Page
    {
        string cusid;
        public Accident_page()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader srn = null;
            cmd.Connection = conn;
            cmd.CommandText = "SELECT TOP(1)[Job_ID] FROM [dbo].[job] ORDER BY [Job_ID] DESC ";
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
                txt_JID.Text = newStr.ToString();
                

            }
            conn.Close();

        }

        private void btn_continue_Click(object sender, RoutedEventArgs e)
        {
            

            if (txt_vehicleNumber.Text.Length == 0)

                MessageBox.Show("Please enter Vehicle number", "TKMotors", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string doc = "";
                if (CB_doc.IsChecked == true)
                    doc = "Included";
                else
                    doc = "Not included";

                ConnectDB.sql = "SELECT [Cus_ID]FROM[dbo].[Vehicle] where Vehicle_ID ='" + txt_vehicleNumber.Text + "'";
                ConnectDB.cmd.CommandType = CommandType.Text;
                ConnectDB.cmd.CommandText = ConnectDB.sql;
                ConnectDB.da = new SqlDataAdapter(ConnectDB.cmd);
                ConnectDB.dt = new DataTable();
                ConnectDB.da.Fill(ConnectDB.dt);

                if (ConnectDB.dt.Rows.Count > 0)
                {

                    cusid = ConnectDB.dt.Rows[0]["Cus_ID"].ToString();



                }

                try
                {

                    ConnectDB.openConnection();
                    ConnectDB.cmd.CommandType = CommandType.Text;
                    ConnectDB.sql = "INSERT INTO [dbo].[job] ([Job_ID],[documentation],[Job_type],[Mechanic_ID],[Job_Status],[Veh_ID],[Cus_ID]) VALUES('" + txt_JID.Text + "',@Documentation,@JobType,@MechanicId,@JobStatus,'" + txt_vehicleNumber.Text + "','" + cusid + "')";
                    ConnectDB.cmd.CommandText = ConnectDB.sql;
                    ConnectDB.cmd.Parameters.Clear();
                    ConnectDB.cmd.Parameters.AddWithValue("@Documentation", doc); //combooooooooooooooooooooooooooooooooooooooooo
                    ConnectDB.cmd.Parameters.AddWithValue("@JobStatus", "New Job");
                    ConnectDB.cmd.Parameters.AddWithValue("@JobType", "Accident");
                    ConnectDB.cmd.Parameters.AddWithValue("@MechanicId", "E0002");

                    int line = ConnectDB.cmd.ExecuteNonQuery();

                    if (line == 1)
                    {







                        ConnectDB.sql = "INSERT INTO [dbo].[Damage_Part] ([Job_ID],[Damage_pts],[Checked])  VALUES (@JobId,@DamagePart,@Checked)";
                        ConnectDB.cmd.CommandText = ConnectDB.sql;

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Front collision");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_frontcol.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Rear collision");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_rearcol.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Apron damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_apron.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "B pillar damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_bpillar.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "A pillar damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_apillar.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "C pillar damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_cpillar.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Fire wall damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_firewall.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Sub frame damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_subframe.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Chassis Damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_chasi.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Floor board damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_floor.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        ConnectDB.cmd.Parameters.Clear();
                        ConnectDB.cmd.Parameters.AddWithValue("@JobId", txt_JID.Text);
                        ConnectDB.cmd.Parameters.AddWithValue("@DamagePart", "Bulk head damage");
                        ConnectDB.cmd.Parameters.AddWithValue("@Checked", C_bulk.IsChecked);
                        ConnectDB.cmd.ExecuteNonQuery();

                        MessageBox.Show("Job Successfully Registered", "Job Registered", MessageBoxButton.OK, MessageBoxImage.Information);
                        Aframe.Navigate(new System.Uri("Accident_page.xaml",
                        UriKind.RelativeOrAbsolute));

                    }

                }
                catch (SqlException ex)
                {
                    if (ex.ErrorCode == -2146232060)
                    {
                        MessageBox.Show("Job ID already exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Database Errors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
           
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            InspectorDashboardWindow s = new InspectorDashboardWindow();
            s.Show();
        }

       
    }
}

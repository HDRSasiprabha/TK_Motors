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
using System.Data.SqlClient;
using System.Configuration;
namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Managerlogininwindow.xaml
    /// </summary>
    public partial class Managerlogininwindow : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public Managerlogininwindow()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        }

        private void btn_mlogin_Click(object sender, RoutedEventArgs e)
        {
          /*  Mechanic_dashboard a = new Mechanic_dashboard();
            this.Content = a;*/
            /*Cashier_dashboard c = new Cashier_dashboard();
            this.Content = c;*/

            Manager_dashboard a = new Manager_dashboard();
              this.Content = a;
            /*UpdateEmployee a = new UpdateEmployee();
            this.Content = a;*/
           /* Employeesignupwindow a = new Employeesignupwindow();
            a.Show();*/

            /*if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (string.IsNullOrEmpty(txt_username.Text))
            {
                MessageBox.Show("Username cannot be blank.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(txt_pswrd.Password))
            {
                MessageBox.Show("Password cannot be blank.", "Login Failed ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Verifyuser(txt_username.Text, txt_pswrd.Password))
            {
                Manager_dashboard a = new Manager_dashboard();
                this.Content = a;

            }
            else
            {
                MessageBox.Show("Incorrrect Username or Password has been entered. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/

        }
        private bool Verifyuser(string username, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select status from manager where Man_EID = '" + username + "' and password = '" + password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["status"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btn_close1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

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
using System.Configuration;
namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    /// 
 
    public partial class ChangePassword : Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public ChangePassword()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        }
        ConnectDB db = new ConnectDB();

        private void btn_cncle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(pb_cpswrd.Password))
                {
                    MessageBox.Show("Current Password required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(pb_npswrd.Password))
                {
                    MessageBox.Show("New Password required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(pb_conpswrd.Password))
                {
                    MessageBox.Show("Confirm Password required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Verifyuser(pb_cpswrd.Password))
                {
                    if (pb_npswrd.Password == pb_conpswrd.Password)
                    {
                        string query = "Update manager set password  = '" + pb_conpswrd.Password + "' where  Man_EID= 'E001'";
                        int i = db.add_update_remove(query);
                        if (i == 1)
                        {
                            MessageBox.Show("Password change Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            cpframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                                   UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            MessageBox.Show("Password cannot change", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("New Password and Confirm Password are not same.Please check again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Current Password is incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_saveChanges_Click(object sender, RoutedEventArgs e)
        {
            cpframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                         UriKind.RelativeOrAbsolute));
           

        }
        private bool Verifyuser(string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select status from manager where Man_EID = 'E001' and password = '" + password + "'";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cpframe.Navigate(new System.Uri("Manager_dashboard.xaml",
               UriKind.RelativeOrAbsolute));
        }
    }
}

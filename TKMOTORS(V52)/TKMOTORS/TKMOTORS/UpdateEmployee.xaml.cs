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
using System.Text.RegularExpressions;


namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : Page
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        public static SqlCommand cmd = new SqlCommand("", con);
        public UpdateEmployee()
        {
            InitializeComponent();
        }
        ConnectDB db = new ConnectDB();
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (txt_eid.Text.Length == 0)
            {

                lbl_eid.Content = "*Employee ID cannot be blank.";
            }

            else if (txt_ename.Text.Length == 0)
            {
                lbl_eid.Content = null;
                lbl_ename.Content = "*Employee name cannot be blank.";
                //MessageBox.Show("Employee name cannot be blank", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (txt_ename.Text.Any(char.IsDigit))
            {
                lbl_eid.Content = null;
                lbl_ename.Content = "*Employee name cannot have numbers.";
                //MessageBox.Show("Name cannot have numbers", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (txt_address.Text.Length == 0)
            {
                lbl_eid.Content = null;
                lbl_ename.Content = null;
                lbl_add.Content = "*Employee address cannot be blank.";
                //MessageBox.Show("Address cannot be blank", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (!Regex.IsMatch(txt_tp.Text, @"^7|0|(?:\+94)[0-9]{9,10}$"))
            {
                lbl_eid.Content = null;
                lbl_ename.Content = null;
                lbl_add.Content = null;
                lbl_tp.Content = "*Please enter valid Telephine number.";
                //MessageBox.Show("Please enter valid Telephine number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (txt_nic.Text.Length > 12 || txt_nic.Text.Length < 10)
            {
                lbl_eid.Content = null;
                lbl_ename.Content = null;
                lbl_add.Content = null;
                lbl_tp.Content = null;
                lbl_nic.Content = "*Please enter valid NIC number.";
                //MessageBox.Show("Please enter valid NIC number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error); 
            }


            else if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                lbl_eid.Content = null;
                lbl_ename.Content = null;
                lbl_add.Content = null;
                lbl_tp.Content = null;
                lbl_nic.Content = null;
                lbl_email.Content = "*Please enter valid email.";
                // MessageBox.Show("Please enter valid email", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                string Position = "";
                if (Rbtn_Mech.IsSelected == true)
                    Position = "Mechanic";
                else if (Rbtn_Painter.IsSelected == true)
                    Position = "Painter";
                else if (Rbtn_Repair.IsSelected == true)
                    Position = "Repair";
                else if (Rbtn_Inspector.IsSelected == true)
                    Position = "Inspector";
                else if (Rbtn_Cashier.IsSelected == true)
                    Position = "Cashier";
                try
                {
                    string query = "Update Employee set  ename= '" + txt_ename.Text + "', address = '" + txt_address.Text + "', telephone=  '" + txt_tp.Text + "', NIC= '" + txt_nic.Text + "', Email= '" + txt_email.Text + "', position='" + Position + "' where Employee_ID= '" + txt_eid.Text + "'";
                    int i = db.add_update_remove(query);
                    if (i == 1)
                    {
                        lbl_eid.Content = null;
                        lbl_ename.Content = null;
                        lbl_add.Content = null;
                        lbl_tp.Content = null;
                        lbl_nic.Content = null;
                        lbl_email.Content = null;
                        MessageBox.Show("Data Update Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_eid.Clear();
                        txt_ename.Clear();
                        txt_nic.Clear();
                        txt_tp.Clear();
                        txt_email.Clear();
                        txt_address.Clear();
                    }
                       
                    
                    else
                    {
                        MessageBox.Show("Data Cannot Update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        txt_eid.Clear();
                        txt_ename.Clear();
                        txt_nic.Clear();
                        txt_tp.Clear();
                        txt_email.Clear();
                        txt_address.Clear();
                    }
                        
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            


        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            uempframe.Navigate(new System.Uri("Manager_dashboard.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            uempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
               UriKind.RelativeOrAbsolute));
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            uempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
               UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            uempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void txt_eid_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (txt_eid.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("Select * from Employee where Employee_ID= '" + txt_eid.Text + "'", con);
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        txt_ename.Text = da.GetValue(2).ToString();
                        txt_address.Text = da.GetValue(3).ToString();
                        txt_tp.Text = da.GetValue(4).ToString();
                        txt_nic.Text = da.GetValue(6).ToString();
                        txt_email.Text = da.GetValue(7).ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    
}

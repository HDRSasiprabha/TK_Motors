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
namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for UpdateParts.xaml
    /// </summary>
    public partial class UpdateParts : Page
    {
        public UpdateParts()
        {
            InitializeComponent();
        }
        ConnectDB db = new ConnectDB();

        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        public static SqlCommand cmd = new SqlCommand("", con);

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_pid.Text))
                {
                    lbl_pid.Content = "*Part ID cannot be empty.";
                    // MessageBox.Show("Part name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_pname.Text))
                {
                    lbl_pid.Content = null;
                    lbl_pname.Content = "*Part name cannot be empty.";
                    // MessageBox.Show("Part name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_price.Text))
                {
                    lbl_pid.Content = null;
                    lbl_pname.Content = null;
                    lbl_price.Content = "*Price cannot be empty.";
                    //MessageBox.Show("Price cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txt_price.Text.Any(char.IsLetter))
                {
                    lbl_pid.Content = null;
                    lbl_pname.Content = null;
                    lbl_price.Content = "*Price cannot be letters.";
                    //MessageBox.Show("Price cannot be letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_qinh.Text))
                {
                    lbl_pid.Content = null;
                    lbl_pname.Content = null;
                    lbl_price.Content = null;
                    lbl_qinh.Content = "*Quantity in Hand cannot be empty.";
                    //MessageBox.Show("Quantity in Hand cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txt_qinh.Text.Any(char.IsLetter))
                {
                    lbl_pid.Content = null;
                    lbl_pname.Content = null;
                    lbl_price.Content = null;
                    lbl_qinh.Content = "*Quantity in Hand cannot be letters.";
                    //MessageBox.Show("Quantity in Hand cannot be letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string query = "Update part set   part_name = '" + txt_pname.Text + "', price =  '" + txt_price.Text + "', quantity_in_hand = '" + txt_qinh.Text + "' where part_ID= '" + txt_pid.Text + "'";
                    int i = db.add_update_remove(query);
                    if (i == 1)
                    {
                        lbl_pid.Content = null;
                        lbl_qinh.Content = null;
                        lbl_pname.Content = null;
                        lbl_price.Content = null;
                        MessageBox.Show("Data Update Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    txt_pid.Clear();
                    txt_pname.Clear();
                    txt_price.Clear();
                    txt_qinh.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Data Cannot Update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        txt_pid.Clear();
                        txt_pname.Clear();
                        txt_price.Clear();
                        txt_qinh.Clear();
                    }
                       
                }
          
                
               
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void txt_pid_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (txt_pid.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Select * from part where part_ID = '" + txt_pid.Text + "'", con);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txt_pname.Text = da.GetValue(1).ToString();
                    txt_price.Text = da.GetValue(2).ToString();
                    txt_qinh.Text = da.GetValue(3).ToString();
                }
                con.Close();
            }
            }
            catch(Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            upframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                UriKind.RelativeOrAbsolute));
          

        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            upframe.Navigate(new System.Uri("InventoryHandling.xaml",
                  UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            upframe.Navigate(new System.Uri("InventoryHandling.xaml",
                  UriKind.RelativeOrAbsolute));
        }
    }
}

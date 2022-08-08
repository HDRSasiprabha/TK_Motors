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
    /// Interaction logic for AddParts.xaml
    /// </summary>
    public partial class AddParts : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public AddParts()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader srn = null;
            cmd.Connection = conn;
            cmd.CommandText = "Select top(1) part_ID from part order by  part_ID  desc ";
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
                txt_partid.Text = newStr.ToString();

            }
            conn.Close();
        }
        ConnectDB db = new ConnectDB();

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txt_pname.Text))
                {
                    lbl_pname.Content = "*Part name cannot be empty.";
                    // MessageBox.Show("Part name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_pprice.Text))
                {
                    lbl_pname.Content = null;
                    lbl_price.Content = "*Price cannot be empty.";
                    //MessageBox.Show("Price cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txt_pprice.Text.Any(char.IsLetter))
                {
                    lbl_pname.Content = null;
                    lbl_price.Content = "*Price cannot be letters.";
                    //MessageBox.Show("Price cannot be letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (string.IsNullOrEmpty(txt_qinh.Text))
                {
                    lbl_pname.Content = null;
                    lbl_price.Content = null;
                    lbl_qinh.Content = "*Quantity in Hand cannot be empty.";
                    //MessageBox.Show("Quantity in Hand cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txt_qinh.Text.Any(char.IsLetter))
                {

                    lbl_pname.Content = null;
                    lbl_price.Content = null;
                    lbl_qinh.Content = "*Quantity in Hand cannot be letters.";
                    //MessageBox.Show("Quantity in Hand cannot be letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string query = "Insert into part values ('" + txt_partid.Text + "', '" + txt_pname.Text + "', '" + txt_pprice.Text + "',  '" + txt_qinh.Text + "') ";
                    int i = db.add_update_remove(query);
                    if (i == 1)
                    {
                        lbl_qinh.Content = null;
                        lbl_pname.Content = null;
                        lbl_price.Content = null;
                        MessageBox.Show("New item added Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_pname.Clear();
                        txt_pprice.Clear();
                        txt_qinh.Clear();

                        apframe.Navigate(new System.Uri("AddParts.xaml",
              UriKind.RelativeOrAbsolute));


                    }

                    else
                    {
                        MessageBox.Show("New item Cannot add", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {

            apframe.Navigate(new System.Uri("Manager_dashboard.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            apframe.Navigate(new System.Uri("InventoryHandling.xaml",
                   UriKind.RelativeOrAbsolute));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            apframe.Navigate(new System.Uri("InventoryHandling.xaml",
                  UriKind.RelativeOrAbsolute));
        }
    }
}

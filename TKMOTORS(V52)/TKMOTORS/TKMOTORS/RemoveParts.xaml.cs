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
    /// Interaction logic for RemoveParts.xaml
    /// </summary>
    public partial class RemoveParts : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public RemoveParts()
        {
            InitializeComponent();
        }
        ConnectDB db = new ConnectDB();
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                da = new SqlDataAdapter("Select * from part where part_ID = '" + txt_part_id.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datagrid_remove.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove  ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                try
                {
                    string query = "Delete from part where part_ID = '" + txt_part_id.Text + "'";
                    int i = db.add_update_remove(query);
                    if (i == 1)
                    {
                        MessageBox.Show("Item Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_part_id.Clear();
                        da = new SqlDataAdapter("Select * from part where part_ID = '" + txt_part_id.Text + "'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        datagrid_remove.ItemsSource = dt.DefaultView;

                    }
                       
                    else
                    {
                        MessageBox.Show("Incorrect or invalid Item ID ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                        

                }
                catch
                {
                    MessageBox.Show("Item cannot delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            rpframe.Navigate(new System.Uri("Manager_dashboard.xaml",
               UriKind.RelativeOrAbsolute));
           
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            rpframe.Navigate(new System.Uri("InventoryHandling.xaml",
               UriKind.RelativeOrAbsolute));
        }

        private void btn_close(object sender, RoutedEventArgs e)
        {
            rpframe.Navigate(new System.Uri("InventoryHandling.xaml",
                  UriKind.RelativeOrAbsolute));

        }
    }
}

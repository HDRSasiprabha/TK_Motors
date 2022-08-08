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
    /// Interaction logic for RemoveEmployee.xaml
    /// </summary>
    public partial class RemoveEmployee : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public RemoveEmployee()
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
                da = new SqlDataAdapter("Select employee_ID as EmployeeID,ename as Name, address as Address , telephone as Telephone, position as Position from employee where employee_ID = '" + txt_emp_id.Text + "'", con);
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
                    string query = "Delete from employee where employee_ID = '" + txt_emp_id.Text + "'";
                    int i = db.add_update_remove(query);
                    if (i == 1)
                    {
                        MessageBox.Show("Employee Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_emp_id.Clear();
                        da = new SqlDataAdapter("Select employee_ID as EmployeeID,ename as Name, address as Address , telephone as Telephone, position as Position from employee where employee_ID = '" + txt_emp_id.Text + "'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        datagrid_remove.ItemsSource = dt.DefaultView;
                    }
                        
                    else
                    {
                        MessageBox.Show("Employee Cannot Delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                        
                }
                catch
                {
                    MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            rempframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                 UriKind.RelativeOrAbsolute));

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            rempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
                        UriKind.RelativeOrAbsolute));
        }
    }
}

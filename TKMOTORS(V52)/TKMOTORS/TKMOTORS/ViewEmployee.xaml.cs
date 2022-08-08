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
    /// Interaction logic for ViewEmployee.xaml
    /// </summary>
    public partial class ViewEmployee : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public ViewEmployee()
        {
            InitializeComponent();
            viewall();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        ConnectDB db = new ConnectDB();
        void viewall()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                da = new SqlDataAdapter("Select employee_ID as EmployeeID,ename as Name, address as Address , telephone as Telephone, position as Position from employee", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datagrid_view.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                da = new SqlDataAdapter("Select employee_ID as EmployeeID,ename as Name, address as Address , telephone as Telephone, position as Position from employee where employee_ID = '" + txt_view_emp_num.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datagrid_view.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btn_viewall_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            vempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
              UriKind.RelativeOrAbsolute));
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
             UriKind.RelativeOrAbsolute));
        }

        private void btn_menu1_Click(object sender, RoutedEventArgs e)
        {
            vempframe.Navigate(new System.Uri("Reports.xaml",
                       UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

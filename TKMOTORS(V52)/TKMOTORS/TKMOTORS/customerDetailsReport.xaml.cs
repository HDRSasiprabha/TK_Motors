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
using System.Windows.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for customerDetailsReport.xaml
    /// </summary>
    public partial class customerDetailsReport : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public customerDetailsReport()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
            /*con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select customer_ID as CustomerID,cname as CustomerName,address as Address,Email from customer", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.DefaultView;
            con.Close();*/



            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        /*private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select customer_ID as CustomerID,cname as CustomerName,address as Address,Email from customer where customer_ID='" + txt_cid.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.DefaultView;
        }

        private void btn_viewAll_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select customer_ID as CustomerID,cname as CustomerName,address as Address,Email from customer", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.DefaultView;
            con.Close();
        }*/
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                TKMotorsDataSet2 dataset = new TKMotorsDataSet2();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Customer;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = "../../ReportCus.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                TKMotorsDataSet2TableAdapters.CustomerTableAdapter
                customerTableAdapter = new
                TKMotorsDataSet2TableAdapters.CustomerTableAdapter();

                customerTableAdapter.ClearBeforeFill = true;
                customerTableAdapter.Fill(dataset.Customer);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }
        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            cdrframe.Navigate(new System.Uri("Manager_dashboard.xaml",
               UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            cdrframe.Navigate(new System.Uri("Reports.xaml",
                 UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cdrframe.Navigate(new System.Uri("Reports.xaml",
                   UriKind.RelativeOrAbsolute));

        }
    }
}

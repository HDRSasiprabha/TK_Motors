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
    /// Interaction logic for AnuualSalesReport.xaml
    /// </summary>
    public partial class AnuualSalesReport : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public AnuualSalesReport()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
            //fillcmb();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            /*con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select Date,sum(Total) as Income,sum(Total - Extra_charge) as Expences,sum(Extra_charge) as Profit from bill group by Date", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.DefaultView;
            con.Close();*/
        }

        /*void fillcmb()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct Date from bill group by Date", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   DateTime  date = dr.GetDateTime(0);
                    cmb_year.Items.Add(date);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                TKMotorsDataSet5 dataset = new TKMotorsDataSet5();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Bill;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = "../../ReportSales.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                TKMotorsDataSet5TableAdapters.BillTableAdapter
                BillTableAdapter = new
                TKMotorsDataSet5TableAdapters.BillTableAdapter();

                BillTableAdapter.ClearBeforeFill = true;
                BillTableAdapter.Fill(dataset.Bill);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            asrframe.Navigate(new System.Uri("Reports.xaml",
                   UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            asrframe.Navigate(new System.Uri("Manager_dashboard.xaml",
               UriKind.RelativeOrAbsolute));
           
            //rv_bill.Dispose();
   

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }

       /* private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select Date ,sum(Total) as Income,sum(Total - Extra_charge) as Expences,sum(Extra_charge) as Profit from bill where Date='" + Convert.ToDateTime(cmb_year.SelectedItem).ToShortDateString() + "' group by Date", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.DefaultView;
        }

        private void btn_viewAll_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            con.Open();
            da = new SqlDataAdapter("select Date,sum(Total) as Income,sum(Total - Extra_charge) as Expences,sum(Extra_charge) as Profit from bill group by Date", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.DefaultView;
            con.Close();
        }
*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            asrframe.Navigate(new System.Uri("Reports.xaml",
                 UriKind.RelativeOrAbsolute));
        }
    }
}

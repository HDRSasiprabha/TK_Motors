﻿using System;
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
    /// Interaction logic for ViewParts.xaml
    /// </summary>
    public partial class ViewParts : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        public ViewParts()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
            //viewall();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
       // ConnectDB db = new ConnectDB();
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            displaydate.Text = DateTime.Now.ToString();

        }
        /* void viewall()
         {
             try
             {
                 con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                 con.Open();
                 da = new SqlDataAdapter("Select * from part", con);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 datagrid_view.ItemsSource = dt.DefaultView;

                 con.Close();
             }
             catch (Exception)
             {
                 MessageBox.Show("Please check again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

             }

         }*/
        /*private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                con.Open();
                da = new SqlDataAdapter(" Select* from part where part_ID = '" + txt_pid.Text + "'", con);
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
*/
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                TKMotorsDataSet1 dataset = new TKMotorsDataSet1();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Part;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = "../../ReportStock.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                TKMotorsDataSet1TableAdapters.PartTableAdapter
                PartTableAdapter = new
                TKMotorsDataSet1TableAdapters.PartTableAdapter();

                PartTableAdapter.ClearBeforeFill = true;
                PartTableAdapter.Fill(dataset.Part);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }



        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            vpframe.Navigate(new System.Uri("Manager_dashboard.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            vpframe.Navigate(new System.Uri("Reports.xaml",
              UriKind.RelativeOrAbsolute));
        }

        private void btn_close(object sender, RoutedEventArgs e)
        {
            vpframe.Navigate(new System.Uri("Reports.xaml",
                 UriKind.RelativeOrAbsolute));
        }
    }
}

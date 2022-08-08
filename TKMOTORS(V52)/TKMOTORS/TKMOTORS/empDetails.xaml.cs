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

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for empDetails.xaml
    /// </summary>
    public partial class empDetails : Page
    {
        public empDetails()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                TKMotorsDataSet dataset = new TKMotorsDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Employee;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = "../../Reportemp.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                TKMotorsDataSetTableAdapters.EmployeeTableAdapter
                EmployeeTableAdapter = new
                TKMotorsDataSetTableAdapters.EmployeeTableAdapter();

                EmployeeTableAdapter.ClearBeforeFill = true;
                EmployeeTableAdapter.Fill(dataset.Employee);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }
    }
}

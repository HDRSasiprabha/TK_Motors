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
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        

        private void Btn_esignup(object sender, RoutedEventArgs e)
        {

            /* InspectorDashboardWindow a = new InspectorDashboardWindow();
             a.Show();*/
            /*ManagerLoginForAddEmployee a = new ManagerLoginForAddEmployee();
            a.Show();*/
            QR a = new QR();
            a.Show();
        }
    }
}

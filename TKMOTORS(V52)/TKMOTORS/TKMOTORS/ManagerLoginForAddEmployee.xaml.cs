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
using System.Windows.Shapes;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for ManagerLoginForAddEmployee.xaml
    /// </summary>
    public partial class ManagerLoginForAddEmployee : Window
    {
        public ManagerLoginForAddEmployee()
        {
            InitializeComponent();
        }

        private void btn_mlogin_Click(object sender, RoutedEventArgs e)
        {
            
           


            Newjob a = new Newjob();
            this.Content = a;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

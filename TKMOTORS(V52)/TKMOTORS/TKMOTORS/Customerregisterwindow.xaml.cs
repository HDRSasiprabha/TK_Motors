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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Customerregisterwindow.xaml
    /// </summary>
    public partial class Customerregisterwindow : Window
    {
        public Customerregisterwindow()
        {
            InitializeComponent();
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            if (txt_NIC.Text.Length == 0)

                MessageBox.Show("Please customer's NIC number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (txt_Name.Text.Any(char.IsDigit))

                MessageBox.Show("Name canot have numbers", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txt_Add.Text.Length == 0)

                MessageBox.Show("Please enter address", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!Regex.IsMatch(txt_Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))

                MessageBox.Show("Please enter valid Email", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                try
                {
                    ConnectDB.openConnection();
                    ConnectDB.cmd.CommandType = CommandType.Text;
                    ConnectDB.sql = " insert into customer ([customer_ID],[cname],[address],[Email]) values ( '" + txt_NIC.Text + "'  ,  '" + txt_Name.Text + "' , '" + txt_Add.Text + "' , '" + txt_Email.Text + "')";

                    ConnectDB.cmd.CommandText = ConnectDB.sql;

                    int line = ConnectDB.cmd.ExecuteNonQuery(); // line=i int i send from class to here, a sending to class, 
                    if (line == 1)
                    {

                        MessageBox.Show("Successfully Registered", "Ïnfo", MessageBoxButton.OK, MessageBoxImage.Information);



                    }
                    else
                        MessageBox.Show("cant save");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Database Errors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
        }

        private void btn_cancle_Click(object sender, RoutedEventArgs e)
        {
            txt_Email.Clear();
            txt_Add.Clear();
            txt_Name.Clear();
            txt_NIC.Clear();

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            InspectorDashboardWindow s = new InspectorDashboardWindow();
            s.Show();
        }

      
    }
}

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
using System.Data;
using System.Data.SqlClient;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Vehicle_registration.xaml
    /// </summary>
    public partial class Vehicle_registration : Page
    {
        public Vehicle_registration()
        {
            InitializeComponent();
        }

        private void btn_Contiue_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Vnumber.Text.Length == 0)

                MessageBox.Show("Please enter vehicle number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (Txt_ONIC.Text.Length > 13 || Txt_ONIC.Text.Length < 10)

            { MessageBox.Show("Please enter valid NIC number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error); }

           else if (txt_Brand.Text.Length == 0)

                MessageBox.Show("Please enter vehicle brand", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txt_Model.Text.Length == 0)

                MessageBox.Show("Please enter vehicle model", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txt_Trim.Text.Length == 0)

                MessageBox.Show("Please enter vehicle trim", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    ConnectDB.openConnection();
                    ConnectDB.cmd.CommandType = CommandType.Text;
                    ConnectDB.sql = " insert into vehicle ([Vehicle_ID],[Cus_ID],[brand],[model_name],[Trim]) values ( '" + txt_Vnumber.Text + "'  ,  '" + Txt_ONIC.Text + "' , '" + txt_Brand.Text + "' , '" + txt_Model.Text + "',  '" + txt_Trim.Text + "')";

                    ConnectDB.cmd.CommandText = ConnectDB.sql;

                    int line = ConnectDB.cmd.ExecuteNonQuery(); // line=i int i send from class to here, a sending to class, 
                    if (line == 1)
                    {

                        MessageBox.Show("Vehicle Registered", "Ïnfo" , MessageBoxButton.OK , MessageBoxImage.Information);

                        txt_Vnumber.Clear();
                        Txt_ONIC.Clear();
                        txt_Brand.Clear();
                        txt_Model.Clear();
                        txt_Trim.Clear();

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

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txt_Vnumber.Clear();
            Txt_ONIC.Clear();
            txt_Brand.Clear();
            txt_Model.Clear();
            txt_Trim.Clear();
        }

      

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            InspectorDashboardWindow s = new InspectorDashboardWindow();
            s.Show();

            /*  vrframe.Navigate(new System.Uri("InspectorDashboardWindow.xaml",
                  UriKind.RelativeOrAbsolute));*/
        }
    }
}

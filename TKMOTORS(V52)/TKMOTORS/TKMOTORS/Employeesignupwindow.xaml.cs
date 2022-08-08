using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using System.IO;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for Employeesignupwindow.xaml
    /// </summary>
    public partial class Employeesignupwindow : Window
    {
        string customerId, email;
        public Employeesignupwindow()
        {
            InitializeComponent();
        }
        ConnectDB obj = new ConnectDB();
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
           if (txt_Fname.Text.Length == 0)
            {
                
                lbl_ename.Content = "*Employee name cannot be blank.";
                //MessageBox.Show("Employee name cannot be blank", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (txt_Fname.Text.Any(char.IsDigit))
            {
               
                lbl_ename.Content = "*Employee name cannot have numbers.";
                //MessageBox.Show("Name cannot have numbers", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (txt_Add.Text.Length == 0)
            {
               
                lbl_ename.Content = null;
                lbl_add.Content = "*Employee address cannot be blank.";
                //MessageBox.Show("Address cannot be blank", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (!Regex.IsMatch(txt_Tpnumber.Text, @"^7|0|(?:\+94)[0-9]{9,10}$"))
            {
                
                lbl_ename.Content = null;
                lbl_add.Content = null;
                lbl_tp.Content = "*Please enter valid Telephine number.";
                //MessageBox.Show("Please enter valid Telephine number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (txt_Nic.Text.Length > 12 || txt_Nic.Text.Length < 10)
            {
                lbl_ename.Content = null;
                lbl_add.Content = null;
                lbl_tp.Content = null;
                lbl_nic.Content = "*Please enter valid NIC number.";
                //MessageBox.Show("Please enter valid NIC number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error); 
            }


            else if (!Regex.IsMatch(txt_Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                
                lbl_ename.Content = null;
                lbl_add.Content = null;
                lbl_tp.Content = null;
                lbl_nic.Content = null;
                lbl_email.Content = "*Please enter valid email.";
                // MessageBox.Show("Please enter valid email", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string Position = "";
                if (Rbtn_Mech.IsSelected == true)
                    Position = "Mechanic";
                else if (Rbtn_Painter.IsSelected == true)
                    Position = "Painter";
                else if (Rbtn_Repair.IsSelected == true)
                    Position = "Repair";
                else if (Rbtn_Inspector.IsSelected == true)
                    Position = "Inspector";
                else if (Rbtn_Cashier.IsSelected == true)
                    Position = "Cashier";
                try
                {
                    ConnectDB.openConnection();
                    ConnectDB.cmd.CommandType = CommandType.Text;
                    ConnectDB.sql = " insert into employee ([ename],[address],[telephone],[password],[Email],[position],[NIC]) values ( '" + txt_Fname.Text + "'  ,  '" + txt_Add.Text + "' , '" + txt_Tpnumber.Text + "' , 1234 ,'" + txt_Email.Text + "', '" + Position + "', '" + txt_Nic.Text + "')";

                    ConnectDB.cmd.CommandText = ConnectDB.sql;

                    int line = ConnectDB.cmd.ExecuteNonQuery(); // line=i int i send from class to here, a sending to class, 
                    if (line == 1)
                    {

                        ConnectDB.sql = "SELECT TOP(1) [EID],[Employee_ID],[ename],[address],[telephone],[password],[Email],[position],[Supervised_EID],[status] FROM [dbo].[employee] ORDER BY [EID] DESC";
                        ConnectDB.cmd.CommandType = CommandType.Text;
                        ConnectDB.cmd.CommandText = ConnectDB.sql;

                        ConnectDB.da = new SqlDataAdapter(ConnectDB.cmd);
                        ConnectDB.dt = new DataTable();
                        ConnectDB.da.Fill(ConnectDB.dt);

                        if (ConnectDB.dt.Rows.Count > 0)
                        {
                            
                            customerId = ConnectDB.dt.Rows[0]["Employee_ID"].ToString();
                            email = ConnectDB.dt.Rows[0]["Email"].ToString();
                            
                            sendMail(customerId, email);
                        }
                        else
                        { 

                        }
                        

                        if (Rbtn_Mech.IsSelected == true)
                        {
                            ConnectDB.openConnection();
                            ConnectDB.cmd.CommandType = CommandType.Text;
                            ConnectDB.sql = "INSERT INTO [dbo].[Mechanic]([Mec_EID])VALUES('" + customerId + "')";
                            ConnectDB.cmd.CommandText = ConnectDB.sql;
                            int line2 = ConnectDB.cmd.ExecuteNonQuery();
                        }
                        else if (Rbtn_Painter.IsSelected == true)
                        {
                            ConnectDB.openConnection();
                            ConnectDB.cmd.CommandType = CommandType.Text;
                            ConnectDB.sql = "INSERT INTO [dbo].[Mechanic]([Mec_EID])VALUES('" + customerId + "')";
                            ConnectDB.cmd.CommandText = ConnectDB.sql;
                            int line3 = ConnectDB.cmd.ExecuteNonQuery();
                        }
                        else if (Rbtn_Repair.IsSelected == true)
                        {
                            ConnectDB.openConnection();
                            ConnectDB.cmd.CommandType = CommandType.Text;
                            ConnectDB.sql = "INSERT INTO [dbo].[Mechanic]([Mec_EID])VALUES('" + customerId + "')";
                            ConnectDB.cmd.CommandText = ConnectDB.sql;
                            int line4 = ConnectDB.cmd.ExecuteNonQuery();
                        }
                        else if (Rbtn_Inspector.IsSelected == true)
                        {
                            ConnectDB.openConnection();
                            ConnectDB.cmd.CommandType = CommandType.Text;
                            ConnectDB.sql = "INSERT INTO [dbo].[Inspector]([Ins_EID])VALUES('" + customerId + "')";
                            ConnectDB.cmd.CommandText = ConnectDB.sql;
                            int line5 = ConnectDB.cmd.ExecuteNonQuery();
                        }
                        else if (Rbtn_Cashier.IsSelected == true)
                        {
                            ConnectDB.openConnection();
                            ConnectDB.cmd.CommandType = CommandType.Text;
                            ConnectDB.sql = "INSERT INTO [dbo].[Cashier]([Cas_EID])VALUES('" + customerId + "')";
                            ConnectDB.cmd.CommandText = ConnectDB.sql;
                            int line6 = ConnectDB.cmd.ExecuteNonQuery();
                        }
                        lbl_ename.Content = null;
                        lbl_add.Content = null;
                        lbl_tp.Content = null;
                        lbl_nic.Content = null;
                        lbl_email.Content = null;
                        MessageBox.Show("Employee registered Successfully", "Employee Registered", MessageBoxButton.OK, MessageBoxImage.Information);
                        txt_Fname.Clear();
                        txt_Add.Clear();
                        txt_Email.Clear();

                        txt_Nic.Clear();
                        txt_Tpnumber.Clear();




                    }
                    else
                        MessageBox.Show("Employee cannot register", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            


           
        }

        private void sendMail(string customerId, string email)
        {

            QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(customerId, QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
            Bitmap bmp = qRCode.GetGraphic(5);
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp);
                DataSet1 reportData = new DataSet1();
                DataSet1.QRCodeRow qRCodeRow = reportData.QRCode.NewQRCodeRow();
                qRCodeRow.Image = ms.ToArray();
                reportData.QRCode.AddQRCodeRow(qRCodeRow);



                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource();
                //reportDataSource.Name = "DataSet1";
                //reportDataSource.Value = reportData.QRCode;
                //reportViewer1.LocalReport.DataSources.Clear();
                //reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                //reportViewer1.RefreshReport();


                //QRimage.Source = BitmapToBitmapImage(bmp);

                sendMail(bmp, email);
            }
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }

        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public void sendMail(Bitmap bmp, string toMail)
        {

            //Mail
            try
            {
                Graphics oGraphic;
                // Here create a new bitmap object of the same height and width of the image.
                Bitmap bmpNew = new Bitmap(bmp.Width, bmp.Height);
                oGraphic = Graphics.FromImage(bmpNew);
                oGraphic.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
                // Release the lock on the image file. Of course,
                // image from the image file is existing in Graphics object
                bmp.Dispose();
                bmp = bmpNew;

                SolidBrush oBrush = new SolidBrush(System.Drawing.Color.Black);
                Font ofont = new Font("Arial", 8);
                oGraphic.DrawString("Some text to write", ofont, oBrush, 10, 10);
                oGraphic.Dispose();
                ofont.Dispose();
                oBrush.Dispose();
                bmp.Save(@"C:\ThePealKitchen\test.jpg", ImageFormat.Jpeg);
                bmp.Dispose();

                MailMessage newMail = new MailMessage();
                newMail.From = new MailAddress("milindascania@gmail.com"); //email vari
                newMail.To.Add(new MailAddress(toMail));
                newMail.Subject = "Test Subject";
                newMail.IsBodyHtml = true;

                var inlineLogo = new LinkedResource(@"C:\ThePealKitchen\test.jpg", "image/png");
                inlineLogo.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                string body = string.Format(@"
            <p>TKMotors</p>
            <img src=""cid:{0}"" />
            <p>Use this QR code to login </p>
        ", inlineLogo.ContentId);

                var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                view.LinkedResources.Add(inlineLogo);
                newMail.AlternateViews.Add(view);

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("milindascania@gmail.com", "scania678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(newMail);
                //attachment.Dispose();
                
                MessageBox.Show("QR Code is sent to Employee E-mail successfully", "QR Code Sent", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            /*catch (Exception )
            {
                MessageBox.Show("QR Code cannot sent to Employee E-mail", "Failed to send QR Code ", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to send QR Code ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            aempframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                 UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            aempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
                 UriKind.RelativeOrAbsolute));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            aempframe.Navigate(new System.Uri("EmployeeHandling.xaml",
                 UriKind.RelativeOrAbsolute));
        }
    }
}

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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace TKMOTORS
{
    /// <summary>
    /// Interaction logic for QR.xaml
    /// </summary>
    public partial class QR : Window
    {
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice CaptureDevice;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        string QR_out;

        public QR()
        {
            InitializeComponent();
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in FilterInfoCollection)
            {
                cboDevice.Items.Add(filterInfo.Name);
            }
            cboDevice.SelectedIndex = 0;

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }




        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Source != null)
            {
                BarcodeReader Reader = new BarcodeReader();
                Result result = Reader.Decode((BitmapImage)pictureBox1.Source);
                if (result != null)
                {
                    //variable for the qr login out put
                    txtQRCode.Text = result.Text;
                    QR_out = Convert.ToString(txtQRCode.Text);  //out put to the QR_out

                    ConnectDB.openConnection();

                    ConnectDB.sql = "SELECT [EID],[Employee_ID],[ename],[address],[telephone],[password],[NIC],[Email],[position] from employee where Employee_ID=@emplyeeid";
                    //DbClass.cmd.CommandType = CommandType.Text;
                    ConnectDB.cmd.CommandText = ConnectDB.sql;
                    ConnectDB.cmd.Parameters.Clear();
                    ConnectDB.cmd.Parameters.AddWithValue("@emplyeeid", QR_out);

                    ConnectDB.da = new SqlDataAdapter(ConnectDB.cmd);
                    ConnectDB.dt = new DataTable();
                    ConnectDB.da.Fill(ConnectDB.dt);

                    if (ConnectDB.dt.Rows.Count > 0)
                    {
                        //System.Windows.Forms.MessageBox.Show("Success");
                        ////check name for position
                        string Name;
                        
                        Name = ConnectDB.dt.Rows[0]["Position"].ToString();
                        App.Current.Properties["NameOfProperty"] = Name;
                        if(Name=="Painter")
                        {
                            Mechanic_dashboard cart = new Mechanic_dashboard();
                            this.Content = cart;
                        }
                        else if (Name=="Mechanic")
                        {
                            Mechanic_dashboard cart = new Mechanic_dashboard();
                            this.Content = cart;
                        }
                        else if (Name == "Repair")
                        {
                            Mechanic_dashboard cart = new Mechanic_dashboard();
                            this.Content = cart;
                        }
                        else if (Name == "Inspector")
                        {
                            InspectorDashboardWindow  cart = new InspectorDashboardWindow();
                            cart.Show();
                        }
                        else if (Name == "Cashier")
                        {
                            Cashier_dashboard cart = new Cashier_dashboard();
                            this.Content = cart;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Wrong QR Code");
                    }


                    ConnectDB.closeConnection();
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            CaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            CaptureDevice.NewFrame += CaptureDevice_NewFrame;
            CaptureDevice.Start();
        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            this.Dispatcher.Invoke(() =>
            {
                pictureBox1.Source = BitmapToBitmapImage((Bitmap)eventArgs.Frame.Clone());
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // if (CaptureDevice.IsRunning)
            //   CaptureDevice.Stop();
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

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow s = new MainWindow();
            s.Show();
        }
    }
}


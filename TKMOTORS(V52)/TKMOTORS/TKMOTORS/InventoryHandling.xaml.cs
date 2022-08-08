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
    /// Interaction logic for InventoryHandling.xaml
    /// </summary>
    public partial class InventoryHandling : Page
    {
        public InventoryHandling()
        {
            InitializeComponent();
        }

        private void Btn_addparts(object sender, RoutedEventArgs e)
        {
            iframe.Navigate(new System.Uri("AddParts.xaml",
            UriKind.RelativeOrAbsolute));
        }

        private void Btn_removeparts(object sender, RoutedEventArgs e)
        {
            iframe.Navigate(new System.Uri("RemoveParts.xaml",
           UriKind.RelativeOrAbsolute));
        }

        private void Btn_upparts(object sender, RoutedEventArgs e)
        {
            iframe.Navigate(new System.Uri("UpdateParts.xaml",
           UriKind.RelativeOrAbsolute));
        }


        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            iframe.Navigate(new System.Uri("Manager_dashboard.xaml",
            UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            iframe.Navigate(new System.Uri("Manager_dashboard.xaml",
                        UriKind.RelativeOrAbsolute));
        }

        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

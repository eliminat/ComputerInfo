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
using System.Management.Automation;
using System.Management;

namespace ComputerInfo
{
 /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Computer Information Retrieval Program by Bill Jaquette
    /// 5/17/2014
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

            //Creates WMI Object for Retrieving/Storing Data
            WMIInfo WMIObject = new WMIInfo();

            // Get Drive Information
            DriveInfo.Text = WMIObject.GetDriveInfo();

            //Get OS Information
            InfoBox.Text = WMIObject.GetOSInfo() + WMIObject.GetCPUInfo() + WMIObject.GetNetworkInfo();
        }

        private void MnuRetrieve_Click(object sender, RoutedEventArgs e)
        {
            //Creates WMI Object for Retrieving/Storing Data
            WMIInfo WMIObject = new WMIInfo();

            // Get Drive Information
            DriveInfo.Text = WMIObject.GetDriveInfo();

            //Get OS Information
            InfoBox.Text = WMIObject.GetOSInfo() + WMIObject.GetCPUInfo() + WMIObject.GetNetworkInfo();
        }

    }

}

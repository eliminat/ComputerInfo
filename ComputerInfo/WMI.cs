using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management;

namespace ComputerInfo
{
    class WMIInfo
    {

        /// <summary>
        /// Returns MAC Address from first Network Card in Computer
        /// </summary>
        /// <returns>MAC Address in string format</returns>
        public string FindMACAddress()
        {
            //create out management class object using the
            //Win32_NetworkAdapterConfiguration class to get the attributes
            //of the network adapter
            ManagementClass mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //create our ManagementObjectCollection to get the attributes with
            ManagementObjectCollection objCol = mgmt.GetInstances();
            string address = String.Empty;
            //loop through all the objects we find
            foreach (ManagementObject obj in objCol)
            {
                if (address == String.Empty)  // only return MAC Address from first card
                {
                    //grab the value from the first network adapter we find
                    //you can change the string to an array and get all
                    //network adapters found as well
                    //check to see if the adapter's IPEnabled
                    //equals true
                    if ((bool)obj["IPEnabled"] == true)
                    {
                        address = obj["MacAddress"].ToString();
                    }
                }
                //dispose of our object
                obj.Dispose();
            }
            //replace the ":" with an empty space, this could also
            //be removed if you wish
            address = address.Replace(":", "");
            //return the mac address
            return address;
        }

        /// Return processorId from first CPU in machine
        /// </summary>
        /// <returns>[string] ProcessorId</returns>
        public string GetCPUId()
        {
            string cpuInfo = String.Empty;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuInfo == String.Empty)
                {
                    // only return cpuInfo from first CPU
                    cpuInfo = obj.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        /// <summary>
        /// method for retrieving the CPU Manufacturer
        /// using the WMI class
        /// </summary>
        /// <returns>CPU Manufacturer</returns>
        public string GetCPUManufacturer()
        {
            string cpuMan = String.Empty;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuMan == String.Empty)
                {
                    // only return manufacturer from first CPU
                    cpuMan = obj.Properties["Manufacturer"].Value.ToString();
                }
            }
            return cpuMan;
        }

        /// </summary>
        /// <returns>Clock speed</returns>
        public int GetCPUCurrentClockSpeed()
        {
            int cpuClockSpeed = 0;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuClockSpeed == 0)
                {
                    // only return cpuStatus from first CPU
                    cpuClockSpeed = Convert.ToInt32(obj.Properties["CurrentClockSpeed"].Value.ToString());
                }
            }
            //return the status
            return cpuClockSpeed;
        }

        /// <summary>
        ///  method for retrieving drive information
        ///  using the WMI class
        ///</summary>
        ///
        public string GetDriveInfo()
        {
            string DriveData = "";
            //Load all drives detected into searcher
            ManagementObjectSearcher searcher =
                                   new ManagementObjectSearcher("root\\CIMV2",
                                   "SELECT * FROM Win32_LogicalDisk");

            //Cycle through each drive and pull the information
            foreach (ManagementObject currentDrive in searcher.Get())
            {
                DriveData += "Drive: " + currentDrive["Caption"] + "\n"
                    //+ "Manufacturer: " + currentDrive["Manufacturer"] + "\n"
                    //+ "Drive Letter: " + currentDrive["DeviceID"] + "\n"
                    //+ "Serial Number: " + currentDrive["SerialNumber"] + "\n"
                    //+ "Status: " + currentDrive["Status"] + "\n"
                          + "Size: " + Convert.ToInt16(((Convert.ToDouble(currentDrive["Size"]) / 1024) / 1024) / 1024) + " GB" + "\n"
                          + "Free Space: " + Convert.ToInt16(((Convert.ToDouble(currentDrive["Freespace"]) / 1024) / 1024) / 1024) + " GB   "
                          + Convert.ToInt16((Convert.ToDouble(currentDrive["Freespace"]) / (Convert.ToDouble(currentDrive["Size"]) + 1)) * 100) + "%" + "\n"
                          + "\n";
            }

            return DriveData;
        }

        ///<summary>
        /// method for retrieving Operating System information
        /// using the WMI class
        ///</summary>
        ///
        public string GetOSInfo()
        {
            string OSData = "";
            // Load Operating System class
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            // Retrieve OS data
            foreach (ManagementObject OperatingSystem in osDetailsCollection)
            {
                OSData += "Operating System: " + OperatingSystem["Caption"] + "\n"
                       + "Free Physical Memory: " + Convert.ToInt16((Convert.ToDouble(OperatingSystem["FreePhysicalMemory"]) / 1024) / 1024) + " GB \n"
                       + "Total Physical Memory: " + Convert.ToInt16((Convert.ToDouble(OperatingSystem["TotalVisibleMemorySize"]) / 1024) / 1024) + " GB \n";
            }

            return OSData;
        }

        public string GetCPUInfo()
        {
            string CPUData = "";

            // Load CPU class
            ManagementObjectSearcher CPUDetails = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection CPUDetailsCollection = CPUDetails.Get();
            // Retrieve OS data
            foreach (ManagementObject CPUInfo in CPUDetailsCollection)
            {
                CPUData += "CPU: " + CPUInfo["Name"] + "\n";
            }

            return CPUData;
        }

        public string GetNetworkInfo()
        {
            string NetworkData = "";

            // Load Network class
            ManagementObjectSearcher NetworkDetails = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection NetworkDetailsCollection = NetworkDetails.Get();
            // Retrieve OS data
            foreach (ManagementObject NetworkInfo in NetworkDetailsCollection)
            {
                //Check if each network adapter has an IP Address and include only the ones that do
                if (NetworkInfo["IPAddress"] != null)
                {
                    if (NetworkInfo["IPAddress"] is Array)
                    {
                        string[] addresses = (string[])NetworkInfo["IPAddress"];
                        NetworkData += "IP Addresses: ";
                        foreach (string IP in addresses)
                        {
                            NetworkData += IP + "\n";
                        }
                        NetworkData += "MAC Address: " + NetworkInfo["MACAddress"] + "\n";
                    }
                    else
                    {
                        NetworkData += "IP Address: " + NetworkInfo["IPAddress"] + "\n"
                                + "MAC Address: " + NetworkInfo["MacAddress"] + "\n";
                    }

                }
            }

            return NetworkData;
        }
    }
}

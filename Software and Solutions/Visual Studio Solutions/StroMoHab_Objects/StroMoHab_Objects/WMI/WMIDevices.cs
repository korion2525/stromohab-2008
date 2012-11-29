using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Management;

namespace StroMoHab_Objects
{
    /// <summary>
    /// Provides a way to get the number of motion capture cameras via WMI
    /// </summary>
    static public class WMIDevices
    {
        /// <summary>
        /// Updates the list of cameras
        /// </summary>
        /// <returns>The list of cameras</returns>
        public static ManagementObjectCollection UpdateCameraList()
        {
                    
            //Searches for the device using its name
            ObjectQuery oq = new System.Management.ObjectQuery("select * from Win32_PnPEntity where Name='NaturalPoint OptiTrack V100'");
            ManagementObjectSearcher query = new ManagementObjectSearcher(oq);
            ManagementObjectCollection queryCollection = query.Get();
            /*
            Console.WriteLine();
            Console.WriteLine("Number of Cameras : " + queryCollection.Count);
            if (queryCollection.Count > 0)
            {
                foreach (ManagementObject mo in queryCollection)
                {
                    Console.WriteLine();
                    Console.WriteLine("USB Camera");

                    

                    foreach (PropertyData propertyData in mo.Properties)
                    {
                       

                        Console.WriteLine("Property Name = " + propertyData.Name);

                        Console.WriteLine("Property Value = " + propertyData.Value);

                    }
                    
                }
            }
            */
            return queryCollection;
        }
    }
}

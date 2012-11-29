using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using Stromohab_MCE;
using Stromohab_MCE_Connection;
using StromoLight_RemoteCalibration;
using System.Runtime.Remoting;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StromoLight_Diagnostics
{
    [Serializable]
    class TightropePostProcessor
    {
        private BackUpData retreivedData;

        public TightropePostProcessor(string filePath)
        {
            retreivedData = new BackUpData();
            try
            {
                FileStream fileStream = new FileStream(filePath + "TightropeBackupData", FileMode.Open);
                if (fileStream.Length > 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    retreivedData = (BackUpData)formatter.Deserialize(fileStream);
                }
                fileStream.Flush();
                fileStream.Close();
            }
            catch (FileNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine("Backup file not found");
            }
        }
    }
}

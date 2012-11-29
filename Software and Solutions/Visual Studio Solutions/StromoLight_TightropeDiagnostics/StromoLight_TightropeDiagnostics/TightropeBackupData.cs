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
    class TightropeBackupData
    {
        private List<Stromohab_MCE.Trackable> trackableList = new List<Trackable>();
        private BackUpData backupData;
        private string fileID;

        public TightropeBackupData(string fileID, Calibration calibration)
        {
            backupData = new BackUpData();
            backupData.CalibrationItem = calibration;
            this.fileID = fileID;
            if (calibration.LeftFoot_Marker != null)
            {
                TCPProcessor.WholeFrameReceivedEvent +=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
            }
            else
            {
                Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent +=
                    new TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
            }
        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            if (MarkerList.listOfMarkers.Count == 2)
            {
                backupData.Marker_Lists.Add(MarkerList.listOfMarkers);
            }
        }

        private void TCPProcessor_TrackableListReceivedEvent(List<Stromohab_MCE.Trackable> newTrackableList)
        {
            List<Marker> tempMarkerList = new List<Marker>();
            trackableList = new List<Stromohab_MCE.Trackable>(newTrackableList);
            foreach (Trackable trackable in trackableList)
            {
                tempMarkerList.Add((Marker)trackable);
            }
            backupData.Marker_Lists.Add(tempMarkerList);
        }

        public void FileTightropeBackupData()
        {
            FileStream filestream = new FileStream(fileID + "TightropeBackupData", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(filestream, backupData);
            }
            catch (SerializationException)
            {
                System.Diagnostics.Debug.WriteLine("Serialization exception in backup");
            }
            filestream.Flush();
            filestream.Close();
        }

        public void TightropeBackupDataCleanUp()
        {
            if (backupData.CalibrationItem.LeftFoot_Marker != null)
            {
                TCPProcessor.WholeFrameReceivedEvent -=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
            }
            else
            {
                Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent -=
                    new TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
            }
        }
    }
}

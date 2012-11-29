using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using StroMoHab_Objects.Communication;
using StroMoHab_Objects.Objects;

namespace StromoLight_Diagnostics
{
    [Serializable]
    class MarkerDataCollection
    {
        private List<List<PositionData>> allPositionData;
        private string fileID;
        public MarkerDataCollection(string fileID)
        {
            this.fileID = fileID;
            allPositionData = new List<List<PositionData>>();
            TCPProcessor.WholeFrameReceivedEvent +=
                new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            if (MarkerList.listOfMarkers.Count == 2)
            {
                List<PositionData> tempMarkerList = new List<PositionData>();
                foreach (Marker singleMarker in MarkerList.listOfMarkers)
                {
                    PositionData markerData = PositionData.ConvertMarkerPosition(singleMarker);
                    tempMarkerList.Add(markerData);
                }
                allPositionData.Add(tempMarkerList);
            }
        }

        public void FileMarkerListPositionData()
        {
            FileStream filestream = new FileStream(fileID + "_MarkerListPositionData", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(filestream, allPositionData);
            }
            catch (SerializationException)
            {
                System.Diagnostics.Debug.WriteLine("Serialization exception in backup");
            }
            filestream.Flush();
            filestream.Close();
        }

        public void FileMarkerListPositionData_CSV()
        {
            StreamWriter dataFile = new StreamWriter(fileID + "_MarkerListPositionData.csv", false);
            dataFile.Write("MarkerID, Time, X, Y, Z, , MarkerID, Time, X, Y, Z\n");
            foreach (List<PositionData> markerPositionList in allPositionData)
            {
                dataFile.Write(markerPositionList[0].MarkerID + "," + (markerPositionList[0].Time) + "," + 
                    markerPositionList[0].XCoordinate + "," + markerPositionList[0].YCoordinate + "," + markerPositionList[0].ZCoordinate + ",,");
                dataFile.Write(markerPositionList[1].MarkerID + "," + markerPositionList[1].Time + "," +
                    markerPositionList[1].XCoordinate + "," + markerPositionList[1].YCoordinate + "," + markerPositionList[1].ZCoordinate + "\n");
            }
            dataFile.Flush();
            dataFile.Close();
        }
    
        public void MarkerDataCollectionCleanUp()
        {
            TCPProcessor.WholeFrameReceivedEvent -=
                new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }

        internal List<List<PositionData>> AllPositionData
        {
            get { return allPositionData; }
            set { allPositionData = value; }
        }
    }
}

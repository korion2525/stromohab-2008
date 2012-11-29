using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stromohab_MCE;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StromoLight_Diagnostics
{
    [Serializable]
    public class RawData
    {
        MarkerList saveMarkerList;
        public RawData(Trackable calibrationTrackable)
        {
            //Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent +=
            //        new TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
        }

        public RawData(Marker calibrationMarker)
        {
            //TCPProcessor.WholeFrameReceivedEvent +=
            //        new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }
        
    }
}

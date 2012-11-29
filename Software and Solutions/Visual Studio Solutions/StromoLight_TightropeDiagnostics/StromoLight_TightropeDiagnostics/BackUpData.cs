using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StromoLight_RemoteCalibration;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using Stromohab_MCE;
using Stromohab_MCE_Connection;



namespace StromoLight_Diagnostics
{
    [Serializable]
    class BackUpData
    {
        
        private List<List<Marker>> marker_Lists;
        private Calibration calibrationItem;

        public BackUpData()
        {
            marker_Lists = new List<List<Marker>>();
        }

        public Calibration CalibrationItem
        {
            get { return calibrationItem; }
            set { calibrationItem = value; }
        }

        public List<List<Marker>> Marker_Lists
        {
            get { return marker_Lists; }
            set { marker_Lists = value; }
        }

    }
}

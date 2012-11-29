using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StromoLight_Diagnostics
{
    [Serializable]
    public class PositionData
    {
        int markerID;
        int time;
        double xCoordinate;
        double yCoordinate;
        double zCoordinate;

        public double ZCoordinate
        {
            get { return zCoordinate; }
            set { zCoordinate = value; }
        }

        public double YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        public double XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public int MarkerID
        {
            get { return markerID; }
            set { markerID = value; }
        }

    }
}

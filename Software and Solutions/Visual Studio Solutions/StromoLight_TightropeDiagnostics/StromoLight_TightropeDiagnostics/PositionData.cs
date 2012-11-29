using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using StroMoHab_Objects.Objects;


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

        public PositionData()
        {

        }

        public PositionData(int markerID, int time, double x, double y, double z)
        {
            this.markerID = markerID;
            this.time = time;
            this.xCoordinate = x;
            this.yCoordinate = y;
            this.zCoordinate = z;
        }

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

        public static PositionData ConvertMarkerPosition(Marker marker)
        {
            PositionData position = new PositionData();
            position.MarkerID = marker.MarkerId;
            position.Time = marker.TimeStamp;
            position.XCoordinate = marker.xCoordinate;
            position.YCoordinate = marker.yCoordinate;
            position.ZCoordinate = marker.zCoordinate;
            return position;
        }
    }
}

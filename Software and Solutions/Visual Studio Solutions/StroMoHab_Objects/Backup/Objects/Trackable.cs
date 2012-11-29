
using System;
using System.Collections.Generic;
namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a collection of markers in a pre-difined configuration
    /// </summary>
    [Serializable]
    public class Trackable : Marker
    {
        #region Member Variables
        private string name;
        private long timeStamp;
        private int id;
        private int x;
        private int y;
        private int z;
        private double pitch;
        private double yaw;
        private double roll;
        private double qx;
        private double qy;
        private double qz;
        private double qw;
        private int trackableIndex;
        private List<Marker> trackableMarkers = null;
        #endregion Member Variables

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Trackable()
        {
            x = 0;
            y = 0;
            z = 0;
            pitch = 0;
            yaw = 0;
            roll = 0;
            qw = 0;
            qx = 0;
            qy = 0;
            qz = 0;
            id = -1;
            timeStamp = -1;
            name = null;
            trackableIndex = -1;
            trackableMarkers = null;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newTrackable">New Trackable</param>
        public Trackable(Trackable newTrackable)
        {
            x = newTrackable.xCoordinate;
            y = newTrackable.yCoordinate;
            x = newTrackable.zCoordinate;
            pitch = newTrackable.Pitch;
            yaw = newTrackable.Yaw;
            roll = newTrackable.Roll;
            qw = newTrackable.qw;
            qx = newTrackable.qx;
            qy = newTrackable.qy;
            qz = newTrackable.qz;
            id = newTrackable.ID;
            timeStamp = newTrackable.TimeStamp;
            name = newTrackable.Name;
            trackableIndex = newTrackable.TrackableIndex;
            trackableMarkers = null;
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// The index inside the Tracking Tools API where the data is stored
        /// </summary>
        public int TrackableIndex
        {
            get { return trackableIndex; }
            set { trackableIndex = value; }
        }

        /// <summary>
        /// Contains a list of the Markers that make up the Trackable
        /// </summary>
        public List<Marker> TrackableMarkers
        {
            get { return trackableMarkers; }
            set { trackableMarkers = value; }
        }

        /// <summary>
        /// Time Stamp for when the trackable data was collected
        /// </summary>
        public new long TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
                base.TimeStamp = value;
            }
        }

        /// <summary>
        /// Name of the trackable e.g. Left Lower Leg
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        /// <summary>
        /// Unique ID number assigned when defining the Trackle in Tracking Tools
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        /// <summary>
        /// x Coordinate
        /// </summary>
        public new int xCoordinate
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                base.xCoordinate = (double)value;
            }
        }
        /// <summary>
        /// y Coordinate
        /// </summary>
        public new int yCoordinate
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                base.yCoordinate = (double)value;
            }
        }
        /// <summary>
        /// z Coordinate
        /// </summary>
        public new int zCoordinate
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
                base.zCoordinate = (double)value;
            }
        }

        /// <summary>
        /// Orientation - Yaw
        /// </summary>
        public double Yaw
        {
            get
            {
                return yaw;
            }
            set
            {
                yaw = value;
            }
        }

        /// <summary>
        /// Orientation - Pitch
        /// </summary>
        public double Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
            }
        }
        /// <summary>
        /// Orientation - Roll
        /// </summary>
        public double Roll
        {
            get
            {
                return roll;
            }
            set
            {
                roll = value;
            }
        }
        /// <summary>
        /// Quaternion rotation data - QW
        /// </summary>
        public double QW
        {
            get
            {
                return qw;
            }
            set
            {
                qw = value;
            }
        }
        /// <summary>
        /// Quaternion rotation data - QX
        /// </summary>
        public double QX
        {
            get
            {
                return qx;
            }
            set
            {
                qx = value;
            }
        }
        /// <summary>
        /// Quaternion rotation data - QY
        /// </summary>
        public double QY
        {
            get
            {
                return qy;
            }
            set
            {
                qy = value;
            }
        }
        /// <summary>
        /// Quaternion rotation data - QZ
        /// </summary>
        public double QZ
        {
            get
            {
                return qz;
            }
            set
            {
                qz = value;
            }
        }
        #endregion Properties
    }
}


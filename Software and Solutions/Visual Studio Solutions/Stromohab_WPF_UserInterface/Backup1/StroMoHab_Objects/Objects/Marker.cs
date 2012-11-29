using System;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Contains marker coordinate data
    /// </summary>
    [Serializable]
    public class Marker
    {
        #region Member Variables
        private double x = 0, y = 0, z = 0, prevX = 0, prevY = 0, prevZ = 0, speed = 0;
        private int markerId = -1;
        private long timeStamp = -1, m_prevTimestamp = -1;
        #endregion Member Variables

        #region Constructors
        /// <summary>
        /// Default constructor - ID and Timestamp initialise to -1
        /// </summary>
        public Marker()
        {
            markerId = -1;
            timeStamp = -1;
            xCoordinate = 0;
            yCoordinate = 0;
            zCoordinate = 0;
        }

        /// <summary>
        /// Constructor - creates new marker using parameter marker value
        /// </summary>
        /// <param name="newMarker"></param>
        public Marker(Marker newMarker)
        {
            markerId = newMarker.MarkerId;
            timeStamp = newMarker.TimeStamp;
            xCoordinate = newMarker.xCoordinate;
            yCoordinate = newMarker.yCoordinate;
            zCoordinate = newMarker.zCoordinate;

            prevX = newMarker.prevXCoordinate;
            prevY = newMarker.prevYCoordinate;
            prevZ = newMarker.prevZCoordinate;
            m_prevTimestamp = newMarker.PrevTimestamp;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// ID number of the marker
        /// </summary>
        public int MarkerId
        {
            get
            {
                return markerId;
            }
            set
            {
                markerId = value;
            }
        }

        /// <summary>
        /// The marker timestamp
        /// </summary>
        public long TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                m_prevTimestamp = timeStamp;
                timeStamp = value;
            }
        }


        /// <summary>
        /// X coordinate of marker
        /// </summary>
        public double xCoordinate
        {
            get
            {
                return x;
            }
            set
            {
                prevX = x;
                x = value;
            }
        }

        /// <summary>
        /// Y coordinate of marker
        /// </summary>
        public double yCoordinate
        {
            get
            {
                return y;
            }
            set
            {
                prevY = y;
                y = value;
            }
        }

        /// <summary>
        /// Z coordinate of marker
        /// </summary>
        public double zCoordinate
        {
            get
            {
                return z;
            }
            set
            {
                prevZ = z;
                z = value;
            }
        }

        /// <summary>
        /// Previous X coordinate of marker
        /// </summary>
        public double prevXCoordinate
        {
            get
            {
                return prevX;
            }
        }
        /// <summary>
        /// Previous Y coordinate of marker
        /// </summary>
        public double prevYCoordinate
        {
            get
            {
                return prevY;
            }
        }
        /// <summary>
        /// Previous Z coordinate of marker
        /// </summary>
        public double prevZCoordinate
        {
            get
            {
                return prevZ;
            }
        }

        /// <summary>
        /// Previous timestamp matching previous coordinates.
        /// </summary>
        public long PrevTimestamp
        {
            get
            {
                return m_prevTimestamp;
            }
        }

        /// <summary>
        /// Speed (scalar) of marker
        /// </summary>
        public double Speed
        {
            get
            {
                try
                {
                    speed = ((x - prevX) + (y - prevY) + (z - prevZ)) / (timeStamp - PrevTimestamp);
                }
                catch (DivideByZeroException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Speed encountered divide by zero error. Exception message: " + ex.Message);
                    return 0;
                }
                return speed;
            }
        }

        #endregion Properties
    }
}

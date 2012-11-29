namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a joint based on two connecting Trackables
    /// </summary>
    public class Joint
    {
        #region Member Variables
        private string name;
        private long timeStamp = -1;
        private int id = -1;
        private double yaw;
        private double pitch;
        private double roll;
        private int trackable1 = -1;
        private int trackable2 = -1;
        private bool exists = false;
        private double yawOffset = 0;
        private double pitchOffset = 0;
        private double rollOffset = 0;
        private int x = -1;
        private int y = -1;
        private int z = -1;
        #endregion Member Variables


        #region Properties

        /// <summary>
        /// TimeStamp
        /// </summary>
        public long TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }

        /// <summary>
        /// xCoordinate
        /// </summary>
        public int xCoordinate
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        /// <summary>
        /// y Coordinate
        /// </summary>
        public int yCoordinate
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// z Coordinate
        /// </summary>
        public int zCoordinate
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
        /// <summary>
        /// True if the Joint exists in the current frame, false if not
        /// </summary>
        public bool Exists
        {
            get
            {
                return exists;
            }
            set
            {
                exists = value;
            }
        }
        /// <summary>
        /// The ID of the trackable furthest away from the center of the body, e.g Joint = Ankle, Trackable1 = Foot
        /// </summary>
        public int Trackable1
        {
            get
            {
                return trackable1;
            }
            set
            {
                trackable1 = value;
            }
        }


        /// <summary>
        /// The ID of the trackable nearest the center of the body, e.g Joint = Ankle, Trackable2 = Lower Leg
        /// </summary>
        public int Trackable2
        {
            get
            {
                return trackable2;
            }
            set
            {
                trackable2 = value;
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
        /// A pre-defined value to offset the orientation of the joint by when it is created
        /// </summary>
        public double YawOffset
        {
            get
            {
                return yawOffset;
            }
            set
            {
                yawOffset = value;
            }
        }

        /// <summary>
        /// A pre-defined value to offset the orientation of the joint by when it is created
        /// </summary>
        public double PitchOffset
        {
            get
            {
                return pitchOffset;
            }
            set
            {
                pitchOffset = value;
            }
        }

        /// <summary>
        /// A pre-defined value to offset the orientation of the joint by when it is created
        /// </summary>
        public double RollOffset
        {
            get
            {
                return rollOffset;
            }
            set
            {
                rollOffset = value;
            }
        }

        /// <summary>
        /// The Name of the joint e.g. Left Ankle
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
        /// Unique ID number of the joint
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

        #endregion Properties

    }


}

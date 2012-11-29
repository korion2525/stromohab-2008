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


namespace StromoLight_Diagnostics
{
    public class Foot
    {
        private int xDirection;// -1 -> indicates heading in -x direction
        private int yDirection;// +1 -> +y direction etc
        private int zDirection;// +1 (+z) is forwards, towards screen; -1 -> moving towards back of treadmill
        private int xPrevDirection;
        private int yPrevDirection;
        private int zPrevDirection;
        private int xChange = 0;
        private int yChange = 0;
        private int zChange = 0;
        private double footdown_yCoordinate;
        private bool footDownEventIsFired = false;
        private Marker currentMarker = null;
        private Marker previousMarker;
        private int currentID;
        public delegate void X_PeakEventHandler(Foot foot);
        public static event X_PeakEventHandler xFootPeakDetected;
        public delegate void Y_PeakEventHandler(Foot foot);
        public static event Y_PeakEventHandler yFootPeakDetected;
        public delegate void Z_PeakEventHandler(Foot foot);
        public static event Z_PeakEventHandler zFootPeakDetected;
        public delegate void FootDownEventHandler(Foot foot);
        public static event FootDownEventHandler FootDownDetected;
        private string footName;
        private Marker calibrationMarker;
        //private Trackable calibrationTrackable;
        private int markerShift;
        private static Foot frontfoot = null;

        //trackable code
        private List<Stromohab_MCE.Trackable> trackableList = new List<Trackable>();

        public Foot(string footName, Marker calibrationMarker)
        {
            this.calibrationMarker = calibrationMarker;
            this.footName = footName;
            previousMarker = calibrationMarker;
            footdown_yCoordinate = calibrationMarker.yCoordinate;
            currentID = calibrationMarker.MarkerId;
            xDirection = 0;// -1 -> indicates heading in -x direction
            yDirection = 0;// +1 -> +y direction etc
            zDirection = 0;// +1 (+z) is forwards, towards screen; -1 -> movving towards back of treadmill
            xPrevDirection = 0;
            yPrevDirection = 0;
            zPrevDirection = 0;
            TCPProcessor.WholeFrameReceivedEvent +=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }

        public Foot(string footName, Trackable calibrationTrackable)
        {
            this.footName = footName;
            //this.calibrationMarker = calibrationMarker;
            //previousTrackable = calibrationTrackable;
            if (footName == "left") currentID = 1;
            if (footName == "right") currentID = 2;
            footdown_yCoordinate = calibrationTrackable.yCoordinate;
            xDirection = 0;// -1 -> indicates heading in -x direction
            yDirection = 0;// +1 -> +y direction etc
            zDirection = 0;// +1 (+z) is forwards, towards screen; -1 -> moving towards back of treadmill
            xPrevDirection = 0;
            yPrevDirection = 0;
            zPrevDirection = 0;
            Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent +=
                    new TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
        }

        private void TCPProcessor_TrackableListReceivedEvent(List<Stromohab_MCE.Trackable> newTrackableList)
        {
            trackableList = new List<Stromohab_MCE.Trackable>(newTrackableList);
            Marker m1 = (Marker)(trackableList.Find(LeftFoot));
            Marker m2 = (Marker)(trackableList.Find(RightFoot));
            if (currentID == 1) currentMarker = m1;
            if (currentID == 2) currentMarker = m2;
            MarkerTrack();
            FrontFoot(m1 ,m2);
        }

        private static bool LeftFoot(Trackable t)
        {
            if (t.ID == 1)
                return true;
            else
                return false;
        }

        private static bool RightFoot(Trackable t)
        {
            if (t.ID == 2)
                return true;
            else
                return false;
        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            if (MarkerList.listOfMarkers.Count == 2)
            {
                currentMarker = MarkerList.listOfMarkers[currentID];
                MarkerTrack();
                FrontFoot(MarkerList.listOfMarkers[0], MarkerList.listOfMarkers[1]);
            }
        }

        private void MarkerTrack()
        {
            double relativePosition = 0;
            double nearestPosition = 0;


            //do diagnostics for each foot instance. Each foot instance subscribes to _WFREvent and
            //is specifically identified by 'currentID'
            
            int xDiff = (int)(currentMarker.xCoordinate - currentMarker.prevXCoordinate + 0.5);
            int yDiff = (int)(currentMarker.yCoordinate - currentMarker.prevYCoordinate + 0.5);
            int zDiff = (int)(currentMarker.zCoordinate - currentMarker.prevZCoordinate + 0.5);

            //find absolute change in position between this and last marker positions
            relativePosition = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(zDiff, 2) + Math.Pow(yDiff, 2));
            nearestPosition = relativePosition;
            markerShift = (int)nearestPosition;

            if (Math.Abs(xDiff) > 1) xChange = xDiff;//check current-previous>1 to illiminate small errors
            if (Math.Abs(yDiff) > 1) yChange = yDiff;//
            if (Math.Abs(zDiff) > 1) zChange = zDiff;//

            xDirection = Math.Sign(xChange);
            yDirection = Math.Sign(yChange);
            zDirection = Math.Sign(zChange);

            //check for peak and foot down events
            if ((xDirection != xPrevDirection) && (xFootPeakDetected != null) && (xDirection != 0))
            {
                xFootPeakDetected(this);
            }
            if ((yDirection != yPrevDirection) && (yFootPeakDetected != null) && (yDirection != 0))
            {
                yFootPeakDetected(this);
            }
            if ((zDirection != zPrevDirection) && (zFootPeakDetected != null) && (zDirection != 0))
            {
                zFootPeakDetected(this);
            }
            if ((zDirection == 1))
            {
                footDownEventIsFired = false;//prevent further footdown events for the current step
            }
            if ((footDownEventIsFired == false) && (zDirection == -1) && (currentMarker.yCoordinate < (footdown_yCoordinate + 5)) && (FootDownDetected != null) && (Math.Abs(xChange) < 10) && (Math.Abs(yChange) < 10))
            {
                footDownEventIsFired = true;
                FootDownDetected(this);
            }
            xPrevDirection = xDirection;
            yPrevDirection = yDirection;
            zPrevDirection = zDirection;
        }



        private void FrontFoot(Marker m1, Marker m2)
        {
            if (m1.zCoordinate >= m2.zCoordinate)
            {
                if (this.Equals(m1))
                {
                    frontfoot = this;
                }
            }
            else
            {
                if (this.Equals(m2))
                {
                    frontfoot = this;
                }
            }
        }

        #region Foot tracking code (depracated)
    
        private void FootTrack()
        {
            //For each new frame need to determine which Marker 'describes' this foot instance
            //An extension of Marker_Bin - it has to allow for feet crossing so can not rely on 
            //which marker is most left/right but has to use historical information to determine
            //the track of this foot instance. Initially previous position only but Kalman filter may be necessary.
            //This function can also determine foot events eg. peaks, foot-down etc.
            double nearestPosition = 9999;
            double relativePosition = 0;

            //if (MarkerList.listOfMarkers.Count != 2)
            
            foreach (Marker singleMarker in MarkerList.listOfMarkers)
            {
                int xDiff = ((int)(singleMarker.xCoordinate + (0.5 * Math.Sign(singleMarker.xCoordinate)))) - ((int)(previousMarker.xCoordinate + (0.5 * Math.Sign(previousMarker.xCoordinate))));
                int yDiff = ((int)(singleMarker.yCoordinate + (0.5 * Math.Sign(singleMarker.yCoordinate)))) - ((int)(previousMarker.yCoordinate + (0.5 * Math.Sign(previousMarker.yCoordinate))));
                int zDiff = ((int)(singleMarker.zCoordinate + (0.5 * Math.Sign(singleMarker.zCoordinate)))) - ((int)(previousMarker.zCoordinate + (0.5 * Math.Sign(previousMarker.zCoordinate))));
                relativePosition = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(zDiff, 2) + Math.Pow(yDiff, 2));

                //Console.WriteLine(this.footName + singleMarker.MarkerId.ToString() + ">>" + " x" + ((int)singleMarker.xCoordinate).ToString() +
                    //" y" + ((int)singleMarker.yCoordinate).ToString() + " z" + ((int)singleMarker.zCoordinate).ToString() + " ><" + relativePosition.ToString());
                //Console.WriteLine("Previous: x" + previousMarker.xCoordinate.ToString() + " y" + previousMarker.yCoordinate.ToString() + " z" + previousMarker.zCoordinate.ToString());

                if (relativePosition < nearestPosition)
                {
                    nearestPosition = relativePosition;
                    markerShift = (int)nearestPosition;
                    currentMarker = new Marker (singleMarker);
                    //if (Math.Abs(xDiff) > 1) xChange = xDiff;//check current-previous>1 to illiminate small errors
                    //if (Math.Abs(yDiff) > 1) yChange = yDiff;//
                    //if (Math.Abs(zDiff) > 1) zChange = zDiff;//
                    xChange = xDiff;
                    yChange = yDiff;
                    zChange = zDiff;
                    //Console.WriteLine("zDiff = " + zDiff.ToString());
                }
            }
            //Console.WriteLine("==" + footName + currentMarker.MarkerId.ToString() + ">nearest<" + nearestPosition.ToString());

            if (nearestPosition < 50)
            {
                //Console.WriteLine(this.footName + this.currentMarker.MarkerId.ToString());


                xDirection = Math.Sign(xChange);
                yDirection = Math.Sign(yChange);
                zDirection = Math.Sign(zChange);

                if ((xDirection != xPrevDirection) && (xFootPeakDetected != null) && (xDirection != 0))
                {
                    xFootPeakDetected(this);
                }
                if ((yDirection != yPrevDirection) && (yFootPeakDetected != null) && (yDirection != 0))
                {
                    yFootPeakDetected(this);
                }
                if ((zDirection != zPrevDirection) && (zFootPeakDetected != null) && (zDirection != 0))
                {
                    zFootPeakDetected(this);
                }

                if ((zDirection == 1))
                {
                    footDownEventIsFired = false;//prevent further footdown events for the current step
                }

                //Console.WriteLine("DownEventData: " + "E" + footDownEventIsFired.ToString() + " zD" + zDirection.ToString() +
                    //" y" + currentMarker.yCoordinate.ToString() + " xChange" + (Math.Abs(xChange)).ToString() + " yChange" + (Math.Abs(yChange)).ToString());
                if ((footDownEventIsFired == false) && (zDirection == -1) && (currentMarker.yCoordinate < (footdown_yCoordinate + 5)) && (Math.Abs(xChange) < 10) && (Math.Abs(yChange) < 10))
                {
                    footDownEventIsFired = true;
                    FootDownDetected(this);
                }

                previousMarker = new Marker(currentMarker);
                xPrevDirection = xDirection;
                yPrevDirection = yDirection;
                zPrevDirection = zDirection;
                //Console.WriteLine("Passing previous: x" + previousMarker.xCoordinate + " y" + previousMarker.yCoordinate + " z" + previousMarker.zCoordinate + "\n");
            }
            //else
            //{
            //currentMarker = null;
            //}
        }
    
#endregion 

        public static Foot Frontfoot
        {
            get { return frontfoot; }
        }

        public int NearestPosition
        {
            get
            {
                return markerShift;
            }
        }

        public string FootName
        {
            get
            {
                return footName;
            }
        }

        public double FootDown_yThreshold
        {
            set
            {
                footdown_yCoordinate = value;
            }
            get
            {
                return footdown_yCoordinate;
            }
        }

        public int XDirection
        {
            get
            {
                return xDirection;
            }
        }

        public int YDirection
        {
            get
            {
                return yDirection;
            }
        }

        public int ZDirection
        {
            get
            {
                return zDirection;
            }
        }
        /// <summary>
        /// Gets the current marker representing this foot
        /// Returns NULL if maker is bad or lost
        /// </summary>
        public Marker CurrentMarker
        {
            get
            {
                return currentMarker;
            }
        }

        public Marker PreviousMarker
        {
            get
            {
                return previousMarker;
            }
        }

        public Marker CalibrationMarker
        {
            get
            {
                return calibrationMarker;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using System.Runtime.Remoting;
using StroMoHab_Objects.Objects;


namespace StromoLight_Diagnostics
{
    public class Foot
    {
        private int xDirection = 0;// -1 -> indicates heading in -x direction
        private int yDirection = 0;// +1 -> +y direction etc
        private int zDirection = 0;// +1 (+z) is forwards, towards screen; -1 -> moving towards back of treadmill
        private int xPrevDirection = 0;
        private int yPrevDirection = 0;
        private int zPrevDirection = 0;
        private double zPreviousCoordinate = 0;
        private double yPreviousCoordinate = 0;
        private double xPreviousCoordinate = 0;
        private int xChange = 0;
        private int yChange = 0;
        private int zChange = 0;
        
        private double footdown_yCoordinate = 0;
        private bool footDownEventIsFired = false;
        private Marker currentMarker = null;
        private Marker previousMarker = null;
        private int currentID = 0;
        //private int previousID = 0;
        public delegate void X_PeakEventHandler(Foot foot);
        public static event X_PeakEventHandler xFootPeakDetected;
        public delegate void Y_PeakEventHandler(Foot foot);
        public static event Y_PeakEventHandler yFootPeakDetected;
        public delegate void Z_PeakEventHandler(Foot foot);
        public static event Z_PeakEventHandler zFootPeakDetected;
        public delegate void FootDownEventHandler(Foot foot);
        public static event FootDownEventHandler FootDownDetected;
        private string footName = null;
        private Marker calibrationMarker = null;
        private Trackable calibrationTrackable;
        private Trackable previousTrackable;
        private int markerShift = 0;
        private static Foot frontfoot = null;

        private List<PositionData> footPositionList;

        //trackable code
        private List<Trackable> trackableList = new List<Trackable>();

        public Foot(string footName)//, Marker calibrationMarker)
        {
            this.footName = footName;
        }

        public void CalibrateFoot(Marker calibrationMarker)
        {
            footPositionList = new List<PositionData>();
            //TCPProcessor.WholeFrameReceivedEvent +=
            //    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
            this.calibrationMarker = calibrationMarker;
            previousMarker = calibrationMarker;
            //
            currentMarker = new Marker(previousMarker);
            //
            PositionData positionCalibration = PositionData.ConvertMarkerPosition(calibrationMarker);
            this.footPositionList.Add(positionCalibration);
            footdown_yCoordinate = calibrationMarker.yCoordinate;
            currentID = calibrationMarker.MarkerId;
            xDirection = 0;// -1 -> indicates heading in -x direction
            yDirection = 0;// +1 -> +y direction etc
            zDirection = 0;// +1 (+z) is forwards, towards screen; -1 -> moving towards back of treadmill
            xPrevDirection = 0;
            yPrevDirection = 0;
            zPrevDirection = 0;
            zPreviousCoordinate = previousMarker.zCoordinate;
            yPreviousCoordinate = previousMarker.yCoordinate;
            xPreviousCoordinate = previousMarker.xCoordinate;
        }
        

        public void CalibrateFoot(Trackable calibrationTrackable)
        {
            //Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent +=
            //    new TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
            if (footName == "left") currentID = 1;
            if (footName == "right") currentID = 2;
            this.calibrationTrackable = calibrationTrackable;
            previousTrackable = calibrationTrackable;
            PositionData positionCalibration = PositionData.ConvertMarkerPosition((Marker)calibrationTrackable);
            this.footPositionList.Add(positionCalibration);
            footdown_yCoordinate = calibrationTrackable.yCoordinate;
            xDirection = 0;// -1 -> indicates heading in -x direction
            yDirection = 0;// +1 -> +y direction etc
            zDirection = 0;// +1 (+z) is forwards, towards screen; -1 -> moving towards back of treadmill
            xPrevDirection = 0;
            yPrevDirection = 0;
            zPrevDirection = 0;
            zPreviousCoordinate = previousTrackable.zCoordinate;
            yPreviousCoordinate = previousTrackable.yCoordinate;
            xPreviousCoordinate = previousTrackable.xCoordinate;
        }

        private void TCPProcessor_TrackableListReceivedEvent(List<Trackable> newTrackableList)
        {
            trackableList = new List<Trackable>(newTrackableList);
            Marker m1 = (Marker)(trackableList.Find(LeftFoot));
            Marker m2 = (Marker)(trackableList.Find(RightFoot));
            if (currentID == 1) currentMarker = m1;
            if (currentID == 2) currentMarker = m2;
            PositionData footData = PositionData.ConvertMarkerPosition(currentMarker);
            this.footPositionList.Add(footData);
            MarkerTrack();
            //FrontFoot(m1, m2);
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

        /*private void TCPProcessor_WholeFrameReceivedEvent()
        {
            //if (MarkerList.listOfMarkers.Count == 0) //ALSoundEnvironment.Soundon = false;
            if (MarkerList.listOfMarkers.Count == 2)
            {
                FrontFoot(MarkerList.listOfMarkers[0], MarkerList.listOfMarkers[1]);
                previousID = currentID;
                //currentID = AssignFootMarker(MarkerList.listOfMarkers, this);
                //currentMarker = MarkerList.listOfMarkers[currentID]; //elaborate marker assignment to account for swapping
                if (currentID != previousID) System.Diagnostics.Debug.WriteLine("Possible marker swap: " + currentMarker.TimeStamp);

                PositionData footData = PositionData.ConvertMarkerPosition(currentMarker);
                this.footPositionList.Add(footData);
                MarkerTrack();
            }
        }*/

        
        /*
        private int AssignFootMarker(List<Marker> currentMarkerList)//existing code
        {
            int x0Diff = (int)(currentMarkerList[0].xCoordinate - xPreviousCoordinate + 0.5);
            int y0Diff = (int)(currentMarkerList[0].yCoordinate - yPreviousCoordinate + 0.5);
            int z0Diff = (int)(currentMarkerList[0].zCoordinate - zPreviousCoordinate + 0.5);
            int x1Diff = (int)(currentMarkerList[1].xCoordinate - xPreviousCoordinate + 0.5);
            int y1Diff = (int)(currentMarkerList[1].yCoordinate - yPreviousCoordinate + 0.5);
            int z1Diff = (int)(currentMarkerList[1].zCoordinate - zPreviousCoordinate + 0.5);
            int relativePosition0 = (int)Math.Sqrt(Math.Pow(x0Diff, 2) + Math.Pow(z0Diff, 2) + Math.Pow(y0Diff, 2));
            int relativePosition1 = (int)Math.Sqrt(Math.Pow(x1Diff, 2) + Math.Pow(z1Diff, 2) + Math.Pow(y1Diff, 2));
            if (relativePosition0 < relativePosition1)
            {
                currentID = currentMarkerList[0].MarkerId;
                if (Math.Abs(x0Diff) > 1) xChange = x0Diff;
                if (Math.Abs(y0Diff) > 1) yChange = y0Diff;
                if (Math.Abs(z0Diff) > 1) zChange = z0Diff;
            }
            else
            {
                currentID = currentMarkerList[1].MarkerId;
                if (Math.Abs(x1Diff) > 1) xChange = x1Diff;
                if (Math.Abs(y1Diff) > 1) yChange = y1Diff;
                if (Math.Abs(z1Diff) > 1) zChange = z1Diff;
            }
            return currentID;
        }
        */

        public void MarkerTrack()
        {
            int xDif = (int)(this.currentMarker.xCoordinate - this.xPreviousCoordinate + 0.5);
            int yDif = (int)(this.currentMarker.yCoordinate - this.yPreviousCoordinate + 0.5);
            int zDif = (int)(this.currentMarker.zCoordinate - this.zPreviousCoordinate + 0.5);
            if (Math.Abs(xDif) > 1) xChange = xDif;
            if (Math.Abs(yDif) > 1) yChange = yDif;
            if (Math.Abs(zDif) > 1) zChange = zDif;
            xDirection = Math.Sign(xChange); //+1 foot moving right; -1 foot moving left
            yDirection = Math.Sign(yChange); //+1 foot moving upwards; -1 foot moving downwards
            zDirection = Math.Sign(zChange); //+1 foot moving forward; -1 foot moving backwards

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
                footDownEventIsFired = false; //Foot down event unlocked when foot travelling forward
            }
            if ((footDownEventIsFired == false) && (zDirection == -1) && (currentMarker.yCoordinate < (footdown_yCoordinate + 30)) && (FootDownDetected != null))// && (Math.Abs(xChange) < 3) && (Math.Abs(yChange) < 3))
            {
                
                footDownEventIsFired = true; //once fired on backward swing, event is locked. Event is unlocked (above) when foot is travelling forward.
                FootDownDetected(this);
            }
            xPrevDirection = xDirection;
            yPrevDirection = yDirection;
            zPrevDirection = zDirection;
            zPreviousCoordinate = currentMarker.zCoordinate;
            yPreviousCoordinate = currentMarker.yCoordinate;
            xPreviousCoordinate = currentMarker.xCoordinate;
        }

        private void FrontFoot(Marker m1, Marker m2)
        {
            if (m1.zCoordinate >= m2.zCoordinate)
            {
                if (this.currentMarker.zCoordinate == m1.zCoordinate)
                {
                    frontfoot = this;
                }
            }
            else
            {
                if (this.currentMarker.zCoordinate == m2.zCoordinate)
                {
                    frontfoot = this;
                }
            }
        }

        #region ConvertMarkerPosition (now static method in PositionData)
        /*private PositionData ConvertMarkerPosition(Marker calibrationMarker)
        {
            PositionData position = new PositionData();
            position.Time = calibrationMarker.TimeStamp;
            position.XCoordinate = calibrationMarker.xCoordinate;
            position.YCoordinate = calibrationMarker.yCoordinate;
            position.ZCoordinate = calibrationMarker.zCoordinate;
            return position;
        }
        */
        #endregion

        //public void FootCleanUp()
        //{
        //    TCPProcessor.WholeFrameReceivedEvent -=
        //            new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        //}

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
                if ((footDownEventIsFired == false) && (zDirection == -1) && (currentMarker.yCoordinate < (footdown_yCoordinate + 15)))// && (Math.Abs(xChange) < 3) && (Math.Abs(yChange) < 3))
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
            set { frontfoot = value; }
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
            set
            {
                currentMarker = value;
            }
        }

        public Marker PreviousMarker
        {
            get
            {
                return previousMarker;
            }
            set
            {
                previousMarker = value;
            }
        }

        public Marker CalibrationMarker
        {
            get
            {
                return calibrationMarker;
            }
        }

        public List<PositionData> FootPositionList
        {
            get { return footPositionList; }
            set { footPositionList = value; }
        }
    }
}

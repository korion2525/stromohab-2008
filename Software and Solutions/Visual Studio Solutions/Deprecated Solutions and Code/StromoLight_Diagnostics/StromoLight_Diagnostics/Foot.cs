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
        private int zDirection;// +1 (+z) is forwards, towards screen; -1 -> movving towards back of treadmill
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
        private int debugcount = 0;
        private int markerShift;

        public Foot(string footName, Marker calibrationMarker)
        {
            this.calibrationMarker = calibrationMarker;
            this.footName = footName;
            previousMarker = calibrationMarker;
            footdown_yCoordinate = calibrationMarker.yCoordinate;

            xDirection = 0;// -1 -> indicates heading in -x direction
            yDirection = 0;// +1 -> +y direction etc
            zDirection = 0;// +1 (+z) is forwards, towards screen; -1 -> movving towards back of treadmill
            xPrevDirection = 0;
            yPrevDirection = 0;
            zPrevDirection = 0;

            TCPProcessor.WholeFrameReceivedEvent +=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);


        }
        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            //For each new frame need to determine which Marker 'describes' this foot instance
            //An extension of Marker_Bin - it has to allow for feet crossing so can not rely on 
            //which marker is most left/right but has to use historical information to determine
            //the track of this foot instance.
            //This function can also determine foot events eg. peaks, foot-down etc.
            double nearestPosition = 9999;
            double relativePosition = 0;
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

                /*if ((xDirection != xPrevDirection) && (xFootPeakDetected != null) && (xDirection != 0))
                {
                    xFootPeakDetected(this);
                }
                if ((yDirection != yPrevDirection) && (yFootPeakDetected != null) && (yDirection != 0))
                {
                    yFootPeakDetected(this);
                }*/
                //if ((zDirection != zPrevDirection) && (zFootPeakDetected != null) && (zDirection != 0))
                //{
                //    zFootPeakDetected(this);
                //}

                if ((zDirection == 1))// && (zDirection != zPrevDirection) && (zDirection != 0))// && (zChange > 2))
                {
                    footDownEventIsFired = false;
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
            set
            {
                calibrationMarker = value;
            }
        }

    }
}

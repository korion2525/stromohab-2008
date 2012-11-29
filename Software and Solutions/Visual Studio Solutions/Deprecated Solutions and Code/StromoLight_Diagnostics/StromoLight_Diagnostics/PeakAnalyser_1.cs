using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using Stromohab_MCE;
using Stromohab_MCE_Connection;
using System.IO;
using ZedGraph;

namespace GaitAnalysis
{
    /// <summary>
    /// Peak analyser class - detects turning points in markers' z-coordinate
    /// </summary>
    public class PeakAnalyser
    {
        /// <summary>
        /// Delegate for peak event handler
        /// </summary>
        /// <param name="marker"></param>
        /// <param name="direction"></param>
        public delegate void PeakEventHandler(Marker marker, int direction);
        /// <summary>
        /// Handler for detected peak event
        /// </summary>
        public static event PeakEventHandler PeakDetected;
        private int[] Direction = new int[6];
        private int[] prevTp = new int[6];
        private int sessionStart = 1;
        private int min_stride_length = 20;
        private bool enabled = false;
        /// <summary>
        /// Constructor
        /// </summary>
        public PeakAnalyser()
        {
            TCPProcessor.WholeFrameReceivedEvent +=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            if (enabled == true)
            {
                if (MarkerList.listOfMarkers.Count == 2)
                {
                    foreach (Marker singleMarker in MarkerList.listOfMarkers)
                    {
                        int current = (int)(singleMarker.zCoordinate + 0.5);
                        int previous = (int)(singleMarker.prevZCoordinate + 0.5);
                        int currentDirection = current - previous;

                        if (sessionStart == 1)
                        {
                            //initialise at zero for 1st frame of markers
                            //ie. no meaningful direction available with one set of positions
                            Direction[singleMarker.MarkerId] = 0;
                            prevTp[singleMarker.MarkerId] = 9999;
                        }
                        else
                        {
                            //test if change of gradient (direction) in zCoordinate
                            if ((Math.Sign(currentDirection) != Direction[singleMarker.MarkerId])
                                && (Math.Sign(currentDirection) != 0)
                                && (Math.Abs(current - prevTp[singleMarker.MarkerId]) > min_stride_length))
                            {
                                //change of Direction indicates turning point
                                Direction[singleMarker.MarkerId] = Math.Sign(currentDirection);
                                prevTp[singleMarker.MarkerId] = current;
                                if (PeakDetected != null)//ie. check that the event is being suscribed to
                                {
                                    Console.WriteLine("Peak in " + singleMarker.MarkerId.ToString() + "\t" + Direction[singleMarker.MarkerId].ToString() + "\t" + singleMarker.TimeStamp.ToString());
                                    PeakDetected(singleMarker, Direction[singleMarker.MarkerId]);
                                }
                            }
                        }
                    }
                    sessionStart = 0;
                }
            }
        }

        
    


        /// <summary>
        /// gets/sets the limit for minimum likely stride length, to help eliminate false positives
        /// </summary>
        public int Min_Stride_Length
        {
            get
            {
                return min_stride_length;
            }
            set
            {
                min_stride_length = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }
    }
}

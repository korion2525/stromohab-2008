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

namespace Stromohab_Diagnostics
{
    /// <summary>
    /// Peak analyser class - detects turning points in markers' z-coordinate
    /// </summary>
    public class PeakAnalyser
    {
        /// <summary>
        /// Delegate for peak event handler
        /// </summary>
        /// <param name="foot"></param>
        /// <param name="marker"></param>
        /// <param name="direction"></param>
        public delegate void PeakEventHandler(string foot, int direction, Marker marker);
        /// <summary>
        /// Handler for detected peak event
        /// </summary>
        public static event PeakEventHandler FootPeakDetected;
        //public static event PeakEventHandler RightPeakDetected;
        private int directionLeft = 0;
        private int directionRight = 0;
        private int previousLeft_TP = 9999;
        private int previousRight_TP = 9999;
        private int min_stride_length = 50;
        private int previousLeft = 0;
        private int previousRight = 0;
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
                Marker leftFootMarker = null;
                Marker rightFootMarker = null;
                leftFootMarker = Marker_Bin.LeftFoot;
                rightFootMarker = Marker_Bin.RightFoot;

                if ((leftFootMarker != null) && (rightFootMarker != null))
                {
                    int currentLeft = (int)(leftFootMarker.zCoordinate + (0.5 * Math.Sign(leftFootMarker.zCoordinate)));
                    int currentLeftDirection = Math.Sign(currentLeft - previousLeft);
                    previousLeft = currentLeft;//keep previous z for <this> <foot> not the marker!

                    int currentRight = (int)(rightFootMarker.zCoordinate + (0.5 * Math.Sign(rightFootMarker.zCoordinate)));
                    int currentRightDirection = Math.Sign(currentRight - previousRight);
                    previousRight = currentRight;//keep previous z for <foot> not the marker!

                    //test if change of gradient (direction) in left zCoordinate
                    if ((currentLeftDirection != directionLeft)
                        && (currentLeftDirection != 0)
                        && (Math.Abs(currentLeft - previousLeft_TP) > min_stride_length))
                    {
                        //change of Direction indicates turning point
                        directionLeft = Math.Sign(currentLeftDirection);
                        previousLeft_TP = currentLeft;
                        if (FootPeakDetected != null)//ie. check that the event is being suscribed to
                        {
                            FootPeakDetected("left", directionLeft, leftFootMarker);
                        }
                    }
                    if ((currentRightDirection != directionRight)
                        && (currentRightDirection != 0)
                        && (Math.Abs(currentRight - previousRight_TP) > min_stride_length))
                    {
                        //change of Direction indicates turning point
                        directionRight = Math.Sign(currentRightDirection);
                        previousRight_TP = currentRight;
                        if (FootPeakDetected != null)//ie. check that the event is being suscribed to
                        {
                            FootPeakDetected("right", directionRight, rightFootMarker);
                        }
                    }
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
        /// <summary>
        /// Enables the peak analyser
        /// </summary>
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

using System;
using System.Collections.Generic;
using System.Text;
using Stromohab_MCE_Connection;

namespace Stromohab_MCE
{
    /// <summary>
    /// Provides static properties to assign markers to the left and right feet
    /// </summary>
    class Marker_Bin // does this class need more intelligence checking?? to make sure that there are left and right feet markers etc 
    {
        private static Marker leftFoot;
        private static Marker rightFoot;
        /// <summary>
        /// Assigns the left foot marker 
        /// </summary>
        public static Marker LeftFoot
        {
            get
            {
                double leftmostX = 1000;
                if (MarkerList.listOfMarkers.Count == 2)
                {
                    foreach (Marker singleMarker in MarkerList.listOfMarkers)
                    {
                        if (singleMarker.xCoordinate < leftmostX)
                        {
                            leftFoot = singleMarker;
                            leftmostX = singleMarker.xCoordinate;
                        }
                    }
                    if ((leftFoot.xCoordinate < -330) || (leftFoot.xCoordinate > 330))
                    {
                        leftFoot = null;
                    } 
                }
                else
                {
                    leftFoot = null;
                }
                return leftFoot;
            }
        }
        /// <summary>
        /// Assigns the right foot marker
        /// </summary>
        public static Marker RightFoot
        {
            get
            {
                double rightmostX = -1000;
                if (MarkerList.listOfMarkers.Count == 2)
                {
                    foreach (Marker singleMarker in MarkerList.listOfMarkers)
                    {
                        if (singleMarker.xCoordinate > rightmostX)
                        {
                            rightFoot = singleMarker;
                            rightmostX = singleMarker.xCoordinate;
                        }
                    }
                    if ((rightFoot.xCoordinate < -330) || (rightFoot.xCoordinate > 330))
                    {
                        rightFoot = null;
                    }
                }
                else
                {
                    rightFoot = null;
                }
                return rightFoot;
            }
        }
    }
}

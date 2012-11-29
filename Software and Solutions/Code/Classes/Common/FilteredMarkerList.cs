using System;
using System.Collections.Generic;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Contains the details of all current Filtered Markers reported by the motion capture system.
    /// </summary>
    public class FilteredMarkerList
    {
        #region Member Variables
        /// <summary>
        /// List of all markers containing latest data.
        /// </summary>
        public static List<Marker> listOfMarkers = new List<Marker>();


        #endregion Member Variables

        #region Methods

        /// <summary>
        /// Returns a marker the list
        /// </summary>
        /// <param name="markerNumber"></param>
        /// <returns></returns>
        public static Marker MarkerFromList(int markerNumber)
        {
            foreach (Marker marker in listOfMarkers)
            {
                if (marker.MarkerId == markerNumber)
                {
                    return (marker);
                }
            }
            return (null);
        }

        #region Marker List Methods
        /// <summary>
        /// Adds a given marker to the marker list
        /// </summary>
        /// <param name="currentMarker"></param>
        public static void AddMarker(Marker currentMarker)
        {
            bool markerFound = false;

            foreach (Marker marker in listOfMarkers)
            {
                if (marker.MarkerId == currentMarker.MarkerId)
                {
                    markerFound = true;
                    if (currentMarker.MarkerId != -1)
                    {
                        UpdateMarkerInList(currentMarker);
                        //return;
                    }
                }
            }

            if (markerFound == false)
            {
                listOfMarkers.Add(new Marker(currentMarker));
            }
        }

        /// <summary>
        /// Removes markers that have been taken out of the capture volume from the list.
        /// </summary>
        /// <param name="currentNumberOfMarkers"></param>
        public static void RemoveExcessMarkersFromList(long currentNumberOfMarkers)
        {
            while (listOfMarkers.Count > currentNumberOfMarkers)
            {
                listOfMarkers.RemoveAt(listOfMarkers.Count - 1);
            }
        }

        private static void UpdateMarkerInList(Marker currentMarker)
        {
            listOfMarkers[currentMarker.MarkerId].TimeStamp = currentMarker.TimeStamp;
            listOfMarkers[currentMarker.MarkerId].xCoordinate = currentMarker.xCoordinate;
            listOfMarkers[currentMarker.MarkerId].yCoordinate = currentMarker.yCoordinate;
            listOfMarkers[currentMarker.MarkerId].zCoordinate = currentMarker.zCoordinate;

        }
        #endregion Marker List Methods
        #endregion Methods
    }
}

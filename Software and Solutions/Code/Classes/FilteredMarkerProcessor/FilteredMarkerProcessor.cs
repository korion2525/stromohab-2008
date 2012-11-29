using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.DataProcessing
{
    /// <summary>
    /// Takes a list of markers and a list of trackables and updates FilteredMarkerList.listOfMarkers with just
    /// the markers that aren't part of the trackables
   /// </summary>
    /// <author>Will Lunniss</author>
    public class FilteredMarkerProcessor
    {
        #region Member Variables
        private static List<Marker> filteredMarkerList = null;
        private static int markerTollerence = 10;
        #endregion Member Variables

        #region Delegates
        /// <summary>
        /// Delegate for the FilteredMarkerListAvaliableEvent
        /// </summary>
        public delegate void FilteredMarkerListAvaliableEventHandler();
        #endregion Delegates

        #region Events
        /// <summary>
        /// FilteredMarkerListAvaliableEvent
        /// </summary>
        public static event FilteredMarkerListAvaliableEventHandler FilteredMarkerListAvaliable;

        private static void OnFilteredMarkerListAvaliable()
        {
            if (FilteredMarkerListAvaliable != null)
                FilteredMarkerListAvaliable();
        }

        #endregion Events

        #region Methods

        #region Methods - Filter Markers

        /// <summary>
        /// Filters out all of the markers that are part of a trackable and leaves just single markers
        /// </summary>
        public static void UpdatedFilteredMarkers(List<Marker> markerList, List<Trackable> trackableList)
        {
            int markerCount = 0;
            filteredMarkerList = new List<Marker>();
            Marker trackableMarker = null;
            if (trackableList.Count != 0) // If there are trackable
            {
                foreach (Marker marker in markerList) // Go through every marker
                {
                    bool singleMarker = true;
                    foreach (Trackable trackable in trackableList) // Go through every trackable
                    {
                        foreach (Marker tempTrackableMarker in trackable.TrackableMarkers) // Go through every marker making up the trackable
                        {
                            trackableMarker = tempTrackableMarker;

                            if ((marker.yCoordinate >= trackableMarker.yCoordinate + trackable.yCoordinate - markerTollerence))
                            {
                                if ((marker.yCoordinate <= trackableMarker.yCoordinate + trackable.yCoordinate + markerTollerence))
                                {
                                    if ((marker.xCoordinate >= trackableMarker.xCoordinate + trackable.xCoordinate - markerTollerence))
                                    {
                                        if ((marker.xCoordinate <= trackableMarker.xCoordinate + trackable.xCoordinate + markerTollerence))
                                        {
                                            if ((marker.zCoordinate >= trackableMarker.zCoordinate + trackable.zCoordinate - markerTollerence))
                                            {
                                                if ((marker.zCoordinate <= trackableMarker.zCoordinate + trackable.zCoordinate + markerTollerence))
                                                    singleMarker = false;
                                                // This marker is part of a trackable and isn't a single marker
                                            }
                                        }
                                    }
                                }
                            }
                            if (!singleMarker)
                                break; // If its been found the break out

                        }
                        if (!singleMarker)
                            break; // If its been found the break out

                    }
                    if (singleMarker) // If its a single marker make a copy and add it to the new list
                    {
                        markerCount++;
                        Marker filteredMarker = new Marker(marker);
                        filteredMarker.MarkerId= markerCount -1;
                        filteredMarkerList.Add(filteredMarker);
                    }
                }
                FilteredMarkerList.listOfMarkers = new List<Marker>(filteredMarkerList);
            }
            else // If there are no trackables then just copy across the list
            {
               FilteredMarkerList.listOfMarkers = new List<Marker>(markerList);
            }
            OnFilteredMarkerListAvaliable();
        }
        #endregion Methods - Filter Markers
        #endregion Methods
    }
}


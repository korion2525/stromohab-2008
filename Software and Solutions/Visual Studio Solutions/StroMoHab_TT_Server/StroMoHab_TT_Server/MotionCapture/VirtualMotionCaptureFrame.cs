using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.MotionCapture
{
    /// <summary>
    /// Stores a single frame of Motion Capture data
    /// </summary>
    /// TODO: Add position to visualiser.
    [Serializable]
    public class VirtualMotionCaptureFrame
    {
        private List<Marker> _markerList;
        private List<Trackable> _trackableList;
        private long _timestamp;
        private float _treadmillSpeed;

        public VirtualMotionCaptureFrame()
        {
            _markerList = null;
            _trackableList = null;
            _timestamp = -1;
            _treadmillSpeed = -1f;

        }
        public VirtualMotionCaptureFrame(List<Marker> markerList, List<Trackable> trackableList, long timestamp, float treadmillSpeed)
        {
            _markerList = new List<Marker>(markerList);
            _trackableList = new List<Trackable>(trackableList);
            _timestamp = timestamp;
            _treadmillSpeed = treadmillSpeed;

        }

        /// <summary>
        /// The Marker Data
        /// </summary>
        public List<Marker> MarkerList
        {
            get { return _markerList; }
            set { _markerList = value; }
        }

        /// <summary>
        /// The Trackable Data
        /// </summary>
        public List<Trackable> TrackableList
        {
            get { return _trackableList; }
            set { _trackableList = value; }
        }

        /// <summary>
        /// The Time Stamp
        /// </summary>
        public long TimeStamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        /// <summary>
        /// The Treadmill Speed
        /// </summary>
        public float TreadmillSpeed
        {
            get { return _treadmillSpeed; }
            set { _treadmillSpeed = value; }
        }
    }
}

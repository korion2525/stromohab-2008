using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.MotionCapture
{
    /// <summary>
    /// Stores a motion capture sub session - the data collected between the treadmill starting and stopping
    /// </summary>
    [Serializable]
    public class VirtualMotionCaptureSubSession
    {
        /// <summary>
        /// The list of frames
        /// </summary>
        private List<VirtualMotionCaptureFrame> _virtualMotionCaptureFrameList;
        /// <summary>
        /// The current playback frame number
        /// </summary>
        private int _currentFrame = 0;
        /// <summary>
        /// The date and time at the start of the session
        /// </summary>
        private DateTime _sessionStartTime;

        public VirtualMotionCaptureSubSession()
        {
            _virtualMotionCaptureFrameList = new List<VirtualMotionCaptureFrame>();
            _sessionStartTime = DateTime.Now;
        }

        

        /// <summary>
        /// Adds a frame of data
        /// </summary>
        /// <param name="frame"></param>
        public void AddFrame(VirtualMotionCaptureFrame frame)
        {
            _virtualMotionCaptureFrameList.Add(frame);
        }

        /// <summary>
        /// Returns the next frame of data
        /// </summary>
        /// <returns></returns>
        public VirtualMotionCaptureFrame GetNextFrame()
        {
            if (_currentFrame > _virtualMotionCaptureFrameList.Count -1)
                return null;

            _currentFrame += 1;
            return _virtualMotionCaptureFrameList[_currentFrame -1];
        }

        /// <summary>
        /// Starts playback from the begining
        /// </summary>
        public void ResetSubSession()
        {
            _currentFrame = 0;
        }
        public List<VirtualMotionCaptureFrame> Frames
        {
            get { return _virtualMotionCaptureFrameList; }
        }

        /// <summary>
        /// Gets the number of frames
        /// </summary>
        /// <returns></returns>
        public int SessionSize()
        {
            return _virtualMotionCaptureFrameList.Count;
        }
    }
}

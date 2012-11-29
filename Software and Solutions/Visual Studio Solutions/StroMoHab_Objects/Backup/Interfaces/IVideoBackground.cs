using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StroMoHab_Objects.Interfaces
{
    /// <summary>
    /// Defines public Video requirements.
    /// </summary>
    public interface IVideoBackground:IDisposable
    {
        /// <summary>
        /// Tbe path to the folder containing the video frame files.
        /// </summary>
        string ContainingFolder { get; set; }
        /// <summary>
        /// The current frame image data.
        /// </summary>
        Bitmap CurrentFrame { get; }
        /// <summary>
        /// The time interval between frames.
        /// </summary>
        double TransitionInterval { get; set; }
        /// <summary>
        /// Starts updating the current frame.
        /// </summary>
        
        ///<summary>
        ///Enables moving to the next video frame.
        ///</summary>
        bool MoveToNextFrame { set; }

        void Start();
        /// <summary>
        /// Pauses updating of the current frame.
        /// </summary>
        /// <param name="PauseLength">the interval to pause for in ms.</param>
        void Pause(int PauseLength);
        /// <summary>
        /// Stops updating of the current frame and resets the current frame back to the first frame. 
        /// </summary>
        void Stop();
    }
}

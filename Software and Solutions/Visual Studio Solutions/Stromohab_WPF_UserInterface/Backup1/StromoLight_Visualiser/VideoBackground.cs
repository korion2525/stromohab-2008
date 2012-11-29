using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading;

using StroMoHab_Objects;
using StroMoHab_Objects.Graphics;
using StroMoHab_Objects.Interfaces;

namespace StromoLight_Visualiser
{
    /// <summary>
    /// Manages the loading and buffering of image data from disk into threadsafe memory.
    /// </summary>
    [Serializable]
    public class VideoBackground : IDisposable, IVideoBackground
    {
        private string _containingFolder = null;
        private List<string> _listOfVideoFilePaths = null;
        Thread _loadFramesThread;
        private const int BUFFERING_WAIT_TIME = 7000;

        private ThreadsafeQueue<Bitmap> _frameQueue = new ThreadsafeQueue<Bitmap>(1000);

        //Background Video variables.

        private bool _needToBuffer = false; //indicates a need to pause moving to new frames while the VideoTextureManager populates the list of frames.
        private bool _bufferTimerActive = false; //indicates buffering is currently taking place.
        private Bitmap _frameHolder1 = null; //holds image data for a frame.
        private Bitmap _frameHolder2 = null; //holds image data for a frame.
        private System.Timers.Timer _frameIntervalTimer = null; //timer controlling the frameInterval.
        private System.Threading.Timer bufferTimer; //timer controlling the length of time to wait while the queue buffers data.
        private bool _moveToNextFrame = false;       

        
        private void CancelNeedToBuffer(object state)
        {
            _needToBuffer = false;
            _bufferTimerActive = false;
        }

        private List<string> ExtractAllFilepathsFromFolder(string containingFolder)
        {
            List<String> allFilePathsFromFolder = new List<string>(Directory.GetFiles(containingFolder, "*.jpeg"));

            if (allFilePathsFromFolder.Count == 0)
            {
                allFilePathsFromFolder = new List<string>(Directory.GetFiles(containingFolder, "*.jpg"));
            }
            return allFilePathsFromFolder;
        }

        /// <summary>
        /// Initialises an instance of StromoLight_Visualiser.VideoTextureManager.
        /// </summary>
        /// <param name="containingFolder">path to folder containing video frame files.</param>
        public VideoBackground(string containingFolder)
        {
            Initialise(containingFolder);
        }

        private void Initialise(string containingFolder)
        {
            if (containingFolder != null)
            {
                _containingFolder = containingFolder;
                _listOfVideoFilePaths = ExtractAllFilepathsFromFolder(_containingFolder);


                _loadFramesThread = new Thread(new ParameterizedThreadStart(LoadAllBitmapsFromDisk));
                _loadFramesThread.Name = "Load Frames Thread";
                _loadFramesThread.Start((object)(_listOfVideoFilePaths));

                //set up the video frame display 
                _frameIntervalTimer = new System.Timers.Timer(33); //default value 33ms: == 30 frames/second
                _frameIntervalTimer.Elapsed += new System.Timers.ElapsedEventHandler(frameIntervalTimer_Elapsed);

                //pause while initally buffer so can display first frame.
                _moveToNextFrame = true;
                while (CurrentFrame == null) ;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public bool MoveToNextFrame
        {
            set
            {
                _moveToNextFrame = value;
            }
        }

        /// <summary>
        /// Triggered by timer indicating next video frame should be displayed.
        /// </summary>
        private void frameIntervalTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _moveToNextFrame = true;
        }


        private volatile bool _stopLoadingBitmapsFromDisk=false;

        /// <summary>
        /// Starts a thread and streams image data into a ThreadsafeQueue.
        /// </summary>
        /// <param name="filePathlist">list of filepath strings</param>
        public void LoadAllBitmapsFromDisk(object filePathlist)
        {
            List<string> listOfFrameTexturePaths = new List<string>((List<string>)(filePathlist));

            while (_stopLoadingBitmapsFromDisk == false)
            {
                //Load bitmap from disk and update queue.
                foreach (string currentFilePath in listOfFrameTexturePaths)
                {
                    if (_stopLoadingBitmapsFromDisk)
                    {
                        break;
                    }
                    
                    Bitmap currentBitmap = new Bitmap(currentFilePath);

                    //keep trying to write to queue until succeed
                    bool writeToFrameQueueSucceeded = false;
                    do
                    {
                        writeToFrameQueueSucceeded = _frameQueue.Enqueue(currentBitmap);
                    }
                    while (writeToFrameQueueSucceeded == false);
                    GC.Collect(20, GCCollectionMode.Forced);
                }
            }
        }

        private void RequestStopLoadingBitmapsFromDisk()
        {
            _stopLoadingBitmapsFromDisk = true;
        }

        #region IDisposable Members

        /// <summary>
        /// Stops any loading operations and allows the thread to exit.
        /// </summary>
        public void Dispose()
        {
            if (_loadFramesThread != null)
            {
                RequestStopLoadingBitmapsFromDisk();
            }
        }

        #endregion

        #region IVideoBackground Members

        /// <summary>
        /// Tbe path to the folder containing the video frame files.
        /// </summary>
        public string ContainingFolder 
        { 
            get
            {
                return _containingFolder;
            }
            set
            {
                //this.RequestStopLoadingBitmapsFromDisk();
                //while (_loadFramesThread.IsAlive) ;

                //Initialise(value);
                
            }
        }

        /// <summary>
        /// The current frame image data.
        /// </summary>
        public Bitmap CurrentFrame
        {
            get
            {
                if (_needToBuffer == false)
                {
                    if (_frameHolder1 == null && _bufferTimerActive == false)
                    {
                        //get next video background
                        _frameHolder1 = _frameQueue.Dequeue();
                    }
                    if (_moveToNextFrame) //if timer has elapsed
                    {
                        if (_frameHolder1 != null)
                        {
                            _frameHolder2 = new Bitmap(_frameHolder1); //frameHolder 2 = frameHolder 1
                            _frameHolder1 = null; //clear frameHolder1
                            _moveToNextFrame = false;
                        }
                        else
                        {
                            _needToBuffer = true;
                        }
                    }
                }
                else
                {
                    if (_bufferTimerActive == false)
                    {
                        _bufferTimerActive = true;
                        System.Threading.TimerCallback tcb = new System.Threading.TimerCallback(CancelNeedToBuffer);
                        bufferTimer = new System.Threading.Timer(tcb, null, BUFFERING_WAIT_TIME, System.Threading.Timeout.Infinite);
                    }
                }
                return (_frameHolder2);
            }
        }
        /// <summary>
        /// The time interval between frames.
        /// </summary>
        public double TransitionInterval
        {
            get
            {
                return (_frameIntervalTimer.Interval);
            }
            set
            {
                if (value > 0 && value < Int32.MaxValue)
                {
                    _frameIntervalTimer.Interval = value;
                }
                else
                {
                    this.Stop();
                }
            }
        }
        /// <summary>
        /// Starts updating the current frame.
        /// </summary>
        public void Start()
        {
            if (_frameIntervalTimer.Enabled == false)
            {
                _frameIntervalTimer.Start();
            }
        }
        /// <summary>
        /// Pauses updating of the current frame.
        /// </summary>
        /// <param name="PauseLength">the interval to pause for in ms.</param>
        public void Pause(int PauseLength)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Stops updating of the current frame and resets the current frame back to the first frame. 
        /// </summary>
        public void Stop()
        {
            _frameIntervalTimer.Stop();
        }

        #endregion
    }
}

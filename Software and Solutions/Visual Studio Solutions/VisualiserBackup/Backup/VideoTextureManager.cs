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

namespace StromoLight_Visualiser
{
    /// <summary>
    /// Manages the loading and buffering of image data from disk into threadsafe memory.
    /// </summary>
    public class VideoTextureManager : IDisposable
    {
        private List<string> m_listOfTextureFilepaths = null;
        Thread _loadFramesThread;

        private ThreadsafeQueue<Bitmap> _frameQueue = new ThreadsafeQueue<Bitmap>(500);

        /// <summary>
        /// Queue containing the video frame data.
        /// </summary>
        public ThreadsafeQueue<Bitmap> FrameQueue
        {
            get { return _frameQueue; }
        }

        /// <summary>
        /// Initialises an instance of StromoLight_Visualiser.VideoTextureManager.
        /// </summary>
        /// <param name="listOfTextureFilePaths"></param>
        public VideoTextureManager(List<string> listOfTextureFilePaths)
        {
            if (listOfTextureFilePaths != null)
            {
                m_listOfTextureFilepaths = new List<string>(listOfTextureFilePaths);
            }

            _loadFramesThread = new Thread(new ParameterizedThreadStart(LoadAllBitmapsFromDisk));
            _loadFramesThread.Name = "Load Frames Thread";
            _loadFramesThread.Start((object)m_listOfTextureFilepaths);
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
    }
}

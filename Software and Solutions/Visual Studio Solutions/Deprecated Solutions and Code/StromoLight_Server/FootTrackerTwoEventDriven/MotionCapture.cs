using System;

namespace Stromohab_MCE
{
    class MotionCapture
    {
        #region Member Variables
        public static PointCloud.NPPointCloud pointcloud = new PointCloud.NPPointCloud();

        static bool m_endOfFrameSent = false;
        #endregion Member Variables

        #region Delegate Declarations
        public delegate void CoordinatesAvailableHandler(Marker marker);
        #endregion Delegate Declarations

        #region Event Declarations
            public static event CoordinatesAvailableHandler CoordinatesAvailableEvent;
        #endregion Event Declarations

        /// <summary>
        /// Loads the calibration file defined in ProfilePath
        /// </summary>
        /// <param name="ProfilePath"></param>
        public static void LoadProfile(string ProfilePath)
        {
            pointcloud.LoadProfile(ProfilePath);
        }

        /// <summary>
        /// Starts the cameras
        /// </summary>
        public static void StartCameras()
        {
            pointcloud.Start();
        }

        /// <summary>
        /// Stops the cameras
        /// </summary>
        public static void StopCameras()
        {
            pointcloud.Stop();
        }

        public static void UpdateCameraList()
        {
            int numberOfCameras = pointcloud.CameraCount;
            OptiTrackCamera camera = new OptiTrackCamera();
            double[] tempRotationMatrix = new double[9];

            for (int i = 0; i < numberOfCameras; i++)
            {
                camera.CameraNumber = i;
                camera.xCoordinate = pointcloud.GetCamera(i).X * 1000;
                camera.yCoordinate = pointcloud.GetCamera(i).Y * 1000;
                camera.zCoordinate = pointcloud.GetCamera(i).Z * 1000;

                for (int j = 0; j < 9; j++)
                {
                    tempRotationMatrix[j] = pointcloud.GetCamera(i).GetRotationMatrix(j);
                }
                camera.RotationMatrix = tempRotationMatrix;
                OptitrackCameraList.AddCamera(camera);

            }

            
        }

        /// <summary>
        /// Updates the list of markers with lastest marker coordinates
        /// </summary>
        public static void UpdateCoordinates()
        {
           
            PointCloud.NPPointCloudFrame frame = new PointCloud.NPPointCloudFrame();
                        
            pointcloud.GetFrame(out frame);

            Marker marker = new Marker();

            while (frame != null)
            {
                for (int i = 0; i < frame.Count; i++)
                {
                    marker.MarkerId = i;
                    marker.TimeStamp = Environment.TickCount;
                    marker.xCoordinate = frame.Item(i).X * 1000;
                    marker.yCoordinate = frame.Item(i).Y * 1000;
                    marker.zCoordinate = frame.Item(i).Z * 1000;                    

                    //MarkerList.RefreshMarkerList(marker, frame.Count);

                    if (CoordinatesAvailableEvent != null)
                    {
                        CoordinatesAvailableEvent(marker);
                        m_endOfFrameSent = false; ;
                    }
                    
                }
                //Send number of markers with marker data
                marker.TimeStamp = frame.Count;
                marker.MarkerId = -2147483648;

                if (CoordinatesAvailableEvent != null && m_endOfFrameSent == false)
                {
                    CoordinatesAvailableEvent(marker);
                    m_endOfFrameSent = true;
                }


                pointcloud.GetFrame(out frame);
            }

            
        }

    }
}


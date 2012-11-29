using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Contains all of the currently connected Motion Capture Cameras
    /// </summary>
    public class OptitrackCameraList
    {
        #region Delegate Declarations
        /// <summary>
        /// Handles transmitting a camera
        /// </summary>
        /// <param name="camera">The motion capture camera</param>
        public delegate void TransmitCameraHandler(OptiTrackCamera camera);
        #endregion Delegate Declarations
        #region Event Declarations
        /// <summary>
        /// Transmit camera event
        /// </summary>
        public static event TransmitCameraHandler TransmitCameraEvent;
        #endregion Event Declarations

        /// <summary>
        /// Contains the list of cameras
        /// </summary>
        public static List<OptiTrackCamera> listOfCameras = new List<OptiTrackCamera>();

        /// <summary>
        /// Adds a camera
        /// </summary>
        /// <param name="camera">The camera to add</param>
        public static void AddCamera(OptiTrackCamera camera)
        {
            bool cameraFound = false;

            foreach (OptiTrackCamera currentCamera in listOfCameras)
            {
                if (camera.CameraNumber == currentCamera.CameraNumber)
                {
                    cameraFound = true;
                    if (currentCamera.CameraNumber != -1)
                    {
                        UpdateCameraInList(currentCamera);
                    }
                }


            }

            if (cameraFound == false)
            {
                listOfCameras.Add(new OptiTrackCamera(camera));
            }
        }


        /// <summary>
        /// Updates a camera in the list
        /// </summary>
        /// <param name="camera">The camera</param>
        private static void UpdateCameraInList(OptiTrackCamera camera)
        {

            listOfCameras[camera.CameraNumber].CameraNumber = camera.CameraNumber;
            listOfCameras[camera.CameraNumber].xCoordinate = camera.xCoordinate;
            listOfCameras[camera.CameraNumber].yCoordinate = camera.yCoordinate;
            listOfCameras[camera.CameraNumber].zCoordinate = camera.zCoordinate;

            for (int i = 0; i < 9; i++)
            {
                listOfCameras[camera.CameraNumber].RotationMatrix[i] = camera.RotationMatrix[i];
            }

        }

        /// <summary>
        /// Transmits the list of cameras
        /// </summary>
        public static void TransmitListOfCameras()
        {
            if (TransmitCameraEvent != null)
            {

                foreach (OptiTrackCamera camera in listOfCameras)
                {
                    TransmitCameraEvent(camera);
                }

            }
        }

        /// <summary>
        /// Returns a camera from the list
        /// </summary>
        /// <param name="cameraNumber"></param>
        /// <returns></returns>
        public static OptiTrackCamera CameraFromList(int cameraNumber)
        {
            foreach (OptiTrackCamera camera in listOfCameras)
            {
                if (camera.CameraNumber == cameraNumber)
                {
                    return (camera);
                }
            }
            return (null);
        }
    }
}

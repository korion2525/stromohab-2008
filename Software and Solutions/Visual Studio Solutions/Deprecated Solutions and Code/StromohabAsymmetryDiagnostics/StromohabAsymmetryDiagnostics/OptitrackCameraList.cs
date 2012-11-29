using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stromohab_MCE
{
    class OptitrackCameraList
    {
        #region Delegate Declarations
            public delegate void TransmitCameraHandler(OptiTrackCamera camera);
        #endregion Delegate Declarations
        #region Event Declarations
            public static event TransmitCameraHandler TransmitCameraEvent;
        #endregion Event Declarations

        
        public static List<OptiTrackCamera> listOfCameras = new List<OptiTrackCamera>();

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

        

        public static void UpdateCameraInList(OptiTrackCamera camera)
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

        public static void TransmitListOfCameras()
        {   
            if (TransmitCameraEvent != null)
            {
                //TCPServer.StopTransmittingMarkers();

                foreach (OptiTrackCamera camera in listOfCameras)
                {
                    TransmitCameraEvent(camera);
                }

               // TCPServer.StartTransmittingMarkers();
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

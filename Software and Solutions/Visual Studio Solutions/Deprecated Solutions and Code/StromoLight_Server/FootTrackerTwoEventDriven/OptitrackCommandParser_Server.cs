using System;

namespace Stromohab_MCE
{

    /// <summary>
    /// 
    /// </summary>
    class OptitrackCommandParser_Server
    {
        #region Delegate Declarations
            /// <summary>
            /// Handles event triggered when cameras are started.
            /// </summary>
            public delegate void CamerasStartedHandler();     
        #endregion Delegate Declarations

        #region Event Declarations
            /// <summary>
            /// Triggered when cameras are started
            /// </summary>
            public static event CamerasStartedHandler camerasStartedEvent;
        #endregion Event Declarations

            /// <summary>
            /// Converts byte to command and then executes command
            /// </summary>
            /// <param name="receivedCommand"></param>
            public void handleCommand(byte[] receivedCommand)
            {
            int command = (int)receivedCommand[2];

            switch (command)
            {
                    // 0 is Start Cameras
                case 0:
                    {
                        MotionCapture.StartCameras();
                        camerasStartedEvent();
                        break;
                    }
                    // 1 is Stop Cameras
                case 1:
                    {
                        MotionCapture.StopCameras();
                        break;
                    }
                    // 2 is Send Camera Coordinates
                case 2:
                    {
                        MotionCapture.UpdateCameraList();
                        OptitrackCameraList.TransmitListOfCameras();
                        break;
                    }
                    // 3 is Start Treadmill At Fixed Speed
                case 3:
                    { 
                        TreadmillController.SetSpeed((float)BitConverter.ToDouble(receivedCommand,4));
                        break;
                    }
                    // 4 is Stop Treadmill
                case 4:
                    {
                        TreadmillController.SetSpeed(0.0f);
                        break;
                    }
                    //8 is toggle feet on/off
                case 8:
                    {
                        break;
                    }

                default:
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid command received: " + command.ToString());
                        break;
                    }
            }
            

        }
    
    }
}

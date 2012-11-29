using System;
using System.Collections.Generic;

using System.Net.Sockets;

namespace StroMoHab_Objects.Communication
{
    /// <summary>
    /// Sends motion capture commands to the server
    /// </summary>
    public class SendMCECommand
    {
        /// <summary>
        /// Start the cameras and begin data streaming
        /// </summary>
        public static void StartCameras()
        {
            TransmitCommand(0);
        }

        /// <summary>
        /// Stop the cameras and end data streaming
        /// </summary>
        public static void StopCameras()
        {
            TransmitCommand(1);
        }

        /// <summary>
        /// Refresh camera list
        /// </summary>
        public static void GetOptiTrackCameraCoordinates()
        {
            TransmitCommand(2);
        }

        /// <summary>
        /// Starts the treadmill at the given speed.
        /// </summary>
        /// <param name="speed"></param>
        public static void SetTreadmillSpeed(float speed)
        {
            TransmitCommand(3,speed);
        }

        /// <summary>
        /// Stops the treadmill
        /// </summary>
        public static void StopTreadmill()
        {
            TransmitCommand(4);
        }

        /// <summary>
        /// Stores calibration data at the server.
        /// </summary>
        public static void Calibrate()
        {
            TransmitCommand(7);
        }

        /// <summary>
        /// Toggles the feet on and off in the visualiser.
        /// </summary>
        public static void ToggleDrawingFeet(bool display)
        {
            TransmitCommand(8,display);
        }

        /// <summary>
        /// Gets the server to send out the speed of the treadmill
        /// </summary>
        public static void ForceTreadmillSpeedUpdate()
        {
            TransmitCommand(11);
        }


        /// <summary>
        /// Sends a command packet to the server. Used for single byte commands.
        /// </summary>
        /// <param name="commandNumber"></param>
        private static void TransmitCommand(int commandNumber)
        {
            //create the command byte that will be sent.
            byte[] commandByte = new byte[4];

            //create a tempoary byte (used to fill the main command byte).
            byte[] tempArray = new byte[2];

            //convert the packet size (4) to bytes. Type UInt16 is 2 bytes long.
            tempArray = BitConverter.GetBytes((UInt16)4);

            //copy the packet size into the first 2 bytes of the main command byte array.
            for (int i = 0; i < 2; i++)
            {
                commandByte[i] = tempArray[i];
            }

            //convert the command number to bytes.
            tempArray = BitConverter.GetBytes((UInt16)commandNumber);

            //copy the command number into bytes 3 and 4 of the main command byte array.
            for (int i = 0; i < 2; i++)
            {
                commandByte[i + 2] = tempArray[i];
            }

            //if the client is connected.
            if (TCPProcessor.Connection != null)
            {
                if (TCPProcessor.Connection.Connected == true)
                {
                    //try to write the main command byte to the network stream.
                    try
                    {
                        NetworkStream clientStream = TCPProcessor.Connection.GetStream();
                        clientStream.Write(commandByte, 0, commandByte.Length);
                        //don't forget to flush the stream.
                        clientStream.Flush();
                    }
                    catch (System.IO.IOException ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Failed to write to the network stream. The connection to the server may not exist. Exception Message: " + ex.Message);
                    }
                }
            }

        }

        /// <summary>
        /// Transmits a multi-byte command (eg. Treadmill speed) to the server.
        /// </summary>
        /// <param name="commandNumber"></param>
        /// <param name="data"></param>
        private static void TransmitCommand(UInt16 commandNumber, float data)
        {
            //create the byte array to send
            byte[] commandByte = new byte[12];

            //create a temporary array (used to fill sections of main command array).
            byte[] tempArray = new byte[2];

            //packet size is 12. Type UInt16 is 2 bytes long.
            tempArray = BitConverter.GetBytes((UInt16)12);

            //copy temp array into main command byte array. First two bytes of command array contain the packet size.
            for (int i = 0; i < 2; i++)
            {
                commandByte[i] = tempArray[i];
            }

            //convert command number into bytes.
            tempArray = BitConverter.GetBytes(commandNumber);

            //and copy into bytes 3 and 4 of the main command byte array.
            for (int i = 0; i < 2; i++)
            {
                commandByte[i + 2] = tempArray[i];
            }

            //create a temporary container for the speed.
            tempArray = new byte[8];

            //convert the speed to bytes.
            tempArray = BitConverter.GetBytes((double)data);

            //copy the speed into the main command array.
            for (int i = 0; i < 8; i++)
            {
                commandByte[i + 4] = tempArray[i];
            }

            //if the connection exists at all
            if (TCPProcessor.Connection != null)
            {
                //if the client is connected to the server.
                if (TCPProcessor.Connection.Connected == true)
                {
                    //try to write the command byte to the network stream.
                    try
                    {
                        NetworkStream clientStream = TCPProcessor.Connection.GetStream();

                        clientStream.Write(commandByte, 0, commandByte.Length);
                        //always remember to flush..
                        clientStream.Flush();
                    }
                    catch (System.IO.IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Failed to write to the network stream. The connection to the server may not exist. Exception Message: " + ex.Message);
                    }
                }
            }
        }

        private static void TransmitCommand(UInt16 commandNumber, bool data)
        {
            //create the byte array to send
            byte[] commandByte = new byte[5];

            //create a temporary array (used to fill sections of main command array).
            byte[] tempArray = new byte[2];

            //packet size is 12. Type UInt16 is 2 bytes long.
            tempArray = BitConverter.GetBytes((UInt16)5);

            //copy temp array into main command byte array. First two bytes of command array contain the packet size.
            for (int i = 0; i < 2; i++)
            {
                commandByte[i] = tempArray[i];
            }

            //convert command number into bytes.
            tempArray = BitConverter.GetBytes(commandNumber);

            //and copy into bytes 3 and 4 of the main command byte array.
            for (int i = 0; i < 2; i++)
            {
                commandByte[i + 2] = tempArray[i];
            }

            //create a temporary container for the data.
            tempArray = new byte[1];

            //convert the bool to bytes.
            tempArray = BitConverter.GetBytes(data);

            //copy the data into the main command array.
                commandByte[4] = tempArray[0];

            //if the connection exists at all
            if (TCPProcessor.Connection != null)
            {
                //if the client is connected to the server.
                if (TCPProcessor.Connection.Connected == true)
                {
                    //try to write the command byte to the network stream.
                    try
                    {
                        NetworkStream clientStream = TCPProcessor.Connection.GetStream();

                        clientStream.Write(commandByte, 0, commandByte.Length);
                        //always remember to flush..
                        clientStream.Flush();
                    }
                    catch (System.IO.IOException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Failed to write to the network stream. The connection to the server may not exist. Exception Message: " + ex.Message);
                    }
                }
            }
        }


    }
}

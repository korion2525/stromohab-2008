using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;

namespace StroMoHab_TT_Server.Treadmill
{
    /// <summary>
    /// Woodway treadmill controller class
    /// </summary>
    class Treadmill
    {
        private float speed;
        private float velocity;
        private float elevation;
        private bool reverse = false;
        private volatile bool _stop = false;
        List<byte> commandList = new List<byte>();//a queue for commands
        SerialPort serialPort1;
        Thread timerThread;
        private float MAX_SPEED;
        private float MAX_ELEVATION;
        
        public Treadmill(string portName)
        {
            InitialiseTreadmill(portName);
            try
            {
                _stop = false;
                timerThread = new Thread(new ThreadStart(sendCommand));
            }
            catch
            {
                MessageBox.Show("Problem starting timed command thread.", "",
                    MessageBoxButtons.OK);
            }            
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ToString());
        }

        void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Exception);
        }

        #region TreadmillPortUnplugged
        public delegate void TreadmillPortUnpluggedEventHandler(); 

        /// <summary>
        /// When SafeWrite() detects a problem with the serial port
        /// </summary>
        public event TreadmillPortUnpluggedEventHandler TreadmillPortUnpluggedEvent;
        #endregion TreadmillPortUnplugged

        #region Treadmill methods

        /// <summary>
        /// Method starts the treadmill at a speed and direction determined by
        /// the Speed and Reverse (velocity) properties.
        /// </summary>
        public void Start()
        {
            //commandList provides a buffer (queue) for commands
            commandList.Add(160);
            if (timerThread.IsAlive == false)
            {
                timerThread.Start();
            }
        }

        /// <summary>
        /// Method stops the treadmill and sets the speed property to zero
        /// </summary>
        public void Halt()
        {
            commandList.Insert(0, 169);
        }

        /// <summary>
        /// Method stops the treadmill and sets the speed and elevation to zero
        /// </summary>
        public void Stop()
        {
            //insert 'stop' command at front of command queue
            commandList.Insert(0, 170);
        }

        /// <summary>
        /// Method causes timer thread to stop and closes serial port connection
        /// </summary>
        public void CloseSession()
        {
            commandList.Insert(0, 171);
        }

        /// <summary>
        /// Method disengages the belt into 'freewheel' mode
        /// </summary>
        public void Disengage()
        {
            commandList.Add(162);
        }

        private void InitialiseTreadmill(string portName)
        {
            serialPort1 = new SerialPort(portName);
            serialPort1.BaudRate = 4800;
            serialPort1.ReadTimeout = 500;
            serialPort1.WriteTimeout = 500;
            speed = 0.0F;
            MAX_SPEED = 3.0F;
            MAX_ELEVATION = 15.0F; 
        }

        #endregion

        /// <summary>
        /// Starts separate timed thread for sending commands to the treadmill
        /// via serial port.
        /// </summary>
        private void sendCommand()
        {
            #region Open serial port
            try
            {
                serialPort1.Open();
            }
            catch (IOException)
            {
                MessageBox.Show("IO Exception, invalid port or parameters.", "",
                    MessageBoxButtons.OK);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid Operation Exception. Port already open.", "",
                    MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("Problem opening serial port.", "", MessageBoxButtons.OK);
            }
            #endregion

            byte command;
            while (!_stop)
            {
                command = 163;
                if (commandList.Count > 0 )//if = 0, default action is to send speed
                {
                    command = commandList[0];
                    if (command != 163) Console.WriteLine(command);
                    commandList.RemoveAt(0);
                }
                switch (command)
                {
                    case 160:
                        byte[] start = new byte[1] { 160 };
                        SafeWrite(start, 0, 1);
                        //Console.WriteLine(serialPort1.ReadByte());
                        break;
                    case 162:
                        speed = 0;
                        elevation = 0;
                        byte[] disengage = new byte[1] { 162 };
                        SafeWrite(disengage, 0, 1);
                        //Console.WriteLine(serialPort1.ReadByte());
                        break;
                    case 163:
                        sendSpeed(speed);
                        //Console.WriteLine(serialPort1.ReadByte());
                        break;
                    case 164:
                        sendElevation();
                        //Console.WriteLine(serialPort1.ReadByte());
                        break;
                    case 169://Halt
                        speed = 0;
                        sendSpeed(speed);
                        //Console.WriteLine(serialPort1.ReadByte());
                        break;
                    case 170:
                        speed = 0;
                        byte[] stop = new byte[1] { 170 };
                        SafeWrite(stop, 0, 1);
                        break;
                    case 171:
                        speed = 0;
                        elevation = 0;
                        _stop = true;
                        break;
                    default:
                        sendSpeed(speed);
                        System.Diagnostics.Debug.WriteLine("Some other command");
                        break;
                }
                Thread.Sleep(120);
            }
            try
            {
                serialPort1.Close();
            }
            catch
            {
                MessageBox.Show("Serial port already closed.", "", MessageBoxButtons.OK);
            }
        }

        private void sendSpeed(float speed)
        {
            if ((speed < 0) || (speed > MAX_SPEED))
            {
                string message = "Speed should be 0.0 - " + MAX_SPEED.ToString() + " mph";
                MessageBox.Show(message, "", MessageBoxButtons.OK);
                speed = 0.0F;
            }
            byte[] commandBytes = new byte[5] { 163, 48, 48, 48, 48 };
            commandBytes[3] = (byte)((int)speed + 48);
            commandBytes[4] = (byte)((speed - (int)speed) * 10 + 48);
             
            SafeWrite(commandBytes, 0, 5);
            
        }

        private void sendElevation()
        {
            if ((elevation < 0) || (elevation > MAX_ELEVATION))
            {
                string message = "Elevation should be 0.0 - " + MAX_ELEVATION + " degres";
                MessageBox.Show(message, "", MessageBoxButtons.OK);
                elevation = 0.0F;
            }
            float converter = elevation;
            byte[] commandBytes = new byte[5] { 164, 48, 48, 48, 48 };
            converter = converter/10.0F;
            commandBytes[2] = (byte)((int)(converter + 48));
            converter = (converter - (int)converter)*10;
            commandBytes[3] = (byte)((int)(converter + 48));
            converter = (converter - (int)converter) * 10;
            commandBytes[4] = (byte)((int)(converter + 48));
            SafeWrite(commandBytes, 0, 5);
            while (serialPort1.BytesToRead == 0) ;
            System.Diagnostics.Debug.WriteLine("Byte from serial port: " + serialPort1.ReadByte());
        }
        /// <summary>
        /// Safely Writes to the serial port, if there is a problem it closes the session and notifys TreadmillContrller
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        private void SafeWrite(byte[] buffer, int offset, int count)
        {
            try
            {
                serialPort1.Write(buffer, offset, count);
            }
            catch
            {
                CloseSession(); // Close the session
				Thread.Sleep(500); // Wait 500 ms then fire the event so that TreadmillController can set m_woodway to null
                if (TreadmillPortUnpluggedEvent != null)
                    TreadmillPortUnpluggedEvent();
                StroMoHab_Objects.Forms.TopMostMessageBox.Show("The Treadmill has been unplugged\n\nPlease fix the problem and press \"Re-Detect Treadmill\"", "Treadmill Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Treadmill properties

		/// <summary>
		/// Returns the status of the timer thread
		/// </summary>
        public bool TimerThreadIsAlive
        {
            get { return timerThread.IsAlive; }
        }

        /// <summary>
        /// gets/sets speed of treadmill in MPH
        /// Direction of the treadmill is determined by the state of the Reverse property
        /// Velocity property is updated to equivalent meters per second
        /// </summary>
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                commandList.Add(163);
            }
        }

        /// <summary>
        /// gets/sets the speed of the treadmill in meters per second
        /// If the set value is negative the Reverse property is set True
        /// and the Speed property is update to the equivalent MPH
        /// </summary>
        public float Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
                speed = (Math.Abs(velocity));
                if (velocity < 0)
                {
                    reverse = true;
                }
            }
        }

        /// <summary>
        /// gets/sets elevation of treadmill in degrees
        /// </summary>
        public float Elevation
        {
            get
            {
                return elevation;
            }
            set
            {
                elevation = value;
                commandList.Add(164);
            }
        }

        /// <summary>
        /// gets/sets whether the treadmill is set to run in reverse
        /// </summary>
        public bool Reverse
        {
            get
            {
                return reverse;
            }
            set
            {
                reverse = value;
            }
        }

        public float MaximumSpeed
        {
            get
            {
                return MAX_SPEED;
            }
            set
            {
                MAX_SPEED = value;
            }
        }

        public float MaximumElevation
        {
            get
            {
                return MAX_ELEVATION;
            }
            set
            {
                MAX_ELEVATION = value;
            }
        }
        #endregion

    }
}

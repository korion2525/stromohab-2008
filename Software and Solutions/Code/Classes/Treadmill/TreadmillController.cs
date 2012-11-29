using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StroMoHab_TT_Server.Treadmill
{
    class TreadmillController
    {
        #region Member Variables

        private static Treadmill m_woodway;
        private static float m_speed = 0f;

        #endregion Member Variables

        #region Delegates
        public delegate void TransmitSpeedEventHandler(float newSpeed);
        #endregion Delegates

        #region Events
        public static event TransmitSpeedEventHandler TransmitSpeedEvent;
        #endregion Events

        #region Methods

        public static void SetSerialPortName(string serialPortName)
        {
            if (m_woodway == null)
            {
                m_woodway = new Treadmill(serialPortName);
                m_woodway.Start();
                m_woodway.TreadmillPortUnpluggedEvent += new Treadmill.TreadmillPortUnpluggedEventHandler(m_woodway_TreadmillPortUnpluggedEvent);
            }
        }

		/// <summary>
		/// Clears the serial port so SetSerialPortName() can be called
		/// </summary>
        public static void ClearSerialPortName()
        {
		
            if (m_woodway != null)
            {
				// Close the session, then wait for the thread to close, then set the treadmill to null, then wait and return
			
                m_woodway.CloseSession();
                while (m_woodway.TimerThreadIsAlive)
                {
                    System.Threading.Thread.Sleep(100);
                }
                System.Threading.Thread.Sleep(500);
                m_woodway = null;
                System.Threading.Thread.Sleep(500);
            }
        }

        /// <summary>
        /// When the treadmill's port gets unplugged set m_woodway to null so it can be set again
        /// </summary>
        static void m_woodway_TreadmillPortUnpluggedEvent()
        {
            
            m_woodway = null;
        }


        public static void SetMaximumSpeed(float maximumSpeed)
        {
            if (m_woodway != null)
            {
                m_woodway.MaximumSpeed = maximumSpeed;
            }
        }

        public static void SetMaximumElevation(float maximumElevation)
        {
            if (m_woodway != null)
            {
                m_woodway.MaximumElevation = maximumElevation;
            }
        }

        public static void SetReverseState(bool reverse)
        {
            if (m_woodway != null)
            {
                m_woodway.Reverse = reverse;
            }
        }

        public static void SetSpeed(float speed)
        {
            
            if (m_woodway != null)
            {
                m_woodway.Speed = speed;
               
            }
            m_speed = speed;
            TransmitSpeedEvent(speed);
        }

        public static void SetElevation(float elevation)
        {
            if (m_woodway != null)
            {
                m_woodway.Elevation = elevation;
            }
        }

        public static float GetMaximumSpeed()
        {
                return m_woodway.MaximumSpeed;       
        }

        public static float GetSpeed()
        {
            return m_speed;
        }

        /// <summary>
        /// Forces the TreadmillController to fire a Transmit Speed Event
        /// </summary>
        public static void TransmitSpeed()
        {
            TransmitSpeedEvent(m_speed);
        }
        #endregion Properties


    }
}

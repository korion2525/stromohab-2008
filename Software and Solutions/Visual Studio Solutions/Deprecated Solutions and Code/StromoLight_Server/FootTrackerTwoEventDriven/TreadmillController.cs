using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TreadMillController;

namespace Stromohab_MCE
{
    class TreadmillController
    {
        #region Member Variables

        private static Treadmill m_woodway;

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
            }
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
                TransmitSpeedEvent(speed);
            }
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


        #endregion Properties


    }
}

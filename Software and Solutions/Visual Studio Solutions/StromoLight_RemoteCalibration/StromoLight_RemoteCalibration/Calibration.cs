using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;

namespace StromoLight_RemoteCalibration
{
    [Serializable]
    public class Calibration : MarshalByRefObject
    {
        #region Member Variables

        Marker m_leftFootMarker;
        Marker m_rightFootMarker;

        Trackable m_leftFootTrackable;
        Trackable m_rightFootTrackable;

        #endregion Member Variables

        #region Properties

        public Trackable LeftFoot_Trackable
        {
            get 
            {
                    return m_leftFootTrackable;
            }
            set 
            { 
                m_leftFootTrackable = value;
            }
        }

        public Marker LeftFoot_Marker
        {
            get
            {
                    return m_leftFootMarker;
            }
            set
            {
                m_leftFootMarker = value;
            }
        }

        public Trackable RightFoot_Trackable
        {
            get 
            {
                    return m_rightFootTrackable;
            }
            set 
            { 
                m_rightFootTrackable = value;
            }
        }

        public Marker RightFoot_Marker
        {
            get
            {
                    return m_rightFootMarker;
            }
            set
            {
                m_rightFootMarker = value;
            }
        }

        #endregion Properties

        /// <summary>
        /// Delegate for CalibrationCompleted Event
        /// </summary>
        /// <param name="m_calibration"></param>
        public delegate void CalibrationDataUpdatedEventHandler();

        private event CalibrationDataUpdatedEventHandler m_calibrationUpdated;

        /// <summary>
        /// CalibrationCompleted Event
        /// </summary>
        public event CalibrationDataUpdatedEventHandler CalibrationUpdated
        {
            add
            {
                m_calibrationUpdated += value;
            }
            remove
            {
                m_calibrationUpdated -= value;
            }
        }
        
        
        #region Public Methods

        public void Reset()
        {
            m_leftFootMarker = null;
            m_rightFootMarker = null;
            m_leftFootTrackable = null;
            m_rightFootTrackable = null;
        }

        /// <summary>
        /// Stores the calibration (inital) positions of the left and right feet.
        /// </summary>
        public void Store(Marker leftFoot, Marker rightFoot)
        {
            m_leftFootMarker = new Marker(leftFoot);
            m_rightFootMarker = new Marker(rightFoot);

            m_leftFootTrackable = null;
            m_rightFootTrackable = null;

            if (m_calibrationUpdated != null)
            {
                m_calibrationUpdated();
            }
        }

        /// <summary>
        /// Stores the calibration (inital) positions of the left and right feet.
        /// </summary>
        public void Store(Trackable leftFoot, Trackable rightFoot)
        {
            m_leftFootTrackable = new Trackable(leftFoot);
            m_rightFootTrackable = new Trackable(rightFoot);

            m_leftFootMarker = null;
            m_rightFootMarker = null;

            if (m_calibrationUpdated != null)
            {
                m_calibrationUpdated();
            }
        }


        /// <summary>
        /// Required to ensure object connection doesn't timeout during remote access.
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        #endregion Public Methods
    }
}

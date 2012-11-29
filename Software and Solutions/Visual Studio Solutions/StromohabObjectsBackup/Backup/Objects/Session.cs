using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a session but does not hold any of the data
    /// </summary>
    [Serializable]
    public class Session
    {
        private DateTime _sessionStartTime = DateTime.Now;
        private DateTime _sessionScheduledTime = DateTime.Now;
        private List<SubSession> _subsessions = new List<SubSession>();
        private string _hospital;
        private string _clinician = "";
        private string _notes;
        private bool _scheduledSession = true;
        private bool _sessionCompleted = false;

        private Task _task = new Task();
        private bool _dataRecorded = false;

        public string Clinician
        {
            get { return _clinician; }
            set { _clinician = value; }
        }

        public Task Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public bool MotionCaptureDataRecorded
        {
            get { return _dataRecorded; }
            set { _dataRecorded = value; }
        }
        /// <summary>
        /// Gets or Sets a value indicating if the session has been completed in full
        /// </summary>
        public bool SessionCompleted
        {
            get { return _sessionCompleted; }
            set { _sessionCompleted = value; }
        }
        /// <summary>
        /// Gets or Sets a value indicating if the session is scheduled (true), or has already been run (false)
        /// </summary>
        public bool ScheduledSession
        {
            get { return _scheduledSession; }
            set { _scheduledSession = value; }
        }
        public TimeSpan Duration
        {
            get
            {
                TimeSpan duration = new TimeSpan();
                foreach (SubSession ss in SubSessions)
                {
                    TimeSpan ts = ss.SubSessionEndTime.Subtract(ss.SubSessionStartTime);
                    duration = duration.Add(ts);

                }
                return duration;
            }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        public DateTime SessionStartTime
        {
            get { return _sessionStartTime; }
            set { _sessionStartTime = value; }
        }
        public DateTime SessionSecheduledTime
        {
            get { return _sessionScheduledTime; }
            set { _sessionScheduledTime = value; }
        }

        public List<SubSession> SubSessions
        {
            get { return _subsessions; }
            set { _subsessions = value; }
        }

        public void AddSubSession(SubSession subSession)
        {
            _subsessions.Add(subSession);
        }


    }
}

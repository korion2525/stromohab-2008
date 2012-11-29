using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a sub session from the treadmill starting to stopping
    /// Stores the start time so that the correct file can be opened, and the end time for convinience
    /// </summary>
    [Serializable]
    public class SubSession
    {
        private DateTime _startTime = DateTime.Now;
        private DateTime _endTime = DateTime.Now;

        public DateTime SubSessionStartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        public DateTime SubSessionEndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
    }
}

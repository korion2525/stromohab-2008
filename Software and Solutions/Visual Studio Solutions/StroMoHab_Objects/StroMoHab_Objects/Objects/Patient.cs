using System.Collections.Generic;
using System;


namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a Patient
    /// </summary>
    [Serializable]
    public class Patient
    {
        private string _firstname = "";
        private string _middlename = "";
        private string _lastname = "";
        private string _patientID = "";
        public enum TitleType {Mr, Mrs, Ms, Miss, Dr};
        public enum GenderType { Male, Female };
        private GenderType _gender;
        private TitleType _title;
        private DateTime _dob;
        private string _notes = "";
        private string _primaryClinician = "";
        private List<Session> _sessions = new List<Session>();
        private Address _address = new Address();
        private ContactNumber _contactNumber = new ContactNumber();
        private SecondaryContact _secondaryContact = new SecondaryContact();

        /// <summary>
        /// Adds a sesion
        /// </summary>
        /// <param name="session"></param>
        public void AddSession(Session session)
        {
            _sessions.Add(session);
        }
        /// <summary>
        /// Gets a string with the date and number of days since the last session
        /// </summary>
        public string LastSessionString
        {
            get
            {
                string str = "";
                int days = 99999;
                DateTime lastSession = DateTime.Today;
                foreach (Session s in Sessions)
                {
                    if (s != null)
                    {
                        if (!s.ScheduledSession)
                        {
                            if (DateTime.Today.Subtract(s.SessionStartTime.Date).Days < days)
                            {
                                days = DateTime.Today.Subtract(s.SessionStartTime.Date).Days;
                                lastSession = s.SessionStartTime;
                            }
                        }
                    }
                }

                if (days == 99999)
                    str = "Never";
                else if (days == 1)
                    str = lastSession.ToLongDateString() + " (" + days + " day ago)";
                else
                    str = lastSession.ToLongDateString() + " (" + days + " days ago)";

                return str;
            }
        }

        /// <summary>
        /// Gets the number of sessions that have been run
        /// </summary>
        public int NumberOfCompletedSessions
        {
            get
            {
                int i = 0;
                foreach (Session s in Sessions)
                {
                    if (s != null)
                    {
                        if (!s.ScheduledSession)
                        {
                            i++;
                        }
                    }
                }
                return i;
            }
        }
        /// <summary>
        /// Gets the number of sessions (both scheduled and completed)
        /// </summary>
        public int NumberOfSessions
        {
            get { return Sessions.Count; }
        }

        /// <summary>
        /// Gets or sets the patients notes
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        /// <summary>
        /// The Patient's First Name
        /// </summary>
        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        /// <summary>
        /// The Patient's Middle Name
        /// </summary>
        public string MiddleName
        {
            get { return _middlename; }
            set { _middlename = value; }
        }

        /// <summary>
        /// The Patient's Last Name
        /// </summary>
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
        /// <summary>
        /// Gets or sets the patient ID (NHS Number)
        /// </summary>
        public string PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }
        /// <summary>
        /// Gets or sets the patients title
        /// </summary>
        public TitleType Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// Gets or sets the patients gender
        /// </summary>
        public GenderType Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        /// <summary>
        /// Gets or sets the patients DOB
        /// </summary>
        public DateTime DOB
        {
            get { return _dob; }
            set { _dob = value; }
        }
        /// <summary>
        /// Gets the patients age in years
        /// </summary>
        public int AgeInYears
        {
            get
            {
                DateTime now = DateTime.Now;

                int age = 0;

                int yearDiff = now.Year - DOB.Year;
                int monthDiff = now.Month - DOB.Month;
                int dayDiff = now.Day - DOB.Day;
                if (monthDiff < 0)
                {
                    age = yearDiff - 1;

                }
                else if (monthDiff == 0)
                {
                    if (dayDiff >= 0)
                        age = yearDiff;
                    else
                        age = yearDiff - 1;

                }
                else if (monthDiff > 0)
                {
                    age = yearDiff;
                }

                return age;
            }
        }
        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// Gets of sets the contact telephone number
        /// </summary>
        public ContactNumber ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        /// <summary>
        /// Sets the patients date of birth from a list of ints
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        public void SetDOB(int year, int month, int day)
        {
            _dob = new DateTime(year, month, day);
        }
        /// <summary>
        /// Gets or sets the list of sessions
        /// </summary>
        public List<Session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }
        /// <summary>
        /// Gets or sets the patients secondary contact
        /// </summary>
        public SecondaryContact SecondaryContact
        {
            get { return _secondaryContact; }
            set { _secondaryContact = value; }
        }
        /// <summary>
        /// Gets the FileName used to store the patient file
        /// </summary>
        public string FileName
        {
            get { return _patientID; }
        }
    }
}

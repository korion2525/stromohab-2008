using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Store the name, login, password details and permisions for a clinician
    /// </summary>
    [Serializable]
    public class Clinician
    {
        #region Member Variables
        private string _firstname = "";
        private string _lastname = "";
        private string _login = "";
        private int _passwordSalt = 0;
        private string _passwordSaltedHash = "";
        private bool _editClincians = false;
        private bool _editPatients = false;
        private bool _editSessions = true;
        private bool _editTasks = true;
        private bool _runSessions = true;
        private bool _scheduleSessions = true;
        private bool _canChangeDetails = true;
        #endregion

        #region Properties

        public string NameString
        {
            get { return _firstname + " " + _lastname; }
        }
        /// <summary>
        /// Set to false to block changing any details (except the password)
        /// </summary>
        public bool CanEditDetails
        {
            get { return _canChangeDetails; }
            set { _canChangeDetails = value; }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the clinician can edit Clinicians
        /// </summary>
        public bool EditClinicians
        {
            get { return _editClincians; }
            set
            {
                if(CanEditDetails)
                 _editClincians = value;
            }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the clinician can edit Patients
        /// </summary>
        public bool EditPatients
        {
            get { return _editPatients; }
            set
            {
                if (CanEditDetails)
                    _editPatients = value;
            }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the clinician can edit Sessions
        /// </summary>
        public bool EditSessions
        {
            get { return _editSessions; }
            set
            {
                if (CanEditDetails)
                    _editSessions = value;
            }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the clinician can schedule sessions
        /// </summary>
        public bool ScheduleSessions
        {
            get { return _scheduleSessions; }
            set
            {
                if (CanEditDetails)
                    _scheduleSessions = value;
            }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the clinician can run sessions
        /// </summary>
        public bool RunSessions
        {
            get { return _runSessions; }
            set
            {
                if (CanEditDetails)
                    _runSessions = value;
            }
        }
        /// <summary>
        /// Gets or sets a bool indicating if the clinician can edit Tasks
        /// </summary>
        public bool EditTasks
        {
            get { return _editTasks; }
            set {

                if (CanEditDetails) 
                    _editTasks = value;
            }
        }

        /// <summary>
        /// The Clinician's First Name
        /// </summary>
        public string FirstName
        {
            get { return _firstname; }
            set
            {
                if (CanEditDetails)
                    _firstname = value;
            }
        }

        /// <summary>
        /// The Clinician's Last Name
        /// </summary>
        public string LastName
        {
            get { return _lastname; }
            set {

                if (CanEditDetails) 
                    _lastname = value;
            }
        }
        /// <summary>
        /// Login
        /// </summary>
        public string LoginName
        {
            get { return _login; }
            set {

                if (CanEditDetails) 
                    _login = value;
            }
        }
        #endregion

        #region Password
        public bool ValidateLogin(string password)
        {
            Password pwd = new Password(password, _passwordSalt);
            if(pwd.ComputeSaltedHash() == _passwordSaltedHash)
                return true;
            else return false;
        }

        public void SetPassword(string password)
        {
            //New salt
            _passwordSalt = Password.CreateRandomSalt();

            //intialise the password
            Password pwd = new Password(password, _passwordSalt);

            //generae and store the salted hash
            _passwordSaltedHash = pwd.ComputeSaltedHash();
        }
        #endregion
    }
}

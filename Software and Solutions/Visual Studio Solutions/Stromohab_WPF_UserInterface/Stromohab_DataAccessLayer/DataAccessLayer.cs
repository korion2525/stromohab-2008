using System;
using System.Diagnostics.Contracts;
using System.Linq;



namespace Stromohab_DataAccessLayer
{
    public class DataAccessLayer
    {
        public static DataAccessLayer Instance = new DataAccessLayer();

        /// <summary>
        /// Creates a Singleton instance of the DAL.
        /// </summary>
        /// <returns>the data access layer.</returns>
        public static DataAccessLayer CreateInstance()
        {
            if (Instance==null)
            {
                Instance = new DataAccessLayer();
            }
            return Instance;
        }

        /// <summary>
        /// Private Constructor prevents external create of DAL instances (by using "new").
        /// </summary>
        private DataAccessLayer()
        {
            
        }


        /// <summary>
        /// Authenticates the attempted login.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>bool indicating the outcome of the authentication</returns>
        public static bool AuthenticateUser(string userName, string password)
        {
            Contract.Requires(userName!=null);
            Contract.Requires(password!=null);
            return  (Authentication.AuthenticateUser(userName, password));
        }

        /// <summary>
        /// Gets a clinicinans patients.
        /// </summary>
        /// <param name="userName">the clinicians username</param>
        /// <returns>all patients asscioated with the clinicians username</returns>
        public static IQueryable GetCliniciansPatients(string userName)
        {
            return (Patients.PatientDataSetForClinician(userName));
        }

        /// <summary>
        /// Adds a patient into the Stromohab system.
        /// </summary>
        /// <param name="firstNames">The patients first name(s)</param>
        /// <param name="lastName">The patients family name or surname</param>
        /// <param name="dateOfBirth">the patients date of birth</param>
        /// <param name="gender">the patients gender ("Male" or "Female"</param>
        /// <param name="contactNumber">a contact telephone number for the patient</param>
        /// <param name="clinicianUserName">the username of the patients clinician</param>
        public static void AddPatient(string firstNames, string lastName, DateTime dateOfBirth, string gender, string contactNumber, string clinicianUserName)
        {
            Patients.AddPatient(firstNames, lastName, dateOfBirth, gender, contactNumber, clinicianUserName);
        }

        /// <summary>
        /// Updates the given patient in the Stromohab system.
        /// </summary>
        /// <param name="patientToEdit">the patient to overwrite into the system.</param>
        public static void UpdatePatient(patient patientToEdit)
        {
            Patients.UpdatePatient(patientToEdit);
        }

        public static void DeletePatientById(int patientId)
        {
            Patients.DeletePatientByID(patientId);
        }
        
        /// <summary>
        /// Selects a given patient from the system, given the patient ID.
        /// </summary>
        /// <param name="patientId">the ID of the patient object to be returned</param>
        /// <returns></returns>
        public static patient SelectPatientFromPatientId(int patientId)
        {
            return (Patients.SelectPatientFromPatientId(patientId));
        }

        public static string PatientFirstNamesFromPatientId(int patientId)
        {
            return (Patients.PatientFirstNamesFromPatientId(patientId));
        }

        public static string PatientLastNameFromPatientId(int patientId)
        {
            return (Patients.PatientLastNameFromPatientId(patientId));
        }

        /// <summary>
        /// Selects a given patients scheduled sessions with a clinician.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IQueryable PatientSessions(int patientId, string userName)
        {
            Contract.Requires(userName != null, "Username cannot be null");
            //if (userName == null)
            //{
              //  throw new ArgumentNullException("userName");
            //}
            return (Sessions.SessionDatasetForCliniciansPatient(patientId, userName));
        }

        public static void AddTask(string taskName, byte[] taskData)
        {
            Tasks.AddTask(taskName, taskData);
        }

    }
}

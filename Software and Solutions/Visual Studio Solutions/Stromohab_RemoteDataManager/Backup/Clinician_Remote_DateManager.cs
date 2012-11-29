using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Remote_DataManager
{
    /// <summary>
    /// Handles exchanging clinician data between the server and patient manager client
    /// </summary>
    public class Clinician_Remote_DateManager : MarshalByRefObject
    {
        private static Clinician _clinician;
        private static List<Clinician> _clinicians = new List<Clinician>();

        /// <summary>
        /// Gets the loged in clinician - returns null if ClientRequestLogin failed
        /// </summary>
        public Clinician ValidatedClinician
        {
            get { return _clinician; }
        }

        /// <summary>
        /// The list of Clinicians
        /// </summary>
        public List<Clinician> Clinicians
        {
            get { return _clinicians; }
        }

        /// <summary>
        /// Requests an updated clinician list
        /// </summary>
        public void ClientRequestUpdatedClinicianList()
        {
            if (UpdateClinicianListRequestedByClient != null)
                UpdateClinicianListRequestedByClient();
        }
        public delegate void UpdateClinicianListRequestedByClientEventHandler( );
        public static event UpdateClinicianListRequestedByClientEventHandler UpdateClinicianListRequestedByClient;

        /// <summary>
        /// Updates a clinician
        /// </summary>
        /// <param name="clinician"></param>
        public void ClientUpdateClinician(Clinician clinician)
        {
            if (UpdateClinicianRequestedByClient != null)
                UpdateClinicianRequestedByClient(clinician);
        }
        public delegate void UpdateClinicianRequestedByClientEventHandler(Clinician clinician);
        public static event UpdateClinicianRequestedByClientEventHandler UpdateClinicianRequestedByClient;

        /// <summary>
        /// Deletes a clinician
        /// </summary>
        /// <param name="clinician"></param>
        public void ClientDeleteClinician(Clinician clinician)
        {
            if (DeleteClinicianRequestedByClient != null)
                DeleteClinicianRequestedByClient(clinician);
        }
        public delegate void DeleteClinicianRequestedByClientEventHandler(Clinician clinician);
        public static event DeleteClinicianRequestedByClientEventHandler DeleteClinicianRequestedByClient;

        /// <summary>
        /// Saves a new clinician
        /// </summary>
        /// <param name="clinician"></param>
        public void ClientSaveNewClinician(Clinician clinician)
        {
            if (NewClinicianSaveRequestedByClient != null)
                NewClinicianSaveRequestedByClient(clinician);
        }
        public delegate void NewClinicianSaveRequestedByClientEventHandler(Clinician clinician);
        public static event NewClinicianSaveRequestedByClientEventHandler NewClinicianSaveRequestedByClient;

        /// <summary>
        /// Validates login details, ValidatedClinician will return the clinician if successful, else it will return null
        /// </summary>
        /// <param name="login">User name</param>
        /// <param name="password">Password</param>
        public void ClientRequestLogin(string login, string password)
        {
            if (LoginRequestByClient != null)
                LoginRequestByClient(login, password);
        }
        public delegate void LoginRequestByClientEventHandler(string login, string password);
        public static event LoginRequestByClientEventHandler LoginRequestByClient;

        /// <summary>
        /// Respond to login request by passing the validated clinician or null
        /// </summary>
        /// <param name="clinician"></param>
        public void ServerRespondToLoginRequest(Clinician clinician)
        {
            _clinician = clinician;
        }
        /// <summary>
        /// Updates the clinician list
        /// </summary>
        /// <param name="clinicians"></param>
        public void ServerUpdateClinicianList(List<Clinician> clinicians)
        {
            _clinicians = new List<Clinician>(clinicians);
        }

    }
}

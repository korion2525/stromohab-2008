using System;
using System.Collections.Generic;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Remote_DataManager
{
    /// <summary>
    /// Handles exchanging patient data between the server and patient manager client
    /// </summary>
    public class Patient_Remote_DataManager : MarshalByRefObject
    {
        #region Member Variables
        /// <summary>
        /// Stores the list of patients
        /// </summary>
        private static List<Patient> _patients = new List<Patient>();
        /// <summary>
        /// Recording status
        /// </summary>
        private static bool _recording = false;
        /// <summary>
        /// Playback status
        /// </summary>
        private static bool _playingback = false;
        #endregion


        #region CalledByClient
        #region Patient Management
        /// <summary>
        /// Deletes the specified patient
        /// </summary>
        /// <param name="patientIndex"></param>
        public void ClientDeletePatient(int patientIndex)
        {
            if (DeletePatientRequestedByClient != null)
                DeletePatientRequestedByClient(patientIndex);
        }
        public delegate void DeletePatientRequestedByClientEventHandler(int patientIndex);
        public static event DeletePatientRequestedByClientEventHandler DeletePatientRequestedByClient;

        /// <summary>
        /// Deletes the motion capture data associate with the session from disk
        /// </summary>
        public void ClientDeleteSession(int patientIndex, int sessionIndex)
        {
            if (DeleteSessionRequestedByClient != null)
                DeleteSessionRequestedByClient(patientIndex,sessionIndex);
        }
        public delegate void DeleteSessionRequestedByClientEventHandler(int patientIndex, int sessionIndex);
        public static event DeleteSessionRequestedByClientEventHandler DeleteSessionRequestedByClient;


        /// <summary>
        /// Requests an updated patient list
        /// </summary>
        public void ClientRequestUpdatedPatientList()
        {
            if (UpdatedPatientListRequestedByClient != null)
                UpdatedPatientListRequestedByClient();
        }
        public delegate void UpdatedPatientListRequestedByClientEventHandler();
        public static event UpdatedPatientListRequestedByClientEventHandler UpdatedPatientListRequestedByClient;

        /// <summary>
        /// Updates the details for the specified patient
        /// </summary>
        /// <param name="patient">The updated patient</param>
        /// <param name="patientIndex">Its current location in the patient list</param>
        public void ClientUpdatePatient(Patient patient, int patientIndex)
        {
            if (UpdatePatientRequestedByClient != null)
                UpdatePatientRequestedByClient(patient, patientIndex);
        }
        public delegate void UpdatePatientRequestedByClientEventHandler(Patient patient, int patientIndex);
        public static event UpdatePatientRequestedByClientEventHandler UpdatePatientRequestedByClient;

        
        /// <summary>
        /// Saves a newly created patient to the server
        /// </summary>
        /// <param name="patient">The new patient</param>
        public void ClientSaveNewPatient(Patient patient)
        {
            if (NewPatientSaveRequestedByClient != null)
                NewPatientSaveRequestedByClient(patient);
        }
        public delegate void NewPatientSaveRequestedByClientEventHandler(Patient patient);
        public static event NewPatientSaveRequestedByClientEventHandler NewPatientSaveRequestedByClient;
        #endregion

        #region Playback
        /// <summary>
        /// Prepares for playback
        /// </summary>
        /// <param name="patientIndex">The patient index</param>
        /// <param name="sessionIndex">The session index</param>
        /// <param name="subSessionIndex">The sub-session index (use 0 to play entier session)</param>
        public void ClientOpenSessionForPlayback(int patientIndex, int sessionIndex, int subSessionIndex)
        {
            if (OpenSessionForPlaybackRequestedByClient != null)
                OpenSessionForPlaybackRequestedByClient(patientIndex,sessionIndex,subSessionIndex);
        }
        public delegate void OpenSessionForPlaybackRequestedByClientEventHandler(int patientIndex, int sessionIndex, int subSessionIndex);
        public static event OpenSessionForPlaybackRequestedByClientEventHandler OpenSessionForPlaybackRequestedByClient;
        /// <summary>
        /// Starts playback
        /// </summary>
        public void ClientStartPlayback()
        {
            if (ChangePlaybackStatusRequestedByClient != null)
                ChangePlaybackStatusRequestedByClient(true);
        }
        /// <summary>
        /// Stops playback
        /// </summary>
        public void ClientStopPlayback()
        {
            if (ChangePlaybackStatusRequestedByClient != null)
                ChangePlaybackStatusRequestedByClient(false);
        }
        public delegate void ChangePlaybackStatusRequestedByClientEventHandler(bool active);
        public static event ChangePlaybackStatusRequestedByClientEventHandler ChangePlaybackStatusRequestedByClient;
        #endregion

        #region Recording
        /// <summary>
        /// Prepares to record a new session
        /// </summary>
        /// <param name="patientIndex">The patient index to associate the session with</param>
        public void ClientPrepareToRecordNewSession(int patientIndex, int sessionIndex)
        {
            if (PrepareToRecordNewSubSessionRequestedByClient != null)
                PrepareToRecordNewSubSessionRequestedByClient(patientIndex, sessionIndex);
        }
        public delegate void PrepareToRecordNewSubSessionRequestedByClientEventHandler(int patientIndex, int sessionIndex);
        public static event PrepareToRecordNewSubSessionRequestedByClientEventHandler PrepareToRecordNewSubSessionRequestedByClient;
        /// <summary>
        /// Starts recording
        /// </summary>
        public void ClientStartRecord()
        {
            if (ChangeRecordStatusRequestedByClient != null)
                ChangeRecordStatusRequestedByClient(true);
        }
        /// <summary>
        /// Stops recording
        /// </summary>
        public void ClientStopRecord()
        {
            if (ChangeRecordStatusRequestedByClient != null)
                ChangeRecordStatusRequestedByClient(false);
        }
        public delegate void ChangeRecordStatusRequestedByClientEventHandler(bool active);
        public static event ChangeRecordStatusRequestedByClientEventHandler ChangeRecordStatusRequestedByClient;
        #endregion
        #endregion


        #region CalledByServer
        /// <summary>
        /// Updates the patient list stored in the remote object for the client
        /// </summary>
        /// <param name="patientList"></param>
        public void ServerPatientListAvailable(List<Patient> patientList)
        {
            PatientList = patientList;
        }
        /// <summary>
        /// Sets the current recording status
        /// </summary>
        /// <param name="recording">True = recording, False = not recording</param>
        public void ServerRecordingStatus(bool recording)
        {
            RecordingStatus = recording;
        }
        /// <summary>
        /// Sets the current playback status
        /// </summary>
        /// <param name="playing">True = playing, False = not playing</param>
        public void ServerPlayingStatus(bool playing)
        {
            PlaybackStatus = playing;
        }
        #endregion


        #region Properties
        /// <summary>
        /// The list of patients
        /// </summary>
        public List<Patient> PatientList
        {
            get { return _patients; }
            private set { _patients = value; }
        }
        /// <summary>
        /// The current recording status
        /// </summary>
        public bool RecordingStatus
        {
            get { return _recording; }
            private set { _recording = value; }
        }
        /// <summary>
        /// The current playback status
        /// </summary>
        public bool PlaybackStatus
        {
            get { return _playingback; }
            private set { _playingback = value; }
        }
        #endregion
    }
}

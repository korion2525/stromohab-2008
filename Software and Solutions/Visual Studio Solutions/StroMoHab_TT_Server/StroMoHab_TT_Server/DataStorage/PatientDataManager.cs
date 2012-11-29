using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;
using StroMoHab_TT_Server.MotionCapture;
using StroMoHab_Remote_DataManager;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StroMoHab_TT_Server.DataStorage
{
    /// <summary>
    /// Manages Patients, Sessions and recording/playback
    /// 
    /// This class manages the patient data that is stored on disk,
    /// sets up and controls VirtualMotionCaptureController and puts
    /// CommandParser into the correct mode to either playback or record
    /// motion capture data
    /// </summary>
    static class PatientDataManager
    {
        //TODO: Record and playback without the treadmill/association with patients
        /*
         * A new method needs to be written that accepts just a DateTime sessionTime rather than a patient, session and subsession index
         * this method will then open that session. Extra changes will be 
         * 
         * VirtualMotionCaptureController_VirtualMotionCaptureSubSessionPlaybackEnded 
         * will need to not try to open the next subsession as there won't be any data so use an if(standalonemode) if statement
         * 
         * VirtualMotionCaptureController_VirtualMotionCaptureSubSessionRecorded
         * will need to send the DateTime sessionStartTime back to the remote object rather than writing it into a patient record
         * 
         * StopRecording will again need an if statement to seperate out any references to patients when in standalonemode
         * 
         * Additional methods inside the remote object will be requires as well as working out how to return to sessionStartTime without pooling if possiable
         * plus the changes noted in VirtualMotioncapturecontroller will be made so that it isn't started and stopped based on the treadmill speed
         * 
         * 
         * 
         * 
         * */

        #region Member Variables
        //Local copy of patients
        private static List<Patient> _patients = new List<Patient>();
        private static int _patientIndex = 0;
        private static Session _currentSession;
        private static int _currentSessionIndex;
        private static int _currentSubSessionIndex;
        private static SubSession _currentSubSession;
        private static Patient_Remote_DataManager _remoteDataManager = new Patient_Remote_DataManager();
        private const string FILE_EXTENTION = ".smhp";
        private static string _patientDIR = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Data\\";
        private static List<string> _patientFileNamesList = new List<string>();
        
        #endregion

        #region Public Server Methods

        /// <summary>
        /// Registers all of the events stored in the remote objected that are fired by the client
        /// </summary>
        public static void RegisterEvents()
        {
            Patient_Remote_DataManager.ChangePlaybackStatusRequestedByClient += new Patient_Remote_DataManager.ChangePlaybackStatusRequestedByClientEventHandler(Patient_Remote_DataManager_ChangePlaybackStatusRequestedByClient);
            Patient_Remote_DataManager.ChangeRecordStatusRequestedByClient += new Patient_Remote_DataManager.ChangeRecordStatusRequestedByClientEventHandler(Patient_Remote_DataManager_ChangeRecordStatusRequestedByClient);
            Patient_Remote_DataManager.NewPatientSaveRequestedByClient += new Patient_Remote_DataManager.NewPatientSaveRequestedByClientEventHandler(Patient_Remote_DataManager_NewPatientSaveRequestedByClient);
            Patient_Remote_DataManager.OpenSessionForPlaybackRequestedByClient += new Patient_Remote_DataManager.OpenSessionForPlaybackRequestedByClientEventHandler(Patient_Remote_DataManager_OpenSessionForPlaybackRequestedByClient);
            Patient_Remote_DataManager.PrepareToRecordNewSubSessionRequestedByClient += new Patient_Remote_DataManager.PrepareToRecordNewSubSessionRequestedByClientEventHandler(Patient_Remote_DataManager_PrepareToRecordNewSessionRequestedByClient);
            Patient_Remote_DataManager.UpdatedPatientListRequestedByClient += new Patient_Remote_DataManager.UpdatedPatientListRequestedByClientEventHandler(Patient_Remote_DataManager_UpdatedPatientListRequestedByClient);
            Patient_Remote_DataManager.UpdatePatientRequestedByClient += new Patient_Remote_DataManager.UpdatePatientRequestedByClientEventHandler(Patient_Remote_DataManager_UpdatePatientRequestedByClient);
            Patient_Remote_DataManager.DeletePatientRequestedByClient += new Patient_Remote_DataManager.DeletePatientRequestedByClientEventHandler(Patient_Remote_DataManager_DeletePatientRequestedByClient);
            Patient_Remote_DataManager.DeleteSessionRequestedByClient += new Patient_Remote_DataManager.DeleteSessionRequestedByClientEventHandler(Patient_Remote_DataManager_DeleteSessionRequestedByClient);
        }
        
        #endregion

        #region Remote Object Event Handlers
        /// <summary>
        /// Deletes a Sessions MC Data
        /// </summary>
        /// <param name="patientIndex"></param>
        /// <param name="sessionIndex"></param>
        static void Patient_Remote_DataManager_DeleteSessionRequestedByClient(int patientIndex, int sessionIndex)
        {
            DeleteMotionCaptureDataForSession(_patients[patientIndex].Sessions[sessionIndex]);
        }
        /// <summary>
        /// Deletes a patient
        /// </summary>
        /// <param name="patientIndex"></param>
        static void Patient_Remote_DataManager_DeletePatientRequestedByClient(int patientIndex)
        {
            if(patientIndex >=0 && patientIndex < _patientFileNamesList.Count)
            {
                if (File.Exists(GetFullFileName(_patients[patientIndex].FileName)))
                {
                    //go through and delete and mc data
                    foreach (Session session in _patients[patientIndex].Sessions)
                    {
                        DeleteMotionCaptureDataForSession(session);
                    }
                    File.Delete(GetFullFileName(_patients[patientIndex].FileName));
                }
            }
            Patient_Remote_DataManager_UpdatedPatientListRequestedByClient();
        }

        /// <summary>
        /// Updates a previously saved patient and deletes the old one if the file name has changed
        /// </summary>
        /// <param name="patient">The patient data</param>
        /// <param name="patientIndex">The patients current location</param>
        static void Patient_Remote_DataManager_UpdatePatientRequestedByClient(Patient patient, int patientIndex)
        {
            if (patient.FileName == _patients[patientIndex].FileName) //If they have the same filename then save over
                SavePatientFile(patient);
            else //otherwise delete the old one and save the new one
            {
                System.IO.File.Delete(GetFullFileName(_patients[patientIndex].FileName));
                SavePatientFile(patient);
            }
            //now update the lists
            Patient_Remote_DataManager_UpdatedPatientListRequestedByClient();

        }
        /// <summary>
        /// Deals with the request for an updated patient list
        /// </summary>
        static void Patient_Remote_DataManager_UpdatedPatientListRequestedByClient()
        {
            LoadInPatientData();
            _remoteDataManager.ServerPatientListAvailable(_patients);
        }
        /// <summary>
        /// Saves a new patient
        /// </summary>
        /// <param name="patient">The new patient</param>
        static void Patient_Remote_DataManager_NewPatientSaveRequestedByClient(Patient patient)
        {
            SavePatientFile(patient);
            LoadInPatientData();
        }

        /// <summary>
        /// Gets ready to record a new session - must be called before Patient_Remote_DataManager_ChangeRecordStatusRequestedByClient()
        /// </summary>
        /// <param name="patientIndex"></param>
        /// <param name="sessionIndex"></param>
        static void Patient_Remote_DataManager_PrepareToRecordNewSessionRequestedByClient(int patientIndex, int sessionIndex)
        {
            _patientIndex = patientIndex;
            _currentSessionIndex = sessionIndex;
        }

        /// <summary>
        /// Starts or stops recording. Must call Patient_Remote_DataManager_PrepareToRecordNewSessionRequestedByClient first
        /// </summary>
        /// <param name="active"></param>
        static void Patient_Remote_DataManager_ChangeRecordStatusRequestedByClient(bool active)
        {
            if (active)
            {
                _currentSession = _patients[_patientIndex].Sessions[_currentSessionIndex];
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }

        /// <summary>
        /// Gets ready to play back a session
        /// </summary>
        /// <param name="patientIndex"></param>
        /// <param name="sessionIndex"></param>
        /// <param name="subSessionIndex"></param>
        static void Patient_Remote_DataManager_OpenSessionForPlaybackRequestedByClient(int patientIndex, int sessionIndex, int subSessionIndex)
        {
            

            //use the indexes to load the correct subsession
            _currentSession = _patients[patientIndex].Sessions[sessionIndex];
            _currentSubSessionIndex = subSessionIndex;
            _currentSubSession = _currentSession.SubSessions[_currentSubSessionIndex];

            //Check the motion capture data is present on disk, if it is, open the subsession, else mark it as not pressent
            if(System.IO.File.Exists(VirtualMotionCaptureController.BuildSubSessionFileName(_currentSubSession.SubSessionStartTime)))
            {
                VirtualMotionCaptureController.OpenMotionCaptureSubSession(_currentSubSession.SubSessionStartTime);
            }
            else
            {
                //data isn't availiable
                _currentSession.MotionCaptureDataRecorded = false;
                _currentSession.SubSessions[_currentSubSessionIndex] = _currentSubSession;
                _patients[_patientIndex].Sessions[_currentSessionIndex] = _currentSession;
                Patient_Remote_DataManager_UpdatePatientRequestedByClient(_patients[_patientIndex], _patientIndex);
            }
        }

        
        /// <summary>
        /// Starts or stops playback - must call Patient_Remote_DataManager_OpenSessionForPlaybackRequestedByClient() first
        /// </summary>
        /// <param name="active"></param>
        static void Patient_Remote_DataManager_ChangePlaybackStatusRequestedByClient(bool active)
        {
            if (active && _currentSession.MotionCaptureDataRecorded)
            {
                StartPlayback();
            }
            else
            {
                StopPlayback();
            }
        }

        #endregion

        #region Playback
        /// <summary>
        /// Fired when playback ends
        /// </summary>
        /// <param name="subSessionStartTime"></param>
        static void VirtualMotionCaptureController_VirtualMotionCaptureSubSessionPlaybackEnded(DateTime subSessionStartTime)
        {
            //If there are multiple subsessions and this wasn't the last, then move onto the next subsession
            //TODO moddify this to allow looping of subsessions and of the complete session
            if (_currentSubSessionIndex < _currentSession.SubSessions.Count -1)
            {
                _currentSubSessionIndex++;
                _currentSubSession = _currentSession.SubSessions[_currentSubSessionIndex];
                if (System.IO.File.Exists(VirtualMotionCaptureController.BuildSubSessionFileName(_currentSubSession.SubSessionStartTime)))
                {
                    VirtualMotionCaptureController.OpenMotionCaptureSubSession(_currentSubSession.SubSessionStartTime);
                }
                else
                {
                    StopPlayback();
                }
            }
            else
            {
                StopPlayback();
            }


        }

        /// <summary>
        /// Starts playback
        /// </summary>
        private static void StartPlayback()
        {
            //Register for the playback ended event and tell the controller to start playback as well as telling command parser to get coordinates from the virtual controller
            VirtualMotionCaptureController.VirtualMotionCaptureSubSessionPlaybackEnded += new VirtualMotionCaptureController.VirtualMotionCaptureSubSessionPlaybackEndedEventHandler(VirtualMotionCaptureController_VirtualMotionCaptureSubSessionPlaybackEnded);
            VirtualMotionCaptureController.MotionCaptureDataPlayback(true);
            Communication.OptitrackCommandParser_Server.VirtualMotionCaputrePlayback = true;
            // set the status in the remote object for the client gui
            _remoteDataManager.ServerPlayingStatus(true);
            _remoteDataManager.ServerRecordingStatus(false);
        }
        private static void StopPlayback()
        {
            // tell the controller to stop playing, de-register events, tell the command parser to get coordinates from live data and stop the treadmill
            VirtualMotionCaptureController.MotionCaptureDataPlayback(false);
            VirtualMotionCaptureController.VirtualMotionCaptureSubSessionPlaybackEnded -= new VirtualMotionCaptureController.VirtualMotionCaptureSubSessionPlaybackEndedEventHandler(VirtualMotionCaptureController_VirtualMotionCaptureSubSessionPlaybackEnded);
            _remoteDataManager.ServerPlayingStatus(false);
            Communication.OptitrackCommandParser_Server.VirtualMotionCaputrePlayback = false;
            Treadmill.TreadmillController.SetSpeed(0f);
        }

        #endregion

        #region Recording
        /// <summary>
        /// Starts recording
        /// </summary>
        private static void StartRecording()
        {
            //register for the recording ended event, put the controller into record mode
            _currentSubSession = new SubSession();
            VirtualMotionCaptureController.MotionCaptureDataRecord(true);
            VirtualMotionCaptureController.VirtualMotionCaptureSubSessionRecorded += new VirtualMotionCaptureController.VirtualMotionCaptureSubSessionRecordedEventHandler(VirtualMotionCaptureController_VirtualMotionCaptureSubSessionRecorded);
            _remoteDataManager.ServerPlayingStatus(false);
            Communication.OptitrackCommandParser_Server.VirtualMotionCaputrePlayback = false;
            _remoteDataManager.ServerRecordingStatus(true);
        }

        /// <summary>
        /// Stops recording
        /// </summary>
        private static void StopRecording()
        {
            //deregister events and stop recording. 
            VirtualMotionCaptureController.VirtualMotionCaptureSubSessionRecorded -= new VirtualMotionCaptureController.VirtualMotionCaptureSubSessionRecordedEventHandler(VirtualMotionCaptureController_VirtualMotionCaptureSubSessionRecorded);
            VirtualMotionCaptureController.MotionCaptureDataRecord(false);
            //update the session and send the data back to the client
            _patients[_patientIndex].Sessions[_currentSessionIndex] = _currentSession;
            Patient_Remote_DataManager_UpdatePatientRequestedByClient(_patients[_patientIndex], _patientIndex);
            _remoteDataManager.ServerRecordingStatus(false);
            
        }

        /// <summary>
        /// Called everytime a sub session is recorded
        /// </summary>
        /// <param name="subSessionStartTime"></param>
        /// <param name="subSessionEndTime"></param>
        private static void VirtualMotionCaptureController_VirtualMotionCaptureSubSessionRecorded(DateTime subSessionStartTime, DateTime subSessionEndTime)
        {
            
            //Set the times
            _currentSubSession.SubSessionStartTime = subSessionStartTime;
            _currentSubSession.SubSessionEndTime = subSessionEndTime;
            _currentSession.MotionCaptureDataRecorded = true;
            //if the first, set the session start time
            if (_currentSession.SubSessions.Count == 0)
            {
                _currentSession.SessionStartTime = _currentSubSession.SubSessionStartTime;
            }
            //Add it to the session
            _currentSession.AddSubSession(_currentSubSession);
            _currentSession.SessionCompleted = true;
            _currentSession.ScheduledSession = false;

            _currentSubSession = new SubSession();
        }
        #endregion

        #region File IO
        /// <summary>
        /// Deletes the MC data associated with the given session
        /// </summary>
        /// <param name="session"></param>
        private static void DeleteMotionCaptureDataForSession(Session session)
        {
            foreach (SubSession sub in session.SubSessions)
            {
                if (File.Exists(VirtualMotionCaptureController.BuildSubSessionFileName(sub.SubSessionStartTime)))
                    File.Delete(VirtualMotionCaptureController.BuildSubSessionFileName(sub.SubSessionStartTime));
            }
        }
        /// <summary>
        /// Loads in the patient data from the patient files
        /// </summary>
        private static void LoadInPatientData()
        {

            _patients.Clear();
            ReadListOfPatientFiles();
            foreach (string file in _patientFileNamesList)
            {
                OpenPatientFile(file);
            }
        }

        /// <summary>
        /// Builds a list of all the avaliable patient files
        /// </summary>
        /// <returns></returns>
        private static bool ReadListOfPatientFiles()
        {
            _patientFileNamesList.Clear();
            try
            {
                DirectoryInfo directory = new DirectoryInfo(_patientDIR);
                if (directory.Exists)
                {
                    FileInfo[] files = directory.GetFiles("*" + FILE_EXTENTION);
                    foreach (FileInfo fileInfo in files)
                    {
                        _patientFileNamesList.Add(Path.GetFileNameWithoutExtension(fileInfo.FullName));
                    }
                }
                else
                    directory.Create();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        /// <summary>
        /// Turns the file name into a full file name including path and extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetFullFileName(string fileName)
        {
            return _patientDIR + fileName + FILE_EXTENTION;
        }
        /// <summary>
        /// Opens a specific patient file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool OpenPatientFile(string fileName)
        {
            string filePath = GetFullFileName(fileName);

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();

                Patient newPatient = (Patient)bFormatter.Deserialize(fs);

                _patients.Add(newPatient);
                fs.Close();
                return true;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Patient file not found. Exception message: " + ex.Message, "Patient File Not Found");
                return false;
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Patient file directory not found. Exception message: " + ex.Message, "Directory Not Found");
                return false;
            }
            catch (System.IO.IOException ex)
            {
                System.Windows.Forms.MessageBox.Show("Patient opening task file. Exception message: " + ex.Message, "Unanticipated Error Opening Patient File");
                return false;
            }

        }

        /// <summary>
        /// Saves a patient file
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private static bool SavePatientFile(Patient patient)
        {
            bool saveSucceeded = false;
            string filePath = GetFullFileName(patient.FileName);
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(fs, patient);
                fs.Close();
                saveSucceeded = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Save Unsuccesful. System Message: " + ex.Message);
            }
            finally
            {
                if (saveSucceeded)
                {
                    //  System.Windows.Forms.MessageBox.Show("File Saved", "Save Successful");
                }
            }
            return saveSucceeded;
        }
        #endregion
    }
}

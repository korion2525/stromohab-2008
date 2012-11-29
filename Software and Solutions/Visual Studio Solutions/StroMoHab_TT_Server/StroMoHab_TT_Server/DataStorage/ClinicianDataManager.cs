using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Remote_DataManager;
using StroMoHab_Objects.Objects;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StroMoHab_TT_Server.DataStorage
{
    /// <summary>
    /// Manages clinicians
    /// </summary>
    static class ClinicianDataManager
    {
        private static Clinician_Remote_DateManager _remote_DataManager = new Clinician_Remote_DateManager();
        private static List<Clinician> _clinicians;
        private const string FILE_EXTENTION = ".smhc";
        private const string FILE_NAME = "Clincians";
        private static string _dataDIR = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Data\\";

        /// <summary>
        /// Generates the default clinician
        /// </summary>
        /// <returns></returns>
        private static Clinician GenderateDefaultClinician()
        {
            Clinician newClinician = new Clinician();
            newClinician.LoginName = "admin";
            newClinician.FirstName = "Clinician";
            newClinician.LastName = "Manager";
            newClinician.EditClinicians = true;
            newClinician.EditPatients = false;
            newClinician.EditSessions = false;
            newClinician.EditTasks = false;
            newClinician.RunSessions = false;
            newClinician.ScheduleSessions = false;
            newClinician.CanEditDetails = false; // block changing details
            newClinician.SetPassword("stromohab");

            return newClinician;

        }

        public static void RegisterEvents()
        {
            Clinician_Remote_DateManager.LoginRequestByClient += new Clinician_Remote_DateManager.LoginRequestByClientEventHandler(Clinician_Remote_DateManager_LoginRequestByClient);
            Clinician_Remote_DateManager.NewClinicianSaveRequestedByClient += new Clinician_Remote_DateManager.NewClinicianSaveRequestedByClientEventHandler(Clinician_Remote_DateManager_NewClinicianSaveRequestedByClient);
            Clinician_Remote_DateManager.UpdateClinicianRequestedByClient += new Clinician_Remote_DateManager.UpdateClinicianRequestedByClientEventHandler(Clinician_Remote_DateManager_UpdateClinicianRequestedByClient);
            Clinician_Remote_DateManager.UpdateClinicianListRequestedByClient += new Clinician_Remote_DateManager.UpdateClinicianListRequestedByClientEventHandler(Clinician_Remote_DateManager_UpdateClinicianListRequestedByClient);
            Clinician_Remote_DateManager.DeleteClinicianRequestedByClient += new Clinician_Remote_DateManager.DeleteClinicianRequestedByClientEventHandler(Clinician_Remote_DateManager_DeleteClinicianRequestedByClient);
        }

        /// <summary>
        /// Deletes a clinician
        /// </summary>
        /// <param name="clinician"></param>
        static void Clinician_Remote_DateManager_DeleteClinicianRequestedByClient(Clinician clinician)
        {
            _clinicians = OpenClinciansFile(); // reload clincians
            int i = 0;
            foreach (Clinician c in _clinicians)
            {
                if (c.LoginName == clinician.LoginName)
                {
                    _clinicians.RemoveAt(i);
                    SaveClinciansFile(_clinicians);
                    return;
                }
                i++;
            }  
        }

        /// <summary>
        /// Gets an updated list of clinicians
        /// </summary>
        static void Clinician_Remote_DateManager_UpdateClinicianListRequestedByClient()
        {
            _clinicians = OpenClinciansFile(); // reload clincians
            _remote_DataManager.ServerUpdateClinicianList(_clinicians);
        }

        /// <summary>
        /// Updates a clincian
        /// </summary>
        /// <param name="clinician"></param>
        static void Clinician_Remote_DateManager_UpdateClinicianRequestedByClient(Clinician clinician)
        {
            _clinicians = OpenClinciansFile(); // reload clincians
            int i = 0;
            foreach (Clinician c in _clinicians)
            {
                if (c.LoginName == clinician.LoginName)
                {
                    _clinicians[i] = clinician;
                    SaveClinciansFile(_clinicians);
                    return;
                }
                i++;
            }            
        }
        /// <summary>
        /// Saves a new clinician (will fail if one with the same login already exists)
        /// </summary>
        /// <param name="clinician"></param>
        static void Clinician_Remote_DateManager_NewClinicianSaveRequestedByClient(Clinician clinician)
        {
            _clinicians = OpenClinciansFile(); // reload clincians
            foreach (Clinician c in _clinicians)
            {
                if (c.LoginName == clinician.LoginName) // already exists so fail as can't have two
                {
                    return;
                }
                
            }
             _clinicians.Add(clinician);
             SaveClinciansFile(_clinicians);
                
        }

        /// <summary>
        /// Processes a login request
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        static void Clinician_Remote_DateManager_LoginRequestByClient(string login, string password)
        {
            _clinicians = OpenClinciansFile(); //Load in the clincians

            Clinician clinician = null;
            //go through all clincians, if you find the same login name then check the password, if it matches then that is the clincian
            foreach (Clinician c in _clinicians)
            {
                if (c.LoginName == login)
                {
                    if (c.ValidateLogin(password))
                    {
                        clinician = c;
                        break;
                    }
                }

            }
            //if login failed then clicnian will be null, otherwise it will be a valid clincian
            RespondToLoginRequest(clinician);
        }

        /// <summary>
        /// Responds to a login request
        /// </summary>
        /// <param name="clinician">A valid clicnian or Null if login failed</param>
        private static void RespondToLoginRequest(Clinician clinician)
        {
            _remote_DataManager.ServerRespondToLoginRequest(clinician);
        }


        #region File IO 
        //Replace this section with database calls if needed
        private static List<Clinician> OpenClinciansFile()
        {
            string filePath = _dataDIR + FILE_NAME + FILE_EXTENTION;

            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();

                    List<Clinician> clinicians = (List<Clinician>)bFormatter.Deserialize(fs);

                    if (clinicians.Count == 0) // if empty make the default user
                    {
                        
                        clinicians.Add(GenderateDefaultClinician());

                        //Close the stream then save
                        fs.Close();
                        SaveClinciansFile(clinicians);

                        return clinicians;

                    }

                    fs.Close();
                    return clinicians;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Clinicians file not found. Exception message: " + ex.Message, "Clinicians File Not Found");
                    return null;
                }
                catch (System.IO.DirectoryNotFoundException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Clinicians file directory not found. Exception message: " + ex.Message, "Directory Not Found");
                    return null;
                }
                catch (System.IO.IOException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error opening Clinicians file. Exception message: " + ex.Message, "Unanticipated Error Opening Clinicians File");
                    return null;
                }
            }
            else
            {
                List<Clinician> clinicians = new List<Clinician>();
                
                clinicians.Add(GenderateDefaultClinician());

                //Close the stream then save
                SaveClinciansFile(clinicians);

                return clinicians;

            }
        }

        private static bool SaveClinciansFile(List<Clinician> clinicians)
        {
            bool saveSucceeded = false;
            string filePath = _dataDIR + FILE_NAME + FILE_EXTENTION;
            try
            {
                if (!Directory.Exists(_dataDIR))
                    Directory.CreateDirectory(_dataDIR);

                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(fs, clinicians);
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

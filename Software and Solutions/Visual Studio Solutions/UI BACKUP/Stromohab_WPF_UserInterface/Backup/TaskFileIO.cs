using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using StroMoHab_Objects.Graphics;

namespace StromoLight_TaskDesigner
{
    class TaskFileIO
    {
        /// <summary>
        /// Attempts to save the current Visualiser state to a binary file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="objectListToSave"></param>
        /// <returns></returns>
        public static bool Save(string filePath, List<OpenGlObject> objectListToSave)
        {
            bool saveSucceeded = false;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(fs, objectListToSave);
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
                    System.Windows.Forms.MessageBox.Show("File Saved", "Save Successful");
                }
            }
            return saveSucceeded;
        }

        /// <summary>
        /// Attempts to open the default task file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<OpenGlObject> Open(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();

                List<OpenGlObject> loadedObjectList = (List<OpenGlObject>)bFormatter.Deserialize(fs);
                return (loadedObjectList);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Default task file not found. Exception message: " + ex.Message, "Default Task File Not Found");
                List<OpenGlObject> emptyObjectList = new List<OpenGlObject>();
                return (emptyObjectList);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Default task file directory not found. Exception message: " + ex.Message, "Directory Not Found");
                List<OpenGlObject> emptyObjectList = new List<OpenGlObject>();
                return (emptyObjectList);
            }
            catch (System.IO.IOException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error opening default task file. Exception message: " + ex.Message, "Unanticipated Error Opening Default Task File");
                List<OpenGlObject> emptyObjectList = new List<OpenGlObject>();
                return (emptyObjectList);
            }


        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;
using System.IO;
using StroMoHab_Remote_DataManager;
using System.Runtime.Serialization.Formatters.Binary;

namespace StroMoHab_TT_Server.DataStorage
{
    /// <summary>
    /// Manages task data held on the server and makes it avaliable to the task designer via the Task_Remote_DataManager
    /// </summary>
    static class TaskDataManager
    {
        #region Member Variables
        private static Task _currentTask;
        private static bool _readOnlyPlaybackMode = false;
        private static List<string> _taskFileNamesList = new List<string>();
        private static Task_Remote_DataManager _remote_DataManager = new Task_Remote_DataManager();
        private const string FILE_EXTENTION = ".smht";

        private static string _taskDIR = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StroMoHab\\StroMoHab\\Data\\";
        #endregion

        #region Methods - Public
        /// <summary>
        /// Registers the events
        /// </summary>
        public static void RegisterEvents()
        {
            Task_Remote_DataManager.TaskRequestByClient += new Task_Remote_DataManager.TaskRequestByClientEventHandler(Remote_DataManager_TaskSetByClient);
            Task_Remote_DataManager.TaskListRequestedByClient += new Task_Remote_DataManager.TaskListRequestedByClientEventHandler(Remote_DataManager_TaskListRequestedByClient);
            Task_Remote_DataManager.TaskSaveRequestedByClient += new Task_Remote_DataManager.TaskSaveRequestedByClientEventHandler(Remote_DataManager_TaskSaveRequestedByClient);
            Task_Remote_DataManager.TaskDeleteRequestedByClient += new Task_Remote_DataManager.TaskDeleteRequestedByClientEventHandler(Task_Remote_DataManager_TaskDeleteRequestedByClient);
        }

        

       
        /// <summary>
        /// Returns the current task
        /// </summary>
        /// <returns></returns>
        public static Task GetCurrentTask()
        {
            if (_currentTask == null)
            {
                ReadListOfTaskFiles();
                SelectTaskFile(0);
                if (_currentTask == null)
                    _currentTask = new Task();
            }
                
           return _currentTask;
        }

        /// <summary>
        /// Sets the current task
        /// </summary>
        /// <param name="task">The task</param>
        /// <param name="readOnly">Is it a read only task</param>
        public static void SetCurrentTask(Task task)
        {
            _currentTask = task;
            _remote_DataManager.ServerSetTask(task);
        }
        #endregion

        #region Methods - Private

        private static void SetTaskList(List<Task> taskList)
        {
           _remote_DataManager.ServerSetTaskList(taskList);
        }

        /// <summary>
        /// Selects a task from the list of task files
        /// </summary>
        /// <param name="taskFileNameEntry"></param>
        private static void SelectTaskFile(int taskFileNameEntry)
        {
            if (_taskFileNamesList != null) // Check a list of tasks has been built
            {
                if(_taskFileNamesList.Count > taskFileNameEntry && taskFileNameEntry >= 0) // Check for a valid entry
                    OpenTaskFileAsCurrentTask(_taskFileNamesList[taskFileNameEntry]); // Open
            }
        }

        #region File IO
        /// <summary>
        /// Reads in the lsit of task files
        /// </summary>
        /// <returns></returns>
        private static bool ReadListOfTaskFiles()
        {
            _taskFileNamesList.Clear();
            try
            {
                DirectoryInfo directory = new DirectoryInfo(_taskDIR);
                if (directory.Exists)
                {
                    FileInfo[] files = directory.GetFiles("*" + FILE_EXTENTION);
                    foreach (FileInfo fileInfo in files)
                    {
                        _taskFileNamesList.Add(Path.GetFileNameWithoutExtension(fileInfo.FullName));
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
        /// Opens a specific task file
        /// </summary>
        /// <param name="fileName"></param>
        private static Task OpenTaskFile(string fileName)
        {
            string filePath = _taskDIR + fileName + FILE_EXTENTION;

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();

                Task newTask = (Task)bFormatter.Deserialize(fs);

                
                fs.Close();
                return newTask;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Task file not found. Exception message: " + ex.Message, "Task File Not Found");
                return null;
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Task file directory not found. Exception message: " + ex.Message, "Directory Not Found");
                return null;
            }
            catch (System.IO.IOException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error opening task file. Exception message: " + ex.Message, "Unanticipated Error Opening Task File");
                return null;
            }

        }
        /// <summary>
        /// Opens a specific task file
        /// </summary>
        /// <param name="fileName"></param>
        private static bool OpenTaskFileAsCurrentTask(string fileName)
        {
            string filePath = _taskDIR + fileName + FILE_EXTENTION;

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();

                Task newTask = (Task)bFormatter.Deserialize(fs);

                SetCurrentTask(newTask);
                fs.Close();
                return true;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Task file not found. Exception message: " + ex.Message, "Task File Not Found");
                return false;
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Task file directory not found. Exception message: " + ex.Message, "Directory Not Found");
                return false;
            }
            catch (System.IO.IOException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error opening task file. Exception message: " + ex.Message, "Unanticipated Error Opening Task File");
                return false;
            }

        }


        /// <summary>
        /// Saves a task file to disk
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private static bool SaveTaskFile(Task task)
        {
            bool saveSucceeded = false;
            string filePath = _taskDIR + task.FileName + FILE_EXTENTION;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(fs, task);
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

        #endregion

        #region Methods - Event Actions

        static void Task_Remote_DataManager_TaskDeleteRequestedByClient(int index)
        {
            if (_taskFileNamesList[index] != null)
            {
                if (System.IO.File.Exists(_taskDIR + _taskFileNamesList[index] + FILE_EXTENTION))
                    System.IO.File.Delete(_taskDIR + _taskFileNamesList[index] + FILE_EXTENTION);
                Remote_DataManager_TaskListRequestedByClient();
            }
        }

        /// <summary>
        /// When the client requests a list of tasks
        /// </summary>
        static void Remote_DataManager_TaskListRequestedByClient()
        {
            if (ReadListOfTaskFiles()) // Try to read the list of task files, if successful make it avaliable
            {
                List<Task> taskList = new List<Task>();
                foreach (String file in _taskFileNamesList)
                {
                    Task t = OpenTaskFile(file);
                    if (t != null)
                        taskList.Add(t);
                }
                _remote_DataManager.ServerSetTaskList(taskList);
                _remote_DataManager.ServerSetTaskNameList(_taskFileNamesList);
            }
        }
        /// <summary>
        /// When the client requests that a task be set
        /// </summary>
        /// <param name="taskFileNameEntry"></param>
        static void Remote_DataManager_TaskSetByClient(int taskFileNameEntry)
        {
            if(_readOnlyPlaybackMode == false) // provided playback mode isn't enabled then let the client pick a task
                SelectTaskFile(taskFileNameEntry);
        }


        static void Remote_DataManager_TaskSaveRequestedByClient(Task newTask)
        {
            if (SaveTaskFile(newTask))
                SetCurrentTask(newTask);
        }

        #endregion
    }
}

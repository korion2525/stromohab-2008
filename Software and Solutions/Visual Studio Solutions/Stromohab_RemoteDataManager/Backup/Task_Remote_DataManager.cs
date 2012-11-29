using System;
using System.Collections.Generic;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Remote_DataManager
{
    /// <summary>
    /// Handles exchanging task data between the server and task designer/client
    /// </summary>
    public class Task_Remote_DataManager : MarshalByRefObject
    {
        #region Member Variables
        /// <summary>
        /// A list of the available tasks
        /// </summary>
        private static List<string> _taskNamesList = new List<string>();
        /// <summary>
        /// The current running task
        /// </summary>
        private static Task _currentTask = new Task();
        /// <summary>
        /// All of the tasks
        /// </summary>
        private static List<Task> _taskList = new List<Task>();
        #endregion

        #region Properties
        /// <summary>
        /// The current task
        /// </summary>
        public Task Task
        {
            get { return _currentTask; }
            set { _currentTask = value; }
        }
        /// <summary>
        /// List of the task names
        /// </summary>
        public List<string> ListOfTasksNames
        {
            get { return _taskNamesList; }
            set { _taskNamesList = value; }
        }
        /// <summary>
        /// List of all the tasks
        /// </summary>
        public List<Task> Tasks
        {
            get { return _taskList; }
        }
        #endregion

        #region CalledByServer
        /// <summary>
        /// Sets the task list
        /// </summary>
        /// <param name="taskList"></param>
        public void ServerSetTaskList(List<Task> taskList)
        {
            _taskList = new List<Task>(taskList);
        }

        /// <summary>
        /// Sets the task for the client
        /// </summary>
        /// <param name="task"></param>
        public void ServerSetTask(Task task)
        {
            _currentTask = task;
        }
        /// <summary>
        /// Makes the list of task names availiable to the client
        /// </summary>
        /// <param name="taskList"></param>
        public void ServerSetTaskNameList(List<string> taskList)
        {
            _taskNamesList = taskList;
        }


        #endregion

        #region CalledByClient
        public delegate void TaskRequestByClientEventHandler(int taskFileNameEntry);

        /// <summary>
        /// Requests a specific task from the server
        /// </summary>
        /// <param name="taskFileNameEntry"></param>
        public void ClientRequestTask(int taskFileNameEntry)
        {
            if (TaskRequestByClient != null)
                TaskRequestByClient(taskFileNameEntry);
        }

        public static event TaskRequestByClientEventHandler TaskRequestByClient;


        public delegate void TaskListRequestedByClientEventHandler();

        /// <summary>
        /// Requests the task list from the server
        /// </summary>
        public void ClientRequestTaskList()
        {
            if (TaskListRequestedByClient != null)
                TaskListRequestedByClient();
        }

        public static event TaskListRequestedByClientEventHandler TaskListRequestedByClient;


        /// <summary>
        /// Saves a task from the client onto the server
        /// </summary>
        /// <param name="newTask"></param>
        public void ClientRequestSaveTask(Task newTask)
        {
            if (TaskSaveRequestedByClient != null)
                TaskSaveRequestedByClient(newTask);
        }
        public delegate void TaskSaveRequestedByClientEventHandler(Task newTask);

        public static event TaskSaveRequestedByClientEventHandler TaskSaveRequestedByClient;

        /// <summary>
        /// Deletes the task stored in the location in the list of task names
        /// </summary>
        /// <param name="index"></param>
        public void ClientRequestDeleteTask(int index)
        {
            if (TaskDeleteRequestedByClient != null)
                TaskDeleteRequestedByClient(index);
        }
        public delegate void TaskDeleteRequestedByClientEventHandler(int index);

        public static event TaskDeleteRequestedByClientEventHandler TaskDeleteRequestedByClient;

        #endregion
    }
}

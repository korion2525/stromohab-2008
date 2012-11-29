using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Messaging;

namespace StromoLight_TaskDesigner
{
    static class Externals
    {
        /// <summary>
        /// Attempts to load the Visualiser.
        /// </summary>
        /// <param name="objectsToDraw"></param>
        public static void LoadVisualiser(StromoLight_RemoteDrawingList.DrawingList objectsToDraw)
        {
            string visualiserPath;
            try
            {
                objectsToDraw.Initialise();
            }
            catch (System.Net.Sockets.SocketException socketException)
            {
                System.Diagnostics.Debug.WriteLine("The Visualiser is not running. Attempting to start the Visualiser. Exeception message: " + socketException.Message);
                try
                {
                    if (System.IO.File.Exists("StromoLight_Visualiser.exe")) // installed to same dir
                        visualiserPath = new DirectoryInfo(Environment.CurrentDirectory) + @"\StromoLight_Visualiser.exe";
                    else // otherwise running from visual studio solutions
                    {
                        DirectoryInfo dInfo = new DirectoryInfo(Environment.CurrentDirectory);
                        // go back down towards the root looking for Visual studio solutions, if you get to the root then break
                        while (dInfo.Name != "Visual Studio Solutions")
                        {
                            if (dInfo.FullName == dInfo.Root.Name)
                                break;
                            dInfo = dInfo.Parent;
                        }
                        #if(DEBUG)
                            //go back up towards the visualiser
                            visualiserPath = dInfo.FullName + @"\StromoLight_Visualiser\StromoLight_Visualiser\bin\Debug\StromoLight_Visualiser.exe";                     
                        #endif
                        #if(!DEBUG)
                            //go back up towards the visualiser
                            visualiserPath = dInfo.FullName + @"\StromoLight_Visualiser\StromoLight_Visualiser\bin\Release\StromoLight_Visualiser.exe";
                        #endif
                    }
                    if (File.Exists(visualiserPath)) // Check that its been found
                    {
                        System.Diagnostics.Process.Start(visualiserPath);
                    }
                    else
                    {
                        throw new System.IO.FileNotFoundException("Cannot find the visualiser");
                    }

                }
                catch (System.IO.IOException ioException)
                {
                    System.Windows.Forms.MessageBox.Show("Visualiser not found. Please start manually. Exception message: " + ioException.Message,"IO Exception");
                    System.Diagnostics.Debug.WriteLine("Exception Message: " + ioException.Message);
                }
            }
        }

        /// <summary>
        /// Loads the default StromoLight task.
        /// </summary>
        /// <param name="objectsToDraw"></param>
        public static void LoadDefaultTask(StromoLight_RemoteDrawingList.DrawingList objectsToDraw)
        {
            bool defaultTaskFileFound = false;
            string defaultTaskPath = null;
            try
            {
                DirectoryInfo defaultTaskDirectoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
                defaultTaskDirectoryInfo = defaultTaskDirectoryInfo.Parent;
                defaultTaskPath = defaultTaskDirectoryInfo.FullName + @"\Tasks\tightropeTest.spf";

                //If running in release enviroment
                if (File.Exists(defaultTaskPath))
                {
                    defaultTaskFileFound = true;
                }


            }
            catch (DirectoryNotFoundException ex)
            {

                System.Windows.Forms.MessageBox.Show("Unable to find default task file. Default task not loaded. Exception message: " + ex.Message, "Default Task File Missing");
            }

            if (defaultTaskFileFound == true)
            {
                //Pause to allow the visualiser to initialise
                System.Threading.Thread.Sleep(5000);

                //Load the default task
                objectsToDraw.ObjectsToDraw = TaskFileIO.Open(defaultTaskPath);

                //if the task doesn't load
                while (objectsToDraw.ObjectsToDraw.Count <= 1)
                {
                    //keep trying until it does.
                    objectsToDraw.ObjectsToDraw = TaskFileIO.Open(defaultTaskPath);
                }
            }

        }

        /// <summary>
        /// Sends a startup notification to the StromoLight queue.
        /// </summary>
        public static void SendNotification(string messageToSend)
        {
            MessageQueue startupQueue = null;
            string startupQueueName = @".\Private$\StromoLight_Startup";

            if (MessageQueue.Exists(startupQueueName))
            {
                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");
                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = messageToSend;
                message.Label = "Message from Task Designer";
                startupQueue.Send(message);
            }
            else
            {
                startupQueue = MessageQueue.Create(@".\Private$\StromoLight_Startup");

                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");

                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = messageToSend;
                message.Label = "Message from Task Designer";
                startupQueue.Send(message);
            }
        }

        public static bool VisualiserLoaded()
        {
            MessageQueue startupQueue = null;
            string startupQueueName = @".\Private$\StromoLight_Startup";

            if (MessageQueue.Exists(startupQueueName))
            {
                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");
                System.Messaging.Message peekedMessage = startupQueue.Peek(System.TimeSpan.FromSeconds(20));

                object lockObject = new object();
                string peekedMessageBody;

                lock (lockObject)
                {
                    peekedMessage.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                    peekedMessageBody = peekedMessage.Body.ToString();
                }

                //return true if visualiser started ok
                if (peekedMessageBody == "VSOK")
                {
                    return (true);
                }

            }
            return (false);
        }
    }
}

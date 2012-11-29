using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Messaging;
using System.IO;
using System.Threading;

using Win32Utilities;

namespace StromoLight_Package.Forms
{
    public partial class Form_SplashScreen : Form
    {
        private MessageQueue m_startupQueue = null;
        private string m_startupQueueName = @".\Private$\StromoLight_Startup";
        private const int NUMBER_OF_MODULES = 3;
        private bool[] m_startupOK = new bool[NUMBER_OF_MODULES] { false, false, false };

        /// <summary>
        /// Main StromoLight Loading Screen.
        /// </summary>
        public Form_SplashScreen()
        {
            InitializeComponent();

            //Setup messaging queue for confirmation messages.
            InitialiseMessaging();
            m_startupQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(m_startupQueue_ReceiveCompleted);

            if ((MessageBox.Show("Start Task Designer?", "Load Module?", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                StartModule("TaskDesigner");
            }
            else
            {
                m_startupOK[0] = true;
                m_startupOK[2] = true;
            }
            if ((MessageBox.Show("Start Diagnostics?", "Load Module?", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                StartModule("TightropeDiagnostics");
            }
            else
            {
                m_startupOK[1] = true;
            }
            

            //StartTaskDesigner();

            //StartDiagnostics();

            //Start looking for startup OK confirmation messages.

            Thread confirmationThread = new Thread(new ThreadStart(WaitForConfirmation));
            confirmationThread.Start();

            

        }

        private void WaitForConfirmation()
        {
            while ((m_startupOK[0] != true) || (m_startupOK[1] != true) || (m_startupOK[2] != true))
            {
                m_startupQueue.BeginReceive(new TimeSpan(0,0,60));
            }
            HideLoadingScreen();
            AlignWindows();
        }

        private void AlignWindows()
        {
            //IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            //IntPtr taskDesignerWindowHandle = Win32.FindWindow(null, "StromoLight Task Designer");
            //IntPtr diagnosticWindowHandle = Win32.FindWindow(null, "StromoLight Diagnostics");

            //Win32.RECT taskDesignerRect = new Win32.RECT();
            //Win32.GetWindowRect(taskDesignerWindowHandle, out taskDesignerRect);

            //Win32.RECT diagnosticWindowRect = new Win32.RECT();
            //Win32.GetWindowRect(diagnosticWindowHandle, out diagnosticWindowRect);

            //if (taskDesignerWindowHandle != (IntPtr)0 && diagnosticWindowHandle != (IntPtr)0)
            //{
            //    Win32.SetWindowPos(visualiserWindowHandle, Win32.HWND_TOP, taskDesignerRect.Right, diagnosticWindowRect.Bottom, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - taskDesignerRect.Right, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom - diagnosticWindowRect.Bottom, Win32.SWP_SHOWWINDOW);
            //}


        }

        /// <summary>
        /// 
        /// </summary>
        private void InitialiseMessaging()
        {
            if (MessageQueue.Exists(m_startupQueueName))
            {
                //MessageQueue.Delete(m_startupQueueName);

               // m_startupQueue = MessageQueue.Create(m_startupQueueName, true);

                m_startupQueue = new System.Messaging.MessageQueue(m_startupQueueName);
                m_startupQueue.Purge();
            }
            else
            {
                m_startupQueue = MessageQueue.Create(m_startupQueueName, false);
            }

        }

        private void m_startupQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            string receivedMessage;
            object lockObject = new object();

            lock (lockObject)
            {
                e.Message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                receivedMessage = e.Message.Body.ToString();
            }

            if (receivedMessage == "TDSOK")
            {
                m_startupOK[0] = true;
            }

            if (receivedMessage == "DSOK")
            {
                m_startupOK[1] = true;
            }

            if (receivedMessage == "VSOK")
            {
                m_startupOK[2] = true;
            }

        }

        private void HideLoadingScreen()
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate() { HideLoadingScreen(); }));
            }
            else
            {
                this.Close();
            }
        }

        private void StartModule(string moduleName)
        {
            string modulePath;
            try
            {
                //Standard release - looks for executable in Deployment directory structure
                {
                    DirectoryInfo dInfo = new DirectoryInfo(Environment.CurrentDirectory);
                    dInfo = dInfo.Parent;
                    modulePath = dInfo.FullName + @"\StromoLight_" + moduleName + @"\StromoLight_" + moduleName + ".exe";
                }
                //Debug release - looks for executable in Development directory structure
#if (DEBUG)
                {
                    DirectoryInfo dInfo = new DirectoryInfo(Environment.CurrentDirectory);
                    dInfo = dInfo.Parent.Parent.Parent.Parent;

                    if (moduleName != "TightropeDiagnostics")
                    {
                        modulePath = dInfo.FullName + @"\StromoLight_" + moduleName + @"\StromoLight_" + moduleName + @"\bin\Release\StromoLight_" + moduleName + ".exe";
                    }
                    else
                    {
                        modulePath = dInfo.FullName + @"\StromoLight_" + moduleName + @"\StromoLight_" + moduleName + @"\bin\Debug\StromoLight_" + moduleName + ".exe";
                    }
                }
#endif


                System.Diagnostics.Process.Start(modulePath);

            }
            catch (System.IO.IOException ioException)
            {
                MessageBox.Show(moduleName + " not found. Please start manually. Exception Message: " + ioException.Message, moduleName.ToUpper() + " Loading Failure. Exception Type: " + ioException.GetType().ToString());
                Environment.Exit(-1);
            }
            catch (System.ComponentModel.Win32Exception win32Exception)
            {
                MessageBox.Show(moduleName + " not found. Please start manually. Exception Message: " + win32Exception.Message, moduleName.ToUpper() + " Loading Failure. Exception Type: " + win32Exception.GetType().ToString());
                Environment.Exit(-1);
            }
        }

    }
}

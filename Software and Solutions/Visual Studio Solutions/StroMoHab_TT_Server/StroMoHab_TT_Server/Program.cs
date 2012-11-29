using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;
using System.Collections;
using System.Runtime.Serialization.Formatters;

namespace StroMoHab_TT_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;




            try
            {
                //setup the channel and register remoting objects
                ChannelServices.RegisterChannel(new TcpChannel(8005), false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(StroMoHab_Remote_DataManager.Task_Remote_DataManager), "TaskRemoteDataManagerConnection", WellKnownObjectMode.Singleton);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(StroMoHab_Remote_DataManager.Patient_Remote_DataManager), "PatientRemoteDataManagerConnection", WellKnownObjectMode.Singleton);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(StroMoHab_Remote_DataManager.Clinician_Remote_DateManager), "ClinicianRemoteDataManagerConnection", WellKnownObjectMode.Singleton);
            }
            catch(System.Net.Sockets.SocketException e)
            {
                if (e.ErrorCode == 10048) // Only one instance allowed so the server is already running
                {
                    MessageBox.Show("Another instance of StroMoHab Server is already running.\nPress OK to exit.", "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(1);
                }
                else // Some un expected error
                {
                    MessageBox.Show(e.Message, "StroMoHab Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-1);
                }
            }

            Application.Run(new Forms.GUI());
        }
    }
}

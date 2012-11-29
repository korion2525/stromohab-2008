using System;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

using SdlDotNet.Core;

using StromoLight_Visualiser.Forms;
using StroMoHab_Objects.Graphics;
using System.Collections;
using System.Windows.Forms;

namespace StromoLight_Visualiser
{
    class Program
    {
        /// <summary>
        /// Entry point of the program
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //Form_SplashScreen splashScreen = new Form_SplashScreen();
            //splashScreen.Show();

            //Enable visual styles so that any windows forms look the same as they would on other stromohab applications
            //e.g. the find server dialog
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            TcpChannel tcpChannel = new TcpChannel(8002);
            
            ChannelServices.RegisterChannel(tcpChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(StromoLight_RemoteDrawingList.DrawingList), "TaskDesignerConnection", WellKnownObjectMode.Singleton);


            SDL_Main sdlMain = new SDL_Main();

            sdlMain.StartSDL();
            //sdlMain.SendStartupNotification();  TODO: include again once message queues have been investigated further (Bug #2).

            //splashScreen.StartCountdown();

            //Start the application loop (Game Loop)

            Events.Run();

        }

    }
}
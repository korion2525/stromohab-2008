using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StroMoHab_Objects.Communication;

namespace StroMoHab_Client
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

            //Check and alert the user if the screen is too small
            int minWidth = 1280;
            int minHeight = 768;
            int width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if(width < minWidth || height < minHeight)
                MessageBox.Show("WARNING!\nThe avaliable screen area (" + width + " x " + height + ") is less than the minimum recomended area (" + minWidth + " x " + minHeight + ")\n\nYou may encounter automatic layout issuse", "StroMoHab Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //Connect to the server
            TCPProcessor.ManagedConnectToServer();
            
            Application.Run(new GUI());
        }
    }
}

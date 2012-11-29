using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StroMoHab_Setup_Packages
{
    static class Program
    {
        static string targetdir = null;
        static bool silent = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Get the installatin directory from the installer
            if (args.Length > 0)
                targetdir = args[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI(targetdir));
        }
    }
}

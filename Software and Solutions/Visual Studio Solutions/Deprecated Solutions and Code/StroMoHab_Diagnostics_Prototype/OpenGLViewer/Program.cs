using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenGLViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SDL sdl = new SDL();
            SdlDotNet.Core.Events.Run();
        }
    }
}

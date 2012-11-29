using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Win32Utilities;

namespace StroMoHab_Task_Designer
{
    /// <summary>
    /// Contains functions to align the windows correctly.
    /// </summary>
    public static class WindowAlignment
    {
        /// <summary>
        /// Brings the visualiser to the front of all windows, and aligns the windows.
        /// </summary>
        public static void BringVisualiserToFrontAndAlignWindows()
        {
            //Look for the visuliser window
            IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");

            //If visualiser window hasn't opened yet, keep looking until it does
            while (visualiserWindowHandle == new IntPtr(0))
            {
                System.Threading.Thread.Sleep(20);
                visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            }

            //Get a Win32 RECT containing the visualiser window properties
            Win32.RECT visualiserRect = new Win32.RECT();
            Win32.GetWindowRect(visualiserWindowHandle, out visualiserRect);


            //If the visualiser window hasn't finished initalising, keep getting it's properties until it has initialised
            while (visualiserRect.Left == 0)
            {
                System.Threading.Thread.Sleep(20);
                Win32.GetWindowRect(visualiserWindowHandle, out visualiserRect);
            }


            Win32.SetWindowPos(visualiserWindowHandle,
                        Win32.HWND_TOP,
                        (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2) + 4,
                        5,
                        (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2),
                        System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height,
                        Win32.SWP_SHOWWINDOW);
           

            //Bring the visualiser window to the front
            Win32.SetForegroundWindow(visualiserWindowHandle);

        }
    
    
    }
}

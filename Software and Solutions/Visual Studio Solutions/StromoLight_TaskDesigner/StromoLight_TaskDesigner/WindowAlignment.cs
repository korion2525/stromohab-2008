using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Win32Utilities;

namespace StromoLight_TaskDesigner
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

            //Find the task designer window
            IntPtr taskDesignerWindowHandle = Win32.FindWindow(null, "StromoLight Task Designer");

            Win32.RECT taskDesignerRect = new Win32.RECT();
            Win32.GetWindowRect(taskDesignerWindowHandle, out taskDesignerRect);

            //If the visualiser window hasn't finished initalising, keep getting it's properties until it has initialised
            while (visualiserRect.Left == 0)
            {
                System.Threading.Thread.Sleep(20);
                Win32.GetWindowRect(visualiserWindowHandle, out visualiserRect);
            }
            
            Win32.SetWindowPos(taskDesignerWindowHandle, Win32.HWND_TOP, taskDesignerRect.Left, (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom - taskDesignerRect.Bottom), taskDesignerRect.Right, taskDesignerRect.Bottom, Win32.SWP_SHOWWINDOW);

            if ((taskDesignerWindowHandle != (IntPtr)0) && (visualiserWindowHandle != (IntPtr)0))
            {
                //Win32.SetWindowPos(visualiserWindowHandle, Win32.HWND_TOP, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Left + taskDesignerRect.Right, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Bottom - taskDesignerRect.Top, visualiserRect.Right, visualiserRect.Bottom, Win32.SWP_SHOWWINDOW);
            }

            //Bring the visualiser window to the front
            Win32.SetForegroundWindow(visualiserWindowHandle);

            //Bring the task designer window to the front
            Win32.SetForegroundWindow(taskDesignerWindowHandle);
        }
    
    
    }
}

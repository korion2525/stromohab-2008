using System;

using System.Runtime.InteropServices;

namespace Win32Utilities
{
    public class Win32
    {
        #region SetWindowPos
        /// <summary>
        /// Sets the position of a window.
        /// </summary>
        /// <param name="hWnd">The handle of the window to move.</param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(
            IntPtr hWnd,               /// window handle
            IntPtr hWndInsertAfter,    // placement order handle
            int X,                  // horizontal position
            int Y,                  // vertical position
            int cx,                 // width (x)
            int cy,                 // height (y)
            uint uFlags);           // window postioning flags

        //values for hWndInsertAfter:
        /// <summary>
        /// Places the window at the top of the Z order.
        /// </summary>
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        /// <summary>
        /// Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        /// <summary>
        /// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        /// <summary>
        /// Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        //values for uFlags:
        /// <summary>
        /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        public const UInt32 SWP_ASYNCWINDOWPOS = 0x4000;
        /// <summary>
        /// Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        public const UInt32 SWP_NOSIZE = 0x0001;
        /// <summary>
        /// Retains the current position (ignores X and Y parameters).
        /// </summary>
        public const UInt32 SWP_NOMOVE = 0x0002;
        /// <summary>
        /// Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        public const UInt32 SWP_NOZORDER = 0x0004;
        /// <summary>
        /// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        public const UInt32 SWP_NOREDRAW = 0x0008;
        /// <summary>
        /// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        public const UInt32 SWP_NOACTIVATE = 0x0010;
        /// <summary>
        /// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        public const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        /// <summary>
        /// Displays the window.
        /// </summary>
        public const UInt32 SWP_SHOWWINDOW = 0x0040;
        /// <summary>
        /// Hides the window.
        /// </summary>
        public const UInt32 SWP_HIDEWINDOW = 0x0080;
        /// <summary>
        /// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        public const UInt32 SWP_NOCOPYBITS = 0x0100;
        /// <summary>
        /// Does not change the owner window's position in the Z order.
        /// </summary>
        public const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        /// <summary>
        /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        public const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

        #endregion SetWindowPos

        #region GetWindowRect

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(
            IntPtr hWnd,        // the window handle
            out RECT lpRect);   // Win32 RECT - not compatible with System.Drawing.Rectangle

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        #endregion GetWindowRect

        #region FindWindow

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        #endregion FindWindow

        #region SendMessage

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, IntPtr lParam);

        public const UInt32 WM_SYSCOMMAND = 0x0112;
        public const UInt32 SC_CLOSE = 0xF060;

        #endregion SendMessage

        #region SetForegroundWindow

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion SetForegroundWindow

        #region SetFocus

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        #endregion SetFocus
    }
}

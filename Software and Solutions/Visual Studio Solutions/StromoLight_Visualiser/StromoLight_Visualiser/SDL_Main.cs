using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Windows.Forms;

using System.Threading;

using StroMoHab_Objects.Graphics;
using StromoLight_Visualiser.Forms;

using Win32Utilities;

using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Graphics;
using Tao.OpenGl;
using StroMoHab_Objects.Objects;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Messaging;
using System.IO;

namespace StromoLight_Visualiser
{
    /// <summary>
    /// The main SDL engine.
    /// </summary>
    /// <remarks>
    /// Handles the graphics updating and the game logic.
    /// </remarks>
    public class SDL_Main
    {
        #region Member Variables;
        //Width of screen
        private int m_width = 1360;
        //Height of screen
        private int m_height = 768;
        //Bits per pixel of screen
        private int m_bpp = 24;
        //Surface to render on
        private Surface m_screen;
        //List of objects to draw
        private static StromoLight_RemoteDrawingList.DrawingList m_objectsToDraw = new StromoLight_RemoteDrawingList.DrawingList();

        private int _videoPlaybackSpeed = 41;
        
        //Flags
        bool m_backfaceCulling;
        bool m_walkBounce = false;
        bool m_drawCollisionModels = false;
        bool m_fullScreen = true;
        bool m_lightingEnabled=false;
        bool m_useTrackables = true;

        /// <summary>
        /// When set to false the standar avatar is used, when true the trackable and joint based 3D avatar is used
        /// </summary>
        bool m_drawAvatar3D = false;

        bool m_drawAvatar = true;

        //Number of textures to load
        const int NUMBER_OF_TEXTURES = 5;
        //Texture Array
        static int[] textures = new int[NUMBER_OF_TEXTURES];

        //Lighting:
        float[] m_lightPosition;
        float[] m_lightAmbient;
        float[] m_lightDiffuse;
        float[] m_spotDirection;

        //Shadows
        const int INFINITY = 100;

        private List<StroMoHab_Objects.Objects.Trackable> m_trackableList = new List<StroMoHab_Objects.Objects.Trackable>();
        private List<StroMoHab_Objects.Objects.Joint> m_jointList = new List<StroMoHab_Objects.Objects.Joint>();

        Avatar avatar = new Avatar(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        StroMoHab_Avatar_Object.Avatar avatar3D = new StroMoHab_Avatar_Object.Avatar();

        Form_TaskDesigner_0 m_frmTaskDesigner_0 = new Form_TaskDesigner_0(Video.WindowHandle);

        Quad_FullScreen backgroundQuad = null;
        
        private VideoTextureManager m_VideoTextureManager = null;


        #endregion Member Variables

        #region Properties

        /// <summary>
        /// Width of Window
        /// </summary>
        protected int Width
        {
            get
            {
                return m_width;
            }
        }

        /// <summary>
        /// Height of Window
        /// </summary>
        protected int Height
        {
            get
            {
                return m_height;
            }
        }

        /// <summary>
        /// Bits per pixel of surface
        /// </summary>
        protected int BitsPerPixel
        {
            get
            {
                return this.m_bpp;
            }
        }

        /// <summary>
        /// Code Title
        /// </summary>
        public static string Title
        {
            get
            {
                return "StromoLight Visualiser";
            }
        }

        /// <summary>
        /// Contains all loaded textures
        /// </summary>
        public static int[] TextureArray
        {
            get
            {
                return textures;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Adds an OpenGl object to the scene
        /// </summary>
        /// <param name="objectToAdd"></param>
        public static void AddObjectToDrawList(OpenGlObject objectToAdd)
        {
            m_objectsToDraw.Add(objectToAdd);
        }

        #endregion Public Methods

        #region Constructors
        /// <summary>
        /// Basic constructor
        /// </summary>
        public SDL_Main()
        {
            AlignWindows();

            string[] commandLineArgs = Environment.GetCommandLineArgs();
            bool fullscreen = false;

            foreach (string currentArg in commandLineArgs)
            {
                if (currentArg == "-f")
                {
                    fullscreen = true;
                }
            }

            Initialise(fullscreen);

            
            Connect();
            StroMoHab_Objects.Communication.TCPProcessor.AvatarSpeedChangedEvent += new StroMoHab_Objects.Communication.TCPProcessor.AvatarSpeedChangedHandler(TCPProcessor_AvatarSpeedChangedEvent);

            //task designer events:
            m_frmTaskDesigner_0.CurrentDistanceChanged += new Form_TaskDesigner_0.CurrentDistanceChangedHandler(m_frmTaskDesigner_0_CurrentDistanceChanged);

            StromoLight_RemoteDrawingList.DrawingList.AvatarMovedInTaskDesigner += new StromoLight_RemoteDrawingList.DrawingList.AvatarMovedInTaskDesignerEventHandler(DrawingList_AvatarMovedInTaskDesigner);

            StroMoHab_Objects.Communication.TCPProcessor.TrackableListReceivedEvent += new StroMoHab_Objects.Communication.TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
            StroMoHab_Objects.Communication.TCPProcessor.JointListReceivedEvent += new StroMoHab_Objects.Communication.TCPProcessor.JointListReceivedHandler(TCPProcessor_JointListReceivedEvent);

            Events.Quit += new EventHandler<QuitEventArgs>(Events_Quit);

        }



        #region Task Designer Event Handlers

        private void m_frmTaskDesigner_0_CurrentDistanceChanged(float newDistance)
        {
            avatar.Z = -newDistance;
        }

        #endregion Task Designer Event Handlers




        #endregion Constructors

        #region Connect

        private void Connect()
        {
            StroMoHab_Objects.Communication.TCPProcessor.ManagedConnectToServer();
            StroMoHab_Objects.Communication.SendMCECommand.StartCameras();
            StroMoHab_Objects.Communication.SendMCECommand.ForceTreadmillSpeedUpdate();
        }

        #endregion Connect

        #region Setup

        #region Initialise
        /// <summary>
        /// Initialises common methods
        /// </summary>
        protected void Initialise(bool fullscreen)
        {
            //Sets keyboard events
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(this.KeyDown);
            Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(Events_KeyboardUp);

            //Sets the ticker to update OpenGL context
            Events.Tick += new EventHandler<TickEventArgs>(this.Tick);
            //Registers Quit event
            Events.Quit += new EventHandler<QuitEventArgs>(this.Quit);

            //Set the Frames per Second
            Events.Fps = 500;

            //Enable Key Repeating
            Keyboard.EnableKeyRepeat(20, 1);

            //Sets Window icon and Title
            this.WindowAttributes();

            Rectangle lastScreenBounds;

            //if (Screen.AllScreens.Length > 1)
            if (Screen.AllScreens[0].Primary == false && fullscreen==true)
            {
                //lastScreenBounds = Screen.AllScreens[Screen.AllScreens.Length - 1].Bounds;
                lastScreenBounds = Screen.AllScreens[0].Bounds;
            }
            else
            {
                try
                {
                    lastScreenBounds = Screen.AllScreens[1].Bounds;
                    fullscreen = false;
                }
                catch (IndexOutOfRangeException ex)
                {
                    lastScreenBounds = Screen.AllScreens[(Screen.AllScreens.Length - 1)].Bounds;
                    fullscreen = false;
                    System.Diagnostics.Debug.WriteLine("Only one screen appears to be connected. Exception Message: " + ex.Message);
                }
            }

            //if (lastScreenBounds.X == 0 && Screen.AllScreens.Length > 1)
            //{
            //    lastScreenBounds = Screen.AllScreens[Screen.AllScreens.Length - 2].Bounds;
            //}

            if (fullscreen)
            {
                //Maximise the screen to the second monitor
                m_screen = Video.SetVideoMode(lastScreenBounds.Width, lastScreenBounds.Height, m_bpp, true, true, false, true, false);
                Win32.SetWindowPos(Video.WindowHandle, Win32.HWND_NOTOPMOST, lastScreenBounds.X, lastScreenBounds.Y, lastScreenBounds.Width, lastScreenBounds.Height, Win32.SWP_SHOWWINDOW);
            }
            else
            {
                IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");

                int x = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2) + 4;
                int y = 5;
                int width = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2) - 16;
                int height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 38;
                Win32.SetWindowPos(visualiserWindowHandle,
                        Win32.HWND_TOP,
                        x,
                        y,
                        width,
                        height,
                        Win32.SWP_SHOWWINDOW);

                m_screen = Video.SetVideoMode(width, height, m_bpp, false, true, false, true, true);
                
            }
            m_fullScreen = fullscreen;

            Transformation transform = new Transformation(90);

            //force toggle methid
            //m_fullScreen = true;
            //ToggleFullscreen();
        }

        #endregion Initialise

        #region Window Attributes
        /// <summary>
        /// Sets the Application Icon and Window Title
        /// </summary>
        protected void WindowAttributes()
        {
            Video.WindowIcon();
            Video.WindowCaption = Title;
        }

        /// <summary>
        /// Centres the SDL graphics windows on the screen.
        /// </summary>
        private void AlignWindows()
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;

            int x = (rect.Width / 2) - (m_width / 4);
            int y = (rect.Height / 2) - (m_height / 4);

            //Win32.SetWindowPos(Video.WindowHandle, Win32.HWND_NOTOPMOST, x, y, this.Width, this.Height, Win32.SWP_SHOWWINDOW);

            //IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            //IntPtr taskDesignerWindowHandle = Win32.FindWindow(null, "StromoLight Task Designer");
            //IntPtr diagnosticWindowHandle = Win32.FindWindow(null, "StromoLight Diagnostics");

            //if ((visualiserWindowHandle != null) && (taskDesignerWindowHandle != null) && (diagnosticWindowHandle != null))
            //{
            //    Win32.RECT visualiserWindowRect = new Win32.RECT();
            //    Win32.GetWindowRect(visualiserWindowHandle, out visualiserWindowRect);

            //    Win32.RECT taskDesignerRect = new Win32.RECT();
            //    Win32.GetWindowRect(taskDesignerWindowHandle, out taskDesignerRect);

            //    Win32.RECT diagnosticWindowRect = new Win32.RECT();
            //    Win32.GetWindowRect(diagnosticWindowHandle, out diagnosticWindowRect);

            //    Win32.SetWindowPos(visualiserWindowHandle, Win32.HWND_TOP, taskDesignerRect.Right, diagnosticWindowRect.Bottom,System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width+taskDesignerRect.Right, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom, Win32.SWP_SHOWWINDOW);
            //}


        }
        #endregion Window Attributes

        #region Reshape
        /// <summary>
        /// Resizes the OpenGL scene whenever the window has been resized.
        /// </summary>
        public virtual void Reshape()
        {
            this.Reshape(500.0f);
        }

        /// <summary>
        /// Resizes the OpenGL scene.
        /// </summary>
        /// <param name="distance"></param>
        protected virtual void Reshape(float distance)
        {
            Rectangle lastScreenBounds = new Rectangle();

            if (Screen.AllScreens[0].Primary == false)
            {
                lastScreenBounds = Screen.AllScreens[0].Bounds;
            }
            else
            {
                try
                {
                    lastScreenBounds = Screen.AllScreens[1].Bounds;
                }
                catch (IndexOutOfRangeException ex)
                {
                    lastScreenBounds = Screen.AllScreens[Screen.AllScreens.Length - 1].Bounds;
                    System.Diagnostics.Debug.WriteLine("Only one screen appears to be connected. Exception Message: " + ex.Message);
                }
            }
            
            //Reset the current Viewport
            if (m_fullScreen)
            {
                Gl.glViewport(0, 0, lastScreenBounds.Width, lastScreenBounds.Height);
                //Gl.glViewport(0, 0, m_width, m_height);
            }
            else
            {
                Gl.glViewport(0, 0, m_screen.Width, m_screen.Height);
                //Gl.glViewport(0, 0, m_width / 2, m_height / 2);
            }
            /* Set up the screen for perspective view 
             * (45 degree viewing angle based on the window's width and height)
             * (0.1f and 500.0f (distance) are the start and ending points for how deep we can draw into the screen) */

            //Select the projection matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            //Reset the Projection Matrix
            Gl.glLoadIdentity();
            //Calculate the aspect ration of the window
            //if (m_screen.Width == lastScreenBounds.Width)
            if (m_fullScreen)
            {
                Glu.gluPerspective(45.0f, (m_screen.Width / m_screen.Height), 0.1f, distance);
                //Glu.gluPerspective(45.0f, (m_width / (float)m_height), 0.1f, distance);
                Gl.glRotatef(-90.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                Glu.gluPerspective(45.0f, -(m_screen.Height / m_screen.Width), 0.1f, distance);
                //Glu.gluPerspective(45.0f, ((m_width / 2) / (float)(m_height / 2)), 0.1f, distance);
            }
            //Select the Modelview Matrix
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Reset the Modelview Matrix
            Gl.glLoadIdentity();
        }
        #endregion Reshape

        #region InitGL
        /// <summary>
        /// Initalises the OpenGL context and does all inital setup.
        /// </summary>
        public virtual void InitGL()
        {
            //Enable smooth shading (looks nicer)
            Gl.glShadeModel(Gl.GL_SMOOTH);
            //Enable anti-aliasing
            Gl.glEnable(Gl.GL_MULTISAMPLE);

            //Set the color the screen clears to (light blue) **not sure why tutorial sets alpha to 0.5f**
            Gl.glClearColor(0.5f, 0.5f, 1.0f, 0.5f);
            //Set up the Depth Buffer (depth buffer calculates what to draw in front of what)
            Gl.glClearDepth(1.0f);
            //Enable depth testing
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Set the type of depth testing to do
            Gl.glDepthFunc(Gl.GL_LEQUAL);
            //Make every successful drawing operation place it's values in the depth buffer
            Gl.glDepthMask(Gl.GL_TRUE);

            //Enable 2D Textures
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            //Load and Generate Textures
            Textures.LoadTextures();
            //Textures.LoadVideoTextures();

            //Set the perspective correction (to best - performance hit but looks nicer)
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);

            //Lighting:
            m_lightPosition = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            m_lightAmbient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            m_lightDiffuse = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            m_spotDirection = new float[] { -1.0f, -1.0f, 0.0f };

            //enable the ambient light
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, m_lightAmbient);
            //enable the diffuse light
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, m_lightDiffuse);
            //make a spotlight
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 20.0f);

            //position the light
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, m_lightPosition);
            //enable the light
            Gl.glEnable(Gl.GL_LIGHT1);




        }
        #endregion InitGL


        #endregion Setup

        #region Start SDL

        /// <summary>
        /// Initialises the SDL context.
        /// </summary>
        public void StartSDL()
        {
            this.Reshape();
            this.InitGL();
            this.InitialiseScene();

            m_objectsToDraw.VisualiserLoading = false;
        }

        #endregion Start SDL

        #region Toggle Fullscreen

        private void ToggleFullscreen()
        {
            Rectangle lastScreenBounds = new Rectangle();

            //Assumes system is running on a 2 screen setup.
            if (Screen.AllScreens[0].Primary == false)
            {
                lastScreenBounds = Screen.AllScreens[0].Bounds;
            }
            else
            {
                try
                {
                    lastScreenBounds = Screen.AllScreens[1].Bounds;
                }
                catch (IndexOutOfRangeException) //if no second screen is found
                {
                    MessageBox.Show("Could not move Visualiser - only one screen detected", "Stromohab Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Toggle fullscreen
            if (m_fullScreen)
            {
                m_objectsToDraw.FullScreen = false;
                //log fullscreen status
                m_fullScreen = false;

                //get visualiser window handle
                IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");

                //move the visualiser window
                int x = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2) + 6;
                int y = 29;
                int width = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2) - 12;
                int height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 35;
                Win32.SetWindowPos(visualiserWindowHandle,
                        Win32.HWND_TOP,
                        x,
                        y,
                        width,
                        height,
                        Win32.SWP_SHOWWINDOW);

                m_screen = Video.SetVideoMode(width, height, m_bpp, false, true, false, true, true);


                //TODO: Look at making the visualiser line up with the diagnostics + task designer windows properly. 
                ////attempt to grab StromoLight window handles
                //IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
                //IntPtr taskDesignerWindowHandle = Win32.FindWindow(null, "StromoLight Task Designer");
                //IntPtr diagnosticWindowHandle = Win32.FindWindow(null, "StromoLight Diagnostics");

                ////create rectangles to represent windows
                //Win32.RECT taskDesignerRect = new Win32.RECT();
                //Win32.RECT diagnosticWindowRect = new Win32.RECT();

                ////set rectangles to respective window sizes
                //Win32.GetWindowRect(taskDesignerWindowHandle, out taskDesignerRect);
                //Win32.GetWindowRect(diagnosticWindowHandle, out diagnosticWindowRect);

                ////if both the task designer and the diagnostic windows are open
                //if (taskDesignerWindowHandle != (IntPtr)0 && diagnosticWindowHandle != (IntPtr)0)
                //{
                //    //move the visualiser window to fit around them
                //    m_screen = Video.SetVideoMode(Screen.PrimaryScreen.WorkingArea.Width - taskDesignerRect.Right, Screen.PrimaryScreen.WorkingArea.Height - diagnosticWindowRect.Bottom, m_bpp, false, true, false, true, true);
                //    Win32.SetWindowPos(visualiserWindowHandle, Win32.HWND_TOP, taskDesignerRect.Right, diagnosticWindowRect.Bottom, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - taskDesignerRect.Right, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - diagnosticWindowRect.Bottom, Win32.SWP_SHOWWINDOW);

                //}
                //else //else move the visualiser window to the bottom right quarter of the primary screen.
                //{
                //Win32.SetWindowPos(visualiserWindowHandle, Win32.HWND_TOP, Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, Win32.SWP_SHOWWINDOW);
                //m_screen = Video.SetVideoMode(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2, m_bpp, false, true, false, true, true);
                // }
            }
            else //(if not currently fullscreen)
            {
                m_objectsToDraw.FullScreen = true;
                //log fullscreen status
                m_fullScreen = true;
                //move the visualiser to the second screen and make the same size as it.
                m_screen = Video.SetVideoMode(lastScreenBounds.Width, lastScreenBounds.Height, m_bpp, false, true, false, true, false);
                Win32.SetWindowPos(Video.WindowHandle, Win32.HWND_NOTOPMOST, lastScreenBounds.X, lastScreenBounds.Y, lastScreenBounds.Width, lastScreenBounds.Height, Win32.SWP_SHOWWINDOW);
            }

            //Recreate rendering context as it's lost when you switch between fullscreen and windowed
            Reshape();
            InitGL();
        }

        #endregion Toggle Fullscreen

        #region Event Handlers

        void TCPProcessor_AvatarSpeedChangedEvent(float newSpeed)
        {
            if (newSpeed == 0.0f)
            {
                avatar.Z = 0.0f;
            }

            avatar.Velocity.Z = -newSpeed / 100.0f;
        }

        void Events_Quit(object sender, QuitEventArgs e)
        {
            m_VideoTextureManager.Dispose();
            Events.QuitApplication();
        }

        void TCPProcessor_JointListReceivedEvent(List<StroMoHab_Objects.Objects.Joint> jointList)
        {
            m_jointList = new List<StroMoHab_Objects.Objects.Joint>(jointList);
        }

        void TCPProcessor_ToggleDrawingOfFeetEvent(bool display)
        {
            m_drawAvatar = display;
        }



        void TCPProcessor_TrackableListReceivedEvent(List<StroMoHab_Objects.Objects.Trackable> trackableList)
        {
            m_trackableList = new List<StroMoHab_Objects.Objects.Trackable>(trackableList);
        }

        void DrawingList_AvatarMovedInTaskDesigner(float zPosition)
        {
            avatar.Z = -zPosition;
        }


        #region Input

        private void KeyDown(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    //Fire event to stop the application loop
                    Events.QuitApplication();
                    Events_Quit(this,new QuitEventArgs());
                    break;
                case Key.F1:
                    ToggleFullscreen();         
                    break;
                case Key.F2:
                    //Toggle backface culling
                    if (m_backfaceCulling == false)
                    {
                        Gl.glEnable(Gl.GL_CULL_FACE);
                        Gl.glCullFace(Gl.GL_BACK);
                        m_backfaceCulling = true;
                    }
                    else if (m_backfaceCulling == true)
                    {
                        Gl.glDisable(Gl.GL_CULL_FACE);
                        m_backfaceCulling = false;
                    }
                    break;
                case Key.F3:
                    {
                        if (m_walkBounce)
                        {
                            m_walkBounce = false;
                        }
                        else
                        {
                            m_walkBounce = true;
                        }
                        break;
                    }
                case Key.F4:
                    {
                        if (m_drawCollisionModels)
                        {
                            m_drawCollisionModels = false;
                        }
                        else
                        {
                            m_drawCollisionModels = true;
                        }
                        break;
                    }
                case Key.F5:
                    {
                        if (m_lightingEnabled)
                        {
                            m_lightingEnabled = false;
                            Gl.glDisable(Gl.GL_LIGHTING);
                        }
                        else
                        {
                            m_lightingEnabled = true;
                            Gl.glEnable(Gl.GL_LIGHTING);
                        }
                        break;
                    }

                case Key.F6:
                    {
                        if (m_drawAvatar3D)
                            m_drawAvatar3D = false;
                        else m_drawAvatar3D = true;
                        break;
                    }
                case Key.F7:
                    {
                        if (m_drawAvatar3D)
                        {
                            if (avatar3D.DrawJoints)
                                avatar3D.DrawJoints = false;
                            else avatar3D.DrawJoints = true;
                        }
                        break;
                    }
                case Key.F11:
                    {
                        CaptureAndSendToTaskDesignerScreenShot();
                        break;
                    }
                case Key.F12:
                    {
                        if (m_frmTaskDesigner_0 != null)
                        {
                            m_frmTaskDesigner_0.Show();
                        }
                        break;
                    }
                case Key.Backspace:
                    {
                        InitialiseScene();
                        break;
                    }
                case Key.A:
                    {
                        //avatar.ViewAngleX += 0.2f;
                        break;
                    }
                case Key.D:
                    {
                        //avatar.ViewAngleX -= 0.2f;
                        break;
                    }
                case Key.S:
                    {
                        //avatar.ViewAngleY -= 0.2f;
                        break;
                    }
                case Key.W:
                    {
                        //avatar.ViewAngleY += 0.2f;
                        break;
                    }
                case Key.Z:
                    {
                        //avatar.ViewDistance += 0.02f;
                        break;
                    }
                case Key.X:
                    {
                        //avatar.ViewDistance -= 0.02f;
                        break;
                    }
                case Key.Space:
                    {
                        m_drawAvatar = !m_drawAvatar;
                        break;
                    }

                case Key.UpArrow:
                    {
                        //avatar.Velocity.X = (float)Math.Sin(avatar.RotationAngle * (3.14159265 / 180)) * 0.07f;
                        avatar.Velocity.Z = -(float)Math.Cos(avatar.RotationAngle * (3.14159265 / 180)) * 0.1f;

                        //Add bounce to walking if set
                        if (m_walkBounce)
                        {
                            avatar.WalkBounceAngle += 10.0f;
                            avatar.Velocity.Y = (float)Math.Sin(avatar.WalkBounceAngle * (3.14159265 / 180)) * 0.02f;
                        }                        

                        break;
                    }
                case Key.DownArrow:
                    {
                        //avatar.Velocity.X = -(float)Math.Sin(avatar.RotationAngle * (3.14159265 / 180)) * 0.07f;
                        avatar.Velocity.Z = (float)Math.Cos(avatar.RotationAngle * (3.14159265 / 180)) * 0.1f;

                        //Add bounce to walking if set
                        if (m_walkBounce)
                        {
                            avatar.WalkBounceAngle += 10.0f;
                            avatar.Velocity.Y = -(float)Math.Sin(avatar.WalkBounceAngle * (3.14159265 / 180)) * 0.02f;
                        }

                        break;
                    }
                case Key.LeftArrow:
                    {
                        //avatar.RotationAngle += 2.0f;
                        
                        //Move left
                        //avatar.Velocity.X = 0.07f;

                        //rotAngle += 2.0f;
                    }
                    break;
                case Key.RightArrow:
                    {
                        //avatar.RotationAngle -= 2.0f;
                        
                        //Move right
                        //avatar.Velocity.X = -0.07f;

                        //rotAngle -= 2.0f;
                        break;
                    }
                case Key.O:
                        {
                            _videoPlaybackSpeed = _videoPlaybackSpeed + 5;
                            break;
                        }
                case Key.P:
                        {
                            if (_videoPlaybackSpeed - 5 > 0)
                            {
                                _videoPlaybackSpeed = _videoPlaybackSpeed - 5;
                            }
                            else
                            {
                                _videoPlaybackSpeed = 5;
                            }

                            break;
                        }
            }


        }

        private void Events_KeyboardUp(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Key.UpArrow:
                    avatar.Velocity.Z = 0.0f;
                    m_objectsToDraw.MoveAvatarInTaskDesigner(avatar.Z);
                    break;
                case Key.DownArrow:
                    avatar.Velocity.Z = 0.0f;
                    m_objectsToDraw.MoveAvatarInTaskDesigner(avatar.Z);
                    break;
                case Key.LeftArrow:
                    avatar.Velocity.X = 0.0f;
                    break;
                case Key.RightArrow:
                    avatar.Velocity.X = 0.0f;
                    break;
            }
        }

        #endregion Input

        /// <summary>
        /// Renders the OpenGL context to the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, TickEventArgs e)
        {
            DrawGLScene();
            Video.GLSwapBuffers();
        }

        /// <summary>
        /// Ensures the application loop is properly shut down (this event must be included)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quit(object sender, QuitEventArgs e)
        {
            Events.QuitApplication();
        }
        #endregion Event Handlers

        #region Test Functions

        private void InitialiseScene()
        {
            m_objectsToDraw.Clear();
            m_drawCollisionModels = false;
            avatar.Z = 0.0f;

            //Corridor corridor = new Corridor(-1.5f, 0.0f, 4.0f, 1.5f, 6.0f, -500.0f);

            //m_objectsToDraw.Add(corridor);

            m_VideoTextureManager = new VideoTextureManager(Textures.ListOfTextureFilePaths("jpeg", true));

        }
        #endregion Test Functions

        

        #region Draw Scene
        /// <summary>
        /// All drawing code goes here
        /// </summary>
        protected void DrawGLScene()
        {
            //Clear the screen and the OpenGl Buffers
            Gl.glClearStencil(0);
            Gl.glClearDepth(1.0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);

            DrawVideoBackground();

            Gl.glLoadIdentity();

            //Move Camera to 3rd Person View
            //Gl.glTranslatef(0.0f, -1.5f, avatar.ViewDistance);

            Gl.glTranslatef(0.0f, -1.0f, -3.5f);
            //Gl.glRotatef(45.0f, 1.0f, 0.0f, 0.0f);
            //Gl.glRotatef(avatar.ViewAngleX, 0.0f, 1.0f, 0.0f);
            //Gl.glRotatef(avatar.ViewAngleY, 1.0f, 0.0f, 0.0f);

            //Draw Avatar
            //avatar.Update(m_trackableList);

            if (m_drawAvatar)
            {
                if (m_drawAvatar3D)// When using the 3D avatar 
                {
                    Gl.glPushMatrix();
                    avatar3D.Draw(m_trackableList, m_jointList);
                    Gl.glPopMatrix();
                }
                else // When using the standard avatar
                    avatar.Draw();
            }

            //Move camera in preparation for drawing all world objects
            //Gl.glRotatef(-avatar.RotationAngle, 0.0f, 1.0f, 0.0f);

            //Draw collision models if set
            if (m_drawCollisionModels)
            {
                if (m_drawAvatar3D) // When using the 3D avatar 
                {
                    foreach (BoundingBox collisionModel in avatar3D.CollisionModel)
                    {
                        collisionModel.Draw();
                    }
                }
                else // When using the standard avatar
                {
                    //avatar.CollisionModel.Draw();
                    if (StroMoHab_Objects.Objects.FilteredMarkerList.listOfMarkers.Count != 0)
                    {
                        avatar.leftFoot.CollisionModel.Draw();
                    }
                }
                
            }
            Gl.glTranslatef(avatar.X, avatar.Y, -avatar.Z);

            //Create temporary list to allow modification of master list during drawing.
            List<OpenGlObject> tempList = new List<OpenGlObject>(m_objectsToDraw.ObjectsToDraw);



            //Draw all objects in temporary list
            foreach (OpenGlObject currentObject in tempList)
            {
                currentObject.Draw();
                currentObject.Move();
                //currentObject.RotateX(1.0f);

                if (m_drawCollisionModels)
                {
                    currentObject.CollisionModel.Draw();
                }

                if (m_drawAvatar3D) // When using the 3D avatar 
                {

                    bool collisionOccured = false;
                    foreach (BoundingBox collisionModel in avatar3D.CollisionModel)
                    {
                        bool collision = Collisions.HaveCollided(currentObject.CollisionModel, collisionModel, -avatar.X, avatar.Y, avatar.Z);
                        if (collision)
                        {
                            collisionOccured = true;
                            break; // If one collision has occured then don't need to check other models.
                        }
                    }
                    if (collisionOccured)
                    {
                        //System.Diagnostics.Debug.WriteLine("OBJECT COLLISION!");
                        currentObject.Velocity.Y = 0.005f;
                    }
                }
                else // When using the standard avatar
                {
                    //Test for collisions
                    if ((StroMoHab_Objects.Objects.FilteredMarkerList.listOfMarkers.Count == 2) && (m_useTrackables == false))
                    {
                        bool CollisionOne = Collisions.HaveCollided(currentObject.CollisionModel, avatar.leftFoot.CollisionModel, -avatar.X, avatar.Y, avatar.Z);
                        bool CollisionTwo = Collisions.HaveCollided(currentObject.CollisionModel, avatar.rightFoot.CollisionModel, -avatar.X, avatar.Y, avatar.Z);

                        if (CollisionOne || CollisionTwo)
                        {
                            // System.Diagnostics.Debug.WriteLine("OBJECT COLLISION!");
                            currentObject.Velocity.Y = 0.005f;
                        }
                    }
                }
                if (currentObject is CompoundOpenGlObject)
                {
                    foreach (BoundingBox collisionModel in ((CompoundOpenGlObject)currentObject).CollisionModel)
                    {
                        //Draw collision models if flag set
                        if (m_drawCollisionModels)
                        {
                            collisionModel.Draw();
                        }

                        //Test for collisions
                        //if (Stromohab_MCE.FilteredMarkerList.listOfMarkers.Count != 0)
                        //{
                        //    bool collision = Collisions.HaveCollided(collisionModel, avatar.leftFoot.CollisionModel, -avatar.X, avatar.Y, avatar.Z);
                        //    if (collision == true)
                        //    {
                        //        //System.Diagnostics.Debug.WriteLine("COLLISION!");
                        //        avatar.Velocity.X = -avatar.Velocity.X * 10.0f;
                        //        avatar.Velocity.Y = -avatar.Velocity.Y;
                        //        //avatar.Velocity = new Vector3f(0.0f, 0.0f, 0.0f);
                        //    }
                        //    else
                        //    {
                        //        //System.Diagnostics.Debug.WriteLine("NO COLLISION");
                        //    }
                        //}

                    }
                }

            }


            //Update Game Logic
            avatar.Move();
            //m_VideoTextureManager.MoveToNextFrame();
            //backgroundQuad.TextureNumber = m_VideoTextureManager.CurrentVideoTextureObject.TextureID;

            //avatar.RotateY(1.0f);
        }
        #endregion Draw Scene

        
        Bitmap _prevVideoBackground;
        bool _needToBuffer = false;
        bool _bufferTimerActive = false;

        private void CancelNeedToBuffer(object state)
        {
            _needToBuffer = false;
            _bufferTimerActive = false;
            _videoPlaybackSpeed = _videoPlaybackSpeed + 15;
        }
        static System.Threading.Timer bufferTimer;

        private void DrawVideoBackground()
        {
            Thread.Sleep(_videoPlaybackSpeed);
            Bitmap _currentVideoBackground=null;
            

            if (_needToBuffer == false)
            {
                //get current video background
                _currentVideoBackground = m_VideoTextureManager.FrameQueue.Dequeue();
            }
            else
            {
                if (_bufferTimerActive == false)
                {
                    _bufferTimerActive = true;
                    System.Threading.TimerCallback tcb = new System.Threading.TimerCallback(CancelNeedToBuffer);
                    bufferTimer = new System.Threading.Timer(tcb, null, 3000, System.Threading.Timeout.Infinite);
                }
            }

            //if succeeded in getting background, draw it to screen.
            if (_currentVideoBackground != null)
            {
                //save current background so that it can be redrawn if queue is empty.
                _prevVideoBackground = new Bitmap(_currentVideoBackground);

                DrawBitmapToScreen(_currentVideoBackground);
            }
            else //have been unable to get next frame (because buffer is empty)
            {
                //if previous frame is valid
                if (_prevVideoBackground != null)
                {
                    _needToBuffer = true;

                    DrawBitmapToScreen(_prevVideoBackground);
                }
            }
        }

        private void DrawBitmapToScreen(Bitmap bitmap)
        {
            //rectangle to store image data
            Rectangle imageStorageSpace = default(Rectangle);
            //get bitmap data
            BitmapData bitmapData = new BitmapData();
            imageStorageSpace = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            bitmapData = bitmap.LockBits(imageStorageSpace, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            //draw the bitmap to the screen
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();
            Gl.glOrtho(0.0, m_screen.Width, 0.0, m_screen.Height, -1, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();

            Gl.glRasterPos3i(0, 0, 0);
            float zoomFactorX = (float)(m_screen.Width) / (float)(bitmap.Width);
            float zoomFactorY = (float)(m_screen.Height) / (float)(bitmap.Height);

            Gl.glPixelZoom(zoomFactorX, zoomFactorY);
            Gl.glDrawPixels(bitmap.Width, bitmap.Height, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            Gl.glPixelZoom(1.0f, 1.0f);

            Gl.glPopMatrix();
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPopMatrix();

            //release the image resources
            bitmap.UnlockBits(bitmapData);

        }


        #region Queue Notifications

        /// <summary>
        /// Sends a startup notification to the StromoLight queue.
        /// </summary>
        public void SendStartupNotification()
        {
            MessageQueue startupQueue = null;
            string startupQueueName = @".\Private$\StromoLight_Startup";

            if (MessageQueue.Exists(startupQueueName))
            {
                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");
                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = "VSOK";
                message.Label = "Message from Visualiser";
                startupQueue.Send(message);
            }
            else
            {
                startupQueue = MessageQueue.Create(@".\Private$\StromoLight_Startup");

                startupQueue = new System.Messaging.MessageQueue(@".\Private$\StromoLight_Startup");

                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = "VSOK";
                message.Label = "Message from Visualiser";
                startupQueue.Send(message);
            }
        }

        #endregion Queue Notifications

        #region Screenshot Methods

        private void CaptureAndSaveScreenShot()
        {
            SaveScreenShot(Environment.CurrentDirectory +"\\image.jpg", ImageFormat.Jpeg);
        }
        private void CaptureAndSendToTaskDesignerScreenShot()
        {
            
            m_objectsToDraw.GeneratingScreenShot = true;
            //capture a screenshot and resize it
            Bitmap screenShot = CaptureScreenShot();
            screenShot = ResizeBitmap(screenShot, 180, 240);

            MemoryStream ms = new MemoryStream();
            // Save to memory using the Jpeg format
            screenShot.Save(ms, ImageFormat.Jpeg);

            // read to end and then close the stream
            byte[] bmpBytes = ms.GetBuffer();
            screenShot.Dispose();
            ms.Close();

            //copy the bytes
            m_objectsToDraw.TaskScreenShot = bmpBytes;

            m_objectsToDraw.GeneratingScreenShot = false;

        }
        /// <summary>
        /// Saves a screen capture in the specified image format to a file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        private void SaveScreenShot(string filename, ImageFormat format)
        {
            Bitmap screenShot = CaptureScreenShot();
            screenShot = ResizeBitmap(screenShot, 180, 240);
            screenShot.Save(filename, format);
        }

        /// <summary>
        /// Saves a picture of the screen to a bitmap image.
        /// </summary>
        /// <returns>The saved bitmap.</returns>
        private Bitmap CaptureScreenShot()
        {
            //TODO Moddify to get the image directly from the buffer

            // create the bitmap to copy the screen shot to
            Bitmap bitmap = new Bitmap(m_screen.Width, m_screen.Height);

            IntPtr visualiserWindowHandle = Win32.FindWindow(null, "StromoLight Visualiser");
            Win32.RECT visRect = new Win32.RECT();
            Win32.GetWindowRect(visualiserWindowHandle, out visRect);
            

            // now copy the screen image to the graphics device from the bitmap
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(new Point(visRect.Left+3,visRect.Top+25), Point.Empty, m_screen.Size);
            }
            return bitmap;
        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }
            return result;
        }

        #endregion Screenshot Methods
    }

}

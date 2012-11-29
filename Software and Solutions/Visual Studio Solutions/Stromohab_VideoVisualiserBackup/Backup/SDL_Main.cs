using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Windows.Forms;

using System.Threading;

using StroMoHab_Objects.Graphics;
using StroMoHab_Objects.Interfaces;

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
        private static StromoLight_RemoteDrawingList.DrawingList _remoteDrawingList = new StromoLight_RemoteDrawingList.DrawingList();

        const float NEAR_CLIPPING_PLANE_DISTANCE = 0.1f;
        const float FAR_CLIPPING_PLANE_DISTANCE = 500f;
        
        //Flags
        bool m_useTrackables = true;

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
       

        Form_TaskDesigner_0 m_frmTaskDesigner_0 = new Form_TaskDesigner_0(Video.WindowHandle);

        private CheckError _checkErrors = new CheckError();

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
            _remoteDrawingList.Add(objectToAdd);
        }

        #endregion Public Methods


        #region Constructors
        /// <summary>
        /// Basic constructor
        /// </summary>
        public SDL_Main()
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            bool fullscreen = false;

            _remoteDrawingList.VisualiserController = new VisualiserController();
            _remoteDrawingList.VisualiserController.FullScreenStateSet += new VisualiserControllerFullScreenStateSetHandler(ToggleFullScreen);
            _remoteDrawingList.VisualiserController.BackfaceCullingStateSet += new VisualiserControllerBackfaceCullingStateSetHandler(ToggleBackfaceCulling);
            _remoteDrawingList.VisualiserController.OpenGlLightingStateSet += new VisualiserControllerOpenGlLightingStateSetHandler(ToggleOpenGlLighting);

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

        private void ToggleOpenGlLighting(bool useOpenGlLighting)
        {
            if (useOpenGlLighting == false)
            {
                Gl.glDisable(Gl.GL_LIGHTING);
            }
            else
            {
                Gl.glEnable(Gl.GL_LIGHTING);
            }
        }

        private void ToggleBackfaceCulling(bool backfaceCulling)
        {
            if (backfaceCulling == true)
            {
                Gl.glEnable(Gl.GL_CULL_FACE);
                Gl.glCullFace(Gl.GL_BACK);
            }
            else
            {
                Gl.glDisable(Gl.GL_CULL_FACE);
            }
        }

        private void ToggleFullScreen(bool fullScreen)
        {
            System.Drawing.Rectangle lastScreenBounds = new Rectangle();

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

            if (fullScreen == false)
            {
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
            }
            else
            {
                //move the visualiser to the second screen and make the same size as it.
                m_screen = Video.SetVideoMode(lastScreenBounds.Width, lastScreenBounds.Height, m_bpp, false, true, false, true, false);
                Win32.SetWindowPos(Video.WindowHandle, Win32.HWND_NOTOPMOST, lastScreenBounds.X, lastScreenBounds.Y, lastScreenBounds.Width, lastScreenBounds.Height, Win32.SWP_SHOWWINDOW);
            }
            Reshape();
            InitGL();

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
                    //fullscreen = false;
                }
                catch (IndexOutOfRangeException ex)
                {
                    lastScreenBounds = Screen.AllScreens[(Screen.AllScreens.Length - 1)].Bounds;
                    fullscreen = false;
                    System.Diagnostics.Debug.WriteLine("Only one screen appears to be connected. Exception Message: " + ex.Message);
                }
            }

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
            _remoteDrawingList.VisualiserController.FullScreen = fullscreen;

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

        #endregion Window Attributes

        #region Reshape
        /// <summary>
        /// Resizes the OpenGL scene whenever the window has been resized.
        /// </summary>
        protected void Reshape()
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
            if (_remoteDrawingList.VisualiserController.FullScreen)
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
             * (NEAR_CLIPPING_PLANE_DISTANCE and FAR_CLIPPING_PLANE_DISTANCE are the start and ending points for how deep we can draw into the screen) */

            //Select the projection matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            //Reset the Projection Matrix
            Gl.glLoadIdentity();
            //Calculate the aspect ration of the window
            if (_remoteDrawingList.VisualiserController.FullScreen)
            {
                Glu.gluPerspective(45.0f, (m_screen.Width / m_screen.Height), NEAR_CLIPPING_PLANE_DISTANCE, FAR_CLIPPING_PLANE_DISTANCE);
                Gl.glRotatef(-90.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                Glu.gluPerspective(45.0f, (m_screen.Height / m_screen.Width), NEAR_CLIPPING_PLANE_DISTANCE, FAR_CLIPPING_PLANE_DISTANCE);
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
            this.InitialiseScene();

            _remoteDrawingList.VisualiserLoading = false;
        }

        #endregion Start SDL

        #region Event Handlers


        void TCPProcessor_AvatarSpeedChangedEvent(float newSpeed)
        {
            if (newSpeed == 0.0f)
            {
                avatar.Z = 0.0f;
                if (_remoteDrawingList.VideoBackground != null && _remoteDrawingList.VisualiserController.UseVideoBackground)
                {
                    _remoteDrawingList.VideoBackground.Stop();
                }

            }
            else
            {
                avatar.Velocity.Z = -newSpeed / 100.0f;

                if (_remoteDrawingList.VideoBackground != null && _remoteDrawingList.VisualiserController.UseVideoBackground)
                {
                    _remoteDrawingList.VideoBackground.TransitionInterval = (1 / newSpeed) * 28;
                    _remoteDrawingList.VideoBackground.Start();

                }
            }

        }

        void Events_Quit(object sender, QuitEventArgs e)
        {
            _remoteDrawingList.VideoBackground.Dispose();
            Events.QuitApplication();
            Environment.Exit(0);
        }

        void TCPProcessor_JointListReceivedEvent(List<StroMoHab_Objects.Objects.Joint> jointList)
        {
            m_jointList = new List<StroMoHab_Objects.Objects.Joint>(jointList);
        }

        void TCPProcessor_ToggleDrawingOfFeetEvent(bool display)
        {
            _remoteDrawingList.VisualiserController.Draw2Point5DAvatar = display;
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
                    Environment.Exit(0);
                    break;
                case Key.F1:
                    _remoteDrawingList.VisualiserController.FullScreen = !_remoteDrawingList.VisualiserController.FullScreen;
                    break;
                case Key.F2:
                    //Toggle backface culling
                    _remoteDrawingList.VisualiserController.BackFaceCulling = !_remoteDrawingList.VisualiserController.BackFaceCulling;
                    break;
                case Key.F4:
                    {
                        _remoteDrawingList.VisualiserController.DrawCollisionModels = !_remoteDrawingList.VisualiserController.DrawCollisionModels;
                        break;
                    }
                case Key.F5:
                    {
                        _remoteDrawingList.VisualiserController.UseOpenGlLighting = !_remoteDrawingList.VisualiserController.UseOpenGlLighting;
                        break;
                    }

                case Key.F6:
                    {
                        _remoteDrawingList.VisualiserController.Draw3DAvatar = !_remoteDrawingList.VisualiserController.Draw3DAvatar;
                        break;
                    }
                case Key.F7:
                    {
                        _remoteDrawingList.VisualiserController.Draw3DAvatarJoints = !_remoteDrawingList.VisualiserController.Draw3DAvatarJoints;
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
                case Key.Space:
                    {
                        _remoteDrawingList.VisualiserController.Draw2Point5DAvatar = !_remoteDrawingList.VisualiserController.Draw2Point5DAvatar;
                        break;
                    }

                case Key.UpArrow:
                    {
                        avatar.Velocity.Z = -(float)Math.Cos(avatar.RotationAngle * (3.14159265 / 180)) * 0.1f;
                        _remoteDrawingList.VideoBackground.MoveToNextFrame = true;
                        break;
                    }
                case Key.DownArrow:
                    {
                        avatar.Velocity.Z = (float)Math.Cos(avatar.RotationAngle * (3.14159265 / 180)) * 0.1f;
                        break;
                    }
                case Key.O:
                        {
                            _remoteDrawingList.VideoBackground.TransitionInterval = _remoteDrawingList.VideoBackground.TransitionInterval + 2;
                            break;
                        }
                case Key.P:
                        {
                            if ((_remoteDrawingList.VideoBackground.TransitionInterval - 2) < 3)
                            {
                                _remoteDrawingList.VideoBackground.TransitionInterval = 3;
                            }
                            else
                            {
                                _remoteDrawingList.VideoBackground.TransitionInterval = (_remoteDrawingList.VideoBackground.TransitionInterval - 2);
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
                    _remoteDrawingList.MoveAvatarInTaskDesigner(avatar.Z);
                    break;
                case Key.DownArrow:
                    avatar.Velocity.Z = 0.0f;
                    _remoteDrawingList.MoveAvatarInTaskDesigner(avatar.Z);
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
            _checkErrors.CheckOpenGLErrors();
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
            _remoteDrawingList.Clear();
            _remoteDrawingList.VisualiserController.DrawCollisionModels = false;
            avatar.Z = 0.0f;

            Corridor corridor = new Corridor(-1.5f, 0.0f, 4.0f, 1.5f, 6.0f, -500.0f);

           _remoteDrawingList.Add(corridor);
#if(DEBUG)
           {
               _remoteDrawingList.VideoBackground = new VideoBackground(@"C:\Users\mag501\Desktop\DemoPathStablised\Augmented");

               

               //m_VideoTextureManager = new VideoBackground("C:\\Users\\mag501\\Desktop\\Path1JPEG");
               //m_VideoTextureManager.Start();
           }
#endif
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

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            if (_remoteDrawingList.VisualiserController.UseVideoBackground)
            {
                DrawVideoBackground();
            }

            //Translate into the scene
            Gl.glTranslatef(0.0f, -1.5f, -3.5f);

            if (_remoteDrawingList.VisualiserController.Draw2Point5DAvatar)
            {
                //Draw the subject
                avatar.Draw();
            }
            //reset view
            Gl.glLoadIdentity();
            //Translate the amount the avatar has moved in Z, and translate the Y to give 3 person viewpoint.
            Gl.glTranslatef(0.0f,-1.5f, -avatar.Z);
            //Draw everything else

            if (_remoteDrawingList.VisualiserController.UseVirtualBackground)
            {
                //Create temporary list to allow modification of master list during drawing.
                List<OpenGlObject> tempObjectsToDrawList = new List<OpenGlObject>(_remoteDrawingList.ObjectsToDraw);

                foreach (OpenGlObject objectToDraw in tempObjectsToDrawList)
                {
                    objectToDraw.Draw();
                }
            }



          //  //Move Camera to 3rd Person View
          //  //Gl.glTranslatef(0.0f, -1.5f, avatar.ViewDistance);

          //  //Gl.glRotatef(45.0f, 1.0f, 0.0f, 0.0f);
          //  //Gl.glRotatef(avatar.ViewAngleX, 0.0f, 1.0f, 0.0f);
          //  //Gl.glRotatef(avatar.ViewAngleY, 1.0f, 0.0f, 0.0f);




          //  //Gl.glTranslatef(0.0f, 1.5f, 3.5f);

          //  ////Draw Avatar
          //  ////avatar.Update(m_trackableList);


          //  //Marker testMarker = new Marker();
          //  //testMarker.xCoordinate = 0f;
          //  //testMarker.yCoordinate = 0f;
          //  //testMarker.zCoordinate = -5f;

          //  //OpenGLMarker testOGLMarker = new OpenGLMarker(testMarker);
          //  //testOGLMarker.Draw();
            


          //  //if (m_drawAvatar)
          //  //{
          //  //    if (m_drawAvatar3D)// When using the 3D avatar 
          //  //    {
          //  //        avatar3D.Draw(m_trackableList, m_jointList);
          //  //    }
          //  //    else // When using the standard avatar
          //  //        avatar.Draw();
          //  //}

          //  ////Move camera in preparation for drawing all world objects
          //  ////Gl.glRotatef(-avatar.RotationAngle, 0.0f, 1.0f, 0.0f);

          //  ////Draw collision models if set
          //  //if (m_drawCollisionModels)
          //  //{
          //  //    if (m_drawAvatar3D) // When using the 3D avatar 
          //  //    {
          //  //        foreach (BoundingBox collisionModel in avatar3D.CollisionModel)
          //  //        {
          //  //            collisionModel.Draw();
          //  //        }
          //  //    }
          //  //    else // When using the standard avatar
          //  //    {
          //  //        //avatar.CollisionModel.Draw();
          //  //        if (StroMoHab_Objects.Objects.FilteredMarkerList.listOfMarkers.Count != 0)
          //  //        {
          //  //            avatar.leftFoot.CollisionModel.Draw();
          //  //        }
          //  //    }
                
          //  //}
          //  //Gl.glTranslatef(avatar.X, avatar.Y, -avatar.Z);

          //  //Create temporary list to allow modification of master list during drawing.
            List<OpenGlObject> tempList = new List<OpenGlObject>(_remoteDrawingList.ObjectsToDraw);



          //  //Draw all objects in temporary list
            //foreach (OpenGlObject currentObject in tempList)
            //{
            //    currentObject.Draw();
            //    currentObject.Move();
            //    //currentObject.RotateX(1.0f);

            //    if (m_drawCollisionModels)
            //    {
            //        currentObject.CollisionModel.Draw();
            //    }

            //    if (m_drawAvatar3D) // When using the 3D avatar 
            //    {

            //        bool collisionOccured = false;
            //        foreach (BoundingBox collisionModel in avatar3D.CollisionModel)
            //        {
            //            bool collision = Collisions.HaveCollided(currentObject.CollisionModel, collisionModel, -avatar.X, avatar.Y, avatar.Z);
            //            if (collision)
            //            {
            //                collisionOccured = true;
            //                break; // If one collision has occured then don't need to check other models.
            //            }
            //        }
            //        if (collisionOccured)
            //        {
            //            //System.Diagnostics.Debug.WriteLine("OBJECT COLLISION!");
            //            currentObject.Velocity.Y = 0.005f;
            //        }
            //    }
            //    else // When using the standard avatar
            //    {
            //        //Test for collisions
            //        if ((StroMoHab_Objects.Objects.FilteredMarkerList.listOfMarkers.Count == 2) && (m_useTrackables == false))
            //        {
            //            bool CollisionOne = Collisions.HaveCollided(currentObject.CollisionModel, avatar.leftFoot.CollisionModel, -avatar.X, avatar.Y, avatar.Z);
            //            bool CollisionTwo = Collisions.HaveCollided(currentObject.CollisionModel, avatar.rightFoot.CollisionModel, -avatar.X, avatar.Y, avatar.Z);

            //            if (CollisionOne || CollisionTwo)
            //            {
            //                // System.Diagnostics.Debug.WriteLine("OBJECT COLLISION!");
            //                currentObject.Velocity.Y = 0.005f;
            //            }
            //        }
            //    }
            //    if (currentObject is CompoundOpenGlObject)
            //    {
            //        foreach (BoundingBox collisionModel in ((CompoundOpenGlObject)currentObject).CollisionModel)
            //        {
            //            //Draw collision models if flag set
            //            if (m_drawCollisionModels)
            //            {
            //                collisionModel.Draw();
            //            }

            //            //Test for collisions
            //            //if (Stromohab_MCE.FilteredMarkerList.listOfMarkers.Count != 0)
            //            //{
            //            //    bool collision = Collisions.HaveCollided(collisionModel, avatar.leftFoot.CollisionModel, -avatar.X, avatar.Y, avatar.Z);
            //            //    if (collision == true)
            //            //    {
            //            //        //System.Diagnostics.Debug.WriteLine("COLLISION!");
            //            //        avatar.Velocity.X = -avatar.Velocity.X * 10.0f;
            //            //        avatar.Velocity.Y = -avatar.Velocity.Y;
            //            //        //avatar.Velocity = new Vector3f(0.0f, 0.0f, 0.0f);
            //            //    }
            //            //    else
            //            //    {
            //            //        //System.Diagnostics.Debug.WriteLine("NO COLLISION");
            //            //    }
            //            //}

            //        }
            //    }

            //}


            //Update Game Logic
            avatar.Move();

            //avatar.RotateY(1.0f);
        }
        #endregion Draw Scene

        #region Video Background

        private void DrawVideoBackground()
        {
            Bitmap bitmapToDraw = _remoteDrawingList.VideoBackground.CurrentFrame;

            if (bitmapToDraw != null)
            {
                DrawBitmapToScreen(bitmapToDraw);
            }
        }

        private void SwitchToOrthoView()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(0.0, m_screen.Width, 0.0, m_screen.Height,NEAR_CLIPPING_PLANE_DISTANCE, FAR_CLIPPING_PLANE_DISTANCE);

        }

        private void SwitchToProjectionView()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            if (_remoteDrawingList.VisualiserController.FullScreen)
            {
                Glu.gluPerspective(45.0f, (m_screen.Width / m_screen.Height), NEAR_CLIPPING_PLANE_DISTANCE, FAR_CLIPPING_PLANE_DISTANCE);
                Gl.glRotatef(-90.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                Glu.gluPerspective(45.0f, (m_screen.Height / m_screen.Width), NEAR_CLIPPING_PLANE_DISTANCE, FAR_CLIPPING_PLANE_DISTANCE);
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

            SwitchToOrthoView();

            //Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Gl.glLoadIdentity();


            Gl.glRasterPos3f(0f, 0f, -FAR_CLIPPING_PLANE_DISTANCE);

            float zoomFactorX = (float)(m_screen.Width) / (float)(bitmap.Width);
            float zoomFactorY = (float)(m_screen.Height) / (float)(bitmap.Height);

            Gl.glPixelZoom(zoomFactorX, zoomFactorY);

            Gl.glDrawPixels(bitmap.Width, bitmap.Height, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);

            Gl.glPixelZoom(1.0f, 1.0f);


            //release the image resources
            bitmap.UnlockBits(bitmapData);

            SwitchToProjectionView();
        }

        #endregion Video Background

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
            
            _remoteDrawingList.GeneratingScreenShot = true;
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
            _remoteDrawingList.TaskScreenShot = bmpBytes;

            _remoteDrawingList.GeneratingScreenShot = false;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.Windows.Forms;
using System.Threading;

using SdlDotNet;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace OpenGLViewer
{


    class SDL
    {
        #region Member Variables
        Skeleton.Skeleton skeleton = new Skeleton.Skeleton();
        private List<Stromohab_MCE.Trackable> trackableList = new List<Stromohab_MCE.Trackable>();
        private OpenGLViewerRemoteControl openGLViewerRemoteControl = new OpenGLViewerRemoteControl();
        private Surface screen;
        private bool resizeable = false;
        private bool fullscreen = false;
        private int height = 735;
        private int width = 470;
        private float rotX = -45;
        private float rotY = 10;
        private List<Stromohab_MCE.Joint> jointList = new List<Stromohab_MCE.Joint>();
        #endregion Member Variables


        #region Initialise
        public SDL()
        {
            Initialise();
            
            #region Connect To Server
            System.Net.IPEndPoint tempIpAddress = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8001);
            Stromohab_MCE_Connection.TCPProcessor.ConnectToServer(tempIpAddress);
            
            Stromohab_MCE_Connection.SendMCECommand.StartCameras();
            Stromohab_MCE_Connection.TCPProcessor.JointListReceivedEvent += new Stromohab_MCE_Connection.TCPProcessor.JointListReceivedHandler(TCPProcessor_JointListReceivedEvent);
            Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedEvent += new Stromohab_MCE_Connection.TCPProcessor.TrackableListReceivedHandler(TCPProcessor_TrackableListReceivedEvent);
            #endregion Connect To Server
            
            
            #region Remoting
            //Register for events and pre-set all trackables to draw

            
            ChannelServices.RegisterChannel(new TcpChannel(8008),false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(OpenGLViewer.OpenGLViewerRemoteControl), "Remote", WellKnownObjectMode.Singleton);

            

            #endregion Remoting
            
        }



        void TCPProcessor_TrackableListReceivedEvent(List<Stromohab_MCE.Trackable> newTrackableList)
        {
            trackableList = new List<Stromohab_MCE.Trackable>(newTrackableList);
        }

        void TCPProcessor_JointListReceivedEvent(List<Stromohab_MCE.Joint> newJointList)
        {
            
            jointList = new List<Stromohab_MCE.Joint>(newJointList);
        }


        public void Initialise()
        {
            //Sets the ticker to update OpenGL context
            Events.Tick += new EventHandler<TickEventArgs>(this.Tick);
            //Registers Quit event
            Events.Quit += new EventHandler<QuitEventArgs>(this.Quit);
            //Adds keyboard events
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(this.Events_KeyDown);

            //Set the fps
            Events.Fps = 30;

            //Give the application a title
            Video.WindowCaption = Title;

            //Enable Key Repeating
            Keyboard.EnableKeyRepeat(20, 1);

            
            //Initialise OpenGL
            InitGL();

            //Setup the screen
            if(!fullscreen)
                screen = Video.SetVideoMode((int)(width), (int)(height), 24, resizeable, true, fullscreen, true, true);
            else
                screen = Video.SetVideoMode((int)(Screen.PrimaryScreen.Bounds.Width), (int)(Screen.PrimaryScreen.Bounds.Height), 24, resizeable, true, fullscreen, true, true);


            for (int i = 0; i < OpenGLViewerRemoteControl.MAX_Trackable_Number; i++)
            {
                OpenGLViewerRemoteControl.trackableIDsArray[i] = true;

            }


        }

        #endregion Initialise


        #region DrawGLScene
        protected void DrawGLScene()
        {
            //Clear the screen and the Depth Buffer
            Gl.glClear((Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT));
            Gl.glLoadIdentity();



            // Sets up camera rotation
            Gl.glRotatef(rotY, 1.0f, 0f, 0f);
            Gl.glRotatef(rotX, 0f, 1f, 0f);

            DrawAxis();
            //Draws the skeleton
            skeleton.Draw(trackableList,OpenGLViewerRemoteControl.trackableIDsArray,OpenGLViewerRemoteControl.trackableOffset);
            if (jointList.Count != 0 && jointList[0].Exists)
            {
                Gl.glColor4f(0.5f, 0.5f, 0.5f, 0f);


                Gl.glTranslatef(jointList[0].xCoordinate / 600f, jointList[0].yCoordinate / 600f - 0.6f, -jointList[0].zCoordinate / 600f);
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex3f(0f, 1000f, 0f);
                Gl.glVertex3f(0f, -1000f, 0f);
                Gl.glVertex3f(0f, 0f, 1000f);
                Gl.glVertex3f(0f, 0f, -1000f);
                Gl.glVertex3f(1000f, 0, 0f);
                Gl.glVertex3f(-1000f, 0, 0f);
                Gl.glEnd();
                Gl.glTranslatef(-jointList[0].xCoordinate / 600f, -jointList[0].yCoordinate / 600f + 0.6f, jointList[0].zCoordinate / 600f);

               
                
            }

        }

        #endregion DrawGLScene


        #region Draw Axes
        private void DrawAxis()
        {

            const float axisSize = 100.0f;
            Gl.glTranslatef(0, -0.6f, 0);
            // draw a line along the z-axis 
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0.0f, 0.0f, -axisSize);
            Gl.glVertex3f(0.0f, 0.0f, axisSize);
            Gl.glEnd();

            // draw a line along the y-axis 
            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0.0f, -axisSize, 0.0f);
            Gl.glVertex3f(0.0f, axisSize, 0.0f);
            Gl.glEnd();

            // draw a line along the x-axis 
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(-axisSize, 0.0f, 0.0f);
            Gl.glVertex3f(axisSize, 0.0f, 0.0f);
            Gl.glEnd();

            Gl.glTranslatef(0,0.6f, 0);
        


        }
        #endregion Draw Axes


        #region MotionCapture

        public void UpdateJointList(List<Stromohab_MCE.Joint> newJointList)
        {
            jointList = new List<Stromohab_MCE.Joint>(newJointList);
        }
       


        public void UpdateTrackableList(List<Stromohab_MCE.Trackable> newTrackableList)
        {
            trackableList = new List<Stromohab_MCE.Trackable>(newTrackableList);
        }



        #endregion MotionCapture


        #region Reshape
        /// <summary>
        /// Resizes window
        /// </summary>
        protected virtual void Reshape()
        {
            this.Reshape(100.0F);
        }

        /// <summary>
        /// Resizes window
        /// </summary>
        /// <param name="distance"></param>
        protected virtual void Reshape(float distance)
        {
            // Reset The Current Viewport
            Gl.glViewport(0, 0, width, height);
            // Select The Projection Matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // Reset The Projection Matrix
            Gl.glLoadIdentity();
            // Calculate The Aspect Ratio Of The Window
            Glu.gluPerspective(45.0F, width/height, 0.1F, distance);
            // Select The Modelview Matrix
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            // Reset The Modelview Matrix
            Gl.glLoadIdentity();
        }

        #endregion Reshape


        #region InitGL
        /// <summary>
        /// Initializes the OpenGL system
        /// </summary>
        protected virtual void InitGL()
        {
            // Enable Smooth Shading
            Gl.glShadeModel(Gl.GL_SMOOTH);
            // Black Background
            Gl.glClearColor(0f,0f,1f, 0.5F);
            // Depth Buffer Setup
            Gl.glClearDepth(1.0F);
            // Enables Depth Testing
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // The Type Of Depth Testing To Do
            Gl.glDepthFunc(Gl.GL_LEQUAL);
            // Really Nice Perspective Calculations
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);


        }

        #endregion InitGL


        #region Events
        private void Events_KeyDown(object sender, KeyboardEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Escape:
                    if (SDLExitEvent != null)
                        SDLExitEvent();
                    Events.QuitApplication();
                    break;
                case Key.LeftArrow:
                    rotX += 5f;
                    break;
                case Key.RightArrow:
                    rotX -= 5f;
                    break;
                case Key.UpArrow:
                    rotY += 5f;
                    if (rotY >= 30f)
                        rotY = 30f;
                    break;
                case Key.DownArrow:
                    rotY -= 5f;
                    if (rotY <= -20f)
                        rotY = -20f;
                    break;
                case Key.R:
                    rotX = -45f;
                    rotY = 10f;
                    break;
                
            }


        }



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
            
            if (SDLExitEvent != null)
                SDLExitEvent();
            Events.QuitApplication();
        }


        public delegate void SDLExitEventHandler();
        public event SDLExitEventHandler SDLExitEvent;
        #endregion Events


        #region Properties
        /// <summary>
        /// Code Title
        /// </summary>
        public static string Title
        {
            get
            {
                return "StroMoHab Skeleton";
            }
        }

        /// <summary>
        /// Sets Window icon and caption
        /// </summary>
        protected void WindowAttributes()
        {
            Video.WindowIcon();
            Video.WindowCaption = Title;
        }
        #endregion Properties


    }
}

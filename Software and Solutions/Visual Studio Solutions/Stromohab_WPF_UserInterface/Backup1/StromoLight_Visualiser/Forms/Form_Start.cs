using System;
using System.Threading;

using System.Windows.Forms;

using System.Drawing;
using SdlDotNet.Graphics;

namespace StromoLight_Visualiser.Forms
{
    /// <summary>
    /// Inital Task Designer Form.
    /// </summary>
    public partial class Form_Start : Form
    {
        SDL_Main sdlMain;
        Surface m_surface;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Form_Start()
        {
            InitializeComponent();
            m_surface = new Surface(this.surfaceControl1.Width,this.surfaceControl1.Height);
        }


        private void Form_Start_Load(object sender, EventArgs e)
        {
            SdlDotNet.Core.Events.Tick += new EventHandler<SdlDotNet.Core.TickEventArgs>(Events_Tick);
            
            sdlMain = new SDL_Main();

            sdlMain.StartSDL();

            Thread thread = new Thread(new ThreadStart(SdlDotNet.Core.Events.Run));
            thread.IsBackground = false;
            thread.Name = "SDL.NET";
            thread.Priority = ThreadPriority.Normal;
            thread.Start();

        }

        void Events_Tick(object sender, SdlDotNet.Core.TickEventArgs e)
        {
            m_surface.Fill(Color.Green);

            this.UpdateForm();  
        }

        private void UpdateForm()
        {
            this.surfaceControl1.Blit(m_surface);
        }
    }
}

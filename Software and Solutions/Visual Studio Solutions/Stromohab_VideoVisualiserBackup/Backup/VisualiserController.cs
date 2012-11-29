using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Interfaces;

namespace StromoLight_Visualiser
{
[Serializable]
    public class VisualiserController:StroMoHab_Objects.Interfaces.IVisualiser
    {
        #region Member Variables
        //flags
        private bool m_fullScreen = true;
        private bool m_backfaceCulling;
        private bool m_drawCollisionModels = false;
        private bool m_lightingEnabled = false;
        private bool m_drawAvatar3D = false; // When set to false the standard avatar is used, when true the trackable and joint based 3D avatar is used
        private bool m_drawAvatar = true;
        private bool _drawVideoBackground = false;
        private bool _drawVirtualBackground = false;

        private StroMoHab_Avatar_Object.Avatar avatar3D = new StroMoHab_Avatar_Object.Avatar();

        #endregion Member Variables

        /// <summary>
        /// Initalises a new instance of a VisualiserController, with default settings.
        /// </summary>
        public VisualiserController()
        {
            //Initalise variables to defaults
            m_fullScreen = true;
            m_backfaceCulling=false;
            m_drawCollisionModels = false;
            m_lightingEnabled = false;
            m_drawAvatar3D = false; 
            m_drawAvatar = true;
            _drawVideoBackground = false;
            _drawVirtualBackground = true;

        }
        
        /// <summary>
        /// Fired when the FullScreen property is modified.
        /// </summary>
        public event VisualiserControllerFullScreenStateSetHandler FullScreenStateSet;

        
        /// <summary>
        /// Fired when the BackFaceCulling property is modified.
        /// </summary>
        public event VisualiserControllerBackfaceCullingStateSetHandler BackfaceCullingStateSet;

       
        /// <summary>
        /// Fired when the UseOpenGlLighting property is modified.
        /// </summary>
        public event VisualiserControllerOpenGlLightingStateSetHandler OpenGlLightingStateSet;

        #region IVisualiser Members
        /// <summary>
        /// Gets or sets the FullScreen state of the Visualiser. Setting to true moves the visualiser to a second sceen and maximises it.
        /// </summary>
        public bool FullScreen
        {
            get
            {
                return m_fullScreen;
            }
            set
            {
                FullScreenStateSet(value);
                m_fullScreen = value;
            }
        }

        /// <summary>
        /// Define use of backface culling in the OpenGL context.
        /// </summary>
        public bool BackFaceCulling
        {
            get
            {
                return (m_backfaceCulling);
            }
            set
            {
                BackfaceCullingStateSet(value);
                m_backfaceCulling = value;
            }
        }

        /// <summary>
        /// Draw collision models in the Visualiser
        /// </summary>
        public bool DrawCollisionModels
        {
            get
            {
                return (m_drawCollisionModels);
            }
            set
            {
                m_drawCollisionModels = value;
            }
        }

        /// <summary>
        /// Use OpenGL Lighting in the Visualiser.
        /// </summary>
        public bool UseOpenGlLighting
        {
            get
            {
                return (m_lightingEnabled);
            }
            set
            {
                OpenGlLightingStateSet(value);
                m_lightingEnabled = value;
            }
        }

        /// <summary>
        /// Draw the 3D avatar.
        /// </summary>
        public bool Draw3DAvatar
        {
            get
            {
                return (m_drawAvatar3D);
            }
            set
            {
                m_drawAvatar3D = value;
                m_drawAvatar = false;
            }
        }

        /// <summary>
        /// Draw the 3D avatar with joints.
        /// </summary>
        public bool Draw3DAvatarJoints
        {
            get
            {
                if (avatar3D != null)
                {
                    return (avatar3D.DrawJoints);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value == true)
                {
                    Draw3DAvatar = true;
                }
                if (avatar3D != null)
                {
                    avatar3D.DrawJoints = value;
                    m_drawAvatar = false;
                }
            }
        }

        /// <summary>
        /// Draw standard avatar.
        /// </summary>
        public bool Draw2Point5DAvatar
        {
            get
            {
                return (m_drawAvatar);
            }
            set
            {
                m_drawAvatar = value;
                m_drawAvatar3D = false;
            }
        }

        /// <summary>
        /// Use video background instead of virtual background. Setting clears all UseBackground flags.
        /// </summary>
        public bool UseVideoBackground
        {
            get
            {
                return (_drawVideoBackground);
            }
            set
            {
                _drawVirtualBackground = false;
                _drawVideoBackground = value;
            }
        }

        /// <summary>
        /// Use virtual background (OpenGL drawn). Setting clears all UseBackground flags.
        /// </summary>
        public bool UseVirtualBackground
        {
            get
            {
                return (_drawVirtualBackground);
            }
            set
            {
                _drawVideoBackground = false;
                _drawVirtualBackground = value;
            }
        }

        /// <summary>
        /// The current speed the visualiser is moving through the scene.
        /// </summary>
        public float Speed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

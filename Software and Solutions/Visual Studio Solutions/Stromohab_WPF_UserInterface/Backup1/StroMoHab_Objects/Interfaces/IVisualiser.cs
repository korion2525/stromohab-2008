using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Interfaces
{
    /// <summary>
    /// Handler for FullScreenStateSet.
    /// </summary>
    /// <param name="fullScreen">set true to switch to fullscreen mode.</param>
    public delegate void VisualiserControllerFullScreenStateSetHandler(bool fullScreen);
    /// <summary>
    /// Handler for BackfaceCullingStateSet.
    /// </summary>
    /// <param name="backfaceCulling">set true to enable backface culling in the Visualiser.</param>
    public delegate void VisualiserControllerBackfaceCullingStateSetHandler(bool backfaceCulling);
    /// <summary>
    /// Handler for OpenGlLightingStateSet.
    /// </summary>
    /// <param name="useOpenGlLighting">set true to enable OpenGL lighting.</param>
    public delegate void VisualiserControllerOpenGlLightingStateSetHandler(bool useOpenGlLighting);
    /// <summary>
    /// Visualiser Interface.
    /// </summary>
    public interface IVisualiser
    {
        /// <summary>
        /// Get or Set the FullScreen state of the Visualiser.
        /// </summary>
        bool FullScreen { get;  set; }
        /// <summary>
        /// Use backface culling in the visualiser OpenGL context.
        /// </summary>
        bool BackFaceCulling { get; set; }
        /// <summary>
        /// Draw collision models in the Visualiser
        /// </summary>
        bool DrawCollisionModels { get; set; }
        /// <summary>
        /// Use OpenGL Lighting in the Visualiser.
        /// </summary>
        bool UseOpenGlLighting { get; set; }
        /// <summary>
        /// Draw the 3D avatar.
        /// </summary>
        bool Draw3DAvatar { get; set; }
        /// <summary>
        /// Draw the 3D avatar with joints.
        /// </summary>
        bool Draw3DAvatarJoints { get; set; }
        /// <summary>
        /// Draw standard avatar.
        /// </summary>
        bool Draw2Point5DAvatar { get; set; }
        /// <summary>
        /// Use video background.
        /// </summary>
        bool UseVideoBackground { get; set; }
        /// <summary>
        /// Use virtual background (OpenGL drawn).
        /// </summary>
        bool UseVirtualBackground { get; set; }
        /// <summary>
        /// The current speed the visualiser is moving through the scene.
        /// </summary>
        float Speed { get; set; }

        event VisualiserControllerFullScreenStateSetHandler FullScreenStateSet;
        event VisualiserControllerBackfaceCullingStateSetHandler BackfaceCullingStateSet;
        event VisualiserControllerOpenGlLightingStateSetHandler OpenGlLightingStateSet;
    }
}

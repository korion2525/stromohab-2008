using System;

using Tao.OpenGl;
using System.Runtime.Remoting;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// The graphical version of Marker.cs (an Optitrack marker).
    /// </summary>
    public class OpenGLMarker : OpenGlObject
    {
        const float SCALING_FACTOR = 400.0f;

        private Marker m_marker;
        private static float m_radius = 0.02f;

        /// <summary>
        /// Sets the height of the foot. TO customise for a patient, modify this value based on the Y position of a marker when their foot is at rest.
        /// </summary>
        private float m_calibratedHeight = 0.05f;

        private const float FOOT_RADIUS = 0.05f;
        private const float FOOT_DEPTH = 0.2f;

        /// <summary>
        /// Constructor. Creates OpenGlMarker from an optitrack Marker.
        /// </summary>
        /// <param name="marker"></param>
        public OpenGLMarker(Marker marker)
            : base((float)(marker.xCoordinate / SCALING_FACTOR) - m_radius, (float)(marker.yCoordinate / SCALING_FACTOR), -(float)(marker.zCoordinate / SCALING_FACTOR) - m_radius, (float)(marker.xCoordinate / SCALING_FACTOR) + m_radius, (float)(marker.yCoordinate / SCALING_FACTOR), -(float)(marker.zCoordinate / SCALING_FACTOR) + m_radius)
        {
            m_marker = marker;
        }

        /// <summary>
        /// Draws the marker.
        /// </summary>
        public override void Draw()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            Gl.glBegin(Gl.GL_TRIANGLES);

            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            //Bottom of foot
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR) - m_calibratedHeight,                       -((float)m_marker.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f((float)m_marker.xCoordinate / SCALING_FACTOR, ((float)m_marker.yCoordinate / SCALING_FACTOR)          - m_calibratedHeight,                                -((float)m_marker.zCoordinate / SCALING_FACTOR) - FOOT_DEPTH);
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR) - m_calibratedHeight,                       -((float)m_marker.zCoordinate / SCALING_FACTOR));

            //Top of foot
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR),  -((float)m_marker.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f((float)m_marker.xCoordinate / SCALING_FACTOR,          ((float)m_marker.yCoordinate / SCALING_FACTOR),  -((float)m_marker.zCoordinate / SCALING_FACTOR) - FOOT_DEPTH);
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR),  -((float)m_marker.zCoordinate / SCALING_FACTOR));

            
            //Back of foot
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR) - m_calibratedHeight,   -((float)m_marker.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR),                        -((float)m_marker.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR),                        -((float)m_marker.zCoordinate / SCALING_FACTOR));

            //Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR) - m_calibratedHeight,   -((float)m_marker.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR),                        -((float)m_marker.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_marker.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_marker.yCoordinate / SCALING_FACTOR) - m_calibratedHeight,   -((float)m_marker.zCoordinate / SCALING_FACTOR));

            //Shadow
            //Color to black
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            
            //Back left vertex
            Gl.glVertex3f((float)(CalculateShadowSize(-FOOT_RADIUS, m_marker.yCoordinate)+m_marker.xCoordinate/SCALING_FACTOR), 0.01f, -((float)m_marker.zCoordinate / SCALING_FACTOR));

            //Back right vertex
            Gl.glVertex3f((float)(CalculateShadowSize(FOOT_RADIUS, m_marker.yCoordinate) + m_marker.xCoordinate / SCALING_FACTOR), 0.01f, -((float)m_marker.zCoordinate / SCALING_FACTOR));

            //Front vertex ("point" of triangle)
            Gl.glVertex3f((float)m_marker.xCoordinate / SCALING_FACTOR, 0.01f, (float)(-CalculateShadowSize(FOOT_DEPTH,m_marker.yCoordinate)-m_marker.zCoordinate /SCALING_FACTOR));

            Gl.glEnd();
        }

        private float CalculateShadowSize(float vertexCoordinateValue, double markerYCoordinate)
        {
            const double LIGHT_POSITION = 1.0f;

            float shadowSize = (float)((vertexCoordinateValue*LIGHT_POSITION) / (LIGHT_POSITION - markerYCoordinate / SCALING_FACTOR));

            return (shadowSize);
        }

    }
}

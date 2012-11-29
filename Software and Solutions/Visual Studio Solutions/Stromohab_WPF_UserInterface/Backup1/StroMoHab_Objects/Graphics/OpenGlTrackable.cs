using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.Runtime.Remoting;
using StroMoHab_Objects.Objects;



namespace StroMoHab_Objects.Graphics
{
    class OpenGlTrackable : OpenGlObject
    {
        const float SCALING_FACTOR = 400.0f;
        private static float m_radius = 0.2f;

        /// <summary>
        /// Sets the height of the foot. TO customise for a patient, modify this value based on the Y position of a marker when their foot is at rest.
        /// </summary>
        private float m_calibratedHeight = 0.1f;

        private const float FOOT_RADIUS = 0.05f;
        private const float FOOT_DEPTH = 0.4f;

        private Trackable m_trackable = new Trackable();

        public OpenGlTrackable(Trackable trackable)
            : base((float)(trackable.xCoordinate/SCALING_FACTOR)-m_radius,(float)(trackable.yCoordinate/SCALING_FACTOR),-(float)(trackable.zCoordinate /SCALING_FACTOR)-m_radius,(float)(trackable.xCoordinate/SCALING_FACTOR)+m_radius,(float)(trackable.yCoordinate/SCALING_FACTOR),-(float)(trackable.zCoordinate/SCALING_FACTOR)+m_radius)
        {
            m_trackable = trackable;
        }

        public override void Draw()
        {
            Gl.glPushMatrix();
            
            Gl.glBegin(Gl.GL_TRIANGLES);

            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            //Bottom of foot
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR) - m_calibratedHeight, -((float)m_trackable.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f((float)m_trackable.xCoordinate / SCALING_FACTOR, ((float)m_trackable.yCoordinate / SCALING_FACTOR) - m_calibratedHeight, -((float)m_trackable.zCoordinate / SCALING_FACTOR) - FOOT_DEPTH);
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR) - m_calibratedHeight, -((float)m_trackable.zCoordinate / SCALING_FACTOR));

            //Top of foot
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR), -((float)m_trackable.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f((float)m_trackable.xCoordinate / SCALING_FACTOR, ((float)m_trackable.yCoordinate / SCALING_FACTOR), -((float)m_trackable.zCoordinate / SCALING_FACTOR) - FOOT_DEPTH);
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR), -((float)m_trackable.zCoordinate / SCALING_FACTOR));


            //Back of foot
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR) - m_calibratedHeight, -((float)m_trackable.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR), -((float)m_trackable.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR), -((float)m_trackable.zCoordinate / SCALING_FACTOR));

            //Gl.glColor3f(0.0f, 1.0f, 0.0f);
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR) - m_calibratedHeight, -((float)m_trackable.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) + FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR), -((float)m_trackable.zCoordinate / SCALING_FACTOR));
            Gl.glVertex3f(((float)m_trackable.xCoordinate / SCALING_FACTOR) - FOOT_RADIUS, ((float)m_trackable.yCoordinate / SCALING_FACTOR) - m_calibratedHeight, -((float)m_trackable.zCoordinate / SCALING_FACTOR));

            //Shadow
            //Color to black
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            //Back left vertex
            Gl.glVertex3f((float)(CalculateShadowSize(-FOOT_RADIUS, m_trackable.yCoordinate) + m_trackable.xCoordinate / SCALING_FACTOR), 0.01f, -((float)m_trackable.zCoordinate / SCALING_FACTOR));

            //Back right vertex
            Gl.glVertex3f((float)(CalculateShadowSize(FOOT_RADIUS, m_trackable.yCoordinate) + m_trackable.xCoordinate / SCALING_FACTOR), 0.01f, -((float)m_trackable.zCoordinate / SCALING_FACTOR));

            //Front vertex ("point" of triangle)
            Gl.glVertex3f((float)m_trackable.xCoordinate / SCALING_FACTOR, 0.01f, (float)(-CalculateShadowSize(FOOT_DEPTH, m_trackable.yCoordinate) - m_trackable.zCoordinate / SCALING_FACTOR));

            Gl.glEnd();
            Gl.glPopMatrix();
        }

        private float CalculateShadowSize(float vertexCoordinateValue, double markerYCoordinate)
        {
            const double LIGHT_POSITION = 1.0f;

            float shadowSize = (float)((vertexCoordinateValue * LIGHT_POSITION) / (LIGHT_POSITION - markerYCoordinate / SCALING_FACTOR));

            return (shadowSize);

        }





    }
}

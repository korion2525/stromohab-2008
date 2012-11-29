using System;
using System.Collections.Generic;

using Tao.OpenGl;




namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Holds bounding box infomation about current polygon face
    /// </summary>
    [Serializable]
    public class BoundingBox : IBoundingBox
    {
        float m_xMin, m_yMin, m_zMin, m_xMax, m_yMax, m_zMax;
        float m_x, m_y, m_z;
        Vector3f m_colour;

        static float s_m_currentRand = 1.0f;

        /// <summary>
        /// Constructor. Takes minimum and maximum values in 3 dimensions.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        public BoundingBox(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            m_xMin = xMin;
            m_yMin = yMin;
            m_zMin = zMin;

            m_xMax = xMax;
            m_yMax = yMax;
            m_zMax = zMax;
            
            Random rand = new Random((int)s_m_currentRand);
            s_m_currentRand += 1.0f;

            m_colour = new Vector3f((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
        }

        /// <summary>
        /// The X-coordinate of the BoundingBox.
        /// </summary>
        public float X
        {
            get
            {
                return m_x;
            }
            set
            {
                m_x = value;
            }
        }

        /// <summary>
        /// The Y-coordinate of the BoundingBox.
        /// </summary>
        public float Y
        {
            get
            {
                return m_y;
            }
            set
            {
                m_y = value;
            }
        }

        /// <summary>
        /// The Z-coordinate of the BoundingBox.
        /// </summary>
        public float Z
        {
            get
            {
                return m_z;
            }
            set
            {
                m_z = value;
            }
        }

        /// <summary>
        /// The leftmost edge of the BoundingBox.
        /// </summary>
        public float XMin
        {
            get
            {
                return m_xMin;
            }
            set
            {
                m_xMin = value;
            }
        }

        /// <summary>
        /// The lowest edge of the BoundingBox.
        /// </summary>
        public float YMin
        {
            get
            {
                return m_yMin;
            }
            set
            {
                m_yMin = value;
            }
        }

        /// <summary>
        /// The BoundingBox edge furthest into the screen.
        /// </summary>
        public float ZMin
        {
            get
            {
                return m_zMin;
            }
            set
            {
                m_zMin = value;
            }
        }

        /// <summary>
        /// The rightmost edge of the BoundingBox.
        /// </summary>
        public float XMax
        {
            get
            {
                return m_xMax;
            }
            set
            {
                m_xMax = value;
            }
        }

        /// <summary>
        /// The highest edge of the BoundingBox.
        /// </summary>
        public float YMax
        {
            get
            {
                return m_yMax;
            }
            set
            {
                m_yMax = value;
            }
        }

        /// <summary>
        /// The BoundingBox edge closest to the observer.
        /// </summary>
        public float ZMax
        {
            get
            {
                return m_zMax;
            }
            set
            {
                m_zMax = value;
            }
        }


        /// <summary>
        /// Draws the BoundingBox.
        /// </summary>
        public void Draw()
        {
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glColor3f(m_colour.X,m_colour.Y,m_colour.Z);

            //Front
            Gl.glVertex3f(m_xMin, m_yMin, m_zMin);
            Gl.glVertex3f(m_xMax, m_yMin, m_zMin);
            Gl.glVertex3f(m_xMax, m_yMax, m_zMin);
            Gl.glVertex3f(m_xMin, m_yMax, m_zMin);

            //Top
            Gl.glVertex3f(m_xMin, m_yMax, m_zMin);
            Gl.glVertex3f(m_xMax, m_yMax, m_zMin);
            Gl.glVertex3f(m_xMax, m_yMax, m_zMax);
            Gl.glVertex3f(m_xMin, m_yMax, m_zMax);

            //Left
            Gl.glVertex3f(m_xMin, m_yMin, m_zMin);
            Gl.glVertex3f(m_xMin, m_yMax, m_zMin);
            Gl.glVertex3f(m_xMin, m_yMax, m_zMax);
            Gl.glVertex3f(m_xMin, m_yMin, m_zMax);

            //Right
            Gl.glVertex3f(m_xMax, m_yMin, m_zMin);
            Gl.glVertex3f(m_xMax, m_yMin, m_zMax);
            Gl.glVertex3f(m_xMax, m_yMax, m_zMax);
            Gl.glVertex3f(m_xMax, m_yMax, m_zMin);

            //Back
            Gl.glVertex3f(m_xMax, m_yMin, m_zMax);
            Gl.glVertex3f(m_xMin, m_yMin, m_zMax);
            Gl.glVertex3f(m_xMin, m_yMax, m_zMax);
            Gl.glVertex3f(m_xMax, m_yMax, m_zMax);

            //Bottom
            Gl.glVertex3f(m_xMin, m_yMin, m_zMin);
            Gl.glVertex3f(m_xMin, m_yMin, m_zMax);
            Gl.glVertex3f(m_xMax, m_yMin, m_zMax);
            Gl.glVertex3f(m_xMax, m_yMin, m_zMin);

            Gl.glEnd();

            Gl.glPopMatrix();
        }

    }
}

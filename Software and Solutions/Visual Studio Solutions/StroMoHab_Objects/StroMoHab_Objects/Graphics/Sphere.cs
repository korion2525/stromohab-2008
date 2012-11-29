using System;

using Tao.OpenGl;

namespace StroMoHab_Objects.Graphics
{
    class Sphere : OpenGlObject
    {
        Glu.GLUquadric m_quadric;
        float m_radius;

        public Sphere(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
            : base(xMin, yMin, zMin, xMax, yMax, zMax)
        {
            if (xMax > xMin)
            {
                m_radius = xMax - xMin;
            }
            else
            {
                m_radius = xMin - xMax;
            }

            m_quadric = Glu.gluNewQuadric();
            try
            {
                Glu.gluQuadricTexture(m_quadric, Gl.GL_TRUE);
                Glu.gluQuadricDrawStyle(m_quadric, Glu.GLU_FILL);
                Glu.gluQuadricNormals(m_quadric, Glu.GLU_SMOOTH);
                Glu.gluQuadricOrientation(m_quadric, Glu.GLU_OUTSIDE);
            }
            finally //if any errors, delete quadric
            {
                Glu.gluDeleteQuadric(m_quadric);
            }
        }

        public override void Draw()
        {
            Gl.glPushMatrix();

            //Gl.glBindTexture(Gl.GL_TEXTURE_2D, SDL_Main.TextureArray[7]);

            Gl.glTranslatef(XMax - XMin, YMax - YMin, ZMax + ZMin);
            Gl.glRotatef(XRotation, 1.0f, 0.0f, 0.0f);


            Glu.gluSphere(m_quadric, m_radius, 500, 500);

            Gl.glPopMatrix();
        }

    }
}

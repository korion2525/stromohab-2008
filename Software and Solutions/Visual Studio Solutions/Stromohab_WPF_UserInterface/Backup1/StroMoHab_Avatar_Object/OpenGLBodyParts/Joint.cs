using Tao.OpenGl;
using System;

namespace StroMoHab_Avatar_Object 
{
    /// <summary>
    /// Represents and OpenGl Joint
    /// </summary>
    class Joint
    {
        private float SCALLINGFACTOR = 800f; // Global scalling factor 
        private int SPHERERESOLUTION = 20; // Number of lines to build up spheres from

        /// <summary>
        /// Draws the Joint
        /// </summary>
        /// <param name="joint">The joint</param>
        public void Draw(StroMoHab_Objects.Objects.Joint joint)
        {
            Gl.glTranslatef(joint.xCoordinate / SCALLINGFACTOR, joint.yCoordinate / SCALLINGFACTOR, -joint.zCoordinate / SCALLINGFACTOR);
            Gl.glScalef(40f / SCALLINGFACTOR, 40f / SCALLINGFACTOR, 40f / SCALLINGFACTOR);
            SetDrawingMode(true);
            DrawUnitSphere();
            SetDrawingMode(false);
            DrawUnitSphere();
            Gl.glScalef(1/40f / SCALLINGFACTOR, 1/40f / SCALLINGFACTOR, 1/40f / SCALLINGFACTOR);
            Gl.glTranslatef(-joint.xCoordinate / SCALLINGFACTOR, -joint.yCoordinate / SCALLINGFACTOR, joint.zCoordinate / SCALLINGFACTOR);
            SetDrawingMode(true);
        }
        /// <summary>
        /// Draws a unit sphere which can be scalled to form and ellipsoid
        /// </summary>
        private void DrawUnitSphere()
        {

            float a = 1f, b = 1f, c = 1f;


            int i, j;
            for (i = 0; i <= SPHERERESOLUTION; i++)
            {
                float lat0 = (float)Math.PI * (-0.5f + (float)(i - 1) / SPHERERESOLUTION);
                float z0 = (float)Math.Sin(lat0);
                float zr0 = (float)Math.Cos(lat0);

                float lat1 = (float)Math.PI * (-0.5f + (float)i / SPHERERESOLUTION);
                float z1 = (float)Math.Sin(lat1);
                float zr1 = (float)Math.Cos(lat1);

                Gl.glBegin(Gl.GL_QUAD_STRIP);
                for (j = 0; j <= SPHERERESOLUTION; j++)
                {
                    float lng = 2 * (float)Math.PI * (float)(j - 1) / SPHERERESOLUTION;
                    float x = (float)Math.Cos(lng);
                    float y = (float)Math.Sin(lng);

                    Gl.glNormal3f(x * zr0, y * zr0, z0);
                    Gl.glVertex3f(x * zr0 * a, y * zr0 * b, z0 * c);
                    Gl.glNormal3f(x * zr1, y * zr1, z1);
                    Gl.glVertex3f(x * zr1 * a, y * zr1 * b, z1 * c);
                }
                Gl.glEnd();
            }
        }
        /// <summary>
        /// Sets the Polygon drawing mode to use
        /// </summary>
        /// <param name="fillNotMesh"></param>
        private void SetDrawingMode(bool fillNotMesh)
        {
            if (fillNotMesh) // Draw a filled polygon
            {
                Gl.glColor3f(1f, 0f, 1f);
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            }
            else // Draw a wire mesh (line) polygon
            {
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                Gl.glColor3f(0f, 0f, 0f);
            }
        }
    }
}

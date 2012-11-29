using System;

using Tao.OpenGl;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// A square or rectangle object.
    /// </summary>
    [Serializable]
    public class Quad : OpenGlObject
    {
        #region Member Variables

        Vector3f[] m_vertices = new Vector3f[4];

        #endregion Member Variables

        #region Constructors

        /// <summary>
        /// Constructor for non-textured quad.
        /// </summary>
        /// <param name="bottomLeftVertex"></param>
        /// <param name="bottomRightVertex"></param>
        /// <param name="topRightVertex"></param>
        /// <param name="topLeftVertex"></param>
        public Quad(Vector3f bottomLeftVertex, Vector3f bottomRightVertex, Vector3f topRightVertex, Vector3f topLeftVertex)
            : base((GetMinValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topLeftVertex.X)), (GetMinValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y)), (GetMinValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z)), (GetMaxValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topRightVertex.X)), (GetMaxValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y)), (GetMaxValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z)))
        {
            m_vertices[0] = bottomLeftVertex;
            m_vertices[1] = bottomRightVertex;
            m_vertices[2] = topRightVertex;
            m_vertices[3] = topLeftVertex;

            CollisionModel = new BoundingBox(GetMinValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topLeftVertex.X),
                                                GetMinValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y),
                                                GetMinValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z),
                                                GetMaxValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topRightVertex.X),
                                                GetMaxValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y),
                                                GetMaxValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z)
                                             );
        }
        
        /// <summary>
        /// Constructor for textured quad.
        /// </summary>
        /// <param name="textureNumber"></param>
        /// <param name="textureWidth"></param>
        /// <param name="textureHeight"></param>
        /// <param name="bottomLeftVertex"></param>
        /// <param name="bottomRightVertex"></param>
        /// <param name="topRightVertex"></param>
        /// <param name="topLeftVertex"></param>
        public Quad(int textureNumber, float textureWidth, float textureHeight, Vector3f bottomLeftVertex, Vector3f bottomRightVertex, Vector3f topRightVertex, Vector3f topLeftVertex)
            : base((GetMinValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topLeftVertex.X)), (GetMinValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y)), (GetMinValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z)), (GetMaxValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topRightVertex.X)), (GetMaxValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y)), (GetMaxValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z)))
        {
            m_vertices[0] = bottomLeftVertex;
            m_vertices[1] = bottomRightVertex;
            m_vertices[2] = topRightVertex;
            m_vertices[3] = topLeftVertex;

            this.TextureNumber = textureNumber;
            this.TextureWidth = textureWidth;
            this.TextureHeight = textureHeight;

            CollisionModel = new BoundingBox(GetMinValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topLeftVertex.X),
                                                GetMinValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y),
                                                GetMinValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z),
                                                GetMaxValue(bottomLeftVertex.X, bottomRightVertex.X, topRightVertex.X, topRightVertex.X),
                                                GetMaxValue(bottomLeftVertex.Y, bottomRightVertex.Y, topRightVertex.Y, topLeftVertex.Y),
                                                GetMaxValue(bottomLeftVertex.Z, bottomRightVertex.Z, topRightVertex.Z, topLeftVertex.Z)
                                             );
        }

        #endregion Constructors

        private static float GetMinValue(float f1, float f2, float f3, float f4)
        {
            float k = 999999;

            for (int i = 0; i < 4; i++)
            {
                if (f1 < k)
                {
                    k = f1;
                }
                if (f2 < k)
                {
                    k = f2;
                }
                if (f3 < k)
                {
                    k = f3;
                }
                if (f4 < k)
                {
                    k = f4;
                }
            }
            return k;

        }

        private static float GetMaxValue(float f1, float f2, float f3, float f4)
        {
            float k = -999999;

            for (int i = 0; i < 4; i++)
            {
                if (f1 > k)
                {
                    k = f1;
                }
                if (f2 > k)
                {
                    k = f2;
                }
                if (f3 > k)
                {
                    k = f3;
                }
                if (f4 > k)
                {
                    k = f4;
                }
            }
            return k;

        }


        #region Properties

        /// <summary>
        /// The bottom left vertext of the quad.
        /// </summary>
        public Vector3f BottomLeftVertex
        {
            get
            {
                return m_vertices[0];
            }
            set
            {
                m_vertices[0] = value;
            }
        }

        /// <summary>
        /// The bottom right vertex of the quad.
        /// </summary>
        public Vector3f BottomRightVertex
        {
            get
            {
                return m_vertices[1];
            }
            set
            {
                m_vertices[1] = value;
            }
        }

        /// <summary>
        /// The top right vertex of the quad.
        /// </summary>
        public Vector3f TopRightVertex
        {
            get
            {
                return m_vertices[2];
            }
            set
            {
                m_vertices[2] = value;
            }
        }

        /// <summary>
        /// The top left vertex of the quad.
        /// </summary>
        public Vector3f TopLeftVertex
        {
            get
            {
                return m_vertices[3];
            }
            set
            {
                m_vertices[3] = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Draws a modified version of the quad if it is selected.
        /// </summary>
        /// <param name="selected"></param>
        public void Draw(bool selected)
        {
            Gl.glPushMatrix();

            Gl.glRotatef(YRotation, 0.0f, 1.0f, 0.0f);

            if (this.TextureNumber != -1)
            {
                //Bind the texture
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, Textures.GetTexture(this.TextureNumber));

                //Begin Square
                Gl.glBegin(Gl.GL_QUADS);

                if (selected == true)
                {
                    Gl.glColor4f(0.0f, 1.0f, 0.0f, 0.0f);
                }
                else
                {
                    Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);
                }
                
                //Bottom Left
                Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glVertex3f(m_vertices[0].X, m_vertices[0].Y, m_vertices[0].Z);
                
                //Bottom Right
                Gl.glTexCoord2f(this.TextureWidth, 0.0f);
                Gl.glVertex3f(m_vertices[1].X, m_vertices[1].Y, m_vertices[1].Z);
                
                //Top Right
                Gl.glTexCoord2f(this.TextureWidth, this.TextureHeight);
                Gl.glVertex3f(m_vertices[2].X, m_vertices[2].Y, m_vertices[2].Z);
                
                //Top Left
                Gl.glTexCoord2f(0.0f, this.TextureHeight);
                Gl.glVertex3f(m_vertices[3].X, m_vertices[3].Y, m_vertices[3].Z);
                
                //End Square
                Gl.glEnd();

            }
            else
            {
                //Begin Square
                Gl.glBegin(Gl.GL_QUADS);

                if (selected == true)
                {
                    Gl.glColor4f(0.0f, 1.0f, 0.0f, 0.0f);
                }
                else
                {
                    Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);
                }

                //Bottom Left
                Gl.glVertex3f(m_vertices[0].X, m_vertices[0].Y, m_vertices[0].Z);

                //Bottom Right
                Gl.glVertex3f(m_vertices[1].X, m_vertices[1].Y, m_vertices[1].Z);

                //Top Right
                Gl.glVertex3f(m_vertices[2].X, m_vertices[2].Y, m_vertices[2].Z);

                //Top Left
                Gl.glVertex3f(m_vertices[3].X, m_vertices[3].Y, m_vertices[3].Z);

                //End Square
                Gl.glEnd();

            }

            Gl.glPopMatrix();
        }

        /// <summary>
        /// Draws the quad.
        /// </summary>
        public override void Draw()
        {
            Gl.glPushMatrix();
            
            Gl.glRotatef(XRotation, 1.0f, 0.0f, 0.0f);
            Gl.glRotatef(YRotation, 0.0f, 1.0f, 0.0f);
            Gl.glRotatef(ZRotation, 0.0f, 0.0f, 1.0f);
            

            if (this.TextureNumber != -1)
            {
                //Bind the texture
                //Gl.glBindTexture(Gl.GL_TEXTURE_2D, Textures.GetTexture(this.TextureNumber));
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, this.TextureNumber);

                //Begin Square
                Gl.glBegin(Gl.GL_QUADS);

                Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);

                //Bottom Left
                Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glVertex3f(m_vertices[0].X, m_vertices[0].Y, m_vertices[0].Z);

                //Bottom Right
                Gl.glTexCoord2f(this.TextureWidth, 0.0f);
                Gl.glVertex3f(m_vertices[1].X, m_vertices[1].Y, m_vertices[1].Z);

                //Top Right
                Gl.glTexCoord2f(this.TextureWidth, this.TextureHeight);
                Gl.glVertex3f(m_vertices[2].X, m_vertices[2].Y, m_vertices[2].Z);

                //Top Left
                Gl.glTexCoord2f(0.0f, this.TextureHeight);
                Gl.glVertex3f(m_vertices[3].X, m_vertices[3].Y, m_vertices[3].Z);

                //End Square
                Gl.glEnd();

            }
            else
            {
                //Begin Square
                Gl.glBegin(Gl.GL_QUADS);

                Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);

                //Bottom Left
                Gl.glVertex3f(m_vertices[0].X, m_vertices[0].Y, m_vertices[0].Z);

                //Bottom Right
                Gl.glVertex3f(m_vertices[1].X, m_vertices[1].Y, m_vertices[1].Z);

                //Top Right
                Gl.glVertex3f(m_vertices[2].X, m_vertices[2].Y, m_vertices[2].Z);

                //Top Left
                Gl.glVertex3f(m_vertices[3].X, m_vertices[3].Y, m_vertices[3].Z);

                //End Square
                Gl.glEnd();

            }

            Gl.glPopMatrix();
        }

        #endregion Methods
    }
}

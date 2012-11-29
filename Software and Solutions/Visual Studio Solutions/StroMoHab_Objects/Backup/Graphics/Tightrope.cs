using System;
using Tao.OpenGl;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Tightrope graphics object
    /// </summary>
    [Serializable]
    public class Tightrope : OpenGlObject
    {
        /// <summary>
        /// Tightrope without texture
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        public Tightrope(float width, float length)
            : base(-width / 2, 0.01f, 0.0f, width / 2, 1.0f, -length)
        {
                   
            CollisionModel = new BoundingBox(-width / 2, 0.0f, 0.0f, width / 2, 0.0f, -length);

        }

        /// <summary>
        /// Tightrope with texture
        /// </summary>
        /// <param name="textureNumber"></param>
        /// <param name="textureWidth"></param>
        /// <param name="textureHeight"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        public Tightrope(int textureNumber, float textureWidth, float textureHeight, float width, float length)
            : base(-width / 2, 0.0f, 0.001f, width / 2, 0.001f, -length)
        {

            this.TextureNumber = textureNumber;
            this.TextureWidth = textureWidth;
            this.TextureHeight = textureHeight;

            CollisionModel = new BoundingBox(-width / 2, 0.0f, 0.0f, width / 2, 0.0f, -length);
        }

        /// <summary>
        /// Draws the tightrope
        /// </summary>
        public override void Draw()
        {
            Gl.glPushMatrix();

            if (this.Selected == true)
            {
                Gl.glColor4f(0.0f, 1.0f, 0.0f, 0.0f);
            }
            else
            {
                Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);
            }


            if (this.TextureNumber == -1)
            {
                //Begin drawing
                Gl.glBegin(Gl.GL_QUADS);

                //Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);

                //Bottom Left
                Gl.glVertex3f(XMin, YMin, ZMax);

                //Bottom Right
                Gl.glVertex3f(XMax, YMin, ZMax);

                //Top Right
                Gl.glVertex3f(XMax, YMax, ZMin);

                //Top Left
                Gl.glVertex3f(XMin, YMax, ZMin);

                //End Square
                Gl.glEnd();
            }
            else
            {
                //Bind the texture
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, Textures.GetTexture(this.TextureNumber));

                //Begin drawing
                Gl.glBegin(Gl.GL_QUADS);

                //Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);

                //Bottom Left
                Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glVertex3f(XMin, YMin, ZMax);

                //Bottom Right
                Gl.glTexCoord2f(this.TextureWidth, 0.0f);
                Gl.glVertex3f(XMax, YMin, ZMax);

                //Top Right
                Gl.glTexCoord2f(this.TextureWidth, this.TextureHeight);
                Gl.glVertex3f(XMax, YMax, ZMin);

                //Top Left
                Gl.glTexCoord2f(0.0f, this.TextureHeight);
                Gl.glVertex3f(XMin, YMax, ZMin);

                //End Square
                Gl.glEnd();
            }


            Gl.glPopMatrix();
        }
    }
}

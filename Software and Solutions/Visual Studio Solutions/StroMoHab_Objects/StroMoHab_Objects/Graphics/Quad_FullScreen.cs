using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace StroMoHab_Objects.Graphics
{
    public class Quad_FullScreen:OpenGlObject
    {
        public Quad_FullScreen(int textureNumber) : base(textureNumber,1.0f,1.0f,-1.0f,-1.0f,-1.0f,1.0f,1.0f,-1.0f)
        {
        }
        public override void Draw()
        {
            Gl.glPushMatrix();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();
            

            //Gl.glRotatef(XRotation, 1.0f, 0.0f, 0.0f);
            //Gl.glRotatef(YRotation, 0.0f, 1.0f, 0.0f);
            //Gl.glRotatef(ZRotation, 0.0f, 0.0f, 1.0f);


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
                Gl.glVertex3i(-1,-1,1);

                //Bottom Right
                Gl.glTexCoord2f(1.0f, 0.0f);
                Gl.glVertex3i(1,-1,1);

                //Top Right
                Gl.glTexCoord2f(1.0f,1.0f);
                Gl.glVertex3i(1,1,1);

                //Top Left
                Gl.glTexCoord2f(0.0f, 1.0f);
                Gl.glVertex3f(-1,1,1);

                //End Square
                Gl.glEnd();

                Gl.glPopMatrix();
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glPopMatrix();

            }
            else
            {
                //Begin Square
                Gl.glBegin(Gl.GL_QUADS);

                Gl.glColor4f(Colour.Red, Colour.Green, Colour.Blue, Colour.Alpha);

                //Bottom Left
                Gl.glVertex3i(-1, -1, -1);

                //Bottom Right
                Gl.glVertex3i(1, -1, -1);

                //Top Right
                Gl.glVertex3i(1, 1, -1);

                //Top Left
                Gl.glVertex3f(-1, 1, -1);

                //End Square
                Gl.glEnd();

                Gl.glPopMatrix();
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glPopMatrix();

            }

            Gl.glPopMatrix();
        }


    }
}

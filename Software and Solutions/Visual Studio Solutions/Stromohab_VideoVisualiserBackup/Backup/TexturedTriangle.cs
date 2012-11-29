//using System;
//using System.IO;
//using System.Drawing;
//using System.Drawing.Imaging;

//using Tao.OpenGl;

//namespace StromoLight_Visualiser
//{
//    class TexturedTriangle : OpenGlObject
//    {
//       public TexturedTriangle()
//        {
//        }

//        public TexturedTriangle(float width, float height, float depth)
//        {
//        }

//        public override void Draw()
//        {
//            Gl.glPushMatrix();

            

//            Gl.glColor3f(1.0f, 1.0f, 1.0f);
//            Gl.glBindTexture(Gl.GL_TEXTURE_2D, SDL_Main.TextureArray[3]);

//            //Begin drawing triangle
//            Gl.glBegin(Gl.GL_TRIANGLES);
//            //Top
//            Gl.glTexCoord2f(0.0f, 1.0f);
//            Gl.glVertex3f(0.0f, Height / 2, 0.0f);

//            //Bottom Left
//            Gl.glTexCoord2f(0.0f, 0.0f);
//            Gl.glVertex3f(-Length / 2, -Height / 2, -Depth);

//            //Bottom Right
//            Gl.glTexCoord2f(1.0f, 0.0f);
//            Gl.glVertex3f(Length / 2, -Height / 2, -Depth);

//            //End drawing triangle
//            Gl.glEnd();

//            Gl.glPopMatrix();
//        }

//    }
//}

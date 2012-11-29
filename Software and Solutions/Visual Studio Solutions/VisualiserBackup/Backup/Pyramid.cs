//using System;

//using Tao.OpenGl;

//namespace StromoLight_Visualiser
//{
//    class Pyramid : OpenGlObject
//    {
//        #region Constructors

//        #endregion Constructors

//        public override void Draw()
//        {
//            Gl.glPushMatrix();

//            //Begin Pyramid
//            //Define faces
//            TexturedTriangle front = new TexturedTriangle(Length, Height, Depth / 2);
//            TexturedTriangle back = new TexturedTriangle(Length, Height, Depth / 2);
//            TexturedTriangle left = new TexturedTriangle(Length, Height, Depth / 2);
//            TexturedTriangle right = new TexturedTriangle(Length, Height, Depth / 2);

//            //Quad bottom = new Quad(Length, Height);
//            //bottom.Z = -Height/2;

//            ////Set face colours so that corners match
//            //front.VertexOneColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            //front.VertexTwoColour = new float[3] { 0.0f, 1.0f, 0.0f };
//            //front.VertexThreeColour = new float[3] { 0.0f, 0.0f, 1.0f };

//            //right.VertexOneColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            //right.VertexTwoColour = new float[3] { 0.0f, 0.0f, 1.0f };
//            //right.VertexThreeColour = new float[3] { 0.0f, 1.0f, 0.0f };

//            //back.VertexOneColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            //back.VertexTwoColour = new float[3] { 0.0f, 1.0f, 0.0f };
//            //back.VertexThreeColour = new float[3] { 0.0f, 0.0f, 1.0f };

//            //left.VertexOneColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            //left.VertexTwoColour = new float[3] { 0.0f, 0.0f, 1.0f };
//            //left.VertexThreeColour = new float[3] { 0.0f, 1.0f, 0.0f };

//            //bottom.TopLeftColour = new float[3] { 0.0f, 0.0f, 1.0f };
//            //bottom.BottomLeftColour = new float[3] { 0.0f, 1.0f, 0.0f };
//            //bottom.BottomRightColour = new float[3] { 0.0f, 0.0f, 1.0f };
//            //bottom.TopRightColour = new float[3] { 0.0f, 1.0f, 0.0f };


//            Gl.glRotatef(-90.0f, 1.0f, 0.0f, 0.0f);
//            //bottom.Draw();
//            Gl.glRotatef(90.0f, 1.0f, 0.0f, 0.0f);

//            front.Draw();
//            Gl.glRotatef(-90.0f, 0.0f, 1.0f, 0.0f);
//            right.Draw();
//            Gl.glRotatef(-90.0f, 0.0f, 1.0f, 0.0f);
//            back.Draw();
//            Gl.glRotatef(-90.0f, 0.0f, 1.0f, 0.0f);
//            left.Draw();

//            Gl.glPopMatrix();
//        }

//    }
//}

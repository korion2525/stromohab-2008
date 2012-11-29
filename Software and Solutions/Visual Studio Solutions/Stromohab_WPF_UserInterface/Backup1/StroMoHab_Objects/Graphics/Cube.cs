using System;
using System.Collections.Generic;
using Tao.OpenGl;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Cube object.
    /// </summary>
    [Serializable]
    public class Cube : OpenGlObject
    {
        Quad m_leftFace, m_rightFace, m_bottomFace, m_topFace, m_frontFace, m_backFace;

        /// <summary>
        /// Constructor for non-textured cube.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        public Cube(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
            : base(xMin, yMin, zMin, xMax, yMax, zMax)
        {
            //XMin = xMin;
            //YMin = yMin;
            //ZMin = zMin;
            //XMax = xMax;
            //YMax = yMax;
            //ZMax = zMax;

            m_leftFace = new Quad(  new Vector3f(XMin, YMin, ZMax),
                                    new Vector3f(XMin, YMin, ZMin),
                                    new Vector3f(XMin, YMax, ZMin),
                                    new Vector3f(XMin, YMax, ZMax)
                                 );

            m_rightFace = new Quad( new Vector3f(XMax, YMin, ZMin),
                                    new Vector3f(XMax, YMin, ZMax),
                                    new Vector3f(XMax, YMax, ZMax),
                                    new Vector3f(XMax, YMax, ZMin)                                  
                                   );

            m_bottomFace = new Quad( new Vector3f(XMin, YMin, ZMax),
                                     new Vector3f(XMax, YMin, ZMax),
                                     new Vector3f(XMax, YMin, ZMin),
                                     new Vector3f(XMin, YMin, ZMin)
                                    );

            m_topFace = new Quad( new Vector3f(XMin, YMax, ZMin),
                                  new Vector3f(XMax, YMax, ZMin),
                                  new Vector3f(XMax, YMax, ZMax),
                                  new Vector3f(XMin, YMax, ZMax)
                                );

            m_frontFace = new Quad( new Vector3f(XMin, YMin, ZMin),
                                    new Vector3f(XMax, YMin, ZMin),
                                    new Vector3f(XMax, YMax, ZMin),
                                    new Vector3f(XMin, YMax, ZMin)
                                  );

            m_backFace = new Quad( new Vector3f(XMax, YMin, ZMax),
                                   new Vector3f(XMin, YMin, ZMax),
                                   new Vector3f(XMin, YMax, ZMax),
                                   new Vector3f(XMax, YMax, ZMax)
                                  );


            //m_leftFace.CollisionModel = new BoundingBox(xMin, YMin, ZMin, xMax, yMax, zMax);

            //CollisionModel = new List<BoundingBox>();
            CollisionModel = new BoundingBox(XMin,YMin,ZMax,XMax,YMax,ZMin);
        }

        /// <summary>
        /// Constructor for textured cube.
        /// </summary>
        /// <param name="textureNumber"></param>
        /// <param name="textureWidth"></param>
        /// <param name="textureHeight"></param>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        public Cube(int textureNumber, float textureWidth, float textureHeight, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
            : base(textureNumber,textureWidth,textureHeight,xMin, yMin, zMin, xMax, yMax, zMax)
        {
            //XMin = xMin;
            //YMin = yMin;
            //ZMin = zMin;
            //XMax = xMax;
            //YMax = yMax;
            //ZMax = zMax;

            this.TextureNumber = textureNumber;
            this.TextureWidth = textureWidth;
            this.TextureHeight = textureHeight;

            if (this.TextureNumber != -1)
            {
                m_leftFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMin, ZMax),
                                         new Vector3f(XMin, YMin, ZMin),
                                         new Vector3f(XMin, YMax, ZMin),
                                         new Vector3f(XMin, YMax, ZMax)
                                      );

                m_rightFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMax, YMin, ZMin),
                                         new Vector3f(XMax, YMin, ZMax),
                                         new Vector3f(XMax, YMax, ZMax),
                                         new Vector3f(XMax, YMax, ZMin)
                                        );

                m_bottomFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMin, ZMax),
                                          new Vector3f(XMax, YMin, ZMax),
                                          new Vector3f(XMax, YMin, ZMin),
                                          new Vector3f(XMin, YMin, ZMin)
                                         );

                m_topFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMax, ZMin),
                                       new Vector3f(XMax, YMax, ZMin),
                                       new Vector3f(XMax, YMax, ZMax),
                                       new Vector3f(XMin, YMax, ZMax)
                                     );

                m_frontFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMin, ZMin),
                                         new Vector3f(XMax, YMin, ZMin),
                                         new Vector3f(XMax, YMax, ZMin),
                                         new Vector3f(XMin, YMax, ZMin)
                                       );

                m_backFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMax, YMin, ZMax),
                                        new Vector3f(XMin, YMin, ZMax),
                                        new Vector3f(XMin, YMax, ZMax),
                                        new Vector3f(XMax, YMax, ZMax)
                                       );

            }
            else
            {
                m_leftFace = new Quad(new Vector3f(XMin, YMin, ZMax),
                                        new Vector3f(XMin, YMin, ZMin),
                                        new Vector3f(XMin, YMax, ZMin),
                                        new Vector3f(XMin, YMax, ZMax)
                                     );

                m_rightFace = new Quad(new Vector3f(XMax, YMin, ZMin),
                                        new Vector3f(XMax, YMin, ZMax),
                                        new Vector3f(XMax, YMax, ZMax),
                                        new Vector3f(XMax, YMax, ZMin)
                                       );

                m_bottomFace = new Quad(new Vector3f(XMin, YMin, ZMax),
                                         new Vector3f(XMax, YMin, ZMax),
                                         new Vector3f(XMax, YMin, ZMin),
                                         new Vector3f(XMin, YMin, ZMin)
                                        );

                m_topFace = new Quad(new Vector3f(XMin, YMax, ZMin),
                                      new Vector3f(XMax, YMax, ZMin),
                                      new Vector3f(XMax, YMax, ZMax),
                                      new Vector3f(XMin, YMax, ZMax)
                                    );

                m_frontFace = new Quad(new Vector3f(XMin, YMin, ZMin),
                                        new Vector3f(XMax, YMin, ZMin),
                                        new Vector3f(XMax, YMax, ZMin),
                                        new Vector3f(XMin, YMax, ZMin)
                                      );

                m_backFace = new Quad(new Vector3f(XMax, YMin, ZMax),
                                       new Vector3f(XMin, YMin, ZMax),
                                       new Vector3f(XMin, YMax, ZMax),
                                       new Vector3f(XMax, YMax, ZMax)
                                      );

            }


            //m_leftFace.CollisionModel = new BoundingBox(xMin, YMin, ZMin, xMax, yMax, zMax);

            //CollisionModel = new List<BoundingBox>();
            CollisionModel = new BoundingBox(XMin, YMin, ZMax, XMax, YMax, ZMin);
        }

        /// <summary>
        /// Draws the cube.
        /// </summary>
        public override void Draw()
        {
            Gl.glPushMatrix();

            //CollisionModel = new BoundingBox(XMin, YMin, ZMax, XMax, YMax, ZMin);

            if (this.TextureNumber != -1)
            {
               m_leftFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMin, ZMax),
                                        new Vector3f(XMin, YMin, ZMin),
                                        new Vector3f(XMin, YMax, ZMin),
                                        new Vector3f(XMin, YMax, ZMax)
                                     );

               m_rightFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMax, YMin, ZMin),
                                        new Vector3f(XMax, YMin, ZMax),
                                        new Vector3f(XMax, YMax, ZMax),
                                        new Vector3f(XMax, YMax, ZMin)
                                       );

               m_bottomFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMin, ZMax),
                                         new Vector3f(XMax, YMin, ZMax),
                                         new Vector3f(XMax, YMin, ZMin),
                                         new Vector3f(XMin, YMin, ZMin)
                                        );

               m_topFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMax, ZMin),
                                      new Vector3f(XMax, YMax, ZMin),
                                      new Vector3f(XMax, YMax, ZMax),
                                      new Vector3f(XMin, YMax, ZMax)
                                    );

               m_frontFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMin, YMin, ZMin),
                                        new Vector3f(XMax, YMin, ZMin),
                                        new Vector3f(XMax, YMax, ZMin),
                                        new Vector3f(XMin, YMax, ZMin)
                                      );

               m_backFace = new Quad(this.TextureNumber, this.TextureWidth, this.TextureHeight, new Vector3f(XMax, YMin, ZMax),
                                       new Vector3f(XMin, YMin, ZMax),
                                       new Vector3f(XMin, YMax, ZMax),
                                       new Vector3f(XMax, YMax, ZMax)
                                      );

            }
            else
            {
                m_leftFace = new Quad(  new Vector3f(XMin, YMin, ZMax),
                                        new Vector3f(XMin, YMin, ZMin),
                                        new Vector3f(XMin, YMax, ZMin),
                                        new Vector3f(XMin, YMax, ZMax)
                                     );

                m_rightFace = new Quad( new Vector3f(XMax, YMin, ZMin),
                                        new Vector3f(XMax, YMin, ZMax),
                                        new Vector3f(XMax, YMax, ZMax),
                                        new Vector3f(XMax, YMax, ZMin)
                                       );

                m_bottomFace = new Quad( new Vector3f(XMin, YMin, ZMax),
                                         new Vector3f(XMax, YMin, ZMax),
                                         new Vector3f(XMax, YMin, ZMin),
                                         new Vector3f(XMin, YMin, ZMin)
                                        );

                m_topFace = new Quad( new Vector3f(XMin, YMax, ZMin),
                                      new Vector3f(XMax, YMax, ZMin),
                                      new Vector3f(XMax, YMax, ZMax),
                                      new Vector3f(XMin, YMax, ZMax)
                                    );

                m_frontFace = new Quad( new Vector3f(XMin, YMin, ZMin),
                                        new Vector3f(XMax, YMin, ZMin),
                                        new Vector3f(XMax, YMax, ZMin),
                                        new Vector3f(XMin, YMax, ZMin)
                                      );

                m_backFace = new Quad( new Vector3f(XMax, YMin, ZMax),
                                       new Vector3f(XMin, YMin, ZMax),
                                       new Vector3f(XMin, YMax, ZMax),
                                       new Vector3f(XMax, YMax, ZMax)
                                      );

            }


            //m_leftFace.CollisionModel = new BoundingBox(xMin, YMin, ZMin, xMax, yMax, zMax);

            //CollisionModel = new List<BoundingBox>();
            //CollisionModel = new BoundingBox(XMin, YMin, ZMax, XMax, YMax, ZMin);


            //Begin Cube
            //Define faces

            //front.TopLeftColour = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };     //Cyan
            //front.BottomLeftColour = new float[] { 0.0f, 0.0f, 1.0f, 0.0f };  //Blue
            //front.BottomRightColour = new float[] { 1.0f, 0.0f, 1.0f, 0.0f }; //Magneta
            //front.TopRightColour = new float[] { 1.0f, 1.0f, 1.0f, 0.0f };    //White

            //left.TopLeftColour = new float[] { 0.0f, 1.0f, 0.0f, 0.0f };      //Green
            //left.BottomLeftColour = new float[] { 0.0f, 0.0f, 0.0f, 0.0f };   //Black
            //left.BottomRightColour = new float[] { 0.0f, 0.0f, 1.0f, 0.0f };  //Blue
            //left.TopRightColour = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };     //Cyan

            //back.TopLeftColour = new float[] { 1.0f, 1.0f, 0.0f, 0.0f };      //Yellow
            //back.BottomLeftColour = new float[] { 1.0f, 0.0f, 0.0f, 0.0f };   //Red
            //back.BottomRightColour = new float[] { 0.0f, 0.0f, 0.0f, 0.0f };  //Black
            //back.TopRightColour = new float[] { 0.0f, 1.0f, 0.0f, 0.0f };     //Green

            //right.TopLeftColour = new float[] { 1.0f, 1.0f, 1.0f, 0.0f };     //White
            //right.BottomLeftColour = new float[] { 1.0f, 0.0f, 1.0f, 0.0f };  //Magneta
            //right.BottomRightColour = new float[] { 1.0f, 0.0f, 0.0f, 0.0f }; //Red
            //right.TopRightColour = new float[] { 1.0f, 1.0f, 0.0f, 0.0f };    //Yellow

            //top.TopLeftColour = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };       //Cyan
            //top.BottomLeftColour = new float[] { 1.0f, 1.0f, 1.0f, 0.0f };    //White
            //top.BottomRightColour = new float[] { 1.0f, 1.0f, 0.0f, 0.0f };   //Yellow
            //top.TopRightColour = new float[] { 0.0f, 1.0f, 0.0f, 0.0f };      //Green

            //bottom.TopLeftColour = new float[] { 1.0f, 0.0f, 1.0f, 0.0f };    //Magneta
            //bottom.BottomLeftColour = new float[] { 0.0f, 0.0f, 1.0f, 0.0f }; //Blue
            //bottom.BottomRightColour = new float[] { 0.0f, 0.0f, 0.0f, 0.0f };//Black
            //bottom.TopRightColour = new float[] { 1.0f, 0.0f, 0.0f, 0.0f };   //Red

            m_leftFace.Draw(this.Selected);
            m_rightFace.Draw(this.Selected);
            m_bottomFace.Draw(this.Selected);
            m_topFace.Draw(this.Selected);
            m_frontFace.Draw(this.Selected);
            m_backFace.Draw(this.Selected);

            Gl.glPopMatrix();

        }

    }
}


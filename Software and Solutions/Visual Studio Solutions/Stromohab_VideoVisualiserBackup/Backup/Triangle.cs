//using System;

//using Tao.OpenGl;

//namespace StromoLight_Visualiser
//{
//    class Triangle : OpenGlObject
//    {
//        private float[] m_vertexOneColour;
//        private float[] m_vertexTwoColour;
//        private float[] m_vertexThreeColour;

//        #region Properties
//        /// <summary>
//        /// Colour of Top Vertex
//        /// </summary>
//        public float[] VertexOneColour
//        {
//            set
//            {
//                m_vertexOneColour = value;
//            }
//        }

//        /// <summary>
//        /// Colour of Bottom Left Vertex
//        /// </summary>
//        public float[] VertexTwoColour
//        {
//            set
//            {
//                m_vertexTwoColour = value;
//            }
//        }

//        /// <summary>
//        /// Colour of Bottom Right Vertex
//        /// </summary>
//        public float[] VertexThreeColour
//        {
//            set
//            {
//                m_vertexThreeColour = value;
//            }
//        }

//        #endregion Properties

//        #region Constructors
//        /// <summary>
//        /// Triangle with default size and position. Default colour is all grey.
//        /// </summary>
//        public Triangle()
//        {
//            m_vertexOneColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            m_vertexTwoColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            m_vertexThreeColour = new float[3] { 1.0f, 0.0f, 0.0f };

//        }

//        /// <summary>
//        /// Triangle with user defined size. Default colour is all grey.
//        /// </summary>
//        /// <param name="width"></param>
//        /// <param name="height"></param>
//        /// <param name="depth"></param>
//        public Triangle(float width, float height, float depth)
//        {

//            CollisionModel = new BoundingBox(X, Y, Z, Length, Height, Depth);

//            m_vertexOneColour = new float[3] { 1.0f, 0.0f, 0.0f };
//            m_vertexTwoColour = new float[3] { 0.0f, 1.0f, 0.0f };
//            m_vertexThreeColour = new float[3] { 0.0f, 0.0f, 1.0f };
//        }
//        #endregion Constructors

//        public override void Draw()
//        {
//            Gl.glPushMatrix();

            
//            //Begin drawing triangle
//            Gl.glBegin(Gl.GL_TRIANGLES);
//            //Top
//            Gl.glColor3f(m_vertexOneColour[0], m_vertexOneColour[1], m_vertexOneColour[2]);
//            Gl.glVertex3f(0.0f, Height/2, Depth/2);
//            //Bottom Left
//            Gl.glColor3f(m_vertexTwoColour[0], m_vertexTwoColour[1], m_vertexTwoColour[2]);
//            Gl.glVertex3f(-Length/2, -Height/2, -Depth/2);
//            //Bottom Right
//            Gl.glColor3f(m_vertexThreeColour[0], m_vertexThreeColour[1], m_vertexThreeColour[2]);
//            Gl.glVertex3f(Length/2, -Height/2, -Depth/2);
//            //End drawing triangle
//            Gl.glEnd();

//            Gl.glPopMatrix();

//            //m_collisionBox.X = X;
//            //m_collisionBox.Y = Y;
//            //m_collisionBox.Z = Z;
//            //m_collisionBox.Length = Length;
//            //m_collisionBox.Height = Depth;
//            //m_collisionBox.Draw();

//        }

//    }
//}

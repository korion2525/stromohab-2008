using System;
using Tao.OpenGl;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// An OpenGL object
    /// </summary>
    [Serializable]
    public class OpenGlObject : IOpenGlObject
    {
        #region Member Variables

        float m_xRotation;
        float m_yRotation;
        float m_zRotation;

        BoundingBox m_boundingBox;

        Vector3f m_minBounds = new Vector3f(-9999.0f, -9999.0f, -9999.0f);
        Vector3f m_maxBounds = new Vector3f(-9999.0f, -9999.0f, -9999.0f);

        Vector3f m_dimension = new Vector3f(-9999.0f, -9999.0f, -9999.0f);

        Vector3f m_position = new Vector3f(0.0f, 0.0f, 0.0f);
        Vector3f m_velocity = new Vector3f(0.0f, 0.0f, 0.0f);
        Vector3f m_acceleration = new Vector3f(0.0f, 0.0f, 0.0f);

        Colour m_colour = new Colour(0.5f, 0.5f, 0.5f, 0.0f);

        int m_textureNumber = -1;
        float m_textureWidth = -1;
        float m_textureHeight = -1;

        bool m_selected = false;
        
        #endregion Member Variables

        #region Properties
        /// <summary>
        /// The object's X coordinate.
        /// </summary>
        public float X
        {
            get
            {
                return m_position.X;
            }
            set
            {
                m_position.X = value;
                m_maxBounds.X = m_position.X + Length / 2;
                m_minBounds.X = m_position.X - Length / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The object's Y coordinate.
        /// </summary>
        public float Y
        {
            get
            {
                return m_position.Y;
            }
            set
            {
                m_position.Y = value;
                m_maxBounds.Y = m_position.Y + Height / 2;
                m_minBounds.Y = m_position.Y - Height / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The object's Z coordinate.
        /// </summary>
        public float Z
        {
            get
            {
                return m_position.Z;
            }
            set
            {
                m_position.Z = value;
                m_maxBounds.Z = m_position.Z + Depth / 2;
                m_minBounds.Z = m_position.Z - Depth / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The leftmost point/edge of the OpenGl object
        /// </summary>
        public float XMin
        {
            get
            {
                return m_minBounds.X;
            }
            set
            {
                m_minBounds.X = value;
                m_dimension.X = XMax - XMin;
                m_position.X = (XMax + XMin) / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The lowest point/edge of the OpenGl object
        /// </summary>
        public float YMin
        {
            get
            {
                return m_minBounds.Y;
            }
            set
            {
                m_minBounds.Y = value;
                m_dimension.Y = YMax - YMin;
                m_position.Y = (YMax + YMin) / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The object's point/edge furthest into the screen
        /// </summary>
        public float ZMin
        {
            get
            {
                return m_minBounds.Z;
            }
            set
            {
                m_minBounds.Z = value;
                m_dimension.Z = ZMin - ZMax;
                m_position.Z = (ZMax + ZMin) / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The rightmost point/edge of the object
        /// </summary>
        public float XMax
        {
            get
            {
                return m_maxBounds.X;
            }
            set
            {
                m_maxBounds.X = value;
                m_dimension.X = XMax - XMin;
                m_position.X = (XMax + XMin) / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The highest point/edge of the OpenGl object
        /// </summary>
        public float YMax
        {
            get
            {
                return m_maxBounds.Y;
            }
            set
            {
                m_maxBounds.Y = value;
                m_dimension.Y = YMax - YMin;
                m_position.Y = (YMax + YMin) / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }

        /// <summary>
        /// The object's point/edge closest to the screen.
        /// </summary>
        public float ZMax
        {
            get
            {
                return m_maxBounds.Z;
            }
            set
            {
                m_maxBounds.Z = value;
                m_dimension.Z = ZMin - ZMax;
                m_position.Z = (ZMax + ZMin) / 2;
                CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
            }
        }


        /// <summary>
        /// Width of the object (in X). (TO BE RENAMED AT AN APPROPRIATE TIME - when we have time to fix what the renaming will break..)
        /// </summary>
        public float Length
        {
            get
            {
                return m_dimension.X;
            }
            set
            {
                m_dimension.X = value;
            }
        }

        /// <summary>
        /// Height of the object (in Y).
        /// </summary>
        public float Height
        {
            get
            {
                return m_dimension.Y;
            }
            set
            {
                m_dimension.Y = value;
            }
        }

        /// <summary>
        /// Depth of the object (in Z).
        /// </summary>
        public float Depth
        {
            get
            {
                return m_dimension.Z;
            }
            set
            {
                m_dimension.Z = value;
            }
        }


        /// <summary>
        /// Objects rotation around X.
        /// </summary>
        public float XRotation
        {
            get
            {
                return m_xRotation;
            }
            set
            {
                m_xRotation = value;
            }
        }

        /// <summary>
        /// Objects rotation around Y.
        /// </summary>
        public float YRotation
        {
            get
            {
                return m_yRotation;
            }
            set
            {
                m_yRotation = value;
            }
        }

        /// <summary>
        /// Objects rotation around Z.
        /// </summary>
        public float ZRotation
        {
            get
            {
                return m_zRotation;
            }
            set
            {
                m_zRotation = value;
            }
        }

        /// <summary>
        /// Object colour (RGBa).
        /// </summary>
        public Colour Colour
        {
            get
            {
                return m_colour;
            }
            set
            {
                m_colour = value;
            }
        }

        /// <summary>
        /// The object's velocity.
        /// </summary>
        public Vector3f Velocity
        {
            get
            {
                return m_velocity;
            }
            set
            {
                m_velocity = value;
            }
        }

        /// <summary>
        /// The collision model of the object.
        /// </summary>
        public BoundingBox CollisionModel
        {
            get
            {
                return m_boundingBox;
            }
            set
            {
                m_boundingBox = value;
            }
        }

        /// <summary>
        /// The number of the texture to assign to the shape. -1 selects no texture.
        /// </summary>
        /// <remarks>
        /// Select the number from the texture array in 
        /// <code>StromoLight_Objects.Graphics.Textures
        /// </code>
        /// . If an invalid texture number is set, default: -1 is set (no texture).
        /// </remarks>
        public int TextureNumber
        {
            get 
            { 
                return m_textureNumber; 
            }
            set
            {
                m_textureNumber = value;
            }
        }

        /// <summary>
        /// The height of the texture (for OpenGl texture mapping).
        /// </summary>
        public float TextureHeight
        {
            get { return m_textureHeight; }
            set { m_textureHeight = value; }
        }

        /// <summary>
        /// The width of the texture (for OpenGl texture mapping).
        /// </summary>
        public float TextureWidth
        {
            get { return m_textureWidth; }
            set { m_textureWidth = value; }
        }

        /// <summary>
        /// Indicates if the current object is selected.
        /// </summary>
        public bool Selected
        {
            get { return m_selected; }
            set { m_selected = value; }
        }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Basic constructor
        /// </summary>
        public OpenGlObject(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XMin = xMin;
            YMin = yMin;
            ZMin = zMin;
            XMax = xMax;
            YMax = yMax;
            ZMax = zMax;

            //X = (XMax + XMin) / 2;
            //Y = (YMax + YMin) / 2;
            //Z = (ZMax + ZMin) / 2;

            m_xRotation = 0;
            m_yRotation = 0;
            m_zRotation = 0;

            m_colour.SingleColour = new float[4] { 1.0f, 1.0f, 1.0f, 0.0f };

        }

        /// <summary>
        /// Constructor for textured OpenGlObject.
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
        public OpenGlObject(int textureNumber, float textureWidth, float textureHeight, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            this.TextureNumber = textureNumber;
            this.TextureWidth = textureWidth;
            this.TextureHeight = textureHeight;

            XMin = xMin;
            YMin = yMin;
            ZMin = zMin;
            XMax = xMax;
            YMax = yMax;
            ZMax = zMax;

            //X = (XMax + XMin) / 2;
            //Y = (YMax + YMin) / 2;
            //Z = (ZMax + ZMin) / 2;

            m_xRotation = 0;
            m_yRotation = 0;
            m_zRotation = 0;

            m_colour.SingleColour = new float[4] { 1.0f, 1.0f, 1.0f, 0.0f };

        }
        #endregion Constructors

        #region Virtual Methods
        /// <summary>
        /// Draws the OpenGL object
        /// </summary>
        public virtual void Draw()
        {
            throw new NotImplementedException("You must implement a draw method");
        }

        /// <summary>
        /// Rotate around X-axis
        /// </summary>
        /// <param name="increment"></param>
        public virtual void RotateX(float increment)
        {
            m_xRotation += increment;
        }

        /// <summary>
        /// Rotate around Y-axis
        /// </summary>
        /// <param name="increment"></param>
        public virtual void RotateY(float increment)
        {
            m_yRotation += increment;
        }

        /// <summary>
        /// Rotate around Z-axis
        /// </summary>
        /// <param name="increment"></param>
        public virtual void RotateZ(float increment)
        {
            m_zRotation += increment;
        }

        #endregion Virtual Methods

        #region Public Methods
        /// <summary>
        /// Moves the OpenGL object
        /// </summary>
        public virtual void Move()
        {
            m_velocity = m_velocity + m_acceleration;
            //m_position = m_position +  m_velocity;

            X = m_position.X + m_velocity.X;
            Y = m_position.Y + m_velocity.Y;
            Z = m_position.Z + m_velocity.Z;
        }

        #endregion Public Methods
    }

}
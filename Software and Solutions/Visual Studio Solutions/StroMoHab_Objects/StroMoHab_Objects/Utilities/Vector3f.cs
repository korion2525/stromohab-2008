using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects
{
    /// <summary>
    /// Structure for holding a 3 dimensional vector
    /// </summary>
    [Serializable]
    public class Vector3f
    {
        #region Member Variables
        float m_x, m_y, m_z;
        #endregion Member Variables

        #region Constructors

        /// <summary>
        /// Constructor - takes 3 coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Constructor - takes an already created vector
        /// </summary>
        /// <param name="v1"></param>
        public Vector3f(Vector3f v1)
        {
            X = v1.X;
            Y = v1.Y;
            Z = v1.Z;
        }

        #endregion Constructors

        #region Properties
        /// <summary>
        /// The X value of the vector.
        /// </summary>
        public float X
        {
            get
            {
                return m_x;
            }
            set
            {
                m_x = value;
            }
        }

        /// <summary>
        /// The Y value of the vector.
        /// </summary>
        public float Y
        {
            get
            {
                return m_y;
            }
            set
            {
                m_y = value;
            }
        }

        /// <summary>
        /// The Z value of the vector.
        /// </summary>
        public float Z
        {
            get
            {
                return m_z;
            }
            set
            {
                m_z = value;
            }
        }
        #endregion Properties

        #region Operator Overloads
        /// <summary>
        /// Vector Addition.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3f operator +(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Vector Subtraction.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static Vector3f operator -(Vector3f v1)
        {
            return new Vector3f((-v1.X), (-v1.Y), (-v1.Z));
        }

        /// <summary>
        /// Vector Multiplication.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="muliplicand"></param>
        /// <returns></returns>
        public static Vector3f operator *(Vector3f v1, float muliplicand)
        {
            return new Vector3f((v1.X * muliplicand), v1.Y * muliplicand, v1.Z * muliplicand);
        }



        #endregion Operator Overloads
    }
}

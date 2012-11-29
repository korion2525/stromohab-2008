using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Describes a vertex.
    /// </summary>
    public class Point3f
    {
        #region Member Variables

        float m_x = -9999.9f;
        float m_y = -9999.9f;
        float m_z = -9999.9f;

        #endregion Member Variables

        #region Properties

        /// <summary>
        /// Vertex X Coordinate.
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
        /// Vertex Y Coordinate.
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
        /// Vertex Z Coordinate
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

        #region Constructors

        /// <summary>
        /// A new Point.
        /// </summary>
        public Point3f()
        {
        }

        /// <summary>
        /// A new point created from data supplied in parameter.
        /// </summary>
        /// <param name="newData"></param>
        public Point3f(Point3f newData)
        {
            X = newData.X;
            Y = newData.Y;
            Z = newData.Z;
        }

        #endregion Constructors
    }
}

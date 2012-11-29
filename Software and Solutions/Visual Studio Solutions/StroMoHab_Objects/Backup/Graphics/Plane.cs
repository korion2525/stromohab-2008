using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Describes a plane, in the format: ax + by + cz + d = 0.
    /// </summary>
    public class Plane
    {
        float m_a = -9999.9f;
        float m_b = -9999.9f;
        float m_c = -9999.9f;
        float m_d = -9999.9f;


        /// <summary>
        /// Ax + By + Cz + D = 0.
        /// </summary>
        public float A
        {
            get { return m_a; }
            set { m_a = value; }
        }

        /// <summary>
        /// Ax + By + Cz + D = 0.
        /// </summary>
        public float B
        {
            get { return m_b; }
            set { m_b = value; }
        }

        /// <summary>
        /// Ax + By + Cz + D = 0.
        /// </summary>
        public float C
        {
            get { return m_c; }
            set { m_c = value; }
        }

        /// <summary>
        /// Ax + By + Cz + D = 0.
        /// </summary>
        public float D
        {
            get { return m_d; }
            set { m_d = value; }
        }

    }
}

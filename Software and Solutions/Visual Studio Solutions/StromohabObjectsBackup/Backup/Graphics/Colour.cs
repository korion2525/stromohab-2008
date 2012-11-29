using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// RGB Alpha as float (Gl4f).
    /// </summary>
    [Serializable]
    public class Colour
    {
        #region Member Variables
        float m_red;
        float m_green;
        float m_blue;
        float m_alpha;
        float [] m_singleColour = new float[4];
        #endregion Member Variables

        #region Properties
        /// <summary>
        /// Single colour vector. Requires array of length 4 (RGBA).
        /// </summary>
        public float[] SingleColour
        {
            get
            {
                return m_singleColour;
            }
            set
            {
                //TO DO: Implement array length checking to avoid index out of bounds exception (thrown when array < length 4 is passed).   
                m_singleColour = value;

                m_red = m_singleColour[0];
                m_green = m_singleColour[1];
                m_blue = m_singleColour[2];
                m_alpha = m_singleColour[3];
            }
        }

        /// <summary>
        /// Red component.
        /// </summary>
        public float Red
        {
            get
            {
                return m_red;
            }
            set
            {
                m_red = value;
            }
        }

        /// <summary>
        /// Green component.
        /// </summary>
        public float Green
        {
            get
            {
                return m_green;
            }
            set
            {
                m_green = value;
            }
        }

        /// <summary>
        /// Blue component.
        /// </summary>
        public float Blue
        {
            get
            {
                return m_blue;
            }
            set
            {
                m_blue = value;
            }
        }
        
        /// <summary>
        /// Alpha component.
        /// </summary>
        public float Alpha
        {
            get
            {
                return m_alpha;
            }
            set
            {
                m_alpha = value;
            }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Structure to hold RGBa colour
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        public Colour(float red, float green, float blue, float alpha)
        {
            m_red = red;
            m_green = green;
            m_blue = blue;
            m_alpha = alpha;
        }
        #endregion Constructors

    }
}

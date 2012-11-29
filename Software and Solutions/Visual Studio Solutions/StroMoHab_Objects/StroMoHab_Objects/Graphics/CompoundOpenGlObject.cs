using System;
using System.Collections.Generic;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// A compound OpenGlObject is an object that consists of multiple OpenGlObjects
    /// </summary>
    [Serializable]
    public class CompoundOpenGlObject : OpenGlObject
    {
        #region Member Variables

        List<BoundingBox> m_listOfCollisionModels;

        #endregion Member Variables

        #region Properties

        /// <summary>
        /// List of collison models for the CompoundOpenGlObject.
        /// </summary>
        public new List<BoundingBox> CollisionModel
        {
            get
            {
                return m_listOfCollisionModels;
            }
            set
            {
                m_listOfCollisionModels = value;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        public CompoundOpenGlObject(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
            : base(xMin, yMin, zMin, xMax, yMax, zMax)
        {
        }

        #endregion Constructors
    }
}
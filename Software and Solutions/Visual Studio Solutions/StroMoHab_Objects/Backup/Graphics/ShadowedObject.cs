using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// An object with the capability to throw a shadow.
    /// </summary>
    public class ShadowedObject
    {
        #region Member Variables

        int             m_numberOfVertices = -9999;
        List<Point3f>   m_verticesList = new List<Point3f>();
        int             m_numberOfFaces = -9999;
        List<Face>      m_facesList = new List<Face>();

        #endregion Member Variables

        #region Properties

        /// <summary>
        /// Number of vertices contained in the object.
        /// </summary>
        public int NumberOfVertices
        {
            get { return m_numberOfVertices; }
            set { m_numberOfVertices = value; }
        }

        /// <summary>
        /// List of all the vertices contained in the object.
        /// </summary>
        public List<Point3f> VerticesList
        {
            get { return m_verticesList; }
            set { m_verticesList = value; }
        }

        /// <summary>
        /// Number of faces on the object.
        /// </summary>
        public int NumberOfFaces
        {
            get { return m_numberOfFaces; }
            set { m_numberOfFaces = value; }
        }

        /// <summary>
        /// List of all the faces on the object.
        /// </summary>
        public List<Face> FacesList
        {
            get { return m_facesList; }
            set { m_facesList = value; }
        }

        #endregion Properties

        #region Constructors



        #endregion Constructors
    }
}

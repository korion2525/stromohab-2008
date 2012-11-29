using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Describes an objects face.
    /// </summary>
    public class Face
    {
        List<int> m_verticesList = new List<int>();    //List of each vertex that makes up the face.
        List<Point3f> m_normals = new List<Point3f>(3);         //Normals to each vertex that make up the face.
        Plane m_planeEquation;                                  //Equation of the plane that contains the face.
        List<int> m_neighbourIndices = new List<int>(3);        //Indices of each other fact next to this one.
        bool m_visible;                                         //Is the face of the object visible to the light?

        /// <summary>
        /// List of each vertex that makes up the face.
        /// </summary>
        public List<int> VerticesList
        {
            get { return m_verticesList; }
            set { m_verticesList = value; }
        }

        /// <summary>
        /// List of all the normals to each vertex that make up the face.
        /// </summary>
        public List<Point3f> Normals
        {
            get { return m_normals; }
            set { m_normals = value; }
        }

        /// <summary>
        /// Equation of the plane that contains the face, in the form Ax + By + Cz + D = 0.
        /// </summary>
        public Plane PlaneEquation
        {
            get { return m_planeEquation; }
            set { m_planeEquation = value; }
        }

        /// <summary>
        /// Indices of each other face next to this one.
        /// </summary>
        public List<int> NeighbourIndices
        {
            get { return m_neighbourIndices; }
            set { m_neighbourIndices = value; }
        }

        /// <summary>
        /// Is the face of the object visible to the light?
        /// </summary>
        public bool Visible
        {
            get { return m_visible; }
            set { m_visible = value; }
        }
    }
}

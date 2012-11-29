using System;

using Tao.OpenGl;
using System.Collections.Generic;
using StroMoHab_Objects.Objects;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// The subjects Avator object.
    /// </summary>
    [Serializable]
    public class Avatar : OpenGlObject
    {
        //Trackables
        private List<Trackable> m_trackableList = new List<Trackable>();

        float m_rotationAngle;
        float m_walkBounceAngle;
        //public Quad leftFoot = new Quad(6, 1.0f, 1.0f,
        //                                    new Vector3f(-0.2f, 0.001f, -0.5f),
        //                                    new Vector3f(0.2f, 0.001f, -0.5f),
        //                                    new Vector3f(0.2f, 0.5f, -0.5f),
        //                                    new Vector3f(-0.2f, 0.5f, -0.5f)
        //                                 );

        /// <summary>
        /// The left foot of the avatar.
        /// </summary>
        public OpenGLMarker leftFoot;

        /// <summary>
        /// The right foot of the avatar.
        /// </summary>
        public OpenGLMarker rightFoot;


        #region Properties

        /// <summary>
        /// Angle to "bounce" the avatar - to simulate head bobbing.
        /// </summary>
        public float WalkBounceAngle
        {
            get
            {
                return m_walkBounceAngle;
            }
            set
            {
                m_walkBounceAngle = value;
            }
        }

        /// <summary>
        /// The amount the avatar is rotated.
        /// </summary>
        public float RotationAngle
        {
            get
            {
                return m_rotationAngle;
            }
            set
            {
                m_rotationAngle = value;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor. Takes left, bottom, far, right, top, near values.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        public Avatar(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
            : base(xMin, yMin, zMin, xMax, yMax, zMax)
        {

        }

        #endregion Constructors


        #region Methods

        /// <summary>
        /// Updates the trackable list
        /// </summary>
        /// <param name="trackableList">The trackable list</param>
        public void Update(List<Trackable> trackableList)
        {
            m_trackableList = new List<Trackable>(trackableList);
        }

        /// <summary>
        /// Draws the avatar.
        /// </summary>
        public override void Draw()
        {
            Gl.glPushMatrix();

            //if (m_trackableList.Count != 0)
            //{
            //    if (m_trackableList[0] != null)
            //    {
            //        OpenGlTrackable trackableLeftFoot = new OpenGlTrackable(m_trackableList[0]);
            //        trackableLeftFoot.Draw();
            //    }
            //}

            //CURRENT MARKER BASED DRAWING

            if (m_trackableList.Count == 0)
            {
                if (FilteredMarkerList.listOfMarkers.Count != 0)
                {
                    if ((FilteredMarkerList.listOfMarkers[0] != null) && (FilteredMarkerList.listOfMarkers.Count != 0))
                    {//exception happened when FilteredMarkerList.listOfMarkers.Count was zero and one!!!!
                        leftFoot = new OpenGLMarker(FilteredMarkerList.listOfMarkers[0]);
                        rightFoot = new OpenGLMarker(FilteredMarkerList.listOfMarkers[0]);

                        this.CollisionModel = leftFoot.CollisionModel;

                        leftFoot.Draw();
                    }
                }
            }

            if (FilteredMarkerList.listOfMarkers.Count == 2)
            {
                rightFoot = new OpenGLMarker(FilteredMarkerList.listOfMarkers[1]);

                rightFoot.Draw();
            }


            ////trackables stuff

            //if (m_trackableList.Count > 0)
            //{
            //    OpenGlTrackable leftFootTrackable = new OpenGlTrackable(m_trackableList[0]);
            //    leftFootTrackable.Draw();

            //    if (m_trackableList.Count > 1)
            //    {
            //        OpenGlTrackable rightFootTrackable = new OpenGlTrackable(m_trackableList[1]);
            //        rightFootTrackable.Draw();
            //    }
            //}

            Gl.glPopMatrix();

        }

        #endregion Methods
    }
}

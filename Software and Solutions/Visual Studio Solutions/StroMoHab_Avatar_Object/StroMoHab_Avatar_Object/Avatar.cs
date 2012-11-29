using System.Collections.Generic;
using StroMoHab_Objects.Graphics;

namespace StroMoHab_Avatar_Object
{
    /// <summary>
    /// Represents a 3D opengl avatar
    /// </summary>
    public class Avatar
    {
        // OpenGL Body Parts
        private Foot leftFoot = new Foot();
        private Foot rightFoot = new Foot();
        private LowerLeg leftLowerLeg = new LowerLeg();
        private LowerLeg rightLowerLeg = new LowerLeg();
        private UpperLeg leftUpperLeg = new UpperLeg();
        private UpperLeg rightUpperLeg = new UpperLeg();
        // The maximum number of joints that are being found - increase if tracking more than the lower body
        private const int MAXNUMJOINTS = 6;
        // Determins if joints are drawn
        private bool DRAWJOINTS = false;
        // A list of openGL joints
        private List<Joint> openGLJointList = new List<Joint>(MAXNUMJOINTS);
        // A list of bounding boxes for collision detection
        private List<BoundingBox> boundingBoxList = new List<BoundingBox>();

        /// <summary>
        ///  Constructor for the 3D Avatar
        /// </summary>
        public Avatar()
        {
            // generate the openGL objects for drawing joints
            for (int i = 0; i < MAXNUMJOINTS; i++)
            {
                Joint openGLJoint = new Joint();
                openGLJointList.Add(openGLJoint);
                openGLJoint = null;
            }
        }

        /// <summary>
        /// Gets the collision models for the avatar
        /// </summary>
        public List<BoundingBox> CollisionModel
        {
            get { return boundingBoxList; }
        }
        /// <summary>
        /// Gets or sets the property defining if joints should be drawn
        /// </summary>
        public bool DrawJoints
        {
            get { return DRAWJOINTS; }
            set { DRAWJOINTS = value; }
        }

        /// <summary>
        /// Draws each Trackable in the list supplied
        /// If joint data is avaliable the limb will be scalled, otherwise it will be a generic size
        /// </summary>
        /// <param name="trackableList">The trackable list</param>
        /// <param name="jointList">The joint list</param>
        public void Draw(List<StroMoHab_Objects.Objects.Trackable> trackableList, List<StroMoHab_Objects.Objects.Joint> jointList)
        {
            //Clear the boundingbox list before it is updated
            boundingBoxList.Clear();

            // If the joint list doesn't have enough joints in it, generate som blank ones
            for (int j = jointList.Count; j < MAXNUMJOINTS + 1; j++)
            {
                StroMoHab_Objects.Objects.Joint joint;
                joint = new StroMoHab_Objects.Objects.Joint();
                jointList.Add(joint);

            }

            // Go through the trackable list and update (Draw) the opengl body parts for the trackables present using joint data if possible
            // add their data to the bounding box list
            foreach (StroMoHab_Objects.Objects.Trackable trackable in trackableList)
            {                 
                switch (trackable.ID)
                { 
                    case 1: // Left Foot
                        leftFoot.Draw(trackable, jointList[0], null);
                        boundingBoxList.Add(leftFoot.CollisionModel);
                        break;
                    case 2: // Right Foot
                        rightFoot.Draw(trackable, jointList[1], null);
                        boundingBoxList.Add(rightFoot.CollisionModel);
                        break;
                        
                    case 3: // Left Lower Leg
                        leftLowerLeg.Draw(trackable, jointList[2], jointList[0]);
                        boundingBoxList.Add(leftLowerLeg.CollisionModel);
                        break;
                    case 4: // Right Lower Leg
                        rightLowerLeg.Draw(trackable, jointList[3], jointList[1]);
                        boundingBoxList.Add(rightLowerLeg.CollisionModel);
                        break;
                    case 5: // Left Upper Leg
                        leftUpperLeg.Draw(trackable, null, jointList[2]);
                        boundingBoxList.Add(leftUpperLeg.CollisionModel);
                        break;
                    case 6: // Right Upper Leg
                        rightUpperLeg.Draw(trackable, null, jointList[3]);
                        boundingBoxList.Add(rightUpperLeg.CollisionModel);
                        break;
                         
                       
                }//Close switch          
            }//Close foreach

            // Draw the joints on the screen
            if(DRAWJOINTS)
            {
                int i=0;
                foreach (StroMoHab_Objects.Objects.Joint joint in jointList)
                {

                    if (joint.Exists)
                        openGLJointList[i].Draw(joint);
                    i++;
                }
            }

        }//Close Method Draw
    }//Close Class
}//Close Namespace

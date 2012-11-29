using System.Collections.Generic;

namespace Skeleton
{
    /// <summary>
    /// Turns a list of Trackables into an OpenGL Skeleton
    /// </summary>
    class Skeleton
    {
        private Foot leftFoot = new Foot();
        private Foot rightFoot = new Foot();
        private LowerLeg leftLowerLeg = new LowerLeg();
        private LowerLeg rightLowerLeg = new LowerLeg();
        private UpperLeg leftUpperLeg = new UpperLeg();
        private UpperLeg rightUpperLeg = new UpperLeg();

        /// <summary>
        /// Draws each Trackable in the list supplied
        /// </summary>
        /// <param name="trackableList">The list of Trackables</param>
        public void Draw(List<Stromohab_MCE.Trackable> trackableList)
        {
            // Go through the trackable list and update (Draw) the opengl body parts for the trackables present
            foreach (Stromohab_MCE.Trackable trackable in trackableList)
            {
                switch (trackable.ID)
                {
                    case 1: // Left Foot
                        leftFoot.Draw(trackable);
                        break;
                    case 2: // Right Foot
                        rightFoot.Draw(trackable);
                        break;
                    case 3: // Left Lower Leg
                        leftLowerLeg.Draw(trackable);
                        break;
                    case 4: // Right Lower Leg
                        rightLowerLeg.Draw(trackable);
                        break;
                    case 5: // Left Upper Leg
                        leftUpperLeg.Draw(trackable);
                        break;
                    case 6: // Right Upper Leg
                        rightUpperLeg.Draw(trackable);
                        break;
                }//Close switch
            }//Close foreach
        }//Close Method Draw

        public void Draw(List<Stromohab_MCE.Trackable> trackableList, bool[] draw, double[,] trackableOffset)
        {
            // Go through the trackable list and update (Draw) the opengl body parts for the trackables present
            foreach (Stromohab_MCE.Trackable trackable in trackableList)
            {
                switch (trackable.ID)
                {
                    case 1: // Left Foot
                        trackable.Pitch = trackable.Pitch - trackableOffset[trackable.ID - 1, 0];
                        trackable.Yaw = trackable.Yaw - trackableOffset[trackable.ID - 1, 1];
                        trackable.Roll = trackable.Roll - trackableOffset[trackable.ID - 1, 2];
                        if(draw[trackable.ID])
                            leftFoot.Draw(trackable);
                        break;
                    case 2: // Right Foot
                        trackable.Pitch = trackable.Pitch - trackableOffset[trackable.ID - 1, 0];
                        trackable.Yaw = trackable.Yaw - trackableOffset[trackable.ID - 1, 1];
                        trackable.Roll = trackable.Roll - trackableOffset[trackable.ID - 1, 2];
                        if (draw[trackable.ID])
                            rightFoot.Draw(trackable);
                        break;
                    case 3: // Left Lower Leg
                        trackable.Pitch = trackable.Pitch - trackableOffset[trackable.ID - 1, 0];
                        trackable.Yaw = trackable.Yaw - trackableOffset[trackable.ID - 1, 1];
                        trackable.Roll = trackable.Roll - trackableOffset[trackable.ID - 1, 2];
                        if (draw[trackable.ID])
                            leftLowerLeg.Draw(trackable);
                        break;
                    case 4: // Right Lower Leg
                        trackable.Pitch = trackable.Pitch - trackableOffset[trackable.ID - 1, 0];
                        trackable.Yaw = trackable.Yaw - trackableOffset[trackable.ID - 1, 1];
                        trackable.Roll = trackable.Roll - trackableOffset[trackable.ID - 1, 2];
                        if (draw[trackable.ID])
                            rightLowerLeg.Draw(trackable);
                        break;
                    case 5: // Left Upper Leg
                        trackable.Pitch = trackable.Pitch - trackableOffset[trackable.ID - 1, 0];
                        trackable.Yaw = trackable.Yaw - trackableOffset[trackable.ID - 1, 1];
                        trackable.Roll = trackable.Roll - trackableOffset[trackable.ID - 1, 2];
                        if (draw[trackable.ID])
                            leftUpperLeg.Draw(trackable);
                        break;
                    case 6: // Right Upper Leg
                        trackable.Pitch = trackable.Pitch - trackableOffset[trackable.ID - 1, 0];
                        trackable.Yaw = trackable.Yaw - trackableOffset[trackable.ID - 1, 1];
                        trackable.Roll = trackable.Roll - trackableOffset[trackable.ID - 1, 2];
                        if (draw[trackable.ID])
                            rightUpperLeg.Draw(trackable);
                        break;
                }//Close switch
            }//Close foreach
        }//Close Method Draw

    }//Close Class
}//Close Namespace

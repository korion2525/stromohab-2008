using Tao.OpenGl;
using System;
using System.Collections.Generic;

namespace StroMoHab_Avatar_Object 
{
    /// <summary>
    /// Represents an OpenGl Foot
    /// </summary>
    class Foot : BodyPart
    {
        /// <summary>
        /// Constructor for the OpenGL Body Part
        /// </summary>
        public Foot()
        {
            // Sets up the default size for the body part
            scaleX = 60 / SCALLINGFACTOR;
            scaleY = 70 / SCALLINGFACTOR;
            scaleZ = 150 / SCALLINGFACTOR;

            offsetY = -scaleY;
        }

        /// <summary>
        /// Calculates the scalling and offset values for the trackable which determin its size and position
        /// </summary>
        /// <param name="trackable"></param>
        /// <param name="joint1"></param>
        /// <param name="joint2"></param>
        protected override void CalculateScallingAndOffsets(StroMoHab_Objects.Objects.Trackable trackable, StroMoHab_Objects.Objects.Joint joint1, StroMoHab_Objects.Objects.Joint joint2)
        {
            if (joint1.Exists)
            {
                // Set up matricies
                StroMoHab_Matrix.PointMatrix joint1PointVector = new StroMoHab_Matrix.PointMatrix(joint1.xCoordinate - trackable.xCoordinate, joint1.yCoordinate - trackable.yCoordinate, joint1.zCoordinate - trackable.zCoordinate);
                StroMoHab_Matrix.RotationMatrix rotationMatrixReverse = new StroMoHab_Matrix.RotationMatrix(-Math.PI * trackable.Pitch / 180, -Math.PI * trackable.Yaw / 180, -Math.PI * trackable.Roll / 180);

                // Rotate the joint 
                StroMoHab_Matrix.PointMatrix joint1NewPointVector = StroMoHab_Matrix.Operations.Rotate(joint1PointVector, rotationMatrixReverse);

                // Calculate the scaling and offsets based on the vector from the trackable to the joint
                scaleZ = (float)joint1NewPointVector.ZCoordinate / SCALLINGFACTOR;
                scaleY = (float)joint1NewPointVector.YCoordinate / SCALLINGFACTOR;

                // Protect against really small limbs
                if (joint1NewPointVector.ZCoordinate < 100)
                    scaleZ = Math.Abs((float)100 / SCALLINGFACTOR);

                offsetY = -scaleY;
            }
        }
        
        /// <summary>
        /// Draws the unit shape the represents the body part
        /// </summary>
        protected override void DrawBodyPart()
        {
            DrawUnitHemiSphere();
        }

        /// <summary>
        /// Calculates the bounding box for the given trackable and updates it
        /// </summary>
        /// <param name="trackable"></param>
        protected override void CalculateBoundingBox(StroMoHab_Objects.Objects.Trackable trackable)
        {
            float xMin, yMin, zMin, xMax, yMax, zMax;
            CalculateMinMaxXYZ(trackable, out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            // Update the bounding box based on the calculated values and any offsets
            UpdateBoundingBox(trackable.xCoordinate / SCALLINGFACTOR + xMin, trackable.yCoordinate / SCALLINGFACTOR + yMin + offsetY/2, -trackable.zCoordinate / SCALLINGFACTOR + zMin, trackable.xCoordinate / SCALLINGFACTOR + xMax, trackable.yCoordinate / SCALLINGFACTOR + yMax + offsetY/2, -trackable.zCoordinate / SCALLINGFACTOR + zMax);
        }


    }
}

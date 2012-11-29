using Tao.OpenGl;
using System;
using System.Collections.Generic;

namespace StroMoHab_Avatar_Object
{
    /// <summary>
    /// Represents and OpenGl Lower Leg
    /// </summary>
    class LowerLeg : BodyPart
    {
        /// <summary>
        /// Constructor for the OpenGL Body Part
        /// </summary>
        public LowerLeg()
        {
            // Sets up the default size for the body part
            scaleX = 50 / SCALLINGFACTOR;
            scaleY = 50 / SCALLINGFACTOR;
            scaleZ = 220 / SCALLINGFACTOR;

            offsetY = - scaleY;
        }
        
        /// <summary>
        /// Calculates the scalling and offset values for the trackable which determin its size and position
        /// </summary>
        /// <param name="trackable"></param>
        /// <param name="joint1"></param>
        /// <param name="joint2"></param>
        protected override void CalculateScallingAndOffsets(StroMoHab_Objects.Objects.Trackable trackable, StroMoHab_Objects.Objects.Joint joint1, StroMoHab_Objects.Objects.Joint joint2)
        {
            if (joint1.Exists && joint2.Exists == false)
            {
                // Set up matricies
                StroMoHab_Matrix.PointMatrix joint1PointVector = new StroMoHab_Matrix.PointMatrix(joint1.xCoordinate - trackable.xCoordinate, joint1.yCoordinate - trackable.yCoordinate, joint1.zCoordinate - trackable.zCoordinate);
                StroMoHab_Matrix.RotationMatrix rotationMatrixReverse = new StroMoHab_Matrix.RotationMatrix(-Math.PI * trackable.Pitch / 180, -Math.PI * trackable.Yaw / 180, -Math.PI * trackable.Roll / 180);

                // Rotate the joint 
                StroMoHab_Matrix.PointMatrix joint1NewPointVector = StroMoHab_Matrix.Operations.Rotate(joint1PointVector, rotationMatrixReverse);

                // Calculate the scaling and offsets based on the vector from the trackable to the joint
                scaleZ = (float)joint1NewPointVector.ZCoordinate / SCALLINGFACTOR;
                scaleY = (float)joint1NewPointVector.YCoordinate / SCALLINGFACTOR;

                if (joint1NewPointVector.ZCoordinate < 150)
                    scaleZ = Math.Abs((float)150 / SCALLINGFACTOR);
                if (joint1NewPointVector.YCoordinate < 30)
                    scaleY = Math.Abs((float)30 / SCALLINGFACTOR);
               

                offsetY = -scaleY;
                
            }
            else if (joint2.Exists && joint1.Exists == false)
            {
                // Set up matricies
                StroMoHab_Matrix.PointMatrix joint2PointVector = new StroMoHab_Matrix.PointMatrix(joint2.xCoordinate - trackable.xCoordinate, joint2.yCoordinate - trackable.yCoordinate, joint2.zCoordinate - trackable.zCoordinate);
                StroMoHab_Matrix.RotationMatrix rotationMatrixReverse = new StroMoHab_Matrix.RotationMatrix(-Math.PI * trackable.Pitch / 180, -Math.PI * trackable.Yaw / 180, -Math.PI * trackable.Roll / 180);

                // Rotate the joint 
                StroMoHab_Matrix.PointMatrix joint2NewPointVector = StroMoHab_Matrix.Operations.Rotate(joint2PointVector, rotationMatrixReverse);

               
                // Calculate the scaling and offsets based on the vector from the trackable to the joint
                scaleZ = Math.Abs((float)joint2NewPointVector.ZCoordinate / SCALLINGFACTOR);
                scaleY = Math.Abs((float)joint2NewPointVector.YCoordinate / SCALLINGFACTOR);
                if (joint2NewPointVector.ZCoordinate < 150)
                    scaleZ = Math.Abs((float)150 / SCALLINGFACTOR);
                if (joint2NewPointVector.YCoordinate < 30)
                    scaleY = Math.Abs((float)30 / SCALLINGFACTOR);
               

                offsetY = -scaleY;

            }
            else if (joint1.Exists && joint2.Exists)
            {
                // Set up matricies
                StroMoHab_Matrix.PointMatrix joint2PointVector = new StroMoHab_Matrix.PointMatrix(joint2.xCoordinate - trackable.xCoordinate, joint2.yCoordinate - trackable.yCoordinate, joint2.zCoordinate - trackable.zCoordinate);
                StroMoHab_Matrix.RotationMatrix rotationMatrixReverse = new StroMoHab_Matrix.RotationMatrix(-Math.PI * trackable.Pitch / 180, -Math.PI * trackable.Yaw / 180, -Math.PI * trackable.Roll / 180);
                StroMoHab_Matrix.PointMatrix joint1PointVector = new StroMoHab_Matrix.PointMatrix(joint1.xCoordinate - trackable.xCoordinate, joint1.yCoordinate - trackable.yCoordinate, joint1.zCoordinate - trackable.zCoordinate);
                
                // Rotate the joint 
                StroMoHab_Matrix.PointMatrix joint1NewPointVector = StroMoHab_Matrix.Operations.Rotate(joint1PointVector, rotationMatrixReverse);

                // Rotate the joint 
                StroMoHab_Matrix.PointMatrix joint2NewPointVector = StroMoHab_Matrix.Operations.Rotate(joint2PointVector, rotationMatrixReverse);

                // Calculate the scaling and offsets based on the vector from the trackable to the joint
                //Half of the distance between the two joints
                scaleZ = (float)(Math.Abs(joint1NewPointVector.ZCoordinate) + Math.Abs(joint2NewPointVector.ZCoordinate) / 2) / SCALLINGFACTOR;
                if(scaleZ < Math.Abs((float)150 / SCALLINGFACTOR))
                    scaleZ = Math.Abs((float)150 / SCALLINGFACTOR);
                scaleY = Math.Abs((float)joint1NewPointVector.YCoordinate / SCALLINGFACTOR);
          

                offsetY = -scaleY;
            }
        }

        /// <summary>
        /// Draws the unit shape the represents the body part
        /// </summary>
        protected override void DrawBodyPart()
        {
            DrawUnitSphere();
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
            UpdateBoundingBox(trackable.xCoordinate / SCALLINGFACTOR + xMin, trackable.yCoordinate / SCALLINGFACTOR + yMin + offsetY, -trackable.zCoordinate / SCALLINGFACTOR + zMin, trackable.xCoordinate / SCALLINGFACTOR + xMax, trackable.yCoordinate / SCALLINGFACTOR + yMax + offsetY, -trackable.zCoordinate / SCALLINGFACTOR + zMax);
        }
    }
}

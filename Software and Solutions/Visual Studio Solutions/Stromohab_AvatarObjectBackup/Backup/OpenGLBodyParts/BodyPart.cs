using Tao.OpenGl;
using System;
using System.Collections.Generic;
using StroMoHab_Objects.Graphics;

namespace StroMoHab_Avatar_Object
{
    /// <summary>
    /// Represents a single body part
    /// </summary>
    public abstract class BodyPart
    {
        /// <summary>
        /// Global scalling factor 
        /// </summary>
        protected float SCALLINGFACTOR = 800f;
        /// <summary>
        /// Number of lines to build up spheres from
        /// </summary>
        protected int SPHERERESOLUTION = 20; 
        /// <summary>
        /// The amount the unit shape should be scalled in the x y and z direction to reach the correct size
        /// </summary>
        protected float scaleX = 0.1f, scaleY = 0.1f, scaleZ = 0.1f;
        /// <summary>
        /// The amount the unit shape should be shiffted in the x y and z direction be in the correct place
        /// </summary>
        protected float offsetX = 0f, offsetY = 0f, offsetZ = 0f;

        /// <summary>
        /// The bounding box used for collision detection
        /// </summary>
        protected BoundingBox bodyPartBoundingBox;

        /// <summary>
        /// Implement code to draw the desired UNIT shape to represnt the body part
        /// </summary>
        protected abstract void DrawBodyPart();

        /// <summary>
        /// Implement code to calculate to size of the body part so that the unit shape can be scalled correctly
        /// and so it can be offset correctly based on connecting joint data
        /// </summary>
        /// <param name="trackable"></param>
        /// <param name="joint1"></param>
        /// <param name="joint2"></param>
        protected abstract void CalculateScallingAndOffsets(StroMoHab_Objects.Objects.Trackable trackable, StroMoHab_Objects.Objects.Joint joint1, StroMoHab_Objects.Objects.Joint joint2);

        /// <summary>
        /// Draws the body part based on the trackable
        /// </summary>
        /// <param name="trackable">The trackable</param>
        /// <param name="joint1">The first joint</param>
        /// <param name="joint2">The second joint</param>
        public virtual void Draw(StroMoHab_Objects.Objects.Trackable trackable, StroMoHab_Objects.Objects.Joint joint1, StroMoHab_Objects.Objects.Joint joint2)
        {
            //CalculateScallingAndOffsets(trackable, joint1, joint2);
            CalculateBoundingBox(trackable);
            MoveIn(trackable);
            ScaleIn();
            SetDrawingMode(true);
            DrawBodyPart();
            SetDrawingMode(false);
            DrawBodyPart();
            ScaleOut();
            MoveOut(trackable);
            SetDrawingMode(true);
        }

        /// <summary>
        /// Scales the unit sphers/hemispheres to the correct ellipsoid shape to represent limbs
        /// </summary>
        protected virtual void ScaleIn()
        {
            Gl.glScalef(scaleX, scaleY, scaleZ);
        }

        /// <summary>
        /// Undoes previous scalling
        /// </summary>
        protected virtual void ScaleOut()
        {
            Gl.glScalef(1/scaleX, 1/scaleY, 1/scaleZ);
        }

        /// <summary>
        /// Moves to the center of the trackalbe and sets the orientation so that the body part can be drawn
        /// </summary>
        protected virtual void MoveIn(StroMoHab_Objects.Objects.Trackable trackable)
        {
            // Move to center point of the trackalbe
            Gl.glTranslatef(trackable.xCoordinate / SCALLINGFACTOR + offsetX, trackable.yCoordinate / SCALLINGFACTOR + offsetY, -trackable.zCoordinate / SCALLINGFACTOR - offsetZ);
            // Rotate based on Pitch Yaw and Roll
            Gl.glRotatef((float)trackable.Pitch, 1f, 0f, 0f);
            Gl.glRotatef((float)trackable.Yaw, 0f, -1f, 0f);
            Gl.glRotatef((float)trackable.Roll, 0f, 0f, -1f);
        }

        /// <summary>
        /// Moves back to x/y/z = 0 and resets the orientation
        /// </summary>
        protected virtual void MoveOut(StroMoHab_Objects.Objects.Trackable trackable)
        {
            // Rotate back
            Gl.glRotatef((float)trackable.Roll, 0f, 0f, 1f);
            Gl.glRotatef((float)trackable.Yaw, 0f, 1f, 0f);
            Gl.glRotatef((float)trackable.Pitch, -1f, 0f, 0f);
            // Move back
            Gl.glTranslatef(-trackable.xCoordinate / SCALLINGFACTOR - offsetX, -trackable.yCoordinate / SCALLINGFACTOR - offsetY, trackable.zCoordinate / SCALLINGFACTOR + offsetZ);
        }

        /// <summary>
        /// Sets the Polygon drawing mode to use
        /// </summary>
        /// <param name="fillNotMesh">True = Fill False = Mesh</param>
        protected virtual void SetDrawingMode(bool fillNotMesh)
        {
            if(fillNotMesh) // Draw a filled polygon
            {
                Gl.glColor3f(1f, 1f, 0f);
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            }
            else // Draw a wire mesh (line) polygon
            {
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                Gl.glColor3f(0f, 0f, 0f);
            }
        }

        /// <summary>
        /// Draws a unit sphere which can be scalled to form and ellipsoid
        /// </summary>
        protected virtual void DrawUnitSphere()
        {
            Gl.glPushMatrix();

            float a = 1f, b = 1f, c = 1f;


            int i, j;
            for (i = 0; i <= SPHERERESOLUTION; i++)
            {
                float lat0 = (float)Math.PI * (-0.5f + (float)(i - 1) / SPHERERESOLUTION);
                float z0 = (float)Math.Sin(lat0);
                float zr0 = (float)Math.Cos(lat0);

                float lat1 = (float)Math.PI * (-0.5f + (float)i / SPHERERESOLUTION);
                float z1 = (float)Math.Sin(lat1);
                float zr1 = (float)Math.Cos(lat1);

                Gl.glBegin(Gl.GL_QUAD_STRIP);
                for (j = SPHERERESOLUTION; j >= 0; j--)
                {
                    float lng = 2 * (float)Math.PI * (float)(j - 1) / SPHERERESOLUTION;
                    float x = (float)Math.Cos(lng);
                    float y = (float)Math.Sin(lng);

                    Gl.glNormal3f(x * zr0, y * zr0, z0);
                    Gl.glVertex3f(x * zr0 * a, y * zr0 * b, z0 * c);
                    Gl.glNormal3f(x * zr1, y * zr1, z1);
                    Gl.glVertex3f(x * zr1 * a, y * zr1 * b, z1 * c);
                }
                Gl.glEnd();
            }
            Gl.glPopMatrix();
        }

        /// <summary>
        /// Draws half a unit sphere that can be scalled to be half an ellipsoid
        /// </summary>
        protected virtual void DrawUnitHemiSphere()
        {
            Gl.glPushMatrix();
            float a = 1f, b = 1f, c = 1f;


            int i, j;
            for (i = 1; i <= SPHERERESOLUTION; i++)
            {
                float lat0 = (float)Math.PI * (-0.5f + (float)(i - 1) / SPHERERESOLUTION);
                float z0 = (float)Math.Sin(lat0);
                float zr0 = (float)Math.Cos(lat0);

                float lat1 = (float)Math.PI * (-0.5f + (float)i / SPHERERESOLUTION);
                float z1 = (float)Math.Sin(lat1);
                float zr1 = (float)Math.Cos(lat1);

                Gl.glBegin(Gl.GL_QUAD_STRIP);
                for (j = 1 + SPHERERESOLUTION / 2; j >= 1; j--)
                {
                    float lng = 2 * (float)Math.PI * (float)(j - 1) / SPHERERESOLUTION;
                    float x = (float)Math.Cos(lng);
                    float y = (float)Math.Sin(lng);

                    Gl.glNormal3f(x * zr0, y * zr0, z0);
                    Gl.glVertex3f(x * zr0 * a, y * zr0 * b, z0 * c);
                    Gl.glNormal3f(x * zr1, y * zr1, z1);
                    Gl.glVertex3f(x * zr1 * a, y * zr1 * b, z1 * c);
                }
                
                Gl.glEnd();
            }
            Gl.glPopMatrix();
        }

        /// <summary>
        /// The collision model of the object.
        /// </summary>
        public BoundingBox CollisionModel
        {
            get
            {
                return bodyPartBoundingBox;
            }
            set
            {
                bodyPartBoundingBox = value;
            }
        }
        /// <summary>
        /// Updates the boundbox for the current object
        /// </summary>
        protected virtual void UpdateBoundingBox(float XMin, float YMin, float ZMin, float XMax, float YMax, float ZMax)
        {
            CollisionModel = new BoundingBox(XMin, YMin, ZMin, XMax, YMax, ZMax);
        }

        /// <summary>
        /// Implement code to calculate the size of the bounding box
        /// </summary>
        protected abstract void CalculateBoundingBox(StroMoHab_Objects.Objects.Trackable trackable);

        /// <summary>
        /// Calculates the min and max x y and z values for the bounding box that encapsulates the trackable
        /// </summary>
        /// <param name="trackable"></param>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="zMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="zMax"></param>
        protected void CalculateMinMaxXYZ(StroMoHab_Objects.Objects.Trackable trackable, out float xMin, out float yMin, out float zMin, out float xMax, out float yMax, out float zMax)
        {
            // Calculate the min and max X Y and Z values
            StroMoHab_Matrix.RotationMatrix rotationMatrix = new StroMoHab_Matrix.RotationMatrix(+Math.PI * trackable.Pitch / 180, +Math.PI * trackable.Yaw / 180, +Math.PI * trackable.Roll / 180);
            List<StroMoHab_Matrix.PointMatrix> pointMatrixList = BuildCuboidCorners(scaleX, scaleY, scaleZ);
            List<StroMoHab_Matrix.PointMatrix> rotatedPointMatrixList = RotateCuboidCorners(pointMatrixList, rotationMatrix);
            FindMaxMinCorners(rotatedPointMatrixList, out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
        }

        ///<summary>
        /// Takes the X, Y, Z vectors from the centre to the exteme corner point on a cuboid and calculates the vectors
        /// to all of the corner points
        /// </summary>
        protected virtual List<StroMoHab_Matrix.PointMatrix> BuildCuboidCorners(float X, float Y, float Z)
        {
            List<StroMoHab_Matrix.PointMatrix> pointMatrixList = new List<StroMoHab_Matrix.PointMatrix>();
            StroMoHab_Matrix.PointMatrix point;

            point = new StroMoHab_Matrix.PointMatrix(X, Y, Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(-X, Y, Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(X, -Y, Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(X, Y, -Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(-X, -Y, Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(X, -Y, -Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(-X, Y, -Z);
            pointMatrixList.Add(point);
            point = new StroMoHab_Matrix.PointMatrix(-X, Y, -Z);
            pointMatrixList.Add(point);

            return pointMatrixList;
        }

        /// <summary>
        /// Takes a list of corner points and rotates them
        /// </summary>
        protected virtual List<StroMoHab_Matrix.PointMatrix> RotateCuboidCorners(List<StroMoHab_Matrix.PointMatrix> pointMatrixList, StroMoHab_Matrix.RotationMatrix rotationMatrix)
        {
            List<StroMoHab_Matrix.PointMatrix> rotatedPointMatrixList = new List<StroMoHab_Matrix.PointMatrix>();
            foreach (StroMoHab_Matrix.PointMatrix pointMatrix in pointMatrixList)
            {
                StroMoHab_Matrix.PointMatrix rotatedPointMatrix = StroMoHab_Matrix.Operations.Rotate(pointMatrix, rotationMatrix);
                rotatedPointMatrixList.Add(rotatedPointMatrix);
            }
            return rotatedPointMatrixList;
        }

        /// <summary>
        /// From the list of corner points finds the min and max x, y, and z values
        /// </summary>
        protected virtual void FindMaxMinCorners(List<StroMoHab_Matrix.PointMatrix> pointMatrixList, out float xMin, out float yMin, out float zMin, out float xMax, out float yMax, out float zMax)
        {
            xMin = 0;
            yMin = 0;
            zMin = 0;
            xMax = 0;
            yMax = 0;
            zMax = 0;
            // Go through al the points and find the min and max X Y and Z values
            foreach (StroMoHab_Matrix.PointMatrix pointMatrix in pointMatrixList)
            {
                if (pointMatrix.XCoordinate < xMin) xMin = (float)pointMatrix.XCoordinate;
                if (pointMatrix.YCoordinate < yMin) yMin = (float)pointMatrix.YCoordinate;
                if (pointMatrix.ZCoordinate < zMin) zMin = (float)pointMatrix.ZCoordinate;
                if (pointMatrix.XCoordinate > xMax) xMax = (float)pointMatrix.XCoordinate;
                if (pointMatrix.YCoordinate > yMax) yMax = (float)pointMatrix.YCoordinate;
                if (pointMatrix.ZCoordinate > zMax) zMax = (float)pointMatrix.ZCoordinate;
            }
        }
    }
}
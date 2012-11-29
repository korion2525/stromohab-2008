using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numeric;
using MathNet.Numerics.LinearAlgebra;

namespace StroMoHab_Matrix
{
    /// <summary>
    /// A collection of classes and methods for interfacing with Math.NET Iridium (Numerics) 2008 August Release, Iteration 16 (v2008.8.16.470)
    /// http://www.mathdotnet.com/downloads/Default.aspx
    /// </summary>
    /// <auther>Will Lunniss</auther>
    public class RotationMatrix
    {
        /// <summary>
        /// The X axis rotation matrix
        /// </summary>
        public Matrix rotationMatrixX;
        /// <summary>
        /// The Y axis rotation matrix
        /// </summary>
        public Matrix rotationMatrixY;
        /// <summary>
        /// The Z axis rotation matrix
        /// </summary>
        public Matrix rotationMatrixZ;

        /// <summary>
        /// Generates the 3 rotation matricies for use with Operations.Rotate()
        /// </summary>
        /// <param name="rotationValueX">Rotation around the X Axis</param>
        /// <param name="rotationValueY">Rotation around the Y Axis</param>
        /// <param name="rotationValueZ">Rotation around the Z Axis</param>
        public RotationMatrix(double rotationValueX, double rotationValueY, double rotationValueZ)
        {
            rotationMatrixX = Matrix.Create(new double[3, 3] { { 1, 0, 0 }, { 0, Math.Cos(rotationValueX), Math.Sin(rotationValueX) }, { 0, -Math.Sin(rotationValueX), Math.Cos(rotationValueX) } });
            rotationMatrixY = Matrix.Create(new double[3, 3] { { Math.Cos(rotationValueY), 0, Math.Sin(rotationValueY) }, { 0, 1, 0 }, { -Math.Sin(rotationValueY), 0, Math.Cos(rotationValueY) } });
            rotationMatrixZ = Matrix.Create(new double[3, 3] { { Math.Cos(rotationValueZ), Math.Sin(rotationValueZ), 0 }, { -Math.Sin(rotationValueZ), Math.Cos(rotationValueZ), 0 }, { 0, 0, 1 } });

        }
    }

    /// <summary>
    /// Represents a single point in 3D space
    /// </summary>
    public class PointMatrix
    {
        /// <summary>
        /// The point matrix
        /// </summary>
        public Matrix pointMatrix;

        /// <summary>
        /// Generates a point matrix (1x3 Vector) from the given inputs
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public PointMatrix(double x, double y, double z)
        {
            pointMatrix = Matrix.Create(new double[3, 1] { { x }, { y }, { z } });

        }

        /// <summary>
        /// Generates an empty point matrix (1x3 Vector)
        /// </summary>
        public PointMatrix()
        {
            pointMatrix = Matrix.Create(new double[3,1] { { 0 }, { 0 }, { 0 } });
        }

        /// <summary>
        /// The X coordinate
        /// </summary>
        public double XCoordinate
        {
            get { return pointMatrix[0, 0]; }
        }
        /// <summary>
        /// The Y coordinate
        /// </summary>
        public double YCoordinate
        {
            get { return pointMatrix[1, 0]; }
        }
        /// <summary>
        /// The Z coordinate
        /// </summary>
        public double ZCoordinate
        {
            get { return pointMatrix[2, 0]; }
        }
    }
    
    /// <summary>
    /// Provides a number of matrix based operations
    /// </summary>
    public static class Operations
    {
        /// <summary>
        /// Rotates the given point by the given rotation matrix and returns a point matrix
        /// </summary>
        /// <param name="pointMatrix">The 1x3 point matrix</param>
        /// <param name="rotationMatrix">The 3 3x3 rotation matricies</param>
        /// <returns>The new 1x3 point matrix</returns>
        public static StroMoHab_Matrix.PointMatrix Rotate(StroMoHab_Matrix.PointMatrix pointMatrix, StroMoHab_Matrix.RotationMatrix rotationMatrix)
        {
            Matrix newPointMatrix = newPointMatrix = rotationMatrix.rotationMatrixZ * pointMatrix.pointMatrix;
            newPointMatrix = rotationMatrix.rotationMatrixY *  newPointMatrix;
            newPointMatrix = rotationMatrix.rotationMatrixX *  newPointMatrix;

            StroMoHab_Matrix.PointMatrix returnedPointMatrix = new PointMatrix();
            returnedPointMatrix.pointMatrix = newPointMatrix;
            return returnedPointMatrix;
        }

        /// <summary>
        /// Transposes a square Matrix without overiding it
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Matrix Transpose(Matrix A)
        {
            Matrix A_Trans = A;
            A_Trans.Transpose();
            return A_Trans;
        }


        /// <summary>
        /// Finds the Rotation matrix of a Trackable by using the Quaternion data
        /// </summary>
        /// <param name="Trackable"></param>
        /// <returns></returns>
        public static Matrix BuildRotorFromQuaternionData(StroMoHab_Objects.Objects.Trackable Trackable)
        {
            // Use the standard conversion from quaternion data into a single 3x3 3 axis of rotation rotation matrix
            Matrix rotationMatrix;
            rotationMatrix = Matrix.Create(new double[,] { { 1 - 2 * Trackable.QY * Trackable.QY - 2 * Trackable.QZ * Trackable.QZ, 2 * Trackable.QX * Trackable.QY - 2 * Trackable.QZ * Trackable.QW, 2 * Trackable.QX * Trackable.QZ + 2 * Trackable.QY * Trackable.QW },
                { 2 * Trackable.QX * Trackable.QY + 2 * Trackable.QZ * Trackable.QW, 1 - 2 * Trackable.QX * Trackable.QX - 2 * Trackable.QZ * Trackable.QZ, 2 * Trackable.QY * Trackable.QZ - 2 * Trackable.QX * Trackable.QW },
                { 2 * Trackable.QX * Trackable.QZ - 2 * Trackable.QY * Trackable.QW, 2 * Trackable.QY * Trackable.QZ + 2 * Trackable.QX * Trackable.QW, 1 - 2 * Trackable.QX * Trackable.QX - 2 * Trackable.QY * Trackable.QY } });

            return rotationMatrix;
        }
    }
}


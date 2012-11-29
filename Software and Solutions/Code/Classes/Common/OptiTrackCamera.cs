using System;
using System.Collections.Generic;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents an OptiTrackCamera
    /// </summary>
    public class OptiTrackCamera
    {
        double x = -1.0, y = -1.0, z = -1.0;
        double[] rotationMatrix = new double[9] { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0};
        int cameraNumber = -1;
        string cameraName = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public OptiTrackCamera()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OptiTrackCamera(OptiTrackCamera newCamera)
        {
            cameraNumber = newCamera.CameraNumber;
            cameraName = newCamera.cameraName;
            x = newCamera.xCoordinate;
            y = newCamera.yCoordinate;
            z = newCamera.zCoordinate;

            for (int i = 0; i < 9; i++)
            {
                rotationMatrix[i] = newCamera.rotationMatrix[i];
            }

        }

        /// <summary>
        /// Camera Number
        /// </summary>
        public int CameraNumber
        {
            get
            {
                return cameraNumber;
            }
            set
            {
                cameraNumber = value;
            }
        }

        /// <summary>
        /// Camera Name
        /// </summary>
        public string CameraName
        {
            get
            {
                return cameraName;
            }
            set
            {
                cameraName = value;
            }
        }

        /// <summary>
        /// x Coordinate
        /// </summary>
        public double xCoordinate
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// y Coordinate
        /// </summary>
        public double yCoordinate
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        /// <summary>
        /// z Coordinate
        /// </summary>
        public double zCoordinate
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        /// <summary>
        /// Camera Rotation Matrix
        /// </summary>
        public double[] RotationMatrix
        {
            get
            {
                return rotationMatrix;
            }

            set
            {
                rotationMatrix = value;
            }
        }

    }
}

using System;
using System.Collections.Generic;

namespace Stromohab_MCE
{
    class OptiTrackCamera
    {
        double x = -1.0, y = -1.0, z = -1.0;
        double[] rotationMatrix = new double[9] { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0};
        int cameraNumber = -1;

        public OptiTrackCamera()
        {
        }

        public OptiTrackCamera(OptiTrackCamera newCamera)
        {
            cameraNumber = newCamera.CameraNumber;
            x = newCamera.xCoordinate;
            y = newCamera.yCoordinate;
            z = newCamera.zCoordinate;

            for (int i = 0; i < 9; i++)
            {
                rotationMatrix[i] = newCamera.rotationMatrix[i];
            }

        }

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

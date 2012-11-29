using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.DataProcessing
{
    /// <summary>
    /// Locates the CoR of two Trackables based on continually updating data.
    /// Each time a new frame of data is avaliable, call FindCoR with the two Trackables
    /// and an updated estimate for the CoR will be located.
    /// 
    /// Note: One CoRLocator class must be instantiated for each pair of Trackbles / each joint
    /// </summary>
    /// <auther>Will Lunniss</auther>
    public class CoRLocator
    {
        #region Member Variables
        // These variables are part of a summation that is updated each frame
        private Matrix RT1_TransRT2 = new Matrix(3, 3);
        private Matrix RT2_TransRT1 = new Matrix(3, 3);
        private Matrix RT1_TransbT1T2 = new Matrix(3, 1);
        private Matrix RT2_TransbT1T2 = new Matrix(3, 1);

        // The CoR from the previous frame is preserved incase a new one can't be calculated
        private Matrix CoR = new Matrix(3, 1);

        // The current frame number
        private int frameNumber = 0;
        #endregion Member Variables

        #region Methods
        /// <summary>
        /// Clears all of the data so that the calculations can start again
        /// </summary>
        public void ClearData()
        {
            RT1_TransRT2 = new Matrix(3, 3);
            RT2_TransRT1 = new Matrix(3, 3);
            RT1_TransbT1T2 = new Matrix(3, 1);
            RT2_TransbT1T2 = new Matrix(3, 1);
            CoR = new Matrix(3, 1);
            frameNumber = 0;
        }

        /// <summary>
        /// Finds an updated CoR for the two Trackables given.
        /// </summary>
        /// <param name="T1">Trackable1</param>
        /// <param name="T2">Trackable2</param>
        /// <returns>The CoR Matrix, containg the x, y, and z coordinates</returns>
        public Matrix FindCoR(Trackable T1, Trackable T2)
        {
            //Increase the frame number
            frameNumber++;

            // Declare rotation matricies
            Matrix RT1, RT2;
            
            // Build rotation matrix from quaternion rotation data
            RT1 = StroMoHab_Matrix.Operations.BuildRotorFromQuaternionData(T1);
            RT2 = StroMoHab_Matrix.Operations.BuildRotorFromQuaternionData(T2);

            // Transpose the two rotation matricies
            Matrix RT1_Trans = StroMoHab_Matrix.Operations.Transpose(RT1);
            Matrix RT2_Trans = StroMoHab_Matrix.Operations.Transpose(RT2);

            // Add the next set of data to the summation
            RT1_TransRT2 = RT1_TransRT2 + RT1_Trans * RT2;
            RT2_TransRT1 = RT2_TransRT1 + RT2_Trans * RT1;
            
            // Put the centre of the Trackables and the distances between them in matrix form
            Matrix T1p = Matrix.Create(new double[, ] { { T1.xCoordinate }, { T1.yCoordinate }, { T1.zCoordinate } });
            Matrix T2p = Matrix.Create(new double[, ] { { T2.xCoordinate }, { T2.yCoordinate }, { T2.zCoordinate } });
            Matrix bT1T2 = T1p - T2p;

            // Add the next set of data to the summation
            RT1_TransbT1T2 = RT1_TransbT1T2 + RT1_Trans * bT1T2;
            RT2_TransbT1T2 = RT2_TransbT1T2 + RT2_Trans * bT1T2;

            // Build matrix b
            Matrix b = new Matrix(6,1);
            for (int i = 0; i < 3; i++)
            {
                b[i, 0] = RT1_TransbT1T2[i, 0];
                b[i + 3, 0] = -RT2_TransbT1T2[i, 0];
            }

            // Build then get the inverse of matrix M
            Matrix M_Inverse = BuildM_Inverse();
            if (M_Inverse == null) // Inversion of M failed because its determinant was 0 so just return the previous CoR
                return CoR;
            
            // Solve Ma = b to find a
            Matrix a = M_Inverse * b;

            // Extract the two parts of a
            Matrix aT1 = new Matrix(3,1);
            Matrix aT2 = new Matrix(3,1);
            for (int i = 0; i < 3; i++)
            {
                    aT1[i, 0] = a[i, 0];
                    aT2[i, 0] = a[i + 3, 0];
            }

            // Calculate the two centres of rotation
            Matrix CoR1 = (RT1 * aT1) + T1p;
            Matrix CoR2 = (RT2 * aT2) + T2p;

            // The final CoR is the average of the two CoR
            for (int i = 0; i < 3; i++)
            {
                CoR[i, 0] = (CoR1[i, 0] + CoR2[i, 0]) / 2;
            }          

            return CoR;
        }

        /// <summary>
        /// Builds the Matrix M and then performs an effcient block based inversion on it
        /// to generate Matrix M_Inverse
        /// </summary>
        /// <returns>M_Inverse</returns>
        private Matrix BuildM_Inverse()
        {
            Matrix M11 = new Matrix(3, 3);
            Matrix M12 = new Matrix(3, 3);
            Matrix M21 = new Matrix(3, 3);
            Matrix M22 = new Matrix(3, 3);
            Matrix M11_Inverse = new Matrix(3, 3);
            Matrix M11_New = new Matrix(3, 3);
            Matrix M12_New = new Matrix(3, 3);
            Matrix M21_New = new Matrix(3, 3);
            Matrix M22_New = new Matrix(3, 3);
            Matrix Sm11;
            Matrix Sm11_Inverse;
            Matrix M_Inverse = new Matrix(6, 6);

            //
            // | M11 M12 |
            // | M21 M22 |
            //

            // Fill in identiy Matrices
            for (int i = 0; i < 3; i++)
            {
                M11[i, i] = frameNumber;
                M22[i, i] = frameNumber;
            }

            // Add in RTX_TransRTX data 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    M12[i, j] = -RT1_TransRT2[i, j];
                    M21[i, j] = -RT2_TransRT1[i, j];
                }
            }

            M11_Inverse = M11.Inverse();
            // Build the Schur complement of M11
            Sm11 = M22 - M21 * M11_Inverse * M12;
            
			//If it can't invert return null - FindCoR will then return the prvious CoR
            if (Sm11.Determinant() != 0)
                Sm11_Inverse = Sm11.Inverse();
            else return null;

            //http://en.wikipedia.org/wiki/Invertible_matrix#Blockwise_inversion
            // Build up the new parts of the interverted matrix
            M11_New = M11_Inverse + M11_Inverse * M12 * Sm11_Inverse * M21 * M11_Inverse;
            M12_New = -M11_Inverse * M12 * Sm11_Inverse;
            M21_New = -Sm11_Inverse * M21 * M11_Inverse;
            M22_New = Sm11_Inverse;

            // Build M_inverse from its sub components Mxx_New
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    M_Inverse[i, j] = M11_New[i, j];
                    M_Inverse[i, j + 3] = M12_New[i, j];
                    M_Inverse[i + 3, j] = M21_New[i, j];
                    M_Inverse[i + 3, j + 3] = M22_New[i, j];
                }
            }
            return M_Inverse;
        }
        #endregion Methods
    }
}

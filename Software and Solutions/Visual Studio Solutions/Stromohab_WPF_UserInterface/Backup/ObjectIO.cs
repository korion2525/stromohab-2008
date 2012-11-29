using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using StroMoHab_Objects.Graphics;

namespace StromoLight_TaskDesigner
{
    static class ObjectIO
    {
        public static void LoadObject(string filePath)
        {
            ShadowedObject testObject = new ShadowedObject();
            
            
            TextReader tr = new StreamReader(filePath);
            string sep = " ";

            testObject.NumberOfVertices = Convert.ToInt32(tr.ReadLine());

            testObject.VerticesList.Capacity = testObject.NumberOfVertices;

            //Read vertices
            for (int i = 0; i < testObject.NumberOfVertices; i++)
            {
                Point3f currentVertex = new Point3f();

                string tempLine;
                string[] tempPoint;

                tempLine = tr.ReadLine();
                
                tempPoint = tempLine.Split(sep.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);

                currentVertex.X = (float)Convert.ToDouble(tempPoint[0]);
                currentVertex.Y = (float)Convert.ToDouble(tempPoint[1]);
                currentVertex.Z = (float)Convert.ToDouble(tempPoint[2]);

                testObject.VerticesList.Add(currentVertex);
            }

            //Read faces
            testObject.NumberOfFaces = Convert.ToInt32(tr.ReadLine());

            testObject.FacesList.Capacity = testObject.NumberOfFaces;

            for (int i = 0; i < testObject.NumberOfFaces; i++)
            {
                Point3f currentVertex = new Point3f();
                Face currentFace = new Face();

                for (int j = 0; j < 3; j++)
                {
                    currentFace.NeighbourIndices.Add(-1);
                }


                string currentFaceBuffer;
                string[] currentFaceDetails;


                currentFaceBuffer = tr.ReadLine();
                currentFaceDetails = currentFaceBuffer.Split(sep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                currentFace.VerticesList.Add(Convert.ToInt32(currentFaceDetails[0]));
                currentFace.VerticesList.Add(Convert.ToInt32(currentFaceDetails[1]));
                currentFace.VerticesList.Add(Convert.ToInt32(currentFaceDetails[2]));

                currentVertex.X = (float)Convert.ToDouble(currentFaceDetails[3]);
                currentVertex.Y = (float)Convert.ToDouble(currentFaceDetails[4]);
                currentVertex.Z = (float)Convert.ToDouble(currentFaceDetails[5]);

                currentFace.Normals.Add(new Point3f(currentVertex));

                currentVertex.X = (float)Convert.ToDouble(currentFaceDetails[6]);
                currentVertex.Y = (float)Convert.ToDouble(currentFaceDetails[7]);
                currentVertex.Z = (float)Convert.ToDouble(currentFaceDetails[8]);

                currentFace.Normals.Add(new Point3f(currentVertex));

                currentVertex.X = (float)Convert.ToDouble(currentFaceDetails[9]);
                currentVertex.Y = (float)Convert.ToDouble(currentFaceDetails[10]);
                currentVertex.Z = (float)Convert.ToDouble(currentFaceDetails[11]);

                currentFace.Normals.Add(new Point3f(currentVertex));

                testObject.FacesList.Add(currentFace);


            }

        }

        //public void SetConnectivity(ShadowedObject currentObject)
        //{
        //    foreach (Face A in currentObject.FacesList)
        //    {
        //        foreach (int currentVertex in A.VerticesList)
        //        {
        //            int vertex2 = A.VerticesList[


        //}
    }
}

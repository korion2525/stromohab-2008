using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
namespace OpenGLViewer
{
    public class OpenGLViewerRemoteControl : MarshalByRefObject
    {

        public const int MAX_Trackable_Number = 5;
        public const int MAX_Joint_Number = 2;

        public static bool[] trackableIDsArray = new bool[MAX_Trackable_Number];

        public static double[,] trackableOffset = new double[MAX_Trackable_Number,3];

        public void UpdateTrackableOffset(double[,] newTrackableOffset)
        {
            trackableOffset = newTrackableOffset;
        }

        public void SetTrackableIDsToDisplay(int ID, bool display)
        {
            
            trackableIDsArray[ID] = display;

        }

        

    }
}

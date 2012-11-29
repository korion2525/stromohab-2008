using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using StromoLight_RemoteCalibration;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Remoting;
using StroMoHab_Objects.Objects;
using StroMoHab_Objects.Communication;

namespace StromoLight_Diagnostics
{
    public class TestSubject
    {
        PersonData personData = null;
        Foot left = null;
        Foot right = null;
        //Calibration calibration = null;
        
        public TestSubject(string ID)
        {
            //this.calibration = cal;
            this.personData = new PersonData();
            this.personData.PersonID = ID;
            try
            {
                //if (calibration.LeftFoot_Marker != null)
                //{
                    left = new Foot("left");
                    right = new Foot("right");
                //}
                //else
                //{
                //    left = new Foot("left");
                //    right = new Foot("right");
                //}
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Problem with connecting to the server. (Accessing calibration file in TestSubject.cs)");
            }
        }

        public void NewTestFrameReceiveEvent()
        {
            TCPProcessor.WholeFrameReceivedEvent +=
                new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }

        public void StopTestFrameReceiveEvent()
        {
            TCPProcessor.WholeFrameReceivedEvent -=
                new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            if (MarkerList.listOfMarkers.Count == 2)
            {
                //FrontFoot(MarkerList.listOfMarkers[0], MarkerList.listOfMarkers[1]);
                left.PreviousMarker = new Marker(left.CurrentMarker);
                right.PreviousMarker = new Marker(right.CurrentMarker);
                AssignFootMarkers(MarkerList.listOfMarkers);
                //currentMarker = MarkerList.listOfMarkers[currentID]; //elaborate marker assignment to account for swapping
                //if (currentID != previousID) System.Diagnostics.Debug.WriteLine("Possible marker swap: " + currentMarker.TimeStamp);

                left.FootPositionList.Add(PositionData.ConvertMarkerPosition(left.CurrentMarker));
                right.FootPositionList.Add(PositionData.ConvertMarkerPosition(right.CurrentMarker));
                left.MarkerTrack();
                right.MarkerTrack();
            }
        }

        public void AssignFootMarkers(List<Marker> currentMarkerList)
        {
            int x0Diff = (int)(left.CurrentMarker.xCoordinate - currentMarkerList[0].xCoordinate);
            int y0Diff = (int)(left.CurrentMarker.yCoordinate - currentMarkerList[0].yCoordinate);
            int z0Diff = (int)(left.CurrentMarker.zCoordinate - currentMarkerList[0].zCoordinate);
            int x1Diff = (int)(left.CurrentMarker.xCoordinate - currentMarkerList[1].xCoordinate);
            int y1Diff = (int)(left.CurrentMarker.yCoordinate - currentMarkerList[1].yCoordinate);
            int z1Diff = (int)(left.CurrentMarker.zCoordinate - currentMarkerList[1].zCoordinate);
            int x2Diff = (int)(right.CurrentMarker.xCoordinate - currentMarkerList[0].xCoordinate);
            int y2Diff = (int)(right.CurrentMarker.yCoordinate - currentMarkerList[0].yCoordinate);
            int z2Diff = (int)(right.CurrentMarker.zCoordinate - currentMarkerList[0].zCoordinate);
            int x3Diff = (int)(right.CurrentMarker.xCoordinate - currentMarkerList[1].xCoordinate);
            int y3Diff = (int)(right.CurrentMarker.yCoordinate - currentMarkerList[1].yCoordinate);
            int z3Diff = (int)(right.CurrentMarker.zCoordinate - currentMarkerList[1].zCoordinate);
            int dif0 = (int)Math.Sqrt(Math.Pow(x0Diff, 2) + Math.Pow(z0Diff, 2) + Math.Pow(y0Diff, 2));
            int dif1 = (int)Math.Sqrt(Math.Pow(x1Diff, 2) + Math.Pow(z1Diff, 2) + Math.Pow(y1Diff, 2));
            int dif2 = (int)Math.Sqrt(Math.Pow(x2Diff, 2) + Math.Pow(z2Diff, 2) + Math.Pow(y2Diff, 2));
            int dif3 = (int)Math.Sqrt(Math.Pow(x3Diff, 2) + Math.Pow(z3Diff, 2) + Math.Pow(y3Diff, 2));
            if (left.CurrentMarker.MarkerId == 0)
            {
                bool swap = true;
                if (((dif0 < dif1) && (dif0 < dif2)) || ((dif0 < dif1) && (dif3 < dif2)) || ((dif3 < dif1) && (dif3 < dif2))) swap = false;
                if (swap == true)
                {
                    left.CurrentMarker = currentMarkerList[1];
                    right.CurrentMarker = currentMarkerList[0];
                }
                else
                {
                    left.CurrentMarker = currentMarkerList[0];
                    right.CurrentMarker = currentMarkerList[1];
                }
            }
            else
            {
                bool swap = true;
                if (((dif0 < dif1) && (dif0 < dif2)) || ((dif0 < dif1) && (dif3 < dif2)) || ((dif3 < dif1) && (dif3 < dif2))) swap = false;
                if (swap == true)
                {
                    left.CurrentMarker = currentMarkerList[0];
                    right.CurrentMarker = currentMarkerList[1];
                }
                else
                {
                    left.CurrentMarker = currentMarkerList[1];
                    right.CurrentMarker = currentMarkerList[0];
                }
            }
           
        }

        public PersonData PersonData
        {
            get { return personData; }
            set { personData = value; }
        }

        //public Calibration Markercalibration
        //{
        //    get { return this.calibration; }
        //    set { this.calibration = value; }
        //}

        public Foot Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        public Foot Left
        {
            get { return this.left; }
            set { this.left = value; }
        }
    }
}

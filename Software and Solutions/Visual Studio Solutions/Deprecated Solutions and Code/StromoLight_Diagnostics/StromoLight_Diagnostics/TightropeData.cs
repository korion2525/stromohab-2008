using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using StromoLight_RemoteCalibration;
using System.Runtime.Remoting;
using System.Net;
using System.Net.Sockets;
using System.Media;
using Stromohab_MCE;

namespace StromoLight_Diagnostics
{
    public class TightropeData
    {
        Foot left;
        Foot right;
        SoundPlayer sound;
        int eventcount = 0;
        StreamWriter outfile;
        bool leftFlag = false;
        bool rightFlag = false;
        Marker storedLeft;
        Marker storedRight;


        public TightropeData(StreamWriter file, Calibration markerHeightCal)
        {
            outfile = file;
            outfile.Write("Right, Left\n");
            left = new Foot("left", markerHeightCal.LeftFoot);
            right = new Foot("right", markerHeightCal.RightFoot);
            
            sound = new SoundPlayer(@"D:/Test/Conga2.wav");
            sound.Load();

            Foot.FootDownDetected += new Foot.FootDownEventHandler(Foot_FootDownDetected);

            //debug output
            Console.WriteLine("Start Left:" + " x" + ((int)markerHeightCal.LeftFoot.xCoordinate).ToString() + " y" +
               ((int)markerHeightCal.LeftFoot.yCoordinate).ToString() + " z" + ((int)markerHeightCal.LeftFoot.zCoordinate).ToString());
            Console.WriteLine("Start Right:" + " x" + ((int)markerHeightCal.RightFoot.xCoordinate).ToString() + " y" +
                ((int)markerHeightCal.RightFoot.yCoordinate).ToString() + " z" + ((int)markerHeightCal.RightFoot.zCoordinate).ToString() + "\n");
        }

        private void Foot_FootDownDetected(Foot foot)
        {
            if (eventcount++ > 2) //ignore initial events to allow for treadmill speed up etc
            {
                if (foot.FootName == "left")
                {
                    if (rightFlag == true)
                    {
                        rightFlag = false;
                        leftFlag = true;
                        outfile.Write(storedRight.xCoordinate.ToString() + "," + foot.CurrentMarker.xCoordinate.ToString() + "\n");
                    }
                    else
                    {
                        leftFlag = true;
                        storedLeft = new Marker(foot.CurrentMarker);
                    }
                }
                else
                {
                    leftFlag = false;
                }
                if (foot.FootName == "right")
                {
                    if (leftFlag == true)
                    {
                        leftFlag = false;
                        rightFlag = true;
                        outfile.Write(foot.CurrentMarker.xCoordinate.ToString() + "," + storedLeft.xCoordinate.ToString() + "\n");
                    }
                    else
                    {
                        rightFlag = true;
                        storedRight = new Marker(foot.CurrentMarker);
                    }
                }
                else
                {
                    rightFlag = false;
                }

                //debug output
                Console.WriteLine(foot.FootName + "Down " + foot.CurrentMarker.MarkerId.ToString() + " x" + foot.CurrentMarker.xCoordinate.ToString()
                    + " y" + foot.CurrentMarker.yCoordinate.ToString() + " z" + foot.CurrentMarker.zCoordinate.ToString() + " NP" + foot.NearestPosition.ToString() + "\n");
                sound.Play();
            }
        }
        //can add asymmetry analysis (exisitng class will require a small rewrite) and an overshoot measure for x-peaks
    }
}

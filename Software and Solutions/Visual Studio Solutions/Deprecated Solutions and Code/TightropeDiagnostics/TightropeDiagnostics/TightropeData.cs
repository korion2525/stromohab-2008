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
//using Stromohab_Diagnostics;
using System.Windows.Forms;

namespace StromoLight_Diagnostics
{
    public class TightropeData
    {
        Foot left;
        Foot right;
        SoundPlayer sound;
        int eventcount = 0;
        StreamWriter Xfile;
        bool leftFlag = false;
        bool rightFlag = false;
        Marker storedLeft;
        Marker storedRight;
        int LSSstate = 0;//cycle stage counters for each of the symmetry variables
        int LCDstate = 0;
        int LSIstate = 0;
        int LDSstate = 0;
        int RSSstate = 0;
        int RCDstate = 0;
        int RSIstate = 0;
        int RDSstate = 0;
        double sssi = 0;
        double cdsi = 0;
        double sisi = 0;
        double dssi = 0;
        double[] LSS, LCD, LSI, LDS;
        double[] RSS, RCD, RSI, RDS;
        int cyclestate = 0;
        List<double> LSSlist;
        List<double> LCDlist;
        List<double> LSIlist;
        List<double> LDSlist;
        List<double> RSSlist;
        List<double> RCDlist;
        List<double> RSIlist;
        List<double> RDSlist;
        bool zeroflag = false;
        string fileID;


        public TightropeData(TestSubject subject, string testname, int testnumber)
        {
            this.fileID = testname+ subject.SubjectID + testnumber.ToString();
            Xfile = new StreamWriter(@"D:Test/" + fileID + "Accuracy.csv", false);
            Xfile.Write("Right, Left\n");
            left = subject.Left;
            right = subject.Right;

            LSS = new double[5];
            LCD = new double[4];
            LSI = new double[4];
            LDS = new double[4];
            RSS = new double[5];
            RCD = new double[4];
            RSI = new double[4];
            RDS = new double[4];
            LSSlist = new List<double>();
            LCDlist = new List<double>();
            LSIlist = new List<double>();
            LDSlist = new List<double>();
            RSSlist = new List<double>();
            RCDlist = new List<double>();
            RSIlist = new List<double>();
            RDSlist = new List<double>();

            sound = new SoundPlayer(@"D:/Test/Conga2.wav");
            sound.Load();
            
            Foot.FootDownDetected += new Foot.FootDownEventHandler(Foot_FootDownDetected);
            Foot.zFootPeakDetected += new Foot.Z_PeakEventHandler(Foot_zFootPeakDetected);
            
            //debug output - feet calibration positions
            Console.WriteLine("Start Left:" + " x" + ((int)left.CalibrationMarker.xCoordinate).ToString() + " y" + 
                ((int)left.CalibrationMarker.yCoordinate).ToString() + " z" + ((int)left.CalibrationMarker.zCoordinate).ToString());
            Console.WriteLine("Start Right:" + " x" + ((int)right.CalibrationMarker.xCoordinate).ToString() + " y" +
                ((int)right.CalibrationMarker.yCoordinate).ToString() + " z" + ((int)right.CalibrationMarker.zCoordinate).ToString() + "\n");
        }

        #region Foot down detection
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
                        Xfile.Write((storedRight.xCoordinate.ToString() + "," + foot.CurrentMarker.xCoordinate.ToString()) + "\n");
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
                        Xfile.Write((foot.CurrentMarker.xCoordinate.ToString() + "," + storedLeft.xCoordinate.ToString()) + "\n");
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
        #endregion

        #region Asymmetry Calculation
        //can add asymmetry analysis (exisitng class will require a small rewrite) and an overshoot measure for x-peaks
        private void Foot_zFootPeakDetected(Foot foot)
        {
            //sound.Play();
            
            
            //first check sequence - if sequence broken, reset all variables
            int currentstate = CycleState(foot);
            if ((cyclestate == 0) || (currentstate != (cyclestate % 4) + 1))
            {
                cyclestate = currentstate;
                //Re-set / re-initialise symmetry variables
                LSSstate = 0;
                LCDstate = 0;
                LSIstate = 0;
                LDSstate = 0;
                RSSstate = 0;
                RCDstate = 0;
                RSIstate = 0;
                RDSstate = 0;
            }
            else
            {
                cyclestate = (cyclestate % 4) + 1;
            }

            
            #region Notes on calculation of symmetry measures
            //Update all symmetry variables affected by this gait event
            //Gait cycle state:
            //  1 = left toe off
            //  2 = left heel down
            //  3 = right toe off
            //  4 = right heel down
            //Some symmetry parameters require events from more than one strid
            //Cyclestates followed by n (eg 2n) indicates an event from a subsequent stride
            //When the correct gait events for a parameter have occurred, the result is summed
            //and a count kept of the number of elements added (for post-calculation of the mean)
            #endregion
            Console.WriteLine("Cycle state: " + cyclestate.ToString());
            switch (cyclestate)
            {
                case 1:
                    //Left swing stance -> 1, 2, 1n
                    LSS[LSSstate] = foot.CurrentMarker.TimeStamp;
                    if (LSSstate == 2)//if end of LSS cycle -> add result to summation
                    {
                        LSS[3] += ((LSS[1] - LSS[0]) / (LSS[2] - LSS[1]));
                        LSS[4]++;
                        LSSstate = 0;
                        LSSlist.Add((LSS[1] - LSS[0]) / (LSS[2] - LSS[1]));
                    }
                    else
                    {
                        LSSstate = 1;
                    }
                    //Right double support -> 4, 1n
                    //Double support calculation only requires 2 events (4, 1n) in that order
                    //If gait cycle event '1' is not received after an event '4', it can be ignored
                    //ie. it will not contribute to a valid calculation
                    if (RDSstate == 1)
                    {
                        RDS[2] += (foot.CurrentMarker.TimeStamp - RDS[0]);
                        RDS[3]++;
                        RDSstate = 0;
                        RDSlist.Add((foot.CurrentMarker.TimeStamp - RDS[0]));
                    }
                    break;

                case 2:
                    //Left swing stance -> 1, 2, 1n
                    if (LSSstate == 1)
                    {
                        LSS[1] = foot.CurrentMarker.TimeStamp;
                        LSSstate = 2;
                    }
                    //Left cycle duration -> 2, 2n
                    //Any '2' event can be stored, only the 2nd will trigger a calculation
                    LCD[LCDstate] = foot.CurrentMarker.TimeStamp;
                    if (LCDstate == 1)
                    {
                        LCD[2] += (LCD[1] - LCD[0]);
                        LCD[3]++;
                        LCDstate = 0;
                        LCDlist.Add(LCD[1] - LCD[0]);
                    }
                    else
                    {
                        LCDstate = 1;
                    }
                    //Left step interval -> 2, 4
                    LSI[0] = foot.CurrentMarker.TimeStamp;
                    LSIstate = 1;
                    //Right step interval -> 4, 2n
                    if (RSIstate == 1)
                    {
                        RSI[2] += (foot.CurrentMarker.TimeStamp - RSI[0]);
                        RSI[3]++;
                        RSIstate = 0;
                        RSIlist.Add(foot.CurrentMarker.TimeStamp - RSI[0]);
                    }
                    //Left double support -> 2, 3
                    LDS[0] = foot.CurrentMarker.TimeStamp;
                    LDSstate = 1;
                    break;

                case 3:
                    //Right swing stance -> 3, 4, 3n
                    RSS[RSSstate] = foot.CurrentMarker.TimeStamp;
                    if (RSSstate == 2)
                    {
                        RSS[3] += ((RSS[1] - RSS[0]) / (RSS[2] - RSS[1]));
                        RSS[4]++;
                        RSSstate = 0;
                        RSSlist.Add((RSS[1] - RSS[0]) / (RSS[2] - RSS[1]));
                    }
                    else
                    {
                        RSSstate = 1;
                    }
                    //Left double support -> 2, 3
                    if (LDSstate == 1)
                    {
                        LDS[2] += (foot.CurrentMarker.TimeStamp - LDS[0]);
                        LDS[3]++;
                        LDSstate = 0;
                        LDSlist.Add(foot.CurrentMarker.TimeStamp - LDS[0]);
                    }
                    break;

                case 4:
                    //Right swing stance -> 3, 4, 3n
                    if (RSSstate == 1)
                    {
                        RSS[1] = foot.CurrentMarker.TimeStamp;
                        RSSstate = 2;
                    }
                    //Right cycle duration -> 4, 4n
                    RCD[RCDstate] = foot.CurrentMarker.TimeStamp;
                    if (RCDstate == 1)
                    {
                        RCD[2] += (RCD[1] - RCD[0]);
                        RCD[3]++;
                        RCDstate = 0;
                        RCDlist.Add(RCD[1] - RCD[0]);
                    }
                    else
                    {
                        RCDstate = 1;
                    }
                    //Left step interval -> 2, 4
                    if (LSIstate == 1)
                    {
                        LSI[2] += (foot.CurrentMarker.TimeStamp - LSI[0]);
                        LSI[3]++;
                        LSIstate = 0;
                        LSIlist.Add(foot.CurrentMarker.TimeStamp - LSI[0]);
                    }
                    //Right step interval -> 4, 2n
                    RSI[RSIstate] = foot.CurrentMarker.TimeStamp;
                    RSIstate = 1;
                    //Right double support -> 4, 1n
                    RDS[0] = foot.CurrentMarker.TimeStamp;
                    RDSstate = 1;
                    break;

                default:
                    Console.WriteLine("Error in gait cycle tracking.");
                    break;
            }
            if (zeroflag)
            {
                double LSSmean = LSS[3] / LSS[4];
                double RSSmean = RSS[3] / RSS[4];
                double LCDmean = LCD[2] / LCD[3];
                double RCDmean = RCD[2] / RCD[3];
                double LSImean = LSI[2] / LSI[3];
                double RSImean = RSI[2] / RSI[3];
                double LDSmean = LDS[2] / LDS[3];
                double RDSmean = RDS[2] / RDS[3];
                sssi = (100 * (RSSmean - LSSmean) / (RSSmean + LSSmean));
                cdsi = (100 * (RCDmean - LCDmean) / (RCDmean + LCDmean));
                sisi = (100 * (RSImean - LSImean) / (RSImean + LSImean));
                dssi = (100 * (RDSmean - LDSmean) / (RDSmean + LDSmean));
            }
            else
            {
                if ((LSS[4] != 0) && (RSS[4] != 0) && (LCD[3] != 0) && (RCD[3] != 0) && (LSI[3] != 0) && (RSI[3] != 0) && (LDS[3] != 0) && (RDS[3] != 0))
                    zeroflag = true;
            }
        }
        #endregion

        public void WriteSymmetryData()
        {
            StreamWriter symmetryFile = new StreamWriter(@"D:/Test/" + fileID + "SymmetryData.csv", false);
            int c = 0;
            symmetryFile.Write("Left swing/stance,");
            while (c != LSSlist.Count - 1)
            {
                symmetryFile.Write(LSSlist[c] + ",");
                c++;
            }
            symmetryFile.Write(LSSlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Right swing/stance,");
            while (c != RSSlist.Count - 1)
            {
                symmetryFile.Write(RSSlist[c] + ",");
                c++;
            }
            symmetryFile.Write(RSSlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Left cycle duration,");
            while (c != LCDlist.Count - 1)
            {
                symmetryFile.Write(LCDlist[c] + ",");
                c++;
            }
            symmetryFile.Write(LCDlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Right cycle duration,");
            while (c != RCDlist.Count - 1)
            {
                symmetryFile.Write(RCDlist[c] + ",");
                c++;
            }
            symmetryFile.Write(RCDlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Left step interval,");
            while (c != LSIlist.Count - 1)
            {
                symmetryFile.Write(LSIlist[c] + ",");
                c++;
            }
            symmetryFile.Write(LSIlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Right step interval,");
            while (c != RSIlist.Count - 1)
            {
                symmetryFile.Write(RSIlist[c] + ",");
                c++;
            }
            symmetryFile.Write(RSIlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Left double support,");
            while (c != LDSlist.Count - 1)
            {
                symmetryFile.Write(LDSlist[c] + ",");
                c++;
            }
            symmetryFile.Write(LDSlist[c] + "\n");
            c = 0;
            symmetryFile.Write("Right double support,");
            while (c != RDSlist.Count - 1)
            {
                symmetryFile.Write(RDSlist[c] + ",");
                c++;
            }
            symmetryFile.Write(RDSlist[c] + "\n");
            symmetryFile.Write("Swing/stance ratio SI," + sssi.ToString() + "\n");
            symmetryFile.Write("Cycle duration SI," + cdsi.ToString() + "\n");
            symmetryFile.Write("Step interval duration SI," + sisi.ToString() + "\n");
            symmetryFile.Write("Double support duration SI," + dssi.ToString());
            symmetryFile.Close();
            Xfile.Close();
        }

        private int CycleState(Foot foot)
        {
            int state = 0;
            if (foot.FootName == "left")
            {
                state = 1;
            }
            else
            {
               state = 3;
            }
            if (foot.ZDirection == -1)
            {
                state++;
            }
            return state;
        }

        #region Symmetry Index (SI) properties
        public double SSSI
        {
            get { return sssi; }
        }
        public double CDSI
        {
            get { return cdsi; }
        }
        public double SISI
        {
            get { return sisi; }
        }
        public double DSSI
        {
            get { return dssi; }
        }
        public List<double> LSSLIST
        {
            get { return LSSlist; }
        }
        public List<double> RSSLIST
        {
            get { return RSSlist; }
        }
        public List<double> LCDLIST
        {
            get { return LCDlist; }
        }
        public List<double> RCDLIST
        {
            get { return RCDlist; }
        }
        public List<double> LDSLIST
        {
            get { return LDSlist; }
        }
        public List<double> LSILIST
        {
            get { return LSIlist; }
        }
        public List<double> RSILIST
        {
            get { return RSIlist; }
        }
        public List<double> RDSLIST
        {
            get { return RDSlist; }
        }
        #endregion
    }
}

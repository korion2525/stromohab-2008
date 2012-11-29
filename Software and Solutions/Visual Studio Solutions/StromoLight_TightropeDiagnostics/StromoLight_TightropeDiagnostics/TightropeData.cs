using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using StromoLight_RemoteCalibration;
using System.Runtime.Remoting;
using System.Net;
using System.Net.Sockets;
using System.Media;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StromoLight_Diagnostics
{
    public class TightropeData
    {
        Foot left = null;
        Foot right = null;
        List<SIdata> DSSI;

        internal List<SIdata> DSSI1
        {
            get { return DSSI; }
            set { DSSI = value; }
        }
        List<SIdata> SISI;

        internal List<SIdata> SISI1
        {
            get { return SISI; }
            set { SISI = value; }
        }
        List<SIdata> CDSI;

        internal List<SIdata> CDSI1
        {
            get { return CDSI; }
            set { CDSI = value; }
        }
        List<SIdata> SSSI;

        internal List<SIdata> SSSI1
        {
            get { return SSSI; }
            set { SSSI = value; }
        }
        List<PositionData > LeftList;
        List<PositionData> RightList;
        List<PositionData> LeftOvershootList;
        List<PositionData> RightOvershootList;
        string fileID;
        int tablepointer = 0;
        int teststart;
        int startcount = 0;
        string foot1 = null;
        string foot2 = null;
        int cyclestate = 0;
        int[] statetable;
        int footdownevents = 0;
        double stridelength = 0;
        double stridestart = 0;

        public double Stridelength
        {
            get { return stridelength; }
            set { stridelength = value; }
        }


        List<float> LTO;
        List<float> LHD;
        List<float> RTO;
        List<float> RHD;

        #region Asynchronous arrays and lists
        /*
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
        List<double> LSSlist;
        List<double> LCDlist;
        List<double> LSIlist;
        List<double> LDSlist;
        List<double> RSSlist;
        List<double> RCDlist;
        List<double> RSIlist;
        List<double> RDSlist;
        */
        #endregion

        public TightropeData(TestSubject subject, string fileID, int startTime)
        {
            
            this.fileID = fileID;
            left = subject.Left;
            right = subject.Right;
            DSSI = new List<SIdata>();
            SISI = new List<SIdata>();
            CDSI = new List<SIdata>();
            SSSI = new List<SIdata>();
            LeftList = new List<PositionData>();
            RightList = new List<PositionData>();
            LeftOvershootList = new List<PositionData>();
            RightOvershootList = new List<PositionData>();
            statetable = new int[8];
            teststart = startTime;

            LTO = new List<float>();
            LHD = new List<float>();
            RTO = new List<float>();
            RHD = new List<float>();

            #region Asynchronous arrays and lists
            /*
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
            */
            #endregion

            Foot.FootDownDetected += new Foot.FootDownEventHandler(Foot_FootDownDetected);
            Foot.zFootPeakDetected += new Foot.Z_PeakEventHandler(Foot_zFootPeakDetected);
            //Foot.xFootPeakDetected += new Foot.X_PeakEventHandler(Foot_xFootPeakDetected);
            
            //debug output - feet calibration positions
            System.Diagnostics.Debug.WriteLine("Start Left" + left.CalibrationMarker.MarkerId + ": x" + ((int)left.CalibrationMarker.xCoordinate).ToString() + " y" + 
                ((int)left.CalibrationMarker.yCoordinate).ToString() + " z" + ((int)left.CalibrationMarker.zCoordinate).ToString() + " Time: " + left.CalibrationMarker.TimeStamp.ToString());
            System.Diagnostics.Debug.WriteLine("Start Right" + right.CalibrationMarker.MarkerId + ": x" + ((int)right.CalibrationMarker.xCoordinate).ToString() + " y" +
                ((int)right.CalibrationMarker.yCoordinate).ToString() + " z" + ((int)right.CalibrationMarker.zCoordinate).ToString() + "\n");

        }

        /*#region Foot overshoot
        private void Foot_xFootPeakDetected(Foot foot)
        {
            if (Foot.Frontfoot != null)
            {
                if (foot.FootName == Foot.Frontfoot.FootName)
                {
                    if (foot.CurrentMarker.yCoordinate > foot.FootDown_yThreshold + 5)
                    {
                        PositionData accuracyData = new PositionData();
                        if ((foot.FootName == "left") && (foot.CurrentMarker.xCoordinate > 0))
                        {
                            accuracyData.XCoordinate = foot.CurrentMarker.xCoordinate;
                            accuracyData.Time = foot.CurrentMarker.TimeStamp - teststart;
                            LeftOvershootList.Add(accuracyData);
                            System.Diagnostics.Debug.WriteLine("Left foot overshoot:" + accuracyData.XCoordinate);
                        }
                        if ((foot.FootName == "right") && (foot.CurrentMarker.xCoordinate < 0))
                        {
                            accuracyData.XCoordinate = foot.CurrentMarker.xCoordinate;
                            accuracyData.Time = foot.CurrentMarker.TimeStamp - teststart;
                            RightOvershootList.Add(accuracyData);
                            System.Diagnostics.Debug.WriteLine("Right foot overshoot:" + accuracyData.XCoordinate);
                        }
                    }
                }
            }
        }
        #endregion*/


        #region Foot down accuracy
        private void Foot_FootDownDetected(Foot foot)
        {
            if (footdownevents++ > 2)//number of events to miss before recording data
            {
                //if (teststart == 0) teststart = foot.CurrentMarker.TimeStamp; //save time at start
                PositionData accuracyData = new PositionData();
                accuracyData.XCoordinate = foot.CurrentMarker.xCoordinate;
                accuracyData.Time = foot.CurrentMarker.TimeStamp - teststart;
                System.Diagnostics.Debug.WriteLine(foot.FootName + "Down" + " Y:" + foot.CurrentMarker.yCoordinate + "(" + foot.FootDown_yThreshold + ")" + "  Position:" + accuracyData.XCoordinate + "  Time:" + accuracyData.Time);
                if (foot.FootName == "left") LeftList.Add(accuracyData);
                if (foot.FootName == "right") RightList.Add(accuracyData);
            }
        }
        #endregion


        #region Synchronous Asymmetry Calculation

        private void Foot_zFootPeakDetected(Foot foot)
        {
            if (startcount == 3)//wait for 2 events before starting cycling
            {
                int currentstate = CycleState(foot);
                if (currentstate != 0)
                {
                    //if (teststart == 0) teststart = foot.CurrentMarker.TimeStamp; //save time at start
                    if (currentstate != ((cyclestate % 4) + 1))
                    {
                        //regular stride sequence interrupted
                        //reset state table and cyclestate to initiate new stride sequence
                        cyclestate = 0;
                        tablepointer = 0;
                        stridestart = 0;
                        foot1 = null;
                        foot2 = null;
                    }
                    else
                    {
                        //process state table - add event and time, calc SI if 2nd block etc
                        float SIfoot1;
                        float SIfoot2;
                        cyclestate = currentstate;
                        statetable[tablepointer] = foot.CurrentMarker.TimeStamp - teststart;
                        System.Diagnostics.Debug.WriteLine("Cyclestate:" + cyclestate + "  Foot event:" + foot.FootName + "(" + foot.ZDirection + ")" + statetable[tablepointer] + "  Tablepointer:" + tablepointer);                        
                        switch (tablepointer)
                        {
                            case 4:
                                SIfoot1 = statetable[2] - statetable[1];
                                SIfoot2 = statetable[4] - statetable[3];
                                DSSI.Add(SymmetryIndex(SIfoot1, SIfoot2));
                                break;

                            case 5:
                                SIfoot1 = statetable[3] - statetable[1];
                                SIfoot2 = statetable[5] - statetable[3];
                                SISI.Add(SymmetryIndex(SIfoot1, SIfoot2));
                                break;

                            case 6:
                                SIfoot1 = (float)((float)(statetable[1] - statetable[0]) / (float)(statetable[4] - statetable[1]));
                                SIfoot2 = (float)((float)(statetable[3] - statetable[2]) / (float)(statetable[6] - statetable[3]));
                                SSSI.Add(SymmetryIndex(SIfoot1, SIfoot2));
                                
                                break;

                            case 7:
                                SIfoot1 = statetable[5] - statetable[1];
                                SIfoot2 = statetable[7] - statetable[3];
                                CDSI.Add(SymmetryIndex(SIfoot1, SIfoot2));

                                //shift statetable block2 -> block1 [2nd stride of previous becomes 1st stride of next]
                                statetable[0] = statetable[4];
                                statetable[1] = statetable[5];
                                statetable[2] = statetable[6];
                                statetable[3] = statetable[7];
                                tablepointer = 3;
                                System.Diagnostics.Debug.WriteLine("\n");
                                break;

                            default:
                                break;
                        }
                        tablepointer++;
                    }
                }
            }
            else
            {
                startcount++;
            }
        }

        private SIdata SymmetryIndex(float SIfoot1, float SIfoot2)
        {
            SIdata siData = new SIdata();
            siData.SymmetryIndex = 100 * (SIfoot2 - SIfoot1) / (SIfoot1 + SIfoot2);
            if (foot1 == "right") siData.SymmetryIndex = - siData.SymmetryIndex; //keep SI relative to right limb
            siData.Time = statetable[tablepointer];
            return siData;
        }

        private int CycleState(Foot foot)
        {
            if (foot1 == null)
            {
                if (foot.ZDirection == 1)
                {
                    foot1 = foot.FootName;
                    System.Diagnostics.Debug.WriteLine("\n\nAt start of new cycle. Foot1 = " + foot1 + "  Marker:" + foot.CurrentMarker.MarkerId);
                }
                if (foot1 == "left")
                {
                    foot2 = "right";
                }
                if (foot1 == "right")
                {
                    foot2 = "left";
                }
            }
            int state = 0;
            if (foot1 != null)
            {
                if ((foot.FootName == foot1) && (foot.ZDirection == 1))
                {
                    state = 1;
                    stridestart = foot.CurrentMarker.zCoordinate;
                }
                if ((foot.FootName == foot1) && (foot.ZDirection == -1))
                {
                    state = 2;
                }
                if ((foot.FootName == foot2) && (foot.ZDirection == 1))
                {
                    state = 3;
                }
                if ((foot.FootName == foot2) && (foot.ZDirection == -1))
                {
                    state = 4;
                    stridelength = foot.CurrentMarker.zCoordinate - stridestart;
                    stridestart = 0;
                }
            }
            return state;
        }

        #endregion


        #region Write data out to file *.csv
        public void ToFile()
        {
            bool moreData = true;
            int c = 0;
            RemoveLastSIdataLine();//Last line of data (at treadmill stop) not reliable.
            StreamWriter dataFile = new StreamWriter(fileID + "_Data.csv", false);
            dataFile.Write("Time, Left Accuracy, , Time, Right Accuracy, , Time, Left overshoot, , Time, Right overshoot, , Time, SSSI, , Time, CDSI, , Time, SISI, , Time, DSSI\n");
            while (moreData)
            {
                moreData = false;
                if (c < LeftList.Count)
                {
                    moreData = true;
                    dataFile.Write(LeftList[c].Time + "," + LeftList[c].XCoordinate + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < RightList.Count)
                {
                    moreData = true;
                    dataFile.Write(RightList[c].Time + "," + RightList[c].XCoordinate + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < LeftOvershootList.Count)
                {
                    moreData = true;
                    dataFile.Write(LeftOvershootList[c].Time + "," + LeftOvershootList[c].XCoordinate + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < RightOvershootList.Count)
                {
                    moreData = true;
                    dataFile.Write(RightOvershootList[c].Time + "," + RightOvershootList[c].XCoordinate + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < SSSI.Count)
                {
                    moreData = true;
                    dataFile.Write(SSSI[c].Time + "," + SSSI[c].SymmetryIndex + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < CDSI.Count)
                {
                    moreData = true;
                    dataFile.Write(CDSI[c].Time + "," + CDSI[c].SymmetryIndex + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < SISI.Count)
                {
                    moreData = true;
                    dataFile.Write(SISI[c].Time + "," + SISI[c].SymmetryIndex + ",,");
                }
                else
                {
                    dataFile.Write(",,,");
                }
                if (c < DSSI.Count)
                {
                    moreData = true;
                    dataFile.Write(DSSI[c].Time + "," + DSSI[c].SymmetryIndex + "\n");
                }
                else
                {
                    dataFile.Write("\n");
                }
                c++;
            }
            dataFile.Flush();
            dataFile.Close();
        }

        public void WriteRawFeetData()
        {
            //Reinstate this binary output of de-swapped feet data if useful
            /*
            FileStream filestream = new FileStream(fileID + "_FeetData", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(filestream, left.FootPositionList);
            formatter.Serialize(filestream, right.FootPositionList);
            filestream.Flush();
            filestream.Close();
            */

            StreamWriter FootFile = new StreamWriter(fileID + "DeSwappedFootFile.csv", false);
            int fcount;
            if (left.FootPositionList.Count < right.FootPositionList.Count)
            {
                fcount = left.FootPositionList.Count;
            }
            else
            {
                fcount = right.FootPositionList.Count;
            }
            FootFile.Write("Time, Left X, Left Y, Left Z,, Right X, Right Y, Right Z\n");
            for (int i = 0; i < fcount; i++)
            {
                FootFile.Write((left.FootPositionList[i].Time-teststart).ToString() + "," + left.FootPositionList[i].XCoordinate + "," + left.FootPositionList[i].YCoordinate + "," + left.FootPositionList[i].ZCoordinate + ",,"
                    + right.FootPositionList[i].XCoordinate + "," + right.FootPositionList[i].YCoordinate + "," + right.FootPositionList[i].ZCoordinate + "\n");
            }
            FootFile.Flush();
            FootFile.Close();
        }

        private void RemoveLastSIdataLine()
        {
            if (CDSI.Count > 0) CDSI.RemoveAt(CDSI.Count - 1);
            if (SSSI.Count > 0) SSSI.RemoveAt(SSSI.Count - 1);
            if (SISI.Count > 0) SISI.RemoveAt(SISI.Count - 1);
            if (DSSI.Count > 0) DSSI.RemoveAt(DSSI.Count - 1);
        }

        #endregion


        public void DataEventCleanUp()
        {
            Foot.FootDownDetected -= new Foot.FootDownEventHandler(Foot_FootDownDetected);
            Foot.zFootPeakDetected -= new Foot.Z_PeakEventHandler(Foot_zFootPeakDetected);
            //Foot.xFootPeakDetected -= new Foot.X_PeakEventHandler(Foot_xFootPeakDetected);
        }

        public int TestStartTime
        {
            get { return teststart; }
            set { teststart = value; }
        }

        #region Asynchronous Asymmetry Calculation
        /*
        //can add asymmetry analysis (exisitng class will require a small rewrite) and an overshoot measure for x-peaks
        private void Foot_zFootPeakDetected(Foot foot)
        {
            //sound.Play();
            
            
            //first check sequence - if sequence broken, reset all variables
            int currentstate = CycleState(foot);
            if ((cyclestate == 0) || (currentstate != ((cyclestate % 4) + 1)))//ie. cyclestate is stored in the class between event calls, the next in the sequence is expected
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
            //Some symmetry parameters require events from more than one stride
            //Cyclestates followed by n (eg 2n) indicates an event from a subsequent stride
            //When the correct gait events for a parameter have occurred, the result is summed
            //and a count kept of the number of elements added (for post-calculation of the mean)
            #endregion
            if (testtime == 0) testtime = foot.CurrentMarker.TimeStamp;
            Console.WriteLine("Cycle state: " + cyclestate.ToString() + " - " + (foot.CurrentMarker.TimeStamp-testtime).ToString());

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
        */

        #endregion


        #region Asynchronous Foot down detection
        /*
        private void Foot_FootDownDetected(Foot foot)
        {
            //if (eventcount++ > 2) //ignore initial events to allow for treadmill speed up etc
            //{
                if (foot.FootName == "left")
                {
                    if (rightFlag == true)
                    {
                        rightFlag = false;
                        leftFlag = true;
                        RightList.Add(storedRight.xCoordinate);
                        LeftList.Add(foot.CurrentMarker.xCoordinate);
                        //Xfile.Write((storedRight.xCoordinate.ToString() + "," + foot.CurrentMarker.xCoordinate.ToString()) + "\n");
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
                        RightList.Add(foot.CurrentMarker.xCoordinate);
                        LeftList.Add(storedLeft.xCoordinate);
                        //Xfile.Write((foot.CurrentMarker.xCoordinate.ToString() + "," + storedLeft.xCoordinate.ToString()) + "\n");
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
                System.Diagnostics.Debug.WriteLine(foot.FootName + "Down " + foot.CurrentMarker.MarkerId.ToString() + " x" + foot.CurrentMarker.xCoordinate.ToString()
                    + " y" + foot.CurrentMarker.yCoordinate.ToString() + " z" + foot.CurrentMarker.zCoordinate.ToString() + " NP" + foot.NearestPosition.ToString() + "\n");
                //sound.Play();
            //}
        }
        */
        #endregion


        #region Write Asynchronous Symmetry Data
        /*
        public void WriteSymmetryDataColumnFormat()
        {
            StreamWriter symmetryFile = new StreamWriter(fileID + "_SymmetryData.csv", false);
            bool moreData = true;
            int c = 0;
            symmetryFile.Write("Left swing/stance,Right swing/stance,Left cycle duration,Right cycle duration,");
            symmetryFile.Write("Left step interval,Right step interval,Left double support,Right double support,");
            symmetryFile.Write("Swing/stance ratio SI,Cycle duration SI,Step interval duration SI,Double support duration SI\n");
            while (moreData)
            {
                moreData = false;
                if (c < LSSlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(LSSlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < RSSlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(RSSlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < LCDlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(LCDlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < RCDlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(RCDlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < LSIlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(LSIlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < RSIlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(RSIlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < LDSlist.Count)
                {
                    moreData = true;
                    symmetryFile.Write(LDSlist[c] + ",");
                }
                else
                {
                    symmetryFile.Write("" + ",");
                }
                if (c < RDSlist.Count)
                {
                    moreData = true;
                    if (c == 0)
                    {
                        symmetryFile.Write(RDSlist[c] + ",");
                        symmetryFile.Write(sssi.ToString() + "," + cdsi.ToString() + "," + sisi.ToString() + "," + dssi.ToString() + "\n");
                    }
                    else
                    {
                        symmetryFile.Write(RDSlist[c] + "\n");
                    }
                }
                else
                {
                    if (c == 0)
                    {
                        symmetryFile.Write("" + ",");
                        symmetryFile.Write(sssi.ToString() + "," + cdsi.ToString() + "," + sisi.ToString() + "," + dssi.ToString() + "\n");
                    }
                    else
                    {
                        symmetryFile.Write("" + "\n");
                    }
                }
                c++;
            }
            symmetryFile.Flush();
            symmetryFile.Close();
        }
        */
        #endregion


        #region Write Asynchronous Accuracy Data
        
        public void WriteAccuracyData()
        {
            StreamWriter Xfile = new StreamWriter(fileID + "_Accuracy.csv", false);
            bool moreData = true;
            int c = 0;
            Xfile.Write("Right, Left\n");
            while (moreData)
            {
                moreData = false;
                if (c < RightList.Count)
                {
                    moreData = true;
                    Xfile.Write(RightList[c] + ",");
                }
                else
                {
                    Xfile.Write("" + ",");
                }
                if (c < LeftList.Count)
                {
                    moreData = true;
                    Xfile.Write(LeftList[c] + "\n");
                }
                else
                {
                    Xfile.Write("" + "\n");
                }
                c++;
            }
            Xfile.Flush();
            Xfile.Close();
        }
        
        #endregion


        #region Asynchronous CycleState
        /*
        private int CycleState(Foot foot)
        {
            int state = 0;
            if ((foot.FootName == "left") && (foot.ZDirection == 1)) state = 1;
            if ((foot.FootName == "left") && (foot.ZDirection == -1)) state = 2;
            if ((foot.FootName == "right") && (foot.ZDirection == 1)) state = 3;
            if ((foot.FootName == "right") && (foot.ZDirection == -1)) state = 4;
            return state;
        }
        */
        #endregion


        #region Asynchronous Symmetry Index (SI) properties
        /*
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
        */
        #endregion
    }
}

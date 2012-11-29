using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stromohab_MCE;
//using Stromohab_MCE_Connection;
using System.Media;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace Stromohab_Diagnostics
{
    /// <summary>
    /// Generates asymmetry data; fires data available event;
    /// passes data in an instance of StrideDiagnostics
    /// </summary>
    class AsymmetryAnalysis
    {
        StrideDiagnostics strideDiagnostics;
        public delegate void AsymmetryEventHandler(StrideDiagnostics diagnostics);
        public static event AsymmetryEventHandler DataAvailable;
        double[] time = new double[8];//storage for the 8 values needed for symmetry metrics
        int strideElement = 0;
        double strideStop = 0;
        double strideStart = 0;//count to ensure elements of stride are recieved in the correct order
        PeakAnalyser peaks;
        int sessionStart = 0;
        bool enabled = false;

        public AsymmetryAnalysis()
        {
            strideDiagnostics = new StrideDiagnostics();
            peaks = new PeakAnalyser();
            PeakAnalyser.FootPeakDetected += new PeakAnalyser.PeakEventHandler(GaitProcessor_PeakDetected);
        }

        

        private void GaitProcessor_PeakDetected(string foot, int direction, Marker footMarker)
        {
            if (enabled == true)
            {
                generateMetrics(foot, direction, footMarker);
            }
        }

        private void generateMetrics(string foot, int direction, Marker footMarker)
        {
            switch (strideElement)
            {
                case 0:
                    if ((foot == "left") && (direction == 1)) //Left toe off
                    {
                        time[0] = footMarker.TimeStamp;
                        strideStart = footMarker.zCoordinate;
                        strideElement = 1;
                    }
                    break;
                case 1:
                    strideElement = 0; //stride data frame is reset if stride stages are detected out of sequence
                    if ((foot == "left") && (direction == -1)) //left heel down
                    {
                        time[1] = footMarker.TimeStamp;
                        strideElement = 2;
                    }
                    break;
                case 2:
                    strideElement = 0;
                    if ((foot == "right") && (direction == 1)) //right toe off
                    {
                        time[2] = footMarker.TimeStamp;
                        strideElement = 3;
                    }
                    break;
                case 3:
                    strideElement = 0;

                    if ((foot == "right") && (direction == -1)) //right heel down
                    {
                        time[3] = footMarker.TimeStamp;
                        strideElement = 4;
                    }
                    break;
                case 4:
                    strideElement = 0;
                    if ((foot == "left") && (direction == 1)) //left toe off etc...
                    {
                        time[4] = footMarker.TimeStamp;
                        strideElement = 5;
                    }
                    break;
                case 5:
                    strideElement = 0;

                    if ((foot == "left") && (direction == -1))
                    {
                        time[5] = footMarker.TimeStamp;
                        strideElement = 6;
                    }
                    break;
                case 6:
                    strideElement = 0;
                    if ((foot == "right") && (direction == 1))
                    {
                        time[6] = footMarker.TimeStamp;
                        strideElement = 7;
                    }
                    break;
                case 7:
                    strideElement = 0;
                    if ((foot == "right") && (direction == -1))
                    {
                        time[7] = footMarker.TimeStamp;
                        strideStop = footMarker.zCoordinate;
                        //now have full set of data for 2 strides for symmetry calculations
                        //do calculations and fire an event
                        symmetryCalculation();
                    }
                    break;
            }
        }


        private void symmetryCalculation()//calculate mean for values of interest and save individual values to file
        {
            if (sessionStart != 0)
            {

                strideDiagnostics.Swing_Stance_Ratio_LEFT = (time[1] - time[0]) / (time[4] - time[1]);
                strideDiagnostics.Swing_Stance_Ratio_RIGHT = (time[3] - time[2]) / (time[6] - time[3]);
                strideDiagnostics.Cycle_Duration_LEFT = time[5] - time[1];
                strideDiagnostics.Cycle_Duration_RIGHT = time[7] - time[3];
                strideDiagnostics.Step_Interval_Duration_LEFT = time[3] - time[1];
                strideDiagnostics.Step_Interval_Duration_RIGHT = time[5] - time[3];
                strideDiagnostics.Double_Support_Duration_LEFT = time[2] - time[1];
                strideDiagnostics.Double_Support_Duration_RIGHT = time[4] - time[3];
                strideDiagnostics.Stride_Length = (strideStop - strideStart) / 2;
                strideDiagnostics.Stride_Period = (time[7] - time[0]) / 2;

                strideDiagnostics.Swing_Stance_SI = (strideDiagnostics.Swing_Stance_Ratio_RIGHT - strideDiagnostics.Swing_Stance_Ratio_LEFT) / (strideDiagnostics.Swing_Stance_Ratio_RIGHT + strideDiagnostics.Swing_Stance_Ratio_LEFT) * 100;
                strideDiagnostics.Cycle_Duration_SI = (strideDiagnostics.Cycle_Duration_RIGHT - strideDiagnostics.Cycle_Duration_LEFT) / (strideDiagnostics.Cycle_Duration_RIGHT + strideDiagnostics.Cycle_Duration_LEFT) * 100;
                strideDiagnostics.Step_Interval_SI = (strideDiagnostics.Step_Interval_Duration_RIGHT - strideDiagnostics.Step_Interval_Duration_LEFT) / (strideDiagnostics.Step_Interval_Duration_RIGHT + strideDiagnostics.Step_Interval_Duration_LEFT) * 100;
                strideDiagnostics.Double_Support_SI = (strideDiagnostics.Double_Support_Duration_RIGHT - strideDiagnostics.Double_Support_Duration_LEFT) / (strideDiagnostics.Double_Support_Duration_RIGHT + strideDiagnostics.Double_Support_Duration_LEFT) * 100;

                if (DataAvailable != null)//ie. check that the event is being subscribed to
                {
                    DataAvailable(strideDiagnostics);
                }
            }
            else
            {
                sessionStart = 1;
            }
        }
        /// <summary>
        /// Enables asymmetry analysis
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }


    }
}

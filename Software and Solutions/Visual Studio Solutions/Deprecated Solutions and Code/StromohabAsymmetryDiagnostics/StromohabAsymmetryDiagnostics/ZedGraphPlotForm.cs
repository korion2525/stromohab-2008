using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ZedGraph;
using Stromohab_MCE;
using Stromohab_MCE_Connection;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;



namespace Stromohab_Diagnostics
{
    /// <summary>
    /// Class ZedGraphPlotForm : Form draws two ZedGraph controls,
    /// one each for left and right feet
    /// </summary>
    class ZedGraphPlotForm : Form
    {
        private ZedGraphControl zedGraphControl1;
        private ZedGraphControl zedGraphControl2;
        private System.ComponentModel.IContainer components;
        
        //List to contain point-pairs for plotting x, y, z coordinates for both feet
        PointPairList xDataL;
        PointPairList yDataL;
        PointPairList zDataL;
        PointPairList xDataR;
        PointPairList yDataR;
        PointPairList zDataR;
        LineItem curveLeft;
        LineItem curveRight;
        double timeStart, time;
        int sampleCount = 0;
        Marker leftFootMarker;
        Marker rightFootMarker;
        
        
        private bool continuePlotting = true;
        delegate void endPlottingCallback();

        public ZedGraphPlotForm()
        {
            InitializeComponent();
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.Size = new Size(ClientRectangle.Width, (int)((ClientRectangle.Height)/2)-5);
            zedGraphControl2.Location = new Point(0, (int)((ClientRectangle.Height) / 2) + 5);
            zedGraphControl2.Size = new Size(ClientRectangle.Width, (int)((ClientRectangle.Height) / 2) - 5);
            this.Visible = true;
        }


        private void CreateGraph()
        {
            GraphPane myPaneLeft = zedGraphControl1.GraphPane;
            GraphPane myPaneRight = zedGraphControl2.GraphPane;

            myPaneLeft.Title.Text = "Plot of Left Foot Motion.\n";
            myPaneLeft.XAxis.Title.Text = "Time (seconds)";
            myPaneLeft.YAxis.Title.Text ="Left foot motion (mm)";

            myPaneRight.Title.Text = "Plot of Right Foot Motion.\n";
            myPaneRight.XAxis.Title.Text = "Time (seconds)";
            myPaneRight.YAxis.Title.Text = "Right foot motion (mm)";

            //lists to save points as they come from the MCE
            xDataL = new PointPairList();
            yDataL = new PointPairList();
            zDataL = new PointPairList();
            xDataR = new PointPairList();
            yDataR = new PointPairList();
            zDataR = new PointPairList();
            
            // Initially, a curve is added with no data points (list is empty)
            // Color is blue, and there will be no symbols
            // 2nd line graph (red) added to show peaks
            curveLeft = myPaneLeft.AddCurve("z-position", zDataL, Color.Blue, SymbolType.None);
            curveLeft = myPaneLeft.AddCurve("y-position", yDataL, Color.Red, SymbolType.None);
            curveLeft = myPaneLeft.AddCurve("x-position", xDataL, Color.Green, SymbolType.None);
            curveRight = myPaneRight.AddCurve("z-position", zDataR, Color.Blue, SymbolType.None);
            curveRight = myPaneRight.AddCurve("y-position", yDataR, Color.Red, SymbolType.None);
            curveRight = myPaneRight.AddCurve("x-position", xDataR, Color.Green, SymbolType.None);
            //curve = myPane.AddCurve("Peak", peakData, Color.Red, SymbolType.Plus);

            // Just manually control the X axis range so it scrolls continuously
            // instead of discrete step-sized jumps
            myPaneLeft.XAxis.Scale.Min = 0;
            myPaneLeft.XAxis.Scale.Max = 6.0;
            myPaneLeft.YAxis.Scale.Min = -800; //if needed, fix Y-axis to a max and min 
            myPaneLeft.YAxis.Scale.Max = 500;
            myPaneLeft.XAxis.Scale.MinorStep = 0.2;
            myPaneLeft.XAxis.Scale.MajorStep = 1.0;
            myPaneRight.XAxis.Scale.Min = 0;
            myPaneRight.XAxis.Scale.Max = 6.0;
            myPaneRight.YAxis.Scale.Min = -800; //if needed, fix Y-axis to a max and min 
            myPaneRight.YAxis.Scale.Max = 500;
            myPaneRight.XAxis.Scale.MinorStep = 0.2;
            myPaneRight.XAxis.Scale.MajorStep = 1.0;

            // Scale the axes
            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.BackColor = System.Drawing.SystemColors.Control;
            this.zedGraphControl1.Location = new System.Drawing.Point(5, 8);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(255, 219);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Location = new System.Drawing.Point(65, 258);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0;
            this.zedGraphControl2.ScrollMaxX = 0;
            this.zedGraphControl2.ScrollMaxY = 0;
            this.zedGraphControl2.ScrollMaxY2 = 0;
            this.zedGraphControl2.ScrollMinX = 0;
            this.zedGraphControl2.ScrollMinY = 0;
            this.zedGraphControl2.ScrollMinY2 = 0;
            this.zedGraphControl2.Size = new System.Drawing.Size(144, 88);
            this.zedGraphControl2.TabIndex = 1;
            // 
            // ZedGraphPlotForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 375);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "ZedGraphPlotForm";
            this.Load += new System.EventHandler(this.ZedGraphPlotForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZedGraphPlotForm_FormClosing);
            this.Resize += new System.EventHandler(this.ZedGraphPlotForm_Resize);
            this.ResumeLayout(false);

        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            //Assign the appropriate MarkerID for left and right feet
            //Marker_Bin returns the left- or right-most Marker for LeftFoot/RightFoot properties 
            leftFootMarker = Marker_Bin.LeftFoot;
            rightFootMarker = Marker_Bin.RightFoot;
            if ((leftFootMarker != null) && (rightFootMarker != null))
            {
                if (MarkerList.listOfMarkers.Count > 0)
                {

                    if (sampleCount == 0)
                    {
                        //timeStart = MarkerList.listOfMarkers[footMarker].TimeStamp;
                        timeStart = leftFootMarker.TimeStamp;
                        sampleCount++;
                    }
                    time = (leftFootMarker.TimeStamp - timeStart) / 1000.0; //Time is measured in seconds

                    if (continuePlotting == false)
                    {
                        endPlotting();
                    }
                    else
                    {
                        zDataL.Add(time, leftFootMarker.zCoordinate);
                        yDataL.Add(time, leftFootMarker.yCoordinate);
                        xDataL.Add(time, leftFootMarker.xCoordinate);
                        zDataR.Add(time, rightFootMarker.zCoordinate);
                        yDataR.Add(time, rightFootMarker.yCoordinate);
                        xDataR.Add(time, rightFootMarker.xCoordinate);

                        // Keep the X scale at a rolling 30 second interval, with one
                        // major step between the max X value and the end of the axis
                        Scale xScaleLeft = zedGraphControl1.GraphPane.XAxis.Scale;
                        Scale xScaleRight = zedGraphControl2.GraphPane.XAxis.Scale;
                        if (time > xScaleLeft.Max - xScaleLeft.MajorStep)
                        {
                            xScaleLeft.Max = time + xScaleLeft.MajorStep;
                            xScaleLeft.Min = xScaleLeft.Max - 6.0;
                        }
                        if (time > xScaleRight.Max - xScaleRight.MajorStep)
                        {
                            xScaleRight.Max = time + xScaleRight.MajorStep;
                            xScaleRight.Min = xScaleRight.Max - 6.0;
                        }
                        // Force a redraw
                        zedGraphControl1.Invalidate();
                        zedGraphControl2.Invalidate();
                    }
                }
            }
        }

        //once active session is ended allow entire plot to be examined i.e add scroll bar etc
        private void endPlotting()
        {
            TCPProcessor.WholeFrameReceivedEvent -=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
            if (zedGraphControl1.InvokeRequired)
            {
                endPlottingCallback d = new endPlottingCallback(endPlotting);
                Invoke(d);
            }
            else
            {
                zedGraphControl1.IsShowHScrollBar = true;
                zedGraphControl1.ScrollMinX = 0;
                zedGraphControl1.ScrollMaxX = time;
                zedGraphControl1.IsAutoScrollRange = false;
                zedGraphControl1.IsEnableHPan = true;
                zedGraphControl1.IsEnableHZoom = true;
                zedGraphControl1.IsEnableVPan = false;
                zedGraphControl1.IsEnableVZoom = false;
            }
            if (zedGraphControl2.InvokeRequired)
            {
                endPlottingCallback d = new endPlottingCallback(endPlotting);
                Invoke(d);
            }
            else
            {
                zedGraphControl2.IsShowHScrollBar = true;
                zedGraphControl2.ScrollMinX = 0;
                zedGraphControl2.ScrollMaxX = time;
                zedGraphControl2.IsAutoScrollRange = false;
                zedGraphControl2.IsEnableHPan = true;
                zedGraphControl2.IsEnableHZoom = true;
                zedGraphControl2.IsEnableVPan = false;
                zedGraphControl2.IsEnableVZoom = false;
            }
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.Size = new Size(ClientRectangle.Width, (int)((ClientRectangle.Height) / 2) - 5);
            zedGraphControl2.Location = new Point(0, (int)((ClientRectangle.Height) / 2) + 5);
            zedGraphControl2.Size = new Size(ClientRectangle.Width, (int)((ClientRectangle.Height) / 2) - 5);
        }

        private void ZedGraphPlotForm_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void ZedGraphPlotForm_Load(object sender, EventArgs e)
        {
            CreateGraph();
            
            TCPProcessor.WholeFrameReceivedEvent +=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);

        }

        private void ZedGraphPlotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            continuePlotting = false;
        }
        /// <summary>
        /// Enables, disables plotting.
        /// Plot form and ZedGraph controls remain active
        /// </summary>
        public bool ContinuePlotting
        {
            get
            {
                return continuePlotting;
            }
            set
            {
                continuePlotting = value;
            }

        }
    
    }
}

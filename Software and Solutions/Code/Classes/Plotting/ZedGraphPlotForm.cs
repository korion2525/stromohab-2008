using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using Stromohab_MCE;
using Stromohab_MCE_Connection;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ScrollingGraphTest
{
    class ZedGraphPlotForm : Form
    {
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.ComponentModel.IContainer components;
        Form plotWindow;
        //List to contain point-pairs for plotting x, y, z coordinates for both feet
        PointPairList xData;
        PointPairList yData;
        PointPairList zData;
        LineItem curves;
        int footMarker;
        string foot;
        private bool continuePlotting;

        public ZedGraphPlotForm(int footMarkerID, string footID)
        {
            footMarker = footMarkerID;
            foot = footID;
            plotWindow = new Form();
            InitializeComponent();
            TCPProcessor.WholeFrameReceivedEvent +=
                    new TCPProcessor.WholeFrameReceivedHandler(TCPProcessor_WholeFrameReceivedEvent);
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.Size = new Size(ClientRectangle.Width, ClientRectangle.Height);
        }




        private void CreateGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Plot of " + foot + " foot motion.\n";
            myPane.XAxis.Title.Text = "Time (seconds)";
            myPane.YAxis.Title.Text =foot + "foot motion (mm)";

            //lists to save points as they come from the MCE
            xData = new PointPairList();
            yData = new PointPairList();
            zData = new PointPairList();
            
            // Initially, a curve is added with no data points (list is empty)
            // Color is blue, and there will be no symbols
            // 2nd line graph (red) added to show peaks
            curve = myPane.AddCurve("z-position", zData, Color.Blue, SymbolType.None);
            curve = myPane.AddCurve("y-position", yData, Color.Red, SymbolType.None);
            curve = myPane.AddCurve("x-position", yData, Color.Green, SymbolType.None);
            //curve = myPane.AddCurve("Peak", peakData, Color.Red, SymbolType.Plus);

            // Just manually control the X axis range so it scrolls continuously
            // instead of discrete step-sized jumps
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 6.0;
            myPane.YAxis.Scale.Min = -700; //if needed, fix Y-axis to a max and min 
            myPane.YAxis.Scale.Max = 400;
            myPane.XAxis.Scale.MinorStep = 0.2;
            myPane.XAxis.Scale.MajorStep = 1.0;

            // Scale the axes
            zedGraphControl1.AxisChange();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
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
            // ZedGraphPlotForm
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "ZedGraphPlotForm";
            this.Load += new System.EventHandler(this.ZedGraphPlotForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZedGraphPlotForm_FormClosing);
            this.Resize += new System.EventHandler(this.ZedGraphPlotForm_Resize);
            this.ResumeLayout(false);

        }

        private void TCPProcessor_WholeFrameReceivedEvent()
        {
            if (MarkerList.listOfMarkers.Count != 0)
            {
                if (sampleCount == 0)
                {
                    timeStart = MarkerList.listOfMarkers[footMarker].TimeStamp;
                    sampleCount++;
                }
                time = (MarkerList.listOfMarkers[footMarker].TimeStamp - timeStart) / 1000.0; //Time is measured in seconds

                if (!continuePlotting) 
                {
                    endPlotting();
                }
                else
                {
                    zData.Add(time, MarkerList.listOfMarkers[footMarker].zCoordinate);
                    yData.Add(time, MarkerList.listOfMarkers[footMarker].yCoordinate);
                    xData.Add(time, MarkerList.listOfMarkers[footMarker].xCoordinate);

                    // Keep the X scale at a rolling 30 second interval, with one
                    // major step between the max X value and the end of the axis
                    Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
                    if (time > xScale.Max - xScale.MajorStep)
                    {
                        xScale.Max = time + xScale.MajorStep;
                        xScale.Min = xScale.Max - 6.0;
                    }

                    // Make sure the Y axis is rescaled to accommodate actual data
                    zedGraphControl1.AxisChange();
                    // Force a redraw
                    zedGraphControl1.Invalidate();
                }
            }
        }


        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.Size = new Size(ClientRectangle.Width, ClientRectangle.Height);
        }

        private void ZedGraphPlotForm_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void ZedGraphPlotForm_Load(object sender, EventArgs e)
        {
            CreateGraph();
        }

        private void ZedGraphPlotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

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

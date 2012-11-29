using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using StroMoHab_Objects.Communication;
using StroMoHab_Objects.Objects;

namespace ZedGraphDiagnosticsControl
{
    public partial class DiagnosticsControlZedGraph : UserControl
    {
        List<Marker> _filteredMarkerList;
        double _graphTickStart;
        private Timer _graphUpdateTimer;
        System.Diagnostics.Stopwatch _timer;

        public DiagnosticsControlZedGraph()
        {
            InitializeComponent();
            if (!TCPProcessor.ConnectedToServer)
            {
                TCPProcessor.ManagedConnectToServer();
            }

            TCPProcessor.FilteredMarkerListReceivedEvent += new TCPProcessor.FilteredMarkerListReceivedHandler(TCPProcessor_FilteredMarkerListReceivedEvent);
            _graphUpdateTimer = new Timer();
            _graphUpdateTimer.Interval = 50;
            _graphUpdateTimer.Tick += new EventHandler(_graphUpdateTimer_Tick);

            InitialiseGraphs();

            _graphTickStart = Environment.TickCount;
            _graphUpdateTimer.Start();

             _timer = new System.Diagnostics.Stopwatch();
            _timer.Start();

            // System.Diagnostics.Debugger.Launch();

            checkBoxPauseSession.Checked = true;

        }
        bool _updatingGraph = false;
        double _maxStrideLength = 0;

        private void _graphUpdateTimer_Tick(object sender, EventArgs e)
        {
            _updatingGraph = true;
            try
            {
                zedGraphControl1.MasterPane.AxisChange(this.CreateGraphics());
                zedGraphControl1.Invalidate();
            }
            catch (NullReferenceException ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception thrown by zedGraphControl1. Message: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception thrown by zedGraphControl1. Message: " + ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception thrown by zedGraphControl1. Message: " + ex.Message);
            }

            try
            {
                if (_filteredMarkerList != null)
                {
                    if (_filteredMarkerList.Count > 0)
                    {
                        labelLeftFootX.Text = "X: " + _filteredMarkerList[0].xCoordinate.ToString();
                        labelLeftFootY.Text = "Y: " + _filteredMarkerList[0].yCoordinate.ToString();
                        labelLeftFootZ.Text = "Z: " + _filteredMarkerList[0].zCoordinate.ToString();

                        if (_filteredMarkerList[0].zCoordinate > _maxStrideLength)
                        {
                            _maxStrideLength = _filteredMarkerList[0].zCoordinate;
                            labelStepLength.Text = "Stride Length: " + _filteredMarkerList[0].zCoordinate.ToString() + " mm";
                        }
                    }
                    if (_filteredMarkerList.Count > 1)
                    {
                        labelRightFootX.Text = "X: " + _filteredMarkerList[1].xCoordinate.ToString();
                        labelRightFootY.Text = "Y: " + _filteredMarkerList[1].yCoordinate.ToString();
                        labelRightFootZ.Text = "Z: " + _filteredMarkerList[1].zCoordinate.ToString();

                        try
                        {
                            labelSymmetry.Text = "Symmetry: " + String.Format("{0:0.00}", (_filteredMarkerList[0].zCoordinate / -_filteredMarkerList[1].zCoordinate));
                        }
                        catch (DivideByZeroException ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Symmetry div/zero exception swallowed. Message: " + ex.Message);
                        }
                        

                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception thrown by zedGraphControl1. Message: " + ex.Message);
            }
            _updatingGraph = false;
        }

        void TCPProcessor_FilteredMarkerListReceivedEvent(List<StroMoHab_Objects.Objects.Marker> filteredMarkerList)
        {
            TCPProcessor.FilteredMarkerListReceivedEvent -= new TCPProcessor.FilteredMarkerListReceivedHandler(TCPProcessor_FilteredMarkerListReceivedEvent);

            _filteredMarkerList = new List<Marker>();

            foreach (Marker currentMarker in filteredMarkerList)
            {
                Marker copyOfMarker = new Marker(currentMarker);
                _filteredMarkerList.Add(copyOfMarker);
            }


            //if (_waitForVisualiser.Enabled)
            //{
            //    return;
            //}
            if (!_updatingGraph)
            {

                //_filteredMarkerList = new List<Marker>(filteredMarkerList);

                if (_filteredMarkerList.Count > 0)
                {
                    if (_filteredMarkerList.Count > 1)
                    {
                        //Make sure index 0 is always the leftmost so can keep GUI consistent.
                        if (_filteredMarkerList[0].xCoordinate < _filteredMarkerList[1].xCoordinate) //this feels backwards to me, but after 2 80+ hour weeks me brain can't figure out the logic. TODO: Investigate and resolve as necessary.
                        {
                            Marker tempMarker = new Marker(_filteredMarkerList[1]);
                            _filteredMarkerList[1] = new Marker(_filteredMarkerList[0]);
                            _filteredMarkerList[0] = new Marker(tempMarker);
                        }
                    }



                    #region ZedGraphControl1

                    if (zedGraphControl1.GraphPane != null)
                    {
                        if (zedGraphControl1.MasterPane.PaneList.Count > 0)
                        {
                            if (zedGraphControl1.MasterPane.PaneList[0] != null)
                            {
                                PopulatePositionGraph(zedGraphControl1.MasterPane.PaneList[0], 0);
                            }
                        }
                        if (zedGraphControl1.MasterPane.PaneList.Count > 1)
                        {
                            if (zedGraphControl1.MasterPane.PaneList[1] != null)
                            {
                                PopulatePositionGraph(zedGraphControl1.MasterPane.PaneList[1], 1);
                            }
                        }
                        if (zedGraphControl1.MasterPane.PaneList.Count > 2)
                        {
                            if (zedGraphControl1.MasterPane.PaneList[2] != null)
                            {
                                PopulateVelocityGraph(zedGraphControl1.MasterPane.PaneList[2], 2);
                            }
                        }
                        if (zedGraphControl1.MasterPane.PaneList.Count > 3)
                        {
                            if (zedGraphControl1.MasterPane.PaneList[3] != null)
                            {
                                PopulateSymmetryGraph(zedGraphControl1.MasterPane.PaneList[3]);
                            }
                        }

                        if (zedGraphControl1.MasterPane.PaneList.Count > 4)
                        {
                            if (zedGraphControl1.MasterPane.PaneList[4] != null)
                            {
                                PopulateVelocityGraph(zedGraphControl1.MasterPane.PaneList[4], 4);
                            }
                        }
                    }


                    #endregion ZedGraphControl1


                }
            }
            TCPProcessor.FilteredMarkerListReceivedEvent += new TCPProcessor.FilteredMarkerListReceivedHandler(TCPProcessor_FilteredMarkerListReceivedEvent);
        }

        List<GraphDataDisplayControlForm> _graphDisplayControlList;
        private void InitialiseGraphs()
        {
            zedGraphControl1.MasterPane.PaneList.Clear();

            _graphDisplayControlList = new List<GraphDataDisplayControlForm>();

            for (int i = 0; i < 5; i++)
            {
                zedGraphControl1.MasterPane.PaneList.Add(new GraphPane());
                zedGraphControl1.MasterPane.PaneList[i].Tag = i.ToString();
                zedGraphControl1.MasterPane.PaneList[i].Border.IsVisible = false;
            }

            _graphDisplayControlList.Add(new GraphDataDisplayControlForm(false, false, false, false, true, false));
            _graphDisplayControlList.Add(new GraphDataDisplayControlForm(false, false, false, false, false, true));
            _graphDisplayControlList.Add(new GraphDataDisplayControlForm(false, false, false, false, true, false));
            _graphDisplayControlList.Add(new GraphDataDisplayControlForm(false, false, false, false, false, false));
            _graphDisplayControlList.Add(new GraphDataDisplayControlForm(false, false, false, false, false, true));

            InitialiseZedGraphPaneWithPositionData(zedGraphControl1.MasterPane.PaneList[0]);
            InitialiseZedGraphPaneWithPositionData(zedGraphControl1.MasterPane.PaneList[1]);

            InitialiseZedGraphPaneWithSpeedData(zedGraphControl1.MasterPane.PaneList[2]);

            InitialiseZedGraphPaneWithSwingSymmetryData(zedGraphControl1.MasterPane.PaneList[3]);

            InitialiseZedGraphPaneWithSpeedData(zedGraphControl1.MasterPane.PaneList[4]);



            zedGraphControl1.MasterPane.SetLayout(this.CreateGraphics(), PaneLayout.ExplicitCol23);
            zedGraphControl1.MasterPane.Border.IsVisible = false;



            zedGraphControl1.IsAntiAlias = false;
            zedGraphControl1.MasterPane.IsAntiAlias = false;


        }


        
        private void PopulatePositionGraph(GraphPane currentPane, int indexOfCurrentPane)
        {
            //TODO: Remove this terrible exception swallower after demo.
            try
            {
                if (currentPane.CurveList.Count > 0)
                {
                    IPointListEdit x0List, x1List, y0List, y1List, z0List, z1List;
                    //double time = (Environment.TickCount - _graphTickStart) / 1000.0;
                    double time;
                    if (_timer != null)
                    {
                        time = (_timer.ElapsedMilliseconds) / 1000.0;
                    }
                    else
                    {
                        time = 0;
                    }


                    //Get curve lists
                    if (_graphDisplayControlList[indexOfCurrentPane].DisplayMediolateral_Left)
                    {
                        LineItem x0Curve = currentPane.CurveList[0] as LineItem;
                        if (x0Curve != null)
                        {
                            x0List = x0Curve.Points as IPointListEdit;
                            x0List.Add(time, _filteredMarkerList[0].xCoordinate);
                        }
                    }

                    if (_graphDisplayControlList[indexOfCurrentPane].DisplayMediolateral_Right)
                    {
                        LineItem x1Curve = currentPane.CurveList[1] as LineItem;
                        if (x1Curve != null)
                        {
                            x1List = x1Curve.Points as IPointListEdit;
                            if (_filteredMarkerList.Count > 1)
                            {
                                x1List.Add(time, _filteredMarkerList[1].xCoordinate);
                            }
                        }
                    }

                    if (_graphDisplayControlList[indexOfCurrentPane].DisplayLongitudinal_Left)
                    {
                        LineItem y0Curve = currentPane.CurveList[2] as LineItem;
                        if (y0Curve != null)
                        {
                            y0List = y0Curve.Points as IPointListEdit;
                            y0List.Add(time, _filteredMarkerList[0].yCoordinate);
                        }
                    }
                    if (_graphDisplayControlList[indexOfCurrentPane].DisplayLongitudinal_Right)
                    {
                        LineItem y1Curve = currentPane.CurveList[3] as LineItem;
                        if (y1Curve != null)
                        {
                            y1List = y1Curve.Points as IPointListEdit;
                            if (_filteredMarkerList.Count > 1)
                            {
                                y1List.Add(time, _filteredMarkerList[1].yCoordinate);
                            }
                        }
                    }
                    if (_graphDisplayControlList[indexOfCurrentPane].DisplayAnteroposterior_Left)
                    {
                        LineItem z0Curve = currentPane.CurveList[4] as LineItem;
                        if (z0Curve != null)
                        {
                            z0List = z0Curve.Points as IPointListEdit;
                            z0List.Add(time, _filteredMarkerList[0].zCoordinate);
                        }
                    }
                    if (_graphDisplayControlList[indexOfCurrentPane].DisplayAnteroposterior_Right)
                    {
                        LineItem z1Curve = currentPane.CurveList[5] as LineItem;
                        if (z1Curve != null)
                        {
                            z1List = z1Curve.Points as IPointListEdit;
                            if (_filteredMarkerList.Count > 1)
                            {
                                z1List.Add(time, _filteredMarkerList[1].zCoordinate);
                            }
                        }
                    }

                    Scale xScale = currentPane.XAxis.Scale;
                    if (time > xScale.Max - xScale.MajorStep)
                    {
                        xScale.Max = time + xScale.MajorStep;
                        xScale.Min = xScale.Max - 30.0;
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("*** EXCEPTION SWALLOWED IN DIAGNOSTICSCONTROLZEDGRAPH ***");
            }

        }

        private void PopulateVelocityGraph(GraphPane currentPane, int indexOfCurrentPane)
        {
            if (currentPane.CurveList.Count > 0)
            {
                IPointListEdit leftFootList, rightFootList;
                //double time = (Environment.TickCount - _graphTickStart) / 1000.0;
                double time;
                if (_timer != null)
                {
                    time = (_timer.ElapsedMilliseconds) / 1000.0;
                }
                else
                {
                    time = 0;
                }
                if (indexOfCurrentPane == 2)
                {
                    LineItem leftFootCurve = currentPane.CurveList[0] as LineItem;
                    if (leftFootCurve != null)
                    {
                        leftFootList = leftFootCurve.Points as IPointListEdit;

                        leftFootList.Add(time, _filteredMarkerList[0].Speed);

                    }
                }
                if (indexOfCurrentPane == 4)
                {
                    LineItem rightFootCurve = currentPane.CurveList[1] as LineItem;
                    if (rightFootCurve != null)
                    {
                        rightFootList = rightFootCurve.Points as IPointListEdit;
                        if (_filteredMarkerList.Count > 1)
                        {
                            rightFootList.Add(time, _filteredMarkerList[1].Speed);
                        }
                    }
                }
                Scale xScale = currentPane.XAxis.Scale;
                if (time > xScale.Max - xScale.MajorStep)
                {
                    xScale.Max = time + xScale.MajorStep;
                    xScale.Min = xScale.Max - 30.0;
                }
            }
        }

        private void PopulateSymmetryGraph(GraphPane currentPane)
        {
            if (currentPane.CurveList.Count > 0)
            {
                if (_filteredMarkerList.Count > 1)
                {
                    IPointListEdit symmetryList;
                    //double time = (Environment.TickCount - _graphTickStart) / 1000.0;
                    double time;
                    if (_timer != null)
                    {
                        time = (_timer.ElapsedMilliseconds) / 1000.0;
                    }
                    else
                    {
                        time = 0;
                    }

                    LineItem symmetryCurve = currentPane.CurveList[0] as LineItem;
                    if (symmetryCurve != null)
                    {
                        symmetryList = symmetryCurve.Points as IPointListEdit;

                        double symmetryMetric = (_filteredMarkerList[0].zCoordinate)/((_filteredMarkerList[0].zCoordinate - _filteredMarkerList[1].zCoordinate) / (_filteredMarkerList[1].zCoordinate / _filteredMarkerList[0].zCoordinate));


                        symmetryList.Add(time, symmetryMetric);

                    }
                    Scale xScale = currentPane.XAxis.Scale;
                    if (time > xScale.Max - xScale.MajorStep)
                    {
                        xScale.Max = time + xScale.MajorStep;
                        xScale.Min = xScale.Max - 30.0;
                    }
                }
            }
        }

        private void ScaleGraphPaneProperties(GraphPane currentPane, double xMin, double xMax, double minorStep, double majorStep)
        {
            currentPane.XAxis.Scale.Min = xMin;
            currentPane.XAxis.Scale.Max = xMax;
            currentPane.XAxis.Scale.MinorStep = minorStep;
            currentPane.XAxis.Scale.MajorStep = majorStep;
            currentPane.AxisChange();

            currentPane.XAxis.MinorTic.IsAllTics = false;
            currentPane.YAxis.MinorTic.IsAllTics = false;

            currentPane.Title.FontSpec.Size = 18;
            currentPane.Title.FontSpec.IsBold = true;
            currentPane.XAxis.Title.FontSpec.Size = 16;
            currentPane.YAxis.Title.FontSpec.Size = 16;
        }

        private void InitialiseZedGraphPaneWithSwingSymmetryData(GraphPane currentPane)
        {

            currentPane.Title.Text = "Swing Crossover Symmetry";
            currentPane.XAxis.Title.Text = "Time (seconds)";
            currentPane.YAxis.Title.Text = "Symmetry";

            currentPane.CurveList.Clear();

            RollingPointPairList crossoverList = new RollingPointPairList(2000);
            LineItem crossoverCurve = currentPane.AddCurve("Swing Crossover Symmetry", crossoverList, Color.Sienna, SymbolType.None);
            SetLineThickness(crossoverCurve);

            ScaleGraphPaneProperties(currentPane, 0, 30, 1, 5);
        }

        private void SetLineThickness(LineItem currentCurve)
        {
            currentCurve.Line.Width += 1.5f;

            //currentCurve.Line.IsOptimizedDraw = true;
            //currentCurve.Symbol.Type = SymbolType.Circle;
        }

        private void InitialiseZedGraphPaneWithSpeedData(GraphPane currentPane)
        {
            currentPane.Title.Text = "Velocity";
            currentPane.XAxis.Title.Text = "Time (seconds)";
            currentPane.YAxis.Title.Text = "Velocity(m/s)";

            currentPane.CurveList.Clear();

            RollingPointPairList leftFootList = new RollingPointPairList(2000);
            RollingPointPairList rightFootList = new RollingPointPairList(2000);
            LineItem leftFootCurve = currentPane.AddCurve("Left Foot", leftFootList, Color.Red, SymbolType.None);
            LineItem rightFootCurve = currentPane.AddCurve("Right Foot", rightFootList, Color.Green, SymbolType.None);

            SetLineThickness(leftFootCurve);
            SetLineThickness(rightFootCurve);

            ScaleGraphPaneProperties(currentPane, 0, 30, 1, 5);
        }

        private void InitialiseZedGraphPaneWithPositionData(GraphPane currentPane)
        {
            currentPane.Title.Text = "Position";
            currentPane.XAxis.Title.Text = "Time (s)";
            currentPane.YAxis.Title.Text = "Position (mm)";

            currentPane.CurveList.Clear();


            RollingPointPairList x0List = new RollingPointPairList(2000);
            RollingPointPairList x1List = new RollingPointPairList(2000);
            LineItem x0Curve = currentPane.AddCurve("Left Foot X", x0List, Color.Red, SymbolType.None);
            LineItem x1Curve = currentPane.AddCurve("Right Foot X", x1List, Color.Green, SymbolType.None);

            RollingPointPairList y0List = new RollingPointPairList(2000);
            RollingPointPairList y1List = new RollingPointPairList(2000);
            LineItem y0Curve = currentPane.AddCurve("Left Foot Y", y0List, Color.Orange, SymbolType.None);
            LineItem y1Curve = currentPane.AddCurve("Right Foot Y", y1List, Color.Blue, SymbolType.None);

            RollingPointPairList z0List = new RollingPointPairList(2000);
            RollingPointPairList z1List = new RollingPointPairList(2000);
            LineItem z0Curve = currentPane.AddCurve("Left Foot Z", z0List, Color.YellowGreen, SymbolType.None);
            LineItem z1Curve = currentPane.AddCurve("Right Foot Z", z1List, Color.Purple, SymbolType.None);

            ScaleGraphPaneProperties(currentPane, 0, 30, 1, 5);
            SetLineThickness(x0Curve);
            SetLineThickness(x1Curve);
            SetLineThickness(y0Curve);
            SetLineThickness(y1Curve);
            SetLineThickness(z0Curve);
            SetLineThickness(z1Curve);

        }

        private void zedGraphControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PointF pointClicked = new PointF(e.X, e.Y);

            ZedGraphControl eventSender = (ZedGraphControl)sender;

            GraphPane paneClicked = eventSender.MasterPane.FindChartRect(pointClicked);

            if (paneClicked != null)
            {
                _graphDisplayControlList[Convert.ToInt32(paneClicked.Tag)].Show();
            }
        }

        private void checkBoxPauseSession_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPauseSession.Checked)
            {
                _graphUpdateTimer.Enabled = false;
                TCPProcessor.FilteredMarkerListReceivedEvent -= new TCPProcessor.FilteredMarkerListReceivedHandler(TCPProcessor_FilteredMarkerListReceivedEvent);
                _timer.Stop();
            }
            else
            {
                _graphUpdateTimer.Enabled = true;
                TCPProcessor.FilteredMarkerListReceivedEvent += new TCPProcessor.FilteredMarkerListReceivedHandler(TCPProcessor_FilteredMarkerListReceivedEvent);
                _timer.Start();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure you wish to end the session?", "End Session Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes))
            {
                this.Dispose();
            }
        }
    }
}

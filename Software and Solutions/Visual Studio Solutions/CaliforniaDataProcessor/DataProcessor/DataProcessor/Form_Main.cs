using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using StromoLight_Diagnostics;

namespace DataProcessor
{
    public partial class Form_Main : Form
    {
        const string PATH_TO_STORED_DATA = @"C:\Users\Public\Documents\StoredData\";
        const string PATH_TO_TRAJECTORIZED_DATA = @"C:\Users\Public\Documents\Stromohab\Trajectorized\";

        

//        const List<string> LOADABLE_FILENAMES = 

        private string PathToData
        {
            get
            {
                if (checkBoxUsePostProcessedData.Checked)
                {
                    return PATH_TO_TRAJECTORIZED_DATA;
                }
                else
                {
                    return PATH_TO_STORED_DATA;
                }
            }
        }


        public Form_Main()
        {
            InitializeComponent();
            LoadableFilenames.Initialise();
            PopulateComboboxSubjectToProcess();
        }

        private void comboBoxSubjectToProcess_DropDown(object sender, EventArgs e)
        {
            PopulateComboboxSubjectToProcess();
        }

        private void comboBoxSubjectToProcess_TextChanged(object sender, EventArgs e)
        {
             PopulateComboboxTestToProcess();
        }

        private void buttonUpdateTopGraph_Click(object sender, EventArgs e)
        {
            List<List<StromoLight_Diagnostics.PositionData>> dataFromFile = null;
            if ((comboBoxSubjectToProcess.Text != "< select a subject >") && (comboBoxTestToProcess.Text != "< select a file >"))
            {
                if ((dataFromFile = LoadData()) != null)
                {
                    UpdateGraph(zedGraphControl1, dataFromFile, SymbolType.XCross);

                    SingleFileProcessor sfp = new SingleFileProcessor();

                    labelLeftFootMeanAbsoluteAccuracy.Text = sfp.MeanAccuracy(dataFromFile[0]).ToString();
                    labelRightFootMeanAbsoluteAccuracy.Text = sfp.MeanAccuracy(dataFromFile[1]).ToString();
                    labelMeanAbsoluteValueDifference.Text = sfp.MeanAbsoluteValueDifference(dataFromFile[0], dataFromFile[1], 'X').ToString();

                    labelLeftFootStandardDeviation.Text = sfp.StandardDeviation(dataFromFile[0]).ToString();
                    labelRightFootStandardDeviation.Text = sfp.StandardDeviation(dataFromFile[1]).ToString();
                    labelDualStandardDeviation.Text = sfp.StandardDeviation(dataFromFile[0], dataFromFile[1]).ToString();

                    labelLeftFootSkewness.Text = sfp.Skewness(dataFromFile[0]).ToString();
                    labelRightFootSkewness.Text = sfp.Skewness(dataFromFile[1]).ToString();

                    labelLeftFootVariance.Text = sfp.Variance(dataFromFile[0]).ToString();
                    labelRightFootVariance.Text = sfp.Variance(dataFromFile[1]).ToString();

                }
                else
                {
                    MessageBox.Show("Cannot Load File");
                }



            }


        }

        private void buttonUpdateBottomGraph_Click(object sender, EventArgs e)
        {
            if ((comboBoxSubjectToProcess.Text != "< select a subject >") && (comboBoxTestToProcess.Text != "< select a file >"))
            {

                List<List<StromoLight_Diagnostics.PositionData>> dataFromFile = null;

                if ((dataFromFile = LoadData()) != null)
                {
                    if (checkBoxDrawHistogram.Checked)
                    {
                        SingleFileProcessor sfp = new SingleFileProcessor();
                        UpdateGraph(zedGraphControl2, sfp.Histogram(dataFromFile[0], 10000));
                    }
                    else
                    {
                        UpdateGraph(zedGraphControl2, dataFromFile, SymbolType.None);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Load File");
                }

                

                
 

            }
        }


        private void UpdateGraph(ZedGraphControl graphControl, List<List<StromoLight_Diagnostics.PositionData>> dataFromFile, SymbolType symbolType)
        {
            //Build the graph
            GraphPane pane1 = graphControl.GraphPane;

            //Restart
            pane1.CurveList.Clear();

            pane1.Title.Text = comboBoxTestToProcess.Text.Replace(".csv", "");
            pane1.XAxis.Title.Text = "Time (ms)";
            pane1.YAxis.Title.Text = "Position (mm)";

            double time, xPosition;
            PointPairList leftPositionList = new PointPairList();
            PointPairList rightPositionList = new PointPairList();

            foreach (StromoLight_Diagnostics.PositionData currentLeftData in dataFromFile[0])
            {
                time = currentLeftData.Time;
                xPosition = currentLeftData.XCoordinate;
                leftPositionList.Add(time, xPosition);
            }

            foreach (StromoLight_Diagnostics.PositionData currentRightData in dataFromFile[1])
            {
                time = currentRightData.Time;
                xPosition = currentRightData.XCoordinate;
                rightPositionList.Add(time, xPosition);
            }

            CurveItem leftCurveXPosition = pane1.AddCurve("Left X Position", leftPositionList, Color.Red, symbolType);
            CurveItem rightCurveXPosition = pane1.AddCurve("Right X Position", rightPositionList, Color.Green, symbolType);

            graphControl.AxisChange();
            graphControl.Invalidate();


        }

        private void UpdateGraph(ZedGraphControl graphControl, CenterSpace.Free.Histo histo)
        {
            //Build the graph
            GraphPane pane1 = graphControl.GraphPane;

            //Restart
            pane1.CurveList.Clear();

            pane1.Title.Text = comboBoxTestToProcess.Text.Replace(".csv", "");
            pane1.XAxis.Title.Text = "Bins";
            pane1.YAxis.Title.Text = "Count";
            //pane1.YAxis.Scale.Max = 3500;
            //pane1.XAxis.Scale.Min = -450;
            //pane1.XAxis.Scale.Max = 450;


            PointPairList histoList = new PointPairList();

            for (int i = 0; i < histo.Counts.Length; i++)
            {
                histoList.Add(histo.BinBoundaries[i],histo.Counts[i]);
            }
            
            BarItem histoBar = pane1.AddBar("Count",histoList,Color.Aqua);

            graphControl.AxisChange();
            graphControl.Invalidate();


        }

        private List<List<StromoLight_Diagnostics.PositionData>> LoadData()
        {
            FileLoader fileIO = new FileLoader();
            List<List<StromoLight_Diagnostics.PositionData>> dataFromFile = null;

            if ((comboBoxTestToProcess.Text.Contains("Data")) && (!comboBoxTestToProcess.Text.Contains("MarkerList")))
            {
                dataFromFile = fileIO.ReadFootDownDataFromCSVFile(PathToData+ comboBoxSubjectToProcess.Text + @"\" + comboBoxTestToProcess.Text);
            }
            if (comboBoxTestToProcess.Text.Contains("DeSwapped"))
            {
                dataFromFile = fileIO.ReadDeswappedPositionDataFromCSVFile(PathToData + comboBoxSubjectToProcess.Text + @"\" + comboBoxTestToProcess.Text);
            }
            if (comboBoxTestToProcess.Text.Contains("PostUnSwapped"))
            {
                dataFromFile = fileIO.ReadPostUnswappedDataFromCSVFile(PathToData + comboBoxSubjectToProcess.Text + @"\" + comboBoxTestToProcess.Text);
            }
            if (comboBoxTestToProcess.Text.Contains("FeetDown"))
            {
                dataFromFile = fileIO.ReadFeetDown_Accuracy(PathToData + comboBoxSubjectToProcess.Text + @"\" + comboBoxTestToProcess.Text);
            }


            return (dataFromFile);
        }

        private void AddFilesToComboboxTestToProcess(string fileNameContains)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathToData + comboBoxSubjectToProcess.Text);
            FileInfo[] fileList = dirInfo.GetFiles("*" + fileNameContains);

            foreach (FileInfo currentFile in fileList)
            {
                comboBoxTestToProcess.Items.Add(currentFile.Name);
            }

        }

        private void checkBoxUsePostProcessedData_CheckedChanged(object sender, EventArgs e)
        {
            PopulateComboboxTestToProcess();
        }

        private void PopulateComboboxTestToProcess()
        {
            if (comboBoxSubjectToProcess.Text != "< select a subject >")
            {
                comboBoxTestToProcess.Items.Clear();
                comboBoxTestToProcess.Items.Add("< select a file >");

                foreach (string currentFilename in LoadableFilenames.Filenames)
                {
                    AddFilesToComboboxTestToProcess(currentFilename);
                }

                comboBoxTestToProcess.Text = comboBoxTestToProcess.Items[0].ToString();
            }
        }

        private void PopulateComboboxSubjectToProcess()
        {
            comboBoxSubjectToProcess.Items.Clear();

            DirectoryInfo dirInfo = new DirectoryInfo(PathToData);
            DirectoryInfo[] allSubjectDirectories = dirInfo.GetDirectories();

            comboBoxSubjectToProcess.Items.Add("< select a subject >");
            foreach (DirectoryInfo currentDirectory in allSubjectDirectories)
            {
                comboBoxSubjectToProcess.Items.Add(currentDirectory.Name);
            }
            comboBoxSubjectToProcess.Text = comboBoxSubjectToProcess.Items[0].ToString();
        }

        private void buttonProcessAllData_Click(object sender, EventArgs e)
        {
            List<List<PositionData>> dataFromFile = new List<List<PositionData>>();
            
            List<double> allXPositionDataFromFiles = new List<double>(comboBoxSubjectToProcess.Items.Count);
            FileLoader fileIO = new FileLoader();


            DirectoryInfo dirInfo = new DirectoryInfo(PathToData);

            foreach (DirectoryInfo directory in dirInfo.GetDirectories())
            {   
                foreach(FileInfo currentFile in directory.GetFiles("Baseline_3*PostUnSwapped.csv"))
                {
                    dataFromFile =  fileIO.ReadPostUnswappedDataFromCSVFile(PATH_TO_TRAJECTORIZED_DATA + @"\" + directory.Name + @"\" + currentFile.Name);
                    
                    SingleFileProcessor sfp = new SingleFileProcessor();

                    for (int i = 0; i < 2; i++)
                    {
                        foreach (double currentXValue in sfp.XValues(dataFromFile[i]))
                        {
                            allXPositionDataFromFiles.Add(currentXValue);
                        }
                    }

                }

            }
            CenterSpace.Free.Histo histo = new CenterSpace.Free.Histo(1000, allXPositionDataFromFiles.ToArray());

            UpdateGraph(zedGraphControl2, histo);

            allXPositionDataFromFiles.Clear();

            foreach (DirectoryInfo directory in dirInfo.GetDirectories())
            {
                foreach (FileInfo currentFile in directory.GetFiles("Baseline_1*PostUnSwapped.csv"))
                {
                    dataFromFile = fileIO.ReadPostUnswappedDataFromCSVFile(PATH_TO_TRAJECTORIZED_DATA + @"\" + directory.Name + @"\" + currentFile.Name);

                    SingleFileProcessor sfp = new SingleFileProcessor();

                    for (int i = 0; i < 2; i++)
                    {
                        foreach (double currentXValue in sfp.XValues(dataFromFile[i]))
                        {
                            allXPositionDataFromFiles.Add(currentXValue);
                        }
                    }

                }

            }
            histo = new CenterSpace.Free.Histo(1000, allXPositionDataFromFiles.ToArray());

            UpdateGraph(zedGraphControl1, histo);

        }

        private void buttonOutputSpreadsheet_Click(object sender, EventArgs e)
        {
            Output output = new Output();

            //output.Save(PathToData);

            output.SaveData(PathToData);
            System.Diagnostics.Process.Start(@"\\APPC05\Users\Public\Documents\Stromohab\SortedSubjectData.csv");

            //System.Diagnostics.Process.Start(@"\\APPC05\Users\Public\Documents\Stromohab\ResultsFile.csv");
            this.Close();
        }



    }

}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace StromoLight_Diagnostics
{
    public partial class Form_LoadData : Form
    {
        List<List<PositionData>> markerPositionData;
        List<PersonData> personDataLoadedList;
        public Form_LoadData()
        {
            InitializeComponent();
        }

        private void btnLoadPath_Click(object sender, EventArgs e)
        {
            fbdLoadPath.ShowDialog();
            lblLoadPath.Text = fbdLoadPath.SelectedPath + @"\";
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            personDataLoadedList = new List<PersonData>();
            LoadPersonData();
        }

        private void LoadPersonData()
        {
            //List<PersonData> List = new List<PersonData>();
            try
            {
                FileStream fileStream = new FileStream(lblLoadPath.Text + "PersonData", FileMode.Open);
                if (fileStream.Length > 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    personDataLoadedList = (List<PersonData>)formatter.Deserialize(fileStream);
                }
                fileStream.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found. Check path and that data has been recorded");
            }
            foreach (PersonData participant in personDataLoadedList)
            {
                lbPersonID.Items.Add(participant.PersonID);
            }
        }

        private bool LoadMarkerPositionData(string filepath)
        {
            try
            {
                FileStream fileStream = new FileStream(filepath, FileMode.Open);
                if (fileStream.Length > 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    markerPositionData = (List<List<PositionData>>)formatter.Deserialize(fileStream);
                }
                fileStream.Close();
                return true;
            }
            catch (FileNotFoundException)
            {
                
                return false;
                //MessageBox.Show("File not found. Check path and that data has been recorded");
            }
        }

        private void btnUnSwap_Click(object sender, EventArgs e)
        {
            foreach (PersonData participant in personDataLoadedList)
            {
                foreach (string filename in participant.FileList)
                {
                    if (LoadMarkerPositionData(lblLoadPath.Text + filename + "_MarkerListPositionData"))
                    {
                        char[] separator = new char[] {'\\'};
                        string[] test = filename.Split(separator, 2);
                        UnSwapMarkersInList(participant.PersonID, test[1]);
                        ProcessFeetData(participant.PersonID, test[1]);
                    }
                    else
                    {
                        lbNotLoaded.Items.Add(filename);
                    }
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string ID;
            ID = lbPersonID.SelectedItem.ToString();
            string filename = lbFiles.SelectedItem.ToString();
            if (LoadMarkerPositionData(lblLoadPath.Text + filename + "_MarkerListPositionData"))
            {
                char[] separator = new char[] { '\\' };
                string[] test = filename.Split(separator, 2);
                
                UnSwapMarkersInList(ID, test[1]);
                ProcessFeetData(ID, test[1]);
            }
            else
            {
                lbNotLoaded.Items.Add(filename);
            }
            //LoadMarkerPositionData(lblLoadPath.Text + lbFiles.SelectedItem.ToString() + "_MarkerListPositionData");
            //LoadMarkerPositionData(@"\\Appc05\Users\Public\Documents\Stromohab\Baseline\Baseline_1_GM0_MarkerListPositionData");
            //UnSwapMarkersInList();
            //ProcessFeetData(ID);
        }

        private void UnSwapMarkersInList(string ID, string testname)
        {
            if (markerPositionData != null)
            {
                int c = 0;
                double[] dif3D;
                while (++c < markerPositionData.Count)
                {
                    dif3D = new double[4];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            double difX = markerPositionData[c - 1][i].XCoordinate - markerPositionData[c][j].XCoordinate;//find current sample nearest to previous sample0
                            double difY = markerPositionData[c - 1][i].YCoordinate - markerPositionData[c][j].YCoordinate;
                            double difZ = markerPositionData[c - 1][i].ZCoordinate - markerPositionData[c][j].ZCoordinate;
                            dif3D[i * 2 + j] = Math.Sqrt(Math.Pow(difX, 2) + Math.Pow(difY, 2) + Math.Pow(difZ, 2));
                        }
                    }
                    //if ((markerPositionData[c][0].Time > 80259456) && (markerPositionData[c][0].Time < 80260049))
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Time: " + markerPositionData[c][0].Time + "  dif3D: " + (int)(dif3D[0]+0.5) + "\t" + (int)(dif3D[1]+0.5) + "\t" + (int)(dif3D[2]+0.5) + "\t" + (int)(dif3D[3]+0.5));
                    //}
                    bool swap = true;
                    //if ((dif3D[0] < dif3D[1]) && (dif3D[0] < dif3D[2])) swap = false;
                    //if ((dif3D[0] < dif3D[1]) && (dif3D[3] < dif3D[2])) swap = false;
                    //if ((dif3D[3] < dif3D[1]) && (dif3D[3] < dif3D[2])) swap = false;
                    if ((dif3D[0] + dif3D[3]) < (dif3D[1] + dif3D[2])) swap = false;
                    foreach (string item in txtIgnoreSwaps.Lines)
                    {
                        try
                        {
                            if (Convert.ToDouble(item) == markerPositionData[c][0].Time) swap = false;
                        }
                        catch (FormatException)
                        {

                        }
                    }
                    if (swap == true)
                    {
                        PositionData tempSample = markerPositionData[c][1];
                        markerPositionData[c][1] = markerPositionData[c][0];
                        markerPositionData[c][0] = tempSample;
                    }
                }
                Environment.CurrentDirectory = lblLoadPath.Text;
                if (!Directory.Exists("Trajectorized")) Directory.CreateDirectory("Trajectorized");
                if (!Directory.Exists("Trajectorized\\" + ID)) Directory.CreateDirectory("Trajectorized\\" + ID);
                DataToFile("Trajectorized\\" + ID + "\\" + testname + "_PostUnSwapped.csv");
            }
        }

        private void DataToFile(string filepath)
        {
            int c = 0;
            lblWritingFile.Text = "Writing file: " + filepath;
            Environment.CurrentDirectory = lblLoadPath.Text;
            if (!File.Exists(filepath))
            {
                StreamWriter toFile = new StreamWriter(filepath, false);
                toFile.Write("Time,X0,Y0,Z0,X1,Y1,Z1\n");
                do
                {
                    toFile.Write(markerPositionData[c][0].Time + "," + markerPositionData[c][0].XCoordinate + "," + markerPositionData[c][0].YCoordinate + "," + markerPositionData[c][0].ZCoordinate + "," +
                        markerPositionData[c][1].XCoordinate + "," + markerPositionData[c][1].YCoordinate + "," + markerPositionData[c][1].ZCoordinate + "\n");
                } while (c++ < markerPositionData.Count - 1);
                toFile.Flush();
                toFile.Close();
            }
        }

        private void lbPersonID_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFiles.Items.Clear();
            foreach (string filename in personDataLoadedList[lbPersonID.SelectedIndex].FileList)
            {
                lbFiles.Items.Add(filename);
            }
            txtPersonDetails.Text = personDataLoadedList[lbPersonID.SelectedIndex].Age + personDataLoadedList[lbPersonID.SelectedIndex].Gender;
        }

        private void ProcessFeetData(string ID, string testID)
        {
            int[,] feetXData = ExtractFootData("X");
            int[,] feetYData = ExtractFootData("Y");
            int[,] averageXFeetData = MovingAverage(feetXData, (int)nudFilterPoints.Value);
            int[,] averageYFeetData = MovingAverage(feetYData, 3);
            //FileFeetData(ID + "\\" + "Averaged_X.csv", averageFeetData);
            string accuracyPath = ID + "\\" + testID + "_FeetDown_Accuracy.csv";
            FootDownAccuracy(averageXFeetData, averageYFeetData, accuracyPath);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //ProcessFeetData();
        }

        private void FootDownAccuracy(int[,] xData, int[,]yData, string filepath)
        {
            int c = 0;
            int leftDownY_threshold = yData[0, 1];
            int rightDownY_threshold = yData[0, 2];
            int median = 0;
            int xMean = 0;
            List<PositionData> left = new List<PositionData>();
            List<PositionData> right = new List<PositionData>();
            int i = xData.Length / 3;
            //bool leftLock = false;
            //bool rightLock = false;
            int leftDownStart = 0;
            int leftDownStop = 0;
            int rightDownStart = 0;
            int rightDownStop = 0;
            PositionData leftdown;
            PositionData rightdown;
            for (int j = 1; j < i; j++)
            {   //Left foot down detect
                if (yData[j, 1] < leftDownY_threshold+2)
                {
                    if (leftDownStart == 0)
                    {
                        leftDownStart = j;
                    }
                }
                else
                {
                    if (leftDownStart != 0)
                    {
                        if ((j - leftDownStart) < 14)//check if there's just a blip in the Y
                        {
                            leftDownStart = 0;
                        }
                        else
                        {
                            leftDownStop = j;
                            median = leftDownStart + (leftDownStop - leftDownStart) / 4;
                            if ((median > 0) && (median < (i - 1)))//check within bounds of array
                            {
                                xMean = (xData[(median - 1), 1] + xData[median, 1] + xData[(median + 1), 1]) / 3;
                                leftdown = new PositionData(0, xData[median, 0], xMean, yData[median, 1], 0);
                                left.Add(leftdown);
                            }
                            leftDownStart = 0;
                            leftDownStop = 0;
                            median = 0;
                            xMean = 0;
                        }
                    }
                }
                //Right foot down detect
                if (yData[j, 2] < rightDownY_threshold)
                {
                    if (rightDownStart == 0)
                    {
                        rightDownStart = j;
                    }
                }
                else
                {
                    if (rightDownStart != 0)
                    {
                        if ((j - rightDownStart) < 14)
                        {
                            rightDownStart = 0;
                        }
                        else
                        {
                            rightDownStop = j;
                            median = rightDownStart + (rightDownStop - rightDownStart) / 4;
                            if ((median > 0) && (median < (i - 1)))
                            {
                                xMean = (xData[median - 1, 2] + xData[median, 2] + xData[median + 1, 2]) / 3;
                                rightdown = new PositionData(0, xData[median, 0], xMean, yData[median, 1], 0);
                                right.Add(rightdown);
                            }
                            rightDownStart = 0;
                            rightDownStop = 0;
                            median = 0;
                            xMean = 0;
                        }
                    }
                }
            }
            Environment.CurrentDirectory = lblLoadPath.Text + "Trajectorized\\";
            lblWritingFile.Text = "Writing file: " + filepath;
            StreamWriter toFile = new StreamWriter(filepath, false);
            toFile.Write("Time, LeftDown_X, Time, RightDown_X\n");
            while ((c < left.Count) && (c < right.Count))
            {
                toFile.Write(left[c].Time + "," + left[c].XCoordinate + "," + right[c].Time + "," + right[c].XCoordinate + "\n");
                c++;
            }
            toFile.Flush();
            toFile.Close();
        }

        private void FileFeetData(string filepath, int[,] feetData)
        {
            Environment.CurrentDirectory = lblLoadPath.Text + "Trajectorized\\";
            //if (!File.Exists(filepath))
            {
                StreamWriter toFile = new StreamWriter(filepath, false);
                toFile.Write("Time, Left_X, Right_X\n");
                int i = feetData.Length / 3;
                for (int j = 0; j < i; j++)
                {
                    toFile.Write(feetData[j,0] + "," + feetData[j,1] + "," + feetData[j,2] + "\n");
                }
                toFile.Flush();
                toFile.Close();
            }
        }

        private int[,] MovingAverage(int[,] ordinateData, int n)//n = point number of moving average
        {
            int c = ordinateData.Length / 3;
            int[,] averagedData = new int[c, 3];
            for (int i = 0;i < c; i++)//move window along the array
            {
                int averageLeft = 0;
                int averageRight = 0;
                int m = n;
                for (int w = i + (1 - n) / 2; w < i + (n + 1) / 2; w++)//average the window values
                {
                    if ((w >= 0) && (w < c))//average will be 'truncated' at top and bottom of array
                    {
                        averageLeft += ordinateData[w, 1];
                        averageRight += ordinateData[w, 2];
                    }
                    else
                    {
                        m--;//allow for 'truncated' average at top and bottom of array
                    }
                }
                averagedData[i, 1] = averageLeft / m;
                averagedData[i, 2] = averageRight / m;
                averagedData[i, 0] = ordinateData[i, 0];
            }
            return averagedData;
        }

        private int[,] ExtractFootData(string ordinate)
        {
            int arrayLength = markerPositionData.Count;
            int[,] feetData = new int[arrayLength, 3];
            int c = 0;
            //int l = 0;
            //int r = 0;

            //work out which List<> index corresponds to which foot
            //use the first value in the list should be the start position on the treadmill, feet side-by-side
            int left = 0;
            int right = 0;
            if (markerPositionData[0][0].XCoordinate < markerPositionData[0][1].XCoordinate)
            {
                right = 1;
            }
            else
            {
                left = 1;
            }

            foreach (List<PositionData> markerList in markerPositionData)
            {
                switch (ordinate)
                {

                    case "X":
                        feetData[c, 1] = (int)markerList[left].XCoordinate;
                        feetData[c, 2] = (int)markerList[right].XCoordinate;
                        break;
                    case "Y":
                        feetData[c, 1] = (int)markerList[left].YCoordinate;
                        feetData[c, 2] = (int)markerList[right].YCoordinate;
                        break;
                    case "Z":
                        feetData[c, 1] = (int)markerList[left].ZCoordinate;
                        feetData[c, 2] = (int)markerList[right].ZCoordinate;
                        break;
                }
                feetData[c, 0] = markerList[left].Time;
                //l += feetData[c, 1];
                //r += feetData[c, 2];
                c++;
                //System.Diagnostics.Debug.WriteLine("Left average = " + l + "Right average = " + r);
            }
            return feetData;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using StromoLight_Diagnostics;

namespace DataProcessor
{
    public class FileLoader
    {
        public List<List<PositionData>> ReadDeswappedPositionDataFromCSVFile(string filePath)
        {
            List<List<PositionData>> fullDataList = new List<List<PositionData>>();

            try
            {
                using (StreamReader readFile = new StreamReader(filePath))
                {


                    List<PositionData> currentLeftData = new List<PositionData>();
                    List<PositionData> currentRightData = new List<PositionData>();

                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');

                        if (row[0].Contains("Time"))
                        {
                            line = readFile.ReadLine();
                            row = line.Split(',');
                        }

                        PositionData currentPositionLeftMarker = new PositionData();
                        PositionData currentPositionRightMarker = new PositionData();

                        currentPositionLeftMarker.MarkerID = 0;
                        currentPositionRightMarker.MarkerID = 1;

                        currentPositionLeftMarker.Time = Convert.ToInt32(row[0]);
                        currentPositionLeftMarker.XCoordinate = Convert.ToInt32(row[1]);
                        currentPositionLeftMarker.YCoordinate = Convert.ToInt32(row[2]);
                        currentPositionLeftMarker.ZCoordinate = Convert.ToInt32(row[3]);

                        currentPositionRightMarker.Time = Convert.ToInt32(row[0]);
                        currentPositionRightMarker.XCoordinate = Convert.ToInt32(row[5]);
                        currentPositionRightMarker.YCoordinate = Convert.ToInt32(row[6]);
                        currentPositionRightMarker.ZCoordinate = Convert.ToInt32(row[7]);

                        currentLeftData.Add(currentPositionLeftMarker);
                        currentRightData.Add(currentPositionRightMarker);
                    }

                    fullDataList.Add(currentLeftData);
                    fullDataList.Add(currentRightData);

                    return (fullDataList);
                }
            }
            catch (IOException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed To Open File");
                return null;
            }

        }

        public List<List<PositionData>> ReadFootDownDataFromCSVFile(string filePath)
        {
            List<List<PositionData>> fullDataList = new List<List<PositionData>>();

            try
            {
                using (StreamReader readFile = new StreamReader(filePath))
                {


                    List<PositionData> currentLeftData = new List<PositionData>();
                    List<PositionData> currentRightData = new List<PositionData>();

                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');

                        if (row[0].Contains("Time"))
                        {
                            line = readFile.ReadLine();
                            row = line.Split(',');
                        }
                        if (row[0] == "")
                        {
                            break;
                        }
                        else
                        {

                            PositionData currentPositionLeftMarker = new PositionData();
                            PositionData currentPositionRightMarker = new PositionData();

                            currentPositionLeftMarker.MarkerID = 0;
                            currentPositionRightMarker.MarkerID = 1;

                            if (row[0] != "")
                            {
                                currentPositionLeftMarker.Time = Convert.ToInt32(row[0]);
                                currentPositionLeftMarker.XCoordinate = Convert.ToInt32(row[1]);
                                currentPositionLeftMarker.YCoordinate = -9999.99;
                                currentPositionLeftMarker.ZCoordinate = -9999.99;

                                currentLeftData.Add(currentPositionLeftMarker);
                            }
                            if (row[3] != "")
                            {
                                currentPositionRightMarker.Time = Convert.ToInt32(row[3]);
                                currentPositionRightMarker.XCoordinate = Convert.ToInt32(row[4]);
                                currentPositionRightMarker.YCoordinate = -9999.99;
                                currentPositionRightMarker.ZCoordinate = -9999.99;

                                currentRightData.Add(currentPositionRightMarker);
                            }


                        }
                    }

                    fullDataList.Add(currentLeftData);
                    fullDataList.Add(currentRightData);

                    return (fullDataList);
                }
            }
            catch (IOException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed To Open File");
                return null;
            }

        }

        public List<List<PositionData>> ReadFeetDown_Accuracy(string filePath)
        {
            List<List<PositionData>> fullDataList = new List<List<PositionData>>();

            try
            {
                using (StreamReader readFile = new StreamReader(new FileStream(filePath,FileMode.Open, FileAccess.Read, FileShare.Read)))
                {


                    List<PositionData> currentLeftData = new List<PositionData>();
                    List<PositionData> currentRightData = new List<PositionData>();

                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');

                        if (row[0].Contains("Time"))
                        {
                            line = readFile.ReadLine();
                            //line = readFile.ReadLine();
                            //line = readFile.ReadLine();
                            row = line.Split(',');
                        }
                        if (row[0] == "")
                        {
                            break;
                        }
                        else
                        {

                            PositionData currentPositionLeftMarker = new PositionData();
                            PositionData currentPositionRightMarker = new PositionData();

                            currentPositionLeftMarker.MarkerID = 0;
                            currentPositionRightMarker.MarkerID = 1;

                            if (row[0] != "")
                            {
                                currentPositionLeftMarker.Time = Convert.ToInt32(row[0]);
                                currentPositionLeftMarker.XCoordinate = Convert.ToInt32(row[1]);
                                currentPositionLeftMarker.YCoordinate = -9999.99;
                                currentPositionLeftMarker.ZCoordinate = -9999.99;

                                currentLeftData.Add(currentPositionLeftMarker);
                            }
                            if (row[3] != "")
                            {
                                currentPositionRightMarker.Time = Convert.ToInt32(row[2]);
                                currentPositionRightMarker.XCoordinate = Convert.ToInt32(row[3]);
                                currentPositionRightMarker.YCoordinate = -9999.99;
                                currentPositionRightMarker.ZCoordinate = -9999.99;

                                currentRightData.Add(currentPositionRightMarker);
                            }


                        }
                    }

                    fullDataList.Add(currentLeftData);
                    fullDataList.Add(currentRightData);

                    return (fullDataList);
                }
            }
            catch (IOException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed To Open File");
                return null;
            }

        }

        internal List<List<PositionData>> ReadPostUnswappedDataFromCSVFile(string filePath)
        {
            List<List<PositionData>> fullDataList = new List<List<PositionData>>();
            try
            {
                using (StreamReader readFile = new StreamReader(filePath))
                {


                    List<PositionData> currentLeftData = new List<PositionData>();
                    List<PositionData> currentRightData = new List<PositionData>();

                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');

                        if (row[0].Contains("Time"))
                        {
                            line = readFile.ReadLine();
                            row = line.Split(',');
                        }

                        PositionData currentPositionLeftMarker = new PositionData();
                        PositionData currentPositionRightMarker = new PositionData();

                        currentPositionLeftMarker.MarkerID = 0;
                        currentPositionRightMarker.MarkerID = 1;

                        currentPositionLeftMarker.Time = Convert.ToInt32(row[0]);
                        currentPositionLeftMarker.XCoordinate = Convert.ToInt32(row[1]);
                        currentPositionLeftMarker.YCoordinate = Convert.ToInt32(row[2]);
                        currentPositionLeftMarker.ZCoordinate = Convert.ToInt32(row[3]);

                        currentPositionRightMarker.Time = Convert.ToInt32(row[0]);
                        currentPositionRightMarker.XCoordinate = Convert.ToInt32(row[4]);
                        currentPositionRightMarker.YCoordinate = Convert.ToInt32(row[5]);
                        currentPositionRightMarker.ZCoordinate = Convert.ToInt32(row[6]);

                        currentLeftData.Add(currentPositionLeftMarker);
                        currentRightData.Add(currentPositionRightMarker);
                    }

                    fullDataList.Add(currentLeftData);
                    fullDataList.Add(currentRightData);

                    return (fullDataList);
                }
            }
            catch (IOException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed To Open File");
                return null;
            }
        }
    }
}

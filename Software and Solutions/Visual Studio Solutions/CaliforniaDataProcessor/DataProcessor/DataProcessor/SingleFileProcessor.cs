using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StromoLight_Diagnostics;
using System.Collections;

namespace DataProcessor
{
    public class SingleFileProcessor
    {
        public double MeanAccuracy(List<StromoLight_Diagnostics.PositionData> testData)
        {
            double mean = 0;
            double sum = 0;

            foreach (StromoLight_Diagnostics.PositionData currentData in testData)
            {
                sum += currentData.XCoordinate;
            }

            mean = sum / (testData.Count);

            return (mean);

        }

        public double MeanAccuracy(List<double> dataList)
        {
            double sum = 0;

            foreach (double currentValue in dataList)
            {
                sum += currentValue;
            }

            return (sum / (dataList.Count));

        }

        public double MeanAbsoluteValueDifference(List<StromoLight_Diagnostics.PositionData> leftFootData, List<StromoLight_Diagnostics.PositionData> rightFootData, char coordinate)
        {

            int arrayLength;

            if (leftFootData.Count < rightFootData.Count)
            {
                arrayLength = leftFootData.Count;
            }
            else
            {
                arrayLength = rightFootData.Count;
            }

            List<double> absoluteValues = null;

            if (arrayLength > 0)
            {
                absoluteValues = new List<double>(arrayLength);

                switch (coordinate.ToString().ToUpper())
                {
                    case "X":
                        {
                            for (int i = 0; (i < leftFootData.Count && i < rightFootData.Count); i++)
                            {
                                absoluteValues.Add(leftFootData[i].XCoordinate - rightFootData[i].XCoordinate);
                            }
                            break;
                        }
                    case "Y":
                        {
                            for (int i = 0; (i < leftFootData.Count && i < rightFootData.Count); i++)
                            {
                                absoluteValues.Add(leftFootData[i].YCoordinate - rightFootData[i].YCoordinate);
                            }
                            break;
                        }
                    case "Z":
                        {
                            for (int i = 0; (i < leftFootData.Count && i < rightFootData.Count); i++)
                            {
                                absoluteValues.Add(leftFootData[i].ZCoordinate - rightFootData[i].ZCoordinate);
                            }
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Only X, Y or Z may be passed as a parameter to the function AbsoluteValueDifference.");
                        }
                }
            }

            return (Mean(absoluteValues));
        }

        private double Mean(List<double> dataList)
        {
            double mean = 0;
            double sum = 0;

            foreach (double currentValue in dataList)
            {
                sum += currentValue;
            }

            mean = (sum / dataList.Count);

            return (mean);
        }

        public double StandardDeviation(List<StromoLight_Diagnostics.PositionData> dataList)
        {
            dnAnalytics.Statistics.DescriptiveStatistics stats = new dnAnalytics.Statistics.DescriptiveStatistics(XValues(dataList));

            return (stats.StandardDeviation);

        }

        public double StandardDeviation(List<StromoLight_Diagnostics.PositionData> leftFootData, List<StromoLight_Diagnostics.PositionData> rightFootData)
        {
            List<StromoLight_Diagnostics.PositionData> concantinatedData = new List<StromoLight_Diagnostics.PositionData>();
            List<double> xValuesTogether = new List<double>();

            concantinatedData = leftFootData.Concat(rightFootData).ToList() ;

            return(this.StandardDeviation(concantinatedData));

        }

        public double StandardDeviation(List<double> dataList)
        {
            dnAnalytics.Statistics.DescriptiveStatistics stats = new dnAnalytics.Statistics.DescriptiveStatistics(dataList);

            return (stats.StandardDeviation);
        }


        public double Skewness(List<PositionData> dataList)
        {
            dnAnalytics.Statistics.DescriptiveStatistics stats = new dnAnalytics.Statistics.DescriptiveStatistics(XValues(dataList));

            return (stats.Skewness);

        }

        public double Variance(List<PositionData> dataList)
        {
            dnAnalytics.Statistics.DescriptiveStatistics stats = new dnAnalytics.Statistics.DescriptiveStatistics(XValues(dataList));

            return (stats.Variance);

        }


        public List<double> XValues(List<PositionData> dataList)
        {
            List<double> xValues = new List<double>();

            foreach (PositionData currentData in dataList)
            {
                xValues.Add(currentData.XCoordinate);
            }

            return (xValues);
        }

        public CenterSpace.Free.Histo Histogram(List<PositionData> dataList, int numberOfBins)
        {
            CenterSpace.Free.Histo histo = new CenterSpace.Free.Histo(numberOfBins, XValues(dataList).ToArray());

            
            return (histo);

        }

    }
}

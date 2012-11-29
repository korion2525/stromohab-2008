using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using StromoLight_Diagnostics;

namespace DataProcessor
{
    public class Output
    {
        private List<String> AllSubjectsList(string pathToData)
        {
            List<String> subjectsList = new List<String>();
            
            DirectoryInfo dirInfo = new DirectoryInfo(pathToData);
            DirectoryInfo[] allSubjectDirectories = dirInfo.GetDirectories();

            foreach (DirectoryInfo currentDirectory in allSubjectDirectories)
            {
                subjectsList.Add(currentDirectory.FullName);
            }

            return (subjectsList);

        }

        public void Save(string pathToData)
        {
            using (StreamWriter fileWriter = new StreamWriter(@"\\APPC05\Users\Public\Documents\Stromohab\ResultsFile.csv"))
            {
                fileWriter.WriteLine("Subject,Baseline_1_Inaccuracy,Vision_2_Inaccuracy,Sound_2_Inaccuracy,Baseline_3_Inaccuracy");

                SingleFileProcessor sfp = new SingleFileProcessor();

                List<double> baseline_1_Accuracy = new List<double>();
                List<double> vision_2_Accuracy = new List<double>();
                List<double> sound_2_Accuracy = new List<double>();
                List<double> baseline_3_Accuracy = new List<double>();

                List<double> baseline1VisionAccuracy_1 = new List<double>();
                List<double> baseline3VisionAccuracy_3 = new List<double>();

                List<double> baseline1SoundAccuracy_1 = new List<double>();
                List<double> baseline3SoundAccuracy_3 = new List<double>();


                foreach (string currentPath in this.AllSubjectsList(pathToData))
                {
                    FileLoader fileLoader = new FileLoader();
                    DirectoryInfo dirInfo = new DirectoryInfo(currentPath);
                    double baseline1Accuracy = -9999.99, vision2Accuracy = -9999.99, sound2Accuracy = -9999.99, baseline3Accuracy = -9999.99;
                    string fileName = null;

                    


                    foreach (FileInfo currentFile in dirInfo.GetFiles("Baseline_1*FeetDown_Accuracy.csv"))
                    {
                        baseline1Accuracy = sfp.StandardDeviation(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0], fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]);
                        fileName = currentFile.Directory.Name;

                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0]))
                        {
                            baseline_1_Accuracy.Add(currentValue);

                            FileInfo[] fileInfoVision = dirInfo.GetFiles();
                            FileInfo[] fileInfo2 = dirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv");

                            foreach (FileInfo currentFileA in fileInfoVision)
                            {

                                if (currentFileA.Name.Contains("Vision_2*FeetDown_Accuracy.csv"))
                                {
                                    baseline1VisionAccuracy_1.Add(currentValue);
                                }

                                if (dirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv") != null)
                                {
                                    baseline1SoundAccuracy_1.Add(currentValue);
                                }
                            }


                        }
                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]))
                        {
                            baseline_1_Accuracy.Add(currentValue);

                            if (dirInfo.GetFiles("Vision_2*FeetDown_Accuracy.csv") != null)
                            {
                                baseline1VisionAccuracy_1.Add(currentValue);
                            }
                            else
                            {
                                if (dirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv") != null)
                                {
                                    baseline1SoundAccuracy_1.Add(currentValue);
                                }
                            }
                        }

                    }

                    foreach (FileInfo currentFile in dirInfo.GetFiles("Vision_2*FeetDown_Accuracy.csv"))
                    {
                        vision2Accuracy = sfp.StandardDeviation(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0], fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]);
                        
                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0]))
                        {
                            vision_2_Accuracy.Add(currentValue);
                        }
                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]))
                        {
                            vision_2_Accuracy.Add(currentValue);
                        }
                    }

                    foreach (FileInfo currentFile in dirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv"))
                    {
                        sound2Accuracy = sfp.StandardDeviation(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0], fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]);

                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0]))
                        {
                            sound_2_Accuracy.Add(currentValue);
                        }
                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]))
                        {
                            sound_2_Accuracy.Add(currentValue);
                        }
                    }

                    foreach (FileInfo currentFile in dirInfo.GetFiles("Baseline_3*FeetDown_Accuracy.csv"))
                    {
                        baseline_3_Accuracy.Add(baseline3Accuracy = sfp.StandardDeviation(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0], fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]));

                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[0]))
                        {
                            baseline_3_Accuracy.Add(currentValue);

                            if (dirInfo.GetFiles("Vision_2*FeetDown_Accuracy.csv") != null)
                            {
                                baseline3VisionAccuracy_3.Add(currentValue);
                            }
                            else
                            {
                                if (dirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv") != null)
                                {
                                    baseline3SoundAccuracy_3.Add(currentValue);
                                }
                            }
                        }
                        foreach (double currentValue in sfp.XValues(fileLoader.ReadFeetDown_Accuracy(currentFile.FullName)[1]))
                        {
                            baseline_3_Accuracy.Add(currentValue);

                            if (dirInfo.GetFiles("Vision_2*FeetDown_Accuracy.csv") != null)
                            {
                                baseline3VisionAccuracy_3.Add(currentValue);
                            }
                            else
                            {
                                if (dirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv") != null)
                                {
                                    baseline3SoundAccuracy_3.Add(currentValue);
                                }
                            }
                        }
                    }




                    double sdBaseline1 = sfp.StandardDeviation(baseline_1_Accuracy);
                    double sdBaseline3 = sfp.StandardDeviation(baseline_1_Accuracy);
                    double sdVision = -9999.99;
                    double sdSound = -9999.99;
                    

                    if (vision2Accuracy !=  -9999.99)
                    {
                        fileWriter.Write("," + vision2Accuracy.ToString());

                        sdVision = sfp.StandardDeviation(vision_2_Accuracy);
                    }
                    else
                    {
                        fileWriter.Write(",");
                    }
                    if (sound2Accuracy != -9999.99)
                    {
                        fileWriter.Write("," + sound2Accuracy.ToString());

                        sdSound = sfp.StandardDeviation(sound_2_Accuracy);
                    }
                    else
                    {
                        fileWriter.Write(",");
                    }

                    fileWriter.Write("," + baseline3Accuracy.ToString() + "\n");

                }

                fileWriter.WriteLine();
                fileWriter.WriteLine();


                fileWriter.WriteLine("Baseline 1 Inaccuracy (all subjects): "    + "," +    sfp.StandardDeviation(baseline_1_Accuracy).ToString());
                fileWriter.WriteLine("Vision 2 Inaccuracy (all subjects): "      + "," +    sfp.StandardDeviation(vision_2_Accuracy).ToString());
                fileWriter.WriteLine("Sound 2 Inaccuracy (all subjects): "       + "," +    sfp.StandardDeviation(sound_2_Accuracy).ToString());
                fileWriter.WriteLine("Baseline 3 Inaccuracy (all subjects): "    + "," +    sfp.StandardDeviation(baseline_3_Accuracy).ToString());

                fileWriter.WriteLine();
                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline/Vision/Baseline: " + "," + sfp.StandardDeviation(baseline1VisionAccuracy_1).ToString() + "," + sfp.StandardDeviation(vision_2_Accuracy).ToString() + "," + sfp.StandardDeviation(baseline3VisionAccuracy_3).ToString());
                //fileWriter.WriteLine("Baseline/Sound/Baseline: " + "," + sfp.StandardDeviation(baseline1SoundAccuracy_1).ToString() + "," + sfp.StandardDeviation(sound_2_Accuracy).ToString() + "," + sfp.StandardDeviation(baseline3SoundAccuracy_3).ToString());

                fileWriter.WriteLine();
                fileWriter.WriteLine();


                for (int i = 0; i < baseline1VisionAccuracy_1.Count;i++)
                {
                    baseline1VisionAccuracy_1[i] = Math.Abs(baseline_1_Accuracy[i]);
                }

                for (int i=0;i<baseline1SoundAccuracy_1.Count;i++)
                {
                    baseline1SoundAccuracy_1[i] = Math.Abs(baseline1SoundAccuracy_1[i]);
                }

                for (int i = 0; i < baseline3VisionAccuracy_3.Count; i++)
                {
                    baseline3VisionAccuracy_3[i] = Math.Abs(baseline3VisionAccuracy_3[i]);
                }

                for (int i = 0; i < vision_2_Accuracy.Count; i++)
                {
                    vision_2_Accuracy[i] = Math.Abs(vision_2_Accuracy[i]);
                }

                for (int i = 0; i < sound_2_Accuracy.Count; i++)
                {
                    sound_2_Accuracy[i] = Math.Abs(sound_2_Accuracy[i]);
                }


                
                
                fileWriter.WriteLine("Baseline 1 Mean: " + "," + sfp.MeanAccuracy(baseline_1_Accuracy).ToString());
                fileWriter.WriteLine("Vision Mean: " + "," + sfp.MeanAccuracy(vision_2_Accuracy).ToString());
                fileWriter.WriteLine("Sound Mean: " + "," + sfp.MeanAccuracy(sound_2_Accuracy).ToString());
                fileWriter.WriteLine("Baseline 3 Mean: " + "," + sfp.MeanAccuracy(baseline_3_Accuracy).ToString());
                    
                fileWriter.WriteLine();
                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline1-Vision Mean: " + "," + sfp.MeanAccuracy(baseline1VisionAccuracy_1).ToString());
                fileWriter.WriteLine("Baseline1-Sound Mean: " + "," + sfp.MeanAccuracy(baseline1SoundAccuracy_1).ToString());
                fileWriter.WriteLine("Vision Mean: " + "," + sfp.MeanAccuracy(vision_2_Accuracy).ToString());
                fileWriter.WriteLine("Sound Mean: " + "," + sfp.MeanAccuracy(sound_2_Accuracy).ToString());
                fileWriter.WriteLine("Vision-Baseline3 Mean: " + "," + sfp.MeanAccuracy(baseline3VisionAccuracy_3).ToString());
                fileWriter.WriteLine("Sound-Baseline3 Mean: " + "," + sfp.MeanAccuracy(baseline3SoundAccuracy_3).ToString());

                fileWriter.Flush();
                fileWriter.Close();
            }
        }

        public List<List<double>> DataLists(string pathToData)
        {
            #region Data for all subjects
            List<List<double>> loadedDataLists = new List<List<double>>(4);
            
            // Both feet together
            List<double> baseline_1_vision_data_BothFeet = new List<double>();
            List<double> baseline_1_sound_data_BothFeet = new List<double>();
            List<double> baseline_1_baseline_data_BothFeet = new List<double>();

            List<double> baseline_3_vision_data_BothFeet = new List<double>();
            List<double> baseline_3_sound_data_BothFeet = new List<double>();
            List<double> baseline_3_baseline_data_BothFeet = new List<double>();

            List<double> vision_2_data_BothFeet = new List<double>();
            List<double> sound_2_data_BothFeet = new List<double>();
            List<double> baseline_2_data_BothFeet = new List<double>();

            // Left and Right feet separately
            //   Test 1
            List<double> baseline_1_vision_data_Left = new List<double>();
            List<double> baseline_1_vision_data_Right = new List<double>();
            List<double> baseline_1_sound_data_Left = new List<double>();
            List<double> baseline_1_sound_data_Right = new List<double>();
            List<double> baseline_1_baseline_data_Left = new List<double>();
            List<double> baseline_1_baseline_data_Right = new List<double>();
            //   Test 3
            List<double> baseline_3_vision_data_Left = new List<double>();
            List<double> baseline_3_vision_data_Right = new List<double>();
            List<double> baseline_3_sound_data_Left = new List<double>();
            List<double> baseline_3_sound_data_Right = new List<double>();
            List<double> baseline_3_baseline_data_Left = new List<double>();
            List<double> baseline_3_baseline_data_Right = new List<double>();
            //Test 2
            List<double> vision_2_data_Left = new List<double>();
            List<double> vision_2_data_Right = new List<double>();
            List<double> sound_2_data_Left = new List<double>();
            List<double> sound_2_data_Right = new List<double>();
            List<double> baseline_2_data_Left = new List<double>();
            List<double> baseline_2_data_Right = new List<double>();


            #endregion Data for all subjects

            DirectoryInfo dirInfo = new DirectoryInfo(pathToData);            

            FileInfo[] baseline_1_fileList = dirInfo.GetFiles("Baseline_1*_FeetDown_Accuracy.csv", SearchOption.AllDirectories);
            FileInfo[] baseline_2_fileList = dirInfo.GetFiles("Baseline_2*_FeetDown_Accuracy.csv", SearchOption.AllDirectories);
            FileInfo[] vision_2_fileList = dirInfo.GetFiles("Vision_2*_FeetDown_Accuracy.csv", SearchOption.AllDirectories);
            FileInfo[] sound_2_fileList = dirInfo.GetFiles("Sound_2*_FeetDown_Accuracy.csv", SearchOption.AllDirectories);
            FileInfo[] baseline_3_fileList = dirInfo.GetFiles("Baseline_3*_FeetDown_Accuracy.csv", SearchOption.AllDirectories);

            FileLoader fileLoader = new FileLoader();

            // BASELINE_1
            foreach (FileInfo currentFileInfo in baseline_1_fileList)
            {
                List<List<PositionData>> currentFilePositionData = fileLoader.ReadFeetDown_Accuracy(currentFileInfo.FullName);

                //For left and right markers
                for (int i = 0; i < 2; i++)
                {
                    SingleFileProcessor sfp = new SingleFileProcessor();

                    DirectoryInfo currentDirInfo = new DirectoryInfo(currentFileInfo.DirectoryName);
                    FileInfo[] currentDirectoryFileList = currentDirInfo.GetFiles("Vision_2*_FeetDown_Accuracy.csv");

                    if (currentDirectoryFileList.Length > 0)
                    {
                        foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                        {
                            //Both feet combined
                            baseline_1_vision_data_BothFeet.Add(currentValueToSave);

                            //Separate feet
                            if (i == 0)
                            {
                                baseline_1_vision_data_Left.Add(currentValueToSave);
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    baseline_1_vision_data_Right.Add(currentValueToSave);
                                }
                            }

                        }
                    }
                    else
                    {
                        currentDirectoryFileList = currentDirInfo.GetFiles("Sound_2*_FeetDown_Accuracy.csv");
                        if (currentDirectoryFileList.Length > 0)
                        {
                            foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                            {
                                //Both feet combined
                                baseline_1_sound_data_BothFeet.Add(currentValueToSave);

                                //Separate feet
                                if (i == 0)
                                {
                                    baseline_1_sound_data_Left.Add(currentValueToSave);
                                }
                                else
                                {
                                    if (i == 1)
                                    {
                                        baseline_1_sound_data_Right.Add(currentValueToSave);
                                    }
                                }

                            }
                        }
                        else
                        {
                            currentDirectoryFileList = currentDirInfo.GetFiles("Baseline_2*_FeetDown_Accuracy.csv");
                            if (currentDirectoryFileList.Length > 0)
                            {
                                foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                                {
                                    //Both feet combined
                                    baseline_1_baseline_data_BothFeet.Add(currentValueToSave);

                                    //Separate feet
                                    if (i == 0)
                                    {
                                        baseline_1_baseline_data_Left.Add(currentValueToSave);
                                    }
                                    else
                                    {
                                        if (i == 1)
                                        {
                                            baseline_1_baseline_data_Right.Add(currentValueToSave);
                                        }
                                    }
                                }
                            }
                        }
                    
                    }
                    
                
                }
            }

            // BASELINE_2
            foreach (FileInfo currentFileInfo in baseline_2_fileList)
            {
                List<List<PositionData>> currentFilePositionData = fileLoader.ReadFeetDown_Accuracy(currentFileInfo.FullName);

                //For left and right markers
                for (int i = 0; i < 2; i++)
                {
                    SingleFileProcessor sfp = new SingleFileProcessor();

                    foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                    {
                        //Both feet
                        baseline_2_data_BothFeet.Add(currentValueToSave);

                        //Separate feet
                        if (i == 0)
                        {
                            baseline_2_data_Left.Add(currentValueToSave);
                        }
                        else
                        {
                            if (i == 1)
                            {
                                baseline_2_data_Right.Add(currentValueToSave);
                            }
                        }
                    }

                }
            }

            // VISION_2
            foreach (FileInfo currentFileInfo in vision_2_fileList)
            {
                List<List<PositionData>> currentFilePositionData = fileLoader.ReadFeetDown_Accuracy(currentFileInfo.FullName);
                
                //For left and right markers
                for (int i = 0; i < 2; i++)
                {
                    SingleFileProcessor sfp = new SingleFileProcessor();

                    foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                    {
                       //Combined feet
                        vision_2_data_BothFeet.Add(currentValueToSave);

                        //Separate feet
                        if (i == 0)
                        {
                            vision_2_data_Left.Add(currentValueToSave);
                        }
                        else
                        {
                            if (i == 1)
                            {
                                vision_2_data_Right.Add(currentValueToSave);
                            }
                        }
                    }

                }
            }

            // SOUND_2
            foreach (FileInfo currentFileInfo in sound_2_fileList)
            {
                List<List<PositionData>> currentFilePositionData = fileLoader.ReadFeetDown_Accuracy(currentFileInfo.FullName);

                //For left and right markers
                for (int i = 0; i < 2; i++)
                {
                    SingleFileProcessor sfp = new SingleFileProcessor();

                    foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                    {
                        sound_2_data_BothFeet.Add(currentValueToSave);

                         //Separate feet
                        if (i == 0)
                        {
                            sound_2_data_Left.Add(currentValueToSave);
                        }
                        else
                        {
                            if (i == 1)
                            {
                                sound_2_data_Right.Add(currentValueToSave);
                            }
                        }
                    }
                    

                }

                
            }

            // BASELINE_3
            foreach (FileInfo currentFileInfo in baseline_3_fileList)
            {
                List<List<PositionData>> currentFilePositionData = fileLoader.ReadFeetDown_Accuracy(currentFileInfo.FullName);

                //For left and right markers
                for (int i = 0; i < 2; i++)
                {
                    SingleFileProcessor sfp = new SingleFileProcessor();

                    DirectoryInfo currentDirInfo = new DirectoryInfo(currentFileInfo.DirectoryName);
                    FileInfo[] currentDirectoryFileList = currentDirInfo.GetFiles("Vision_2*_FeetDown_Accuracy.csv");

                    if (currentDirectoryFileList.Length > 0)
                    {
                        foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                        {
                            //Both feet
                            baseline_3_vision_data_BothFeet.Add(currentValueToSave);

                            //Separate feet
                            if (i == 0)
                            {
                                baseline_3_vision_data_Left.Add(currentValueToSave);
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    baseline_3_vision_data_Right.Add(currentValueToSave);
                                }
                            }

                        }
                    }
                    else
                    {
                        currentDirectoryFileList = currentDirInfo.GetFiles("Sound_2*_FeetDown_Accuracy.csv");
                        if (currentDirectoryFileList.Length > 0)
                        {
                            foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                            {
                                baseline_3_sound_data_BothFeet.Add(currentValueToSave);

                                //Separate feet
                                if (i == 0)
                                {
                                    baseline_3_sound_data_Left.Add(currentValueToSave);
                                }
                                else
                                {
                                    if (i == 1)
                                    {
                                        baseline_3_sound_data_Right.Add(currentValueToSave);
                                    }
                                }
                            }
                        }
                        else
                        {
                            currentDirectoryFileList = currentDirInfo.GetFiles("Baseline_2*FeetDown_Accuracy.csv");
                            if (currentDirectoryFileList.Length > 0)
                            {
                                foreach (double currentValueToSave in sfp.XValues(currentFilePositionData[i]))
                                {
                                    //Both feet
                                    baseline_3_baseline_data_BothFeet.Add(currentValueToSave);

                                    //Separate feet
                                    if (i == 0)
                                    {
                                        baseline_3_baseline_data_Left.Add(currentValueToSave);
                                    }
                                    else
                                    {
                                        if (i == 1)
                                        {
                                            baseline_3_baseline_data_Right.Add(currentValueToSave);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            loadedDataLists.Add(baseline_1_vision_data_BothFeet);    //0
            loadedDataLists.Add(baseline_1_sound_data_BothFeet);     //1
            loadedDataLists.Add(vision_2_data_BothFeet);             //2
            loadedDataLists.Add(sound_2_data_BothFeet);              //3
            loadedDataLists.Add(baseline_3_vision_data_BothFeet);    //4
            loadedDataLists.Add(baseline_3_sound_data_BothFeet);     //5
            loadedDataLists.Add(baseline_1_baseline_data_BothFeet);  //6
            loadedDataLists.Add(baseline_3_baseline_data_BothFeet);  //7
            loadedDataLists.Add(baseline_2_data_BothFeet);           //8

            //   Test 1
            loadedDataLists.Add(baseline_1_vision_data_Left);   //9
            loadedDataLists.Add(baseline_1_vision_data_Right);  //10    
            loadedDataLists.Add(baseline_1_sound_data_Left);    //11    
            loadedDataLists.Add(baseline_1_sound_data_Right);   //12    
            loadedDataLists.Add(baseline_1_baseline_data_Left); //13
            loadedDataLists.Add(baseline_1_baseline_data_Right);//14
            //   Test 3
            loadedDataLists.Add(baseline_3_vision_data_Left);   //15
            loadedDataLists.Add(baseline_3_vision_data_Right);  //16
            loadedDataLists.Add(baseline_3_sound_data_Left);    //17
            loadedDataLists.Add(baseline_3_sound_data_Right);   //18
            loadedDataLists.Add(baseline_3_baseline_data_Left); //19
            loadedDataLists.Add(baseline_3_baseline_data_Right);//20
            //Test 2
            loadedDataLists.Add(vision_2_data_Left);    //21
            loadedDataLists.Add(vision_2_data_Right);   //22
            loadedDataLists.Add(sound_2_data_Left);     //23
            loadedDataLists.Add(sound_2_data_Right);    //24
            loadedDataLists.Add(baseline_2_data_Left);  //25
            loadedDataLists.Add(baseline_2_data_Right); //26
            

            return (loadedDataLists);
        }

        

        public void SaveData(string pathToData)
        {
            List<List<double>> loadedDataLists = DataLists(pathToData);

            using (StreamWriter fileWriter = new StreamWriter(@"\\APPC05\Users\Public\Documents\Stromohab\ResultsFileLatest.csv"))
            {
                SingleFileProcessor sfp = new SingleFileProcessor();

                StreamWriter sortedFileWriter = new StreamWriter(@"\\APPC05\Users\Public\Documents\Stromohab\SortedSubjectData.csv");
                sortedFileWriter.WriteLine("Test,Subject,LF Mean,RF Mean,LF SD,RF SD");

                fileWriter.WriteLine("," + "Absolute Mean" + "," + "Standard Deviation");
                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline-Baseline_1:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[6])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[6])).ToString());
                fileWriter.WriteLine("Baseline-Baseline_2:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[8])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[8])).ToString());
                fileWriter.WriteLine("Baseline-Baseline_3:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[7])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[7])).ToString());

                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline-Vison_1:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[0])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[0])).ToString());
                fileWriter.WriteLine("Vision_2:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[2])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[2])).ToString());
                fileWriter.WriteLine("Baseline-Vision_3:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[4])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[4])).ToString());

                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline-Sound:_1" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[1])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[1])).ToString());
                fileWriter.WriteLine("Sound_2: " + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[3])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[3])).ToString());
                fileWriter.WriteLine("Baseline-Sound_3:" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[5])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[5])).ToString());

                fileWriter.WriteLine();
                fileWriter.WriteLine("Subject Number" + "," + "Baseline-Baseline_1" + "," + "Baseline-Sound_1" + "," + "Baseline-Vision_1" + ",," + "Baseline_2" + "," + "Vision_2" + "," + "Sound_2" + ",," + "Baseline-Baseline_3" + "," + "Baseline-Vision_3" + "," + "Baseline-Sound_3");

                /* *** PROCESS SubjectDataByTest HERE *** */

                foreach (string currentSubjectPath in AllSubjectsList(pathToData))
                {
                    DirectoryInfo currentDirInfo = new DirectoryInfo(currentSubjectPath);
                    FileInfo[] currentDirectoryFileList_Baseline1 = currentDirInfo.GetFiles("Baseline_1*FeetDown_Accuracy.csv",SearchOption.TopDirectoryOnly);
                    FileInfo[] currentDirectoryFileList_Baseline2 = currentDirInfo.GetFiles("Baseline_2*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                    FileInfo[] currentDirectoryFileList_Vision2 = currentDirInfo.GetFiles("Vision_2*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                    FileInfo[] currentDirectoryFileList_Sound2 = currentDirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                    FileInfo[] currentDirectoryFileList_Baseline3 = currentDirInfo.GetFiles("Baseline_3*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);


                    FileLoader fileLoader = new FileLoader();

                    string currentSubjectID = currentDirInfo.Name;

                    foreach (FileInfo currentBaselineFile in currentDirectoryFileList_Baseline1)
                    {
                        List<List<PositionData>> currentFileData = fileLoader.ReadFeetDown_Accuracy(currentBaselineFile.FullName);

                        if (currentDirectoryFileList_Vision2.Length > 0)
                        {
                            fileWriter.Write(currentSubjectID + ",,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentBaselineFile.FullName))));
                            fileWriter.Write(",,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Vision2[0].FullName))));
                            fileWriter.Write(",,,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");

                            sortedFileWriter.Write("Vision," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentBaselineFile.FullName))) + "\n");
                            sortedFileWriter.Write("Vision," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Vision2[0].FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Vision2[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Vision2[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Vision2[0].FullName))) + "\n");
                            sortedFileWriter.Write("Vision," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");
                        }
                        else
                        {
                            if (currentDirectoryFileList_Sound2.Length > 0)
                            {
                                fileWriter.Write(currentSubjectID + ",," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentBaselineFile.FullName))));
                                fileWriter.Write(",,,,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Sound2[0].FullName))));
                                fileWriter.Write(",,,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");

                                sortedFileWriter.Write("Sound," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentBaselineFile.FullName))) + "\n");
                                sortedFileWriter.Write("Sound," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Sound2[0].FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Sound2[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Sound2[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Sound2[0].FullName))) + "\n");
                                sortedFileWriter.Write("Sound," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");
                            }
                            else
                            {
                                if (currentDirectoryFileList_Baseline2.Length > 0)
                                {
                                    fileWriter.Write(currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentBaselineFile.FullName))));
                                    fileWriter.Write(",,,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline2[0].FullName))));
                                    fileWriter.Write(",,,," + sfp.MeanAccuracy(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");

                                    sortedFileWriter.Write("Control," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentBaselineFile.FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentBaselineFile.FullName))) + "\n");
                                    sortedFileWriter.Write("Control," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline2[0].FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline2[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline2[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline2[0].FullName))) + "\n");
                                    sortedFileWriter.Write("Control," + currentSubjectID + "," + sfp.MeanAccuracy(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.MeanAccuracy(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllLeftFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "," + sfp.StandardDeviation(AbsoluteValues(AllRightFootXValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");
                                }
                            }
                        }
                    }

 
                }

                fileWriter.WriteLine();
                fileWriter.WriteLine();

                /* DO LEFT FOOT / RIGHT FOOT STUFF HERE */

                fileWriter.WriteLine();
                fileWriter.WriteLine();

                fileWriter.WriteLine("," + "Absolute Mean Accuracy" + ",,," + "Accuracy SD\n" + "," + "Left Foot" + "," + "Right Foot" +  "," + "Left Foot" + "," + "Right Foot");
                fileWriter.WriteLine("Baseline-Baseline_1" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[13])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[14])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[13])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[14])).ToString());
                fileWriter.WriteLine("Baseline_2" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[25])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[26])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[25])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[26])).ToString());
                fileWriter.WriteLine("Baseline-Baseline_3" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[19])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[20])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[19])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[20])).ToString());
                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline-Vision_1" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[9])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[10])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[9])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[10])).ToString());
                fileWriter.WriteLine("Vision_2" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[21])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[22])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[21])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[22])).ToString());
                fileWriter.WriteLine("Baseline-Vision_3" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[15])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[16])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[15])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[16])).ToString());
                fileWriter.WriteLine();

                fileWriter.WriteLine("Baseline-Sound_1" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[11])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[12])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[11])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[12])).ToString());
                fileWriter.WriteLine("Sound_2" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[23])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[24])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[23])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[24])).ToString());
                fileWriter.WriteLine("Baseline-Sound_3" + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[17])).ToString() + "," + sfp.MeanAccuracy(AbsoluteValues(loadedDataLists[18])).ToString()+ "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[17])).ToString() + "," + sfp.StandardDeviation(AbsoluteValues(loadedDataLists[18])).ToString());


                //fileWriter.WriteLine("STANDARD DEVATION");

                //foreach (string currentSubjectPath in AllSubjectsList(pathToData))
                //{
                //    DirectoryInfo currentDirInfo = new DirectoryInfo(currentSubjectPath);
                //    FileInfo[] currentDirectoryFileList_Baseline1 = currentDirInfo.GetFiles("Baseline_1*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                //    FileInfo[] currentDirectoryFileList_Baseline2 = currentDirInfo.GetFiles("Baseline_2*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                //    FileInfo[] currentDirectoryFileList_Vision2 = currentDirInfo.GetFiles("Vision_2*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                //    FileInfo[] currentDirectoryFileList_Sound2 = currentDirInfo.GetFiles("Sound_2*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);
                //    FileInfo[] currentDirectoryFileList_Baseline3 = currentDirInfo.GetFiles("Baseline_3*FeetDown_Accuracy.csv", SearchOption.TopDirectoryOnly);


                //    FileLoader fileLoader = new FileLoader();

                //    string currentSubjectID = currentDirInfo.Name;

                //    foreach (FileInfo currentBaselineFile in currentDirectoryFileList_Baseline1)
                //    {
                //        List<List<PositionData>> currentFileData = fileLoader.ReadFeetDown_Accuracy(currentBaselineFile.FullName);

                //        if (currentDirectoryFileList_Vision2.Length > 0)
                //        {
                //            fileWriter.Write(currentSubjectID + ",,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentBaselineFile.FullName))));
                //            fileWriter.Write(",,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Vision2[0].FullName))));
                //            fileWriter.Write(",,,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");
                //        }
                //        else
                //        {
                //            if (currentDirectoryFileList_Sound2.Length > 0)
                //            {
                //                fileWriter.Write(currentSubjectID + ",," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentBaselineFile.FullName))));
                //                fileWriter.Write(",,,,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Sound2[0].FullName))));
                //                fileWriter.Write(",,,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");
                //            }
                //            else
                //            {
                //                if (currentDirectoryFileList_Baseline2.Length > 0)
                //                {
                //                    fileWriter.Write(currentSubjectID + "," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentBaselineFile.FullName))));
                //                    fileWriter.Write(",,,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline2[0].FullName))));
                //                    fileWriter.Write(",,,," + sfp.StandardDeviation(AbsoluteValues(AllValuesFromFile(currentDirectoryFileList_Baseline3[0].FullName))) + "\n");
                //                }
                //            }
                //        }
                //    }


                //}


                fileWriter.Flush();
                fileWriter.Close();
                sortedFileWriter.Flush();
                sortedFileWriter.Close();

            }



        }

        private List<double> AllValuesFromFile(string pathToFile)
        {
            FileLoader fileLoader = new FileLoader();
            SingleFileProcessor sfp = new SingleFileProcessor();


            List<List<PositionData>> dataFromFile = fileLoader.ReadFeetDown_Accuracy(pathToFile);
            List<double> allValuesList = new List<double>();

            foreach (double currentValue in sfp.XValues(dataFromFile[0]))
            {
                allValuesList.Add(currentValue);
            }

            foreach (double currentValue in sfp.XValues(dataFromFile[1]))
            {
                allValuesList.Add(currentValue);
            }

            return (allValuesList);

        }

        private List<double> AllLeftFootXValuesFromFile(string pathToFile)
        {
            FileLoader fileLoader = new FileLoader();
            SingleFileProcessor sfp = new SingleFileProcessor();

            List<List<PositionData>> dataFromFile = fileLoader.ReadFeetDown_Accuracy(pathToFile);

            return (sfp.XValues(dataFromFile[0]));
        }

        private List<double> AllRightFootXValuesFromFile(string pathToFile)
        {
            FileLoader fileLoader = new FileLoader();
            SingleFileProcessor sfp = new SingleFileProcessor();

            List<List<PositionData>> dataFromFile = fileLoader.ReadFeetDown_Accuracy(pathToFile);

            return (sfp.XValues(dataFromFile[1]));
        }

        private List<double> AbsoluteValues(List<double> dataList)
        {
            List<double> absoluteBaseLineVisionValues = new List<double>();

            foreach (double currentValue in dataList)
            {
                absoluteBaseLineVisionValues.Add(Math.Abs(currentValue));
            }

            return (absoluteBaseLineVisionValues);
        }



    }
}

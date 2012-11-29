using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.Treadmill
{
    /// <summary>
    /// This virtual Treadmill controller is a way to detect the speed of the treadmill using motion capture
    /// </summary>
    class MotionCaptureTreadmill
    {
        #region Member Variables

        TreadmillCalibrationForm calibrationForm = new TreadmillCalibrationForm();

        /// <summary>
        /// Specifys the minimum speed that the treadmill can go
        /// </summary>
        private const float minSpeed = 0f;
        /// <summary>
        /// Specifys the maximum speed that the treadmill can go
        /// </summary>
        private const float maxSpeed = 3.0f;
        /// <summary>
        /// Determins if the treadmill speed sound be detected
        /// </summary>
        private bool processTreadmillSpeed = false;
        /// <summary>
        /// Counts how many times the speed has changed
        /// </summary>
        private int speedChangeCounter = 0;
        /// <summary>
        /// The current speed (excluding the conversion into real world speed)
        /// </summary>
        private double speed = 0;
        /// <summary>
        /// The current speed including conversion
        /// </summary>
        private double realSpeed = 0;
        /// <summary>
        /// The previous speed
        /// </summary>
        private double oldSpeed = 0;
        /// <summary>
        /// Conversion factor from m/s into MPH
        /// </summary>
        private const double mpsToMPH = 2.23693629f;
        /// <summary>
        /// Conversion from speed to real world speed of the treadmill
        /// </summary>
        private float treadmillToVisuliserConversionValue = 1;
        /// <summary>
        /// The X position of the treadmill marers
        /// </summary>
        private double treadmillMarkerXPos = 0;
        /// <summary>
        /// The Y position of the treadmill marers
        /// </summary>
        private double treadmillMarkerYPos = 0;
        /// <summary>
        /// Determins how many previous speeds to save and average out
        /// </summary>
        private const int numberOfSpeedValues = 20;
        /// <summary>
        /// Stores a number of previous speed values so that they can be averaged out
        /// </summary>
        private double[] speedValues = new double[numberOfSpeedValues];
        /// <summary>
        /// Indexes the array of previous speed values
        /// </summary>
        private int speedValuesIndex = 0;
        /// <summary>
        /// The marker number that is being used to calculate the treadmill speed
        /// </summary>
        private int treadmillMarkerNumber = 0;
        /// <summary>
        /// The number of attempts to try and find the speed of the treadmill before assuming its stopped (1 attempt occurs every 10ms so 100 = 1 second)
        /// </summary>
        private const int timeOut = 100;
        /// <summary>
        /// The number of failed attempts to find the speed of the treadmill
        /// </summary>
        private int failedAttempts = 0;

        private bool InCalibration = false;

        #endregion Member Variables


        #region Treadmill Speed Detection

        /// <summary>
        /// Calibrates the treadmill speed detection variables
        /// </summary>
        public void CalibrateTreadmill()
        {
            InCalibration = true;
            //Prompt the user to prepare for calibration
            System.Windows.Forms.MessageBox.Show("Please place the markers on the treadmill and turn in on", "StroMoHab Server - Treadmill Speed Detection Calibration", System.Windows.Forms.MessageBoxButtons.OK);

            bool done = false;
            int attempts = 0;


            while (done == false) // While the values aren't found keep trying
            {
                if (FilteredMarkerList.listOfMarkers.Count >= 1) // Check for more 0 markers, assume user hasn't left any extra markers
                {
                    treadmillMarkerXPos = FilteredMarkerList.listOfMarkers[0].xCoordinate;
                    treadmillMarkerYPos = FilteredMarkerList.listOfMarkers[0].yCoordinate;
                    done = true;
                    System.Diagnostics.Debug.WriteLine("Treadmill x - y: " + treadmillMarkerXPos + " - " + treadmillMarkerYPos);
                }
            /*double treadmillMarkerMaxY = -9999;
            double treadmillMarkerMeanX = 0;
            double treadmillMarkerMeanY = 0;
            int maxY_markerCount = 0;

            while (done == false) // While the values aren't found keep trying
            {
                if (FilteredMarkerList.listOfMarkers.Count >= 1) // Check for more 0 markers, assume user hasn't left any extra markers
                {
                    foreach (Marker speedMarker in FilteredMarkerList.listOfMarkers)
                    {
                        if (maxY_markerCount < 10)//first make sure of belt surface y-value by checking for the maximum
                        {
                            if (speedMarker.yCoordinate > treadmillMarkerMaxY)
                            {
                                treadmillMarkerMaxY = speedMarker.yCoordinate;
                                maxY_markerCount++;
                            }
                        }
                        else//then take the mean of the next 10 values within, say, 5mm of this
                        {
                            if ((treadmillMarkerMaxY - speedMarker.yCoordinate) < 5)
                            {
                                treadmillMarkerMeanX += speedMarker.xCoordinate;
                                treadmillMarkerMeanY += speedMarker.yCoordinate;
                                maxY_markerCount++;
                            }
                        }
                    }
                    if (maxY_markerCount == 20)
                    {
                        done = true;
                        treadmillMarkerXPos = treadmillMarkerMeanX / 10;
                        treadmillMarkerYPos = treadmillMarkerMeanY / 10;
                    }
                }*/
                else
                    attempts++; //If 0 markers are visiable mark as a failed attempt

                if (speed == 0) // If the treadmill isn't moving then mark as not done to avoid invalid speed calibration
                {
                    done = false;
                    if (System.Windows.Forms.MessageBox.Show("Detection Failed - The treadmill is not moving!\nPlease place the markers on the treadmill and turn in on", "StroMoHab Server - Treadmill Speed Detection Calibration", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        attempts = 0;
                    }
                    else return;
                }
                if (attempts == 100) //If no markers were visiable for a number of goes then pause and alert the user
                {
                    if (System.Windows.Forms.MessageBox.Show("Detection Failed - Not enough markers!\nPlease place the markers on the treadmill and turn in on", "StroMoHab Server - Treadmill Speed Detection Calibration", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        attempts = 0;
                    }
                    else return;
                }
            }
            // Give the user the option to calibrate the treadmill speed
            if (System.Windows.Forms.MessageBox.Show("Do you want to calibrate the speed of the treadmill?", "StroMoHab Server - Treadmill Speed Detection Calibration", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {

                calibrationForm.Speed = speed;
                calibrationForm.TreadmillToVisualiserConversionValue = treadmillToVisuliserConversionValue;

                calibrationForm.ShowDialog();

                treadmillToVisuliserConversionValue = calibrationForm.TreadmillToVisualiserConversionValue;


            }

            // Update the speed based on the new conversion value and save the calibration
            UpdateSpeed();
            SaveTreadmillCalibration();
            InCalibration = false;
        }

        /// <summary>
        /// Saves the treadmill calibration settings
        /// </summary>
        public void SaveTreadmillCalibration()
        {
            StroMoHab_TT_Server.Properties.Settings.Default.TreadmillX = treadmillMarkerXPos;
            StroMoHab_TT_Server.Properties.Settings.Default.TreadmillY = treadmillMarkerYPos;
            StroMoHab_TT_Server.Properties.Settings.Default.TreadmillSpeedRatio = treadmillToVisuliserConversionValue;
            StroMoHab_TT_Server.Properties.Settings.Default.Save();

        }

        /// <summary>
        /// Loads the treadmill calibration settings
        /// </summary>
        public void LoadTreadmillCalibration()
        {
            treadmillMarkerXPos = StroMoHab_TT_Server.Properties.Settings.Default.TreadmillX;
            treadmillMarkerYPos = StroMoHab_TT_Server.Properties.Settings.Default.TreadmillY;
            treadmillToVisuliserConversionValue = StroMoHab_TT_Server.Properties.Settings.Default.TreadmillSpeedRatio;
        }

        /// <summary>
        /// Gets or Sets the property to enable/dissable detecting the treadmill speed
        /// </summary>
        public bool DetectTreadmillSpeed
        {
            set
            {
                processTreadmillSpeed = value;
                StroMoHab_TT_Server.Properties.Settings.Default.DetectTreadmillSpeed = value;
                try
                {
                    StroMoHab_TT_Server.Properties.Settings.Default.Save();
                }
                catch (System.Configuration.ConfigurationException ex)
                {
                    System.Diagnostics.Debug.WriteLine("No user config file exists: Exception message: " + ex.Message);
                }
            }
            get
            {
                return processTreadmillSpeed;
            }
        }

        /// <summary>
        ///  Trys to find the treadmill speed
        /// </summary>
        /// <param name="newMarkerList"></param>
        /// <returns>Returns the marker numbers that was used for speed detection or -1 if it wasn't found</returns>
        public int[] FindSpeed(List<Marker> newMarkerList)
        {

            List<Marker> markerList = new List<Marker>(newMarkerList);
            bool foundSpeed = false;

            //If the marker number being used is still valid
            if (markerList.Count > 0 && treadmillMarkerNumber < markerList.Count)
            {

                //Check to see if the marker is out of the x y range that was previously calibrated
                if (Math.Abs(markerList[treadmillMarkerNumber].xCoordinate - treadmillMarkerXPos) > 20 || Math.Abs(markerList[treadmillMarkerNumber].yCoordinate - treadmillMarkerYPos) > 10)
                    treadmillMarkerNumber++; //
                else // Marker is in the correct small x y channel
                {
                    // If the marker has moved in the correct z direction (and hasn't switched)
                    if (markerList[treadmillMarkerNumber].zCoordinate - 20 < markerList[treadmillMarkerNumber].prevZCoordinate)
                    {
                        // Calculate the distance and time
                        double distance = -markerList[treadmillMarkerNumber].zCoordinate + markerList[treadmillMarkerNumber].prevZCoordinate;
                        double time = markerList[treadmillMarkerNumber].TimeStamp - markerList[treadmillMarkerNumber].PrevTimestamp;

                        //Convert into a speed and save into the array of values
                        speedValues[speedValuesIndex] = (10000 * distance / time) * mpsToMPH; //(mm/ms) = (m/s)
                                                                                              //10^4 multiplier to allow for long Timestamp (100ns resolution)
                        // Round it to 1 dp
                        speedValues[speedValuesIndex] = Math.Round(speedValues[speedValuesIndex], 1);

                        //Increase the index of the array and if its reached the max reset it to 0 (its uses as a FIFO buffer)
                        speedValuesIndex++;
                        if (speedValuesIndex == numberOfSpeedValues)
                            speedValuesIndex = 0;

                        // Set the speed to the average of all the values in the array and round it to 1 dp
                        speed = 0;
                        for (int i = 0; i < numberOfSpeedValues; i++)
                        {
                            speed = speedValues[i] + speed;
                        }
                        speed = speed / numberOfSpeedValues;
                        speed = Math.Round(speed, 1);


                        // Only update if the speed has changed
                        if (speed != oldSpeed)
                        {
                            //Speed has changed so count up
                            speedChangeCounter++;

                            //Speed has to change for more than 0.50 seconds before its counted as not just a small fluxuation or if the treadmill isn't moving for 0.1 seconds
                            if (speed != 0)
                            {
                                if (speedChangeCounter >= 50)
                                {
                                    oldSpeed = speed;
                                    speedChangeCounter = 0;

                                    // Update the speed
                                    UpdateSpeed();
                                }
                            }
                            else
                            {
                                if (speedChangeCounter >= 10)
                                {
                                    oldSpeed = speed;
                                    speedChangeCounter = 0;

                                    // Update the speed
                                    UpdateSpeed();
                                }
                            }
                        }
                        else // else if the speed is the same as it was previously reset the speedchangecounter
                            speedChangeCounter = 0;

                        // If the speed was found reset the failed attemps
                        failedAttempts = 0;
                        foundSpeed = true;


                    }
                }

            } // if out of range of the markers go back to the first marker and mark as a failed attempt
            else
            {
                treadmillMarkerNumber = 0;
                failedAttempts++;
            }



            // If its failed more than the timeOut value then assume the treadmill has stopped with no markers visiable
            if (failedAttempts >= timeOut)
            {
                failedAttempts = 0;
                oldSpeed = 0;
                speed = 0;
                UpdateSpeed();
            }

            if (InCalibration) // If currently calibrating, then provide the speed
            {
                calibrationForm.Speed = speed;
                treadmillToVisuliserConversionValue = calibrationForm.TreadmillToVisualiserConversionValue;
            }

            if (foundSpeed) // If the speed was found then there must be markers on the treadmill so find them
            {
                // Build a list of the first two (there should only be 1 and very rarely 2 visiable) markers on the treadmill
                // if the setup later results in there being 3+ treadmill markers visiable at once then update accordingly
                //With additional belt markers and new camera positions more markers are visible
                int[] treadmillMakrerNumbers = new int[3] {-1, -1, -1};//, -1, -1 };//index increased from 3 to 5
                int markerNumberIndex = 0;
                foreach (Marker tempMarker in newMarkerList)
                {
                    if (Math.Abs(tempMarker.xCoordinate - treadmillMarkerXPos) < 20 && Math.Abs(tempMarker.yCoordinate - treadmillMarkerYPos) < 10)
                    {
                        treadmillMakrerNumbers[markerNumberIndex] = tempMarker.MarkerId;
                        markerNumberIndex++;
                    }
                    if (markerNumberIndex == 2)//increased from 2 to 4
                        break; // Assuming there can only be a maximum of 2 markers means once they are found there is no point searching for more
                }
                return treadmillMakrerNumbers;
            } // If the speed wasn't found then return null as there are no applicable markers
            else return null;
        }


        /// <summary>
        /// Sends the updated speed to the TreadmillController
        /// </summary>
        public void UpdateSpeed()
        {
            if (!Communication.OptitrackCommandParser_Server.VirtualMotionCaputrePlayback)
            {
                realSpeed = Math.Round(speed * treadmillToVisuliserConversionValue, 1);
                if (realSpeed != TreadmillController.GetSpeed()) //only update if different
                {
                    if (realSpeed >= minSpeed && realSpeed <= maxSpeed)
                    {
                        TreadmillController.SetSpeed((float)realSpeed);
                    }
                }
            }
        }
        #endregion Treadmill Speed Detection

    }
}








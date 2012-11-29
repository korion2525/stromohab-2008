using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using Stromohab_MCE;
//using Stromohab_MCE_Connection;
using System.Timers;
using System.IO;

namespace Stromohab_Diagnostics
{
    class Sonifier
    {
        private bool leftToe;
        private bool leftHeel;
        private bool rightToe;
        private bool rightHeel;
        
        //private bool strideBeat;//for metronomic beat

        //Number of sounds to load - defines sizes of sound lists + arrays (Mat G)
        const int NUMBER_OF_SOUNDS_TO_LOAD = 4;
        
        //List of sounds to load - makes code more scalable (Mat G)
        SoundPlayer[] soundPlayerList = new SoundPlayer[NUMBER_OF_SOUNDS_TO_LOAD];

        //SoundPlayer leftToe_Sound;
        //SoundPlayer rightToe_Sound;
        //SoundPlayer leftHeel_Sound;
        //SoundPlayer rightHeel_Sound;
        private bool enabled = false;
        //private static System.Timers.Timer metronome;

        string soundPath;
        
        PeakAnalyser beats;

        public Sonifier()
        {
            beats = new PeakAnalyser();
            PeakAnalyser.FootPeakDetected += new PeakAnalyser.PeakEventHandler(Foot_PeakDetected);
            //metronome.Elapsed += new ElapsedEventHandler(metronome_beat);
            //metronome.Enabled = false;
            //metronome.Interval = 2000;
            //metronome.Enabled = true;

            soundPath = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName + @"\Sounds";

            string[] soundFileList = new string[NUMBER_OF_SOUNDS_TO_LOAD]  {"cymbal.wav",
                                                                            "Conga2.wav",
                                                                            "clave.wav",
                                                                            "bleep.wav"};

            for (int i = 0; i < soundFileList.Length; i++)
            {
                if (File.Exists(soundPath + @"\" + soundFileList[i]))
                {
                    try
                    {
                        soundPlayerList[i] = new SoundPlayer(soundPath + @"\" + soundFileList[i]);
                    }
                    catch (System.IO.FileNotFoundException ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Cannot load sound files. Please ensure that the \"Sounds\" folder is in the executable partent directory. Exception Message: " + ex.Message, "Cannot Find Sound Files");
                        Environment.Exit(-1);
                    }
                }
            }

            //leftToe_Sound = new SoundPlayer(soundPath + "cymbal.wav");
            //rightToe_Sound = new SoundPlayer(soundPath + "Conga2.wav");
            //leftHeel_Sound = new SoundPlayer(soundPath + "clave.wav");
            //rightHeel_Sound = new SoundPlayer(soundPath + "bleep.wav");

            try
            {
                soundPlayerList[0].Load();
                soundPlayerList[1].Load();
                soundPlayerList[2].Load();
                soundPlayerList[3].Load();
            }
            catch (FileNotFoundException ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot load sound files. Please ensure that the \"Sounds\" folder is in the executable parent directory. Exception Message: " + ex.Message, "Cannot Find Sound Files");
                Environment.Exit(-1);
            }
            catch (System.NullReferenceException exNull)
            {
                System.Windows.Forms.MessageBox.Show("Cannot load sound files. Please ensure that the \"Sounds\" folder is in the executable parent directory. Exception Message: " + exNull.Message, "Cannot Load Sound Files");
                Environment.Exit(-1);
            }

        }

        //private static void metronome_beat(object source, ElapsedEventArgs e)
        //{
        //
        //}

        private void Foot_PeakDetected(string foot, int direction, Marker footMarker)
        {
            if ((leftHeel == true) && (direction == -1) && (foot == "left"))
            {
                soundPlayerList[2].Play();
                Console.WriteLine("Left:  " + footMarker.MarkerId.ToString() + "\t" + footMarker.zCoordinate.ToString() + "\t" +
                    footMarker.prevZCoordinate.ToString() + "\t" + footMarker.TimeStamp.ToString());
            }
            if ((rightHeel == true) && (direction == -1) && (foot == "right"))
            {
                soundPlayerList[3].Play();
                Console.WriteLine("Right:  " + footMarker.MarkerId.ToString() + "\t" + footMarker.zCoordinate.ToString() + "\t" +
                    footMarker.prevZCoordinate.ToString() + "\t" + footMarker.TimeStamp.ToString());
            }
            if ((leftToe == true) && (direction == 1) && (foot == "left"))
            {
                soundPlayerList[0].Play();
            }
            if ((rightToe == true) && (direction == 1) && (foot == "right"))
            {
                soundPlayerList[1].Play();
            }
        }
        
        
        /// <summary>
        /// Enables and disables Sonifier
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
                beats.Enabled = enabled;
            }
        }
        /// <summary>
        /// Enables sound for left toe event
        /// </summary>
        public bool LeftToe
        {
            get
            {
                return leftToe;
            }
            set
            {
                leftToe = value;
            }
        }
        /// <summary>
        /// Enables sound for left heel event
        /// </summary>
        public bool LeftHeel
        {
            get
            {
                return leftHeel;
            }
            set
            {
                leftHeel = value;
            }
        }
        /// <summary>
        /// Enables sound for right toe event
        /// </summary>
        public bool RightToe
        {
            get
            {
                return rightToe;
            }
            set
            {
                rightToe = value;
            }
        }
        /// <summary>
        /// Enables sound for right heel event
        /// </summary>
        public bool RightHeel
        {
            get
            {
                return rightHeel;
            }
            set
            {
                rightHeel = value;
            }
        }
        /// <summary>
        /// Gets/sets path for left toe off sound
        /// </summary>
        public string LeftToe_Sound
        {
            get
            {
                return soundPlayerList[0].SoundLocation;
            }
            set
            {
                soundPlayerList[0].SoundLocation = soundPath + value;
                soundPlayerList[0].Load();
            }
        }
        /// <summary>
        /// Gets/sets path for left heel down sound
        /// </summary>
        public string LeftHeel_Sound
        {
            get
            {
                return soundPlayerList[2].SoundLocation;
            }
            set
            {
                soundPlayerList[2].SoundLocation = soundPath + value;
                soundPlayerList[2].Load();
            }
        }
        /// <summary>
        /// Gets/sets path for right toe off sound
        /// </summary>
        public string RightToe_Sound
        {
            get
            {
                return soundPlayerList[1].SoundLocation;
            }
            set
            {
                soundPlayerList[1].SoundLocation = soundPath + value;
                soundPlayerList[1].Load();
            }
        }
        /// <summary>
        /// Gets/sets path for right heel down sound
        /// </summary>
        public string RightHeel_Sound
        {
            get
            {
                return soundPlayerList[3].SoundLocation;
            }
            set
            {
                soundPlayerList[3].SoundLocation = soundPath + value;
                soundPlayerList[3].Load();
            }
        }
    }
}

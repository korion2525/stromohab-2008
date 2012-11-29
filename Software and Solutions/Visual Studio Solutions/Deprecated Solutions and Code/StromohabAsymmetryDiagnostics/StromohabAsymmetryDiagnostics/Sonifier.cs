using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using Stromohab_MCE;
using Stromohab_MCE_Connection;
using System.Timers;

namespace Stromohab_Diagnostics
{
    class Sonifier
    {
        private bool leftToe;
        private bool leftHeel;
        private bool rightToe;
        private bool rightHeel;
        //private bool strideBeat;//for metronomic beat

        SoundPlayer leftToe_Sound;
        SoundPlayer rightToe_Sound;
        SoundPlayer leftHeel_Sound;
        SoundPlayer rightHeel_Sound;
        private bool enabled = false;
        //private static System.Timers.Timer metronome;

        string soundPath = Environment.CurrentDirectory.ToString() + "/Sounds/";
        
        PeakAnalyser beats;

        public Sonifier()
        {
            beats = new PeakAnalyser();
            PeakAnalyser.FootPeakDetected += new PeakAnalyser.PeakEventHandler(Foot_PeakDetected);
            //metronome.Elapsed += new ElapsedEventHandler(metronome_beat);
            //metronome.Enabled = false;
            //metronome.Interval = 2000;
            //metronome.Enabled = true;
            
            
            leftToe_Sound = new SoundPlayer(soundPath + "cymbal.wav");
            rightToe_Sound = new SoundPlayer(soundPath + "Conga2.wav");
            leftHeel_Sound = new SoundPlayer(soundPath + "clave.wav");
            rightHeel_Sound = new SoundPlayer(soundPath + "bleep.wav");
            leftToe_Sound.Load();
            rightToe_Sound.Load();
            leftHeel_Sound.Load();
            rightHeel_Sound.Load();
        }

        //private static void metronome_beat(object source, ElapsedEventArgs e)
        //{
        //
        //}

        private void Foot_PeakDetected(string foot, int direction, Marker footMarker)
        {
            if ((leftHeel == true) && (direction == -1) && (foot == "left"))
            {
                leftHeel_Sound.Play();
                Console.WriteLine("Left:  " + footMarker.MarkerId.ToString() + "\t" + footMarker.zCoordinate.ToString() + "\t" +
                    footMarker.prevZCoordinate.ToString() + "\t" + footMarker.TimeStamp.ToString());
            }
            if ((rightHeel == true) && (direction == -1) && (foot == "right"))
            {
                rightHeel_Sound.Play();
                Console.WriteLine("Right:  " + footMarker.MarkerId.ToString() + "\t" + footMarker.zCoordinate.ToString() + "\t" +
                    footMarker.prevZCoordinate.ToString() + "\t" + footMarker.TimeStamp.ToString());
            }
            if ((leftToe == true) && (direction == 1) && (foot == "left"))
            {
                leftToe_Sound.Play();
            }
            if ((rightToe == true) && (direction == 1) && (foot == "right"))
            {
                rightToe_Sound.Play();
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
                return leftToe_Sound.SoundLocation;
            }
            set
            {
                leftToe_Sound.SoundLocation = soundPath + value;
                leftToe_Sound.Load();
            }
        }
        /// <summary>
        /// Gets/sets path for left heel down sound
        /// </summary>
        public string LeftHeel_Sound
        {
            get
            {
                return leftHeel_Sound.SoundLocation;
            }
            set
            {
                leftHeel_Sound.SoundLocation = soundPath + value;
                leftHeel_Sound.Load();
            }
        }
        /// <summary>
        /// Gets/sets path for right toe off sound
        /// </summary>
        public string RightToe_Sound
        {
            get
            {
                return rightToe_Sound.SoundLocation;
            }
            set
            {
                rightToe_Sound.SoundLocation = soundPath + value;
                rightToe_Sound.Load();
            }
        }
        /// <summary>
        /// Gets/sets path for right heel down sound
        /// </summary>
        public string RightHeel_Sound
        {
            get
            {
                return rightHeel_Sound.SoundLocation;
            }
            set
            {
                rightHeel_Sound.SoundLocation = soundPath + value;
                rightHeel_Sound.Load();
            }
        }
    }
}

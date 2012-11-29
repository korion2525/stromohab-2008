using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ALManagedStaticClass;

namespace StromoLight_Diagnostics
{
    public class ALSoundEnvironment
    {
        Foot left = null;
        Foot right = null;
        //Thread updateThread = null;
        //static bool soundon = false;
        bool stopUpdate = false;
        bool footDown = false;

        public ALSoundEnvironment()
        {
            //left = person.Left;
            //right = person.Right;

            ALsetup.SLsetenvironment();
            ALsetup.SLreadWAV(52280);

            
            //if (!ALsetup.SLsetenvironment())
            //{
            //    MessageBox.Show("Sound is not fully functioning!");
            //}
            
        }

        private void Foot_FootDownDetected(Foot foot)
        {
            if (footDown == true)
            {
                ALsetup.SLvarygain((float)(1/((Math.Abs(foot.CurrentMarker.xCoordinate) / 10)+0.05f)));
                ALsetup.SLPlayWAV();
            }

        }

        public void DiscreteFootDown_On()
        {
            Foot.FootDownDetected += new Foot.FootDownEventHandler(Foot_FootDownDetected);
        }

        public void DiscreteFootDown_Off()
        {
            Foot.FootDownDetected -= new Foot.FootDownEventHandler(Foot_FootDownDetected);
        }

        public void StartSound()
        {
            stopUpdate = false;
            //soundon = true;
            ALsetup.SLplayTone(300);
            Thread updateThread = new Thread(new ThreadStart(KeepPlaying));
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        private void KeepPlaying()
        {
            int st = Environment.TickCount;
            float pitchparameter, distance;

            try
            {
                while ((ALsetup.SLtoneupdate()))
                {
                    //System.Diagnostics.Debug.WriteLine("In >KeepPlaying< " + tc.ToString());
                    //tc++;
                    if (stopUpdate) Thread.CurrentThread.Abort();
                    //if (Foot.Frontfoot != null)
                    //{
                    if ((left.CurrentMarker != null) && (right.CurrentMarker != null))
                    {
                        if (left.CurrentMarker.zCoordinate > right.CurrentMarker.zCoordinate)
                        {
                            distance = (float)(Math.Abs(left.CurrentMarker.xCoordinate));
                        }
                        else
                        {
                            distance = (float)(Math.Abs(right.CurrentMarker.xCoordinate));
                        }



                        //distance = (float)(Math.Abs(Foot.Frontfoot.CurrentMarker.xCoordinate));
                        /*}
                        else
                        {
                            double zL = -9999;
                            double zR = -9999;
                            if (left.CurrentMarker != null) zL = left.CurrentMarker.zCoordinate;
                            if (right.CurrentMarker != null) zR = right.CurrentMarker.zCoordinate;
                            if (zL > zR)
                            {
                                distance = (float)(Math.Abs(left.CurrentMarker.xCoordinate));
                            }
                            else
                            {
                                distance = (float)(Math.Abs(right.CurrentMarker.xCoordinate));
                            }

                        }*/
                        //System.Diagnostics.Debug.WriteLine("Frontfoot x:" + distance);
                        //if (distance < 450)
                        //{
                        if (distance < 5)
                        {
                            pitchparameter = 0.005f;
                        }
                        else
                        {
                            pitchparameter = (float)(1 / (2 * Math.Log10((double)(1.1 + (distance * 0.01))) + 0.02f));
                        }
                        ALsetup.SLchangepitch(pitchparameter);
                        //}
                        //else
                        //{
                        //    System.Diagnostics.Debug.WriteLine("----> Distance >450 <----");
                        //ALsetup.SLstoptone();
                        //}
                        //if (distance < 400) ALsetup.SLplayTone(250);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("---->Frontfoot NULL<----");
                    }
                }
                System.Diagnostics.Debug.WriteLine("Sound. Update loop ceased:" + (Environment.TickCount - st).ToString());
            }
            catch (NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine("Null in Foot.Frontfoot");
            }
            catch (ThreadAbortException)
            {
                System.Diagnostics.Debug.WriteLine("Update thread has aborted");
            }
        }

        public void LoadWAV()
        {
            ALsetup.SLreadWAV(52280);
            ALsetup.SLPlayWAV();
        }

        public void EndCurrentEnvironment()
        {
            //try
            //{
            //updateThread.Abort();
            //}
            //catch (ThreadAbortException)
            //{
            //    System.Diagnostics.Debug.WriteLine("Thread aborted");
            //}
            ALsetup.SLcloseEnvironment();
        }

        public void StopPlaying()
        {
            ALsetup.SLstoptone();
            //updateThread.Abort();
        }

        /*public static bool Soundon
        {
            get { return soundon; }
            set { soundon = value; }
        }*/

        public bool StopUpdate
        {
            get { return stopUpdate; }
            set { stopUpdate = value; }
        }

        public Foot Left
        {
            get { return left; }
            set { left = value; }
        }

        public Foot Right
        {
            get { return right; }
            set { right = value; }
        }

        public bool FootDown
        {
            get { return footDown; }
            set
            {
                footDown = value;
            }
        }
    }
}

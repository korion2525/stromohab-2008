using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALManagedStaticClass;
using System.Windows.Forms;
using System.Threading;

namespace StromoLight_Diagnostics
{
    public class ALSoundEnvironment
    {
        TestSubject person;
        Foot left;
        Foot right;
        Thread update;
        Thread monitor;

        public ALSoundEnvironment(TestSubject person)
        {
            this.person = person;
            left = person.Left;
            right = person.Right;

            ALsetup.SLsetenvironment();
            //if (!ALsetup.SLsetenvironment())
            //{
            //    MessageBox.Show("Sound is not fully functioning!");
            //}
            StartSound();
        }

        private void StartSound()
        {
            float pitch;
            ALsetup.SLplayTone();
            while (ALsetup.SLtoneupdate())
            {
                pitch = (float)Foot.Frontfoot.CurrentMarker.xCoordinate / 100;
                ALsetup.SLchangepitch(pitch);
            }



            //update = new Thread(new ThreadStart(KeepPlaying));
            //monitor = new Thread(new ThreadStart(Monitor));

        }

        private void KeepPlaying()
        {
            while(ALsetup.SLtoneupdate());
        }

        private void Monitor()
        {
            float pitch = (float)Foot.Frontfoot.CurrentMarker.xCoordinate/100;
            ALsetup.SLchangepitch(pitch);
        }

    }
}

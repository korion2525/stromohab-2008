using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StromoLight_RemoteCalibration;

namespace StromoLight_Diagnostics
{
    public class TestSubject
    {
        string name;
        uint age;
        string gender;
        string subjectID;
        Foot left;
        Foot right;
        Calibration calibration;

        public TestSubject()
        {

        }

        public TestSubject(string name, string subjectID, string gender, uint age, Calibration cal)
        {
            this.calibration = cal;
            this.age = age;
            this.name = name;
            this.gender = gender;
            this.subjectID = subjectID;
            if (calibration.LeftFoot_Marker != null)
            {
                left = new Foot("left", calibration.LeftFoot_Marker);
                right = new Foot("right", calibration.RightFoot_Marker);
            }
            else
            {
                left = new Foot("left", calibration.LeftFoot_Trackable);
                right = new Foot("right", calibration.RightFoot_Trackable);
            }
        }


        public Calibration Markercalibration
        {
            get { return calibration; }
            set { calibration = value; }
        }

        public Foot Right
        {
            get { return right; }
            set { right = value; }
        }

        public Foot Left
        {
            get { return left; }
            set { left = value; }
        }

        public string SubjectID
        {
            get { return subjectID; }
            set { subjectID = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public uint Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}

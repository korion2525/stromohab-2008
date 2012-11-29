using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StromoLight_Diagnostics
{
    class SIdata
    {
        int time;
        float symmetryIndex;

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public float SymmetryIndex
        {
            get { return symmetryIndex; }
            set { symmetryIndex = value; }
        }
    }
}

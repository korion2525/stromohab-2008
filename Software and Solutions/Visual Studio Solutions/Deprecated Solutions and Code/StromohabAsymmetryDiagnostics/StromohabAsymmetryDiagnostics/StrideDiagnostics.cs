using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stromohab_Diagnostics
{
    /// <summary>
    /// Class StrideDiagnostics provides properties giving asymmetry data
    /// for the most recently completed a double stride sequence
    /// </summary>
    class StrideDiagnostics
    {
        private double swing_stance_ratio_LEFT = 0;
        private double swing_stance_ratio_RIGHT = 0;
        private double cycle_duration_LEFT = 0;
        private double cycle_duration_RIGHT = 0;
        private double step_interval_duration_LEFT = 0;
        private double step_interval_duration_RIGHT = 0;
        private double double_support_duration_LEFT = 0;
        private double double_support_duration_RIGHT = 0;
        private double swing_stance_SI = 0;
        private double cycle_duration_SI = 0;
        private double step_interval_SI = 0;
        private double double_support_SI = 0;
        private double stride_length = 0;
        private double stride_period = 0;

        public StrideDiagnostics()
        {
            
        }
        /// <summary>
        /// Provides the mean of two consecutive strides in millimeters
        /// </summary>
        public double Stride_Length
        {
            get
            {
                return stride_length;
            }
            set
            {
                stride_length = value;
            }
        }
        /// <summary>
        /// Provides mean of two consecutive stride periods in milliseconds
        /// </summary>
        public double Stride_Period
        {
            get
            {
                return stride_period;
            }
            set
            {
                stride_period = value;
            }
        }
        /// <summary>
        /// Provides %tage swing stance symmetry index
        /// </summary>
        public double Swing_Stance_SI
        {
            get
            {
                return swing_stance_SI;
            }
            set
            {
                swing_stance_SI = value;
            }
        }
        /// <summary>
        /// Provides %tage cycle duration symmetry index
        /// </summary>
        public double Cycle_Duration_SI
        {
            get
            {
                return cycle_duration_SI;
            }
            set
            {
                cycle_duration_SI = value;
            }
        }
        /// <summary>
        /// Provides %tage step interval symmetry index
        /// </summary>
        public double Step_Interval_SI
        {
            get
            {
                return step_interval_SI;
            }
            set
            {
                step_interval_SI = value;
            }
        }
        /// <summary>
        /// Provides %tage double support symmetry index
        /// </summary>
        public double Double_Support_SI
        {
            get
            {
                return double_support_SI;
            }
            set
            {
                double_support_SI = value;
            }
        }

        public double Swing_Stance_Ratio_LEFT
        {
            get
            {
                return swing_stance_ratio_LEFT;
            }
            set
            {
                swing_stance_ratio_LEFT = value;
            }
        }
        public double Swing_Stance_Ratio_RIGHT
        {
            get
            {
                return swing_stance_ratio_RIGHT;
            }
            set
            {
                swing_stance_ratio_RIGHT = value;
            }
        }
        public double Cycle_Duration_LEFT
        {
            get
            {
                return cycle_duration_LEFT;
            }
            set
            {
                cycle_duration_LEFT = value;
            }
        }
        public double Cycle_Duration_RIGHT
        {
            get
            {
                return cycle_duration_RIGHT;
            }
            set
            {
                cycle_duration_RIGHT = value;
            }
        }
        public double Step_Interval_Duration_RIGHT
        {
            get
            {
                return step_interval_duration_RIGHT;
            }
            set
            {
                step_interval_duration_RIGHT = value;
            }
        }
        public double Step_Interval_Duration_LEFT
        {
            get
            {
                return step_interval_duration_LEFT;
            }
            set
            {
                step_interval_duration_LEFT = value;
            }
        }
        public double Double_Support_Duration_RIGHT
        {
            get
            {
                return double_support_duration_RIGHT;
            }
            set
            {
                double_support_duration_RIGHT = value;
            }
        }
        public double Double_Support_Duration_LEFT
        {
            get
            {
                return double_support_duration_LEFT;
            }
            set
            {
                double_support_duration_LEFT = value;
            }
        }
    }
}

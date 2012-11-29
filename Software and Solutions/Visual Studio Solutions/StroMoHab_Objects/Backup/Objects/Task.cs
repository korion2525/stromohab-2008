using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StroMoHab_Objects.Graphics;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a Task and its associated properties
    /// </summary>
    [Serializable]
    public class Task
    {
        #region Member Variables
        private List<OpenGlObject> _objectList = new List<OpenGlObject>();
        private byte[] _taskPreview;
        private string _description = "";
        private int _distance = 0;
        private int _averageSpacing = 0;
        private DistanceRangeType _distanceRange;
        private NumberOfObjectsRangeType _numberOfObjectsRange;        
        private TaskTypeType _taskType = TaskTypeType.Free_Walking;
        private string _fileName = "";

        #endregion

        #region Enum
        /*
         * A note about Enums
         * 
         * use [Description("STRING")] to embed a string with spaces and different chars in that can be extracted and used for comboboxes etc
         * 
         * RANGE Enums:
         * First entry should be 'ALL'
         * All other entrys need a description string and then
         * 
         * xMINVALUE_MAXVALUE
         * 
         * the exception is
         * xMINVALUE
         * this is treaded as MINVALUE -> INFINITY
         * 
         * this is decoded by RangeTypeToMinMax and ensures that by using drop down boxes that get filled in with the description string
         * that only moddifications to the enum types below are required, the GUI will base its text on their content and once that tasks
         * objectlist is updated, the ranges will be re-calculated with the new values. This means if you add more ranges, then you need
         * to re-save the task files.
         * 
         * 
         * */


        /// <summary>
        /// A type to store different task types
        /// </summary>
        public enum TaskTypeType
        {
            All,
            [Description("Free Walking")]
            Free_Walking,
            [Description("Obstacle Avoidance")]
            Obstacle_Avoidance,
            [Description("Video Background")]
            Video_Background
        };
        /// <summary>
        /// A type to store a range of Distances
        /// </summary>
        public enum DistanceRangeType {
            All,
            [Description("Infinite (No Objects)")]
            x0_0,
            [Description("1 - 24 Metres")]
            x1_24,
            [Description("25 - 49 Metres")]
            x25_49,
            [Description("50 - 99 Metres")]
            x50_99,
            [Description("100 - 249 Metres")]
            x100_249,
            [Description("250 - 499 Metres")]
            x250_499,
            [Description("500 - 999 Metres")]
            x500_999,
            [Description("1000+ Metres")]
            x1000,
         };
        /// <summary>
        /// A type to store a range of NumberOfObjects
        /// </summary>
        public enum NumberOfObjectsRangeType
        {
            All,
            [Description("0")]
            x0_0,
            [Description("1 - 9")]
            x1_9,
            [Description("10 - 24")]
            x10_24,
            [Description("25 - 49")]
            x25_49,
            [Description("50 - 99")]
            x5_99,
            [Description("100+")]
            x100,
        }
        #endregion

        #region Methods
        /// <summary>
        /// Extracts the min and max value out of a range type (Distance or NumberOfObjects)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static bool RangeTypeToMinMax(Enum type, out int min, out int max)
        {
            //initialise the out values
            min = 0;
            max = 0;

            // if its the first entry then return false as we don't want to class it as "ALL" as its too general
            if (type.ToString() == "All")
                return false;


            string str = type.ToString().Substring(1); // Get all chars after the first char (as the first char is 'x')

            string split = "_"; // break up the ranges char

            //split into 1 or 2 numbers
            string[] minmax = str.Split(split.ToCharArray());

            //something is wrong so return
            if (minmax.Length == 0)
                return false;
            // there is only one value so we count it as a value->infinity range
            else if (minmax.Length == 1)
            {
                min = Int32.Parse(minmax[0]);
                max = -1;
                return true;
            }//there are two values so we have a min and a max
            else if (minmax.Length == 2)
            {
                min = Int32.Parse(minmax[0]);
                max = Int32.Parse(minmax[1]);
                return true;
            }

            //should never get here but if we do something went wrong
            return false;

        }
        /// <summary>
        /// Recalculates the distance and average spacing and rangers
        /// </summary>
        private void UpdateFields()
        {
            //find the distance of the furthes object
            int dist = 0;
            for (int i = 1; i < _objectList.Count; i++)
            {
                if ((-_objectList[i].ZMax) > dist)
                    dist = (int)-_objectList[i].ZMax;
            }
            _distance = dist;

            ///calculate the average spacing
            int spacing = 0;
            if (NumberOfObjects != 0)
            {
                if (dist != 0)
                {
                    spacing = dist / NumberOfObjects;
                }
            }
            _averageSpacing = spacing;

            FillOutDistanceRange();
            FillOutNumberOfObjectsRange();

        }
        /// <summary>
        /// Finds the number of objects range
        /// </summary>
        private void FillOutNumberOfObjectsRange()
        {
            int min = 0;
            int max = 0;
            foreach (Task.NumberOfObjectsRangeType type in Enum.GetValues(typeof(Task.NumberOfObjectsRangeType)))
            {
                //go through the types until a valid min max value is found
                if (Task.RangeTypeToMinMax(type, out min, out max))
                {
                    //if max = -1 then it is a MINVALUE - > Infinity range
                    if (max == -1)
                    {
                        //if it fits then save the type and break
                        if (NumberOfObjects >= min)
                        {
                            _numberOfObjectsRange = type;
                            break;
                        }
                    }//else its a min and max
                    else
                    {
                        //if it fits then save the type and break
                        if (NumberOfObjects <= max && NumberOfObjects >= min)
                        {
                            _numberOfObjectsRange = type;
                            break;
                        }
                    }

                }
            }
        }
        /// <summary>
        /// finds the distance range
        /// </summary>
        private void FillOutDistanceRange()
        {
            int min = 0;
            int max = 0;
            //go through the types until a valid min max value is found
            foreach (Task.DistanceRangeType type in Enum.GetValues(typeof(Task.DistanceRangeType)))
            {
                if (Task.RangeTypeToMinMax(type, out min, out max))
                {
                    if (max == -1)//if max = -1 then it is a MINVALUE - > Infinity range
                    {
                        if (Distance >= min)
                        {
                            _distanceRange = type;
                            return;
                        }
                    }
                    else //minvalue - maxvalue
                    {
                        //if it fits then save the type and break
                        if (Distance <= max && Distance >= min)
                        {
                            _distanceRange = type;
                            return;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Gets the description for the given enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null)
                return null;
            //transform the byte array into an image
            MemoryStream ms = new MemoryStream(byteArray);
            Image img = Image.FromStream(ms);
            // Do NOT close the stream!

            return img;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the range of distances that this task falls in
        /// </summary>
        public DistanceRangeType DistanceRange
        {
            get { return _distanceRange; }
        }
        /// <summary>
        /// Gets the range of number of objects that this task falls in
        /// </summary>
        public NumberOfObjectsRangeType NumberOfObjectsRange
        {
            get { return _numberOfObjectsRange; }
        }

        /// <summary>
        /// Gets or sets the type of task
        /// </summary>
        public TaskTypeType TaskType
        {
            get { return _taskType; }
            set { _taskType = value; }
        }
        /// <summary>
        /// Number of objects in the task excluding the corridor
        /// </summary>
        public int NumberOfObjects
        {

            get
            {
                if (_objectList.Count > 0)
                    return _objectList.Count - 1;
                else
                    return 0;

            }
        }
        /// <summary>
        /// Gets the average distance between objects (distance/number of objects)
        /// </summary>
        public int AverageDistanceBetweenObjects
        {
            get
            {
                return _averageSpacing;
            }
        }

        /// <summary>
        /// The distance in meters from the start of the task to the last object
        /// </summary>
        public int Distance
        {
            get
            {
                return _distance;
            }
        }
        /// <summary>
        /// Byte arrary containing a preview of the task
        /// </summary>
        public byte[] PreviewImage
        {
            get { return _taskPreview; }
            set { _taskPreview = value; }
        }
        /// <summary>
        /// The list of objects
        /// </summary>
        public List<OpenGlObject> ObjectList
        {
            get { return _objectList; }
            set
            {
                _objectList = value;
                // after the object list has been changed re-evalue files (distance, type, ranges etc)
                UpdateFields();
            }
            
        }

        /// <summary>
        /// Decrsiption of the task
        /// </summary>
        public string TaskDescription
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// The file name
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        /// <summary>
        /// An image containing a preview of the task
        /// </summary>
        public Image PreviewImageAsImage
        {
            get { return ByteArrayToImage(_taskPreview); }
        }
        #endregion

    }
}

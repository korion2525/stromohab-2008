using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Objects
{
    [Serializable]
    public class Address
    {
        private string _line1 = "";
        private string _line2 = "";
        private string _line3 = "";
        private string _town = "";
        private string _county = "";
        private Postcode _postcode = new Postcode();

        /// <summary>
        /// Postcode
        /// </summary>
        public Postcode Postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }
        /// <summary>
        /// First line
        /// </summary>
        public string Line1
        {
            get { return _line1; }
            set { _line1 = value; }
        }
        /// <summary>
        /// Second line
        /// </summary>
        public string Line2
        {
            get { return _line2; }
            set { _line2 = value; }
        }
        /// <summary>
        /// Third line
        /// </summary>
        public string Line3
        {
            get { return _line3; }
            set { _line2 = value; }
        }
        /// <summary>
        /// Town
        /// </summary>
        public string Town
        {
            get { return _town; }
            set { _town = value; }
        }
        /// <summary>
        /// County
        /// </summary>
        public string County
        {
            get { return _county; }
            set { _county = value; }
        }
        

    }
}

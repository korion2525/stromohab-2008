using System;
using System.Text.RegularExpressions;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Stores Valid UK Landline and Mobile Numbers
    /// </summary>
    [Serializable]
    public class ContactNumber
    {
        const string _landLineRegex = "(\\s*\\(?0\\d{4}\\)?\\s*\\d{6}\\s*)|(\\s*\\(?0\\d{3}\\)?\\s*\\d{3}\\s*\\d{4}\\s*)";
        const string _mobileRegex = "^(\\+44\\s?7\\d{3}|\\(?07\\d{3}\\)?)\\s?\\d{3}\\s?\\d{3}$";
        string _number = "";

        /// <summary>
        /// Trys to parse the number
        /// </summary>
        /// <param name="value">The string to store as a phone number</param>
        /// <returns>True if valid, false if not</returns>
        public bool TryParse(string value)
        {
            //remove chars
            value = value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ","").Trim();

            //try to match to landline
            if(Regex.Match(value,_landLineRegex).Success)
            {
                _number = value;
                return true;
            }
                //try to match to mobile
            else if (Regex.Match(value, _mobileRegex).Success)
            {
                _number = value;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the Validated Phone Number
        /// </summary>
        public string Number
        {
            get{ return _number;}
            
        }
    }
}

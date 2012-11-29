using System;  
using System.Collections.Generic;  
using System.Text;  
using System.Text.RegularExpressions;

namespace StroMoHab_Objects.Objects  
{  
    /// <summary>  
    /// Represents a United Kingdom postcode.  
    /// </summary>  
    /// <remarks>  
    /// For more details, see http://ianfnelson.com/blog/archive/2007/05/29/PostcodeStruct.aspx  
    /// </remarks>  
    [Serializable()]  
    public struct Postcode  
    {  
        private static string regexBS7666Outer =   
            "(?<outCode>[A-PR-UWYZ]" +   
            "([0-9]{1,2}|([A-HK-Y][0-9]|[A-HK-Y][0-9]([0-9]|[ABEHMNPRV-Y]))|[0-9][A-HJKS-UW]))";  
        private static string regexBS7666Inner = "(?<inCode>[0-9][ABD-HJLNP-UW-Z]{2})";  
        private static string regexBS7666Full = regexBS7666Outer + regexBS7666Inner;  
        private static string regexBS7666OuterStandAlone = string.Concat(regexBS7666Outer + "\\s*$");  
        private static string regexBfpoOuter = "(?<outCode>BFPO)";  
        private static string regexBfpoInner = "(?<inCode>[0-9]{1,3})";  
        private static string regexBfpoFull = regexBfpoOuter + regexBfpoInner;  
        private static string regexBfpoOuterStandalone = string.Concat(regexBfpoOuter + "\\s*$");  
        private static string[,] exceptionsToTheRule =   
        {  
            {"GIR","0AA"},      // Girobank  
            {"SAN","TA1"},      // Santa Claus  
            {"ASCN","1ZZ"},     // Ascension Island  
            {"BIQQ","1ZZ"},     // British Antarctic Territory  
            {"BBND","1ZZ"},     // British Indian Ocean Territory  
            {"FIQQ", "1ZZ"},    // Falkland Islands  
            {"PCRN", "1ZZ"},    // Pitcairn Islands  
            {"STHL", "1ZZ"},    // Saint Helena  
            {"SIQQ", "1ZZ"},    // South Georgia and the Sandwich Islands  
            {"TDCU", "1ZZ"},    // Tristan da Cunha  
            {"TKCA", "1ZZ"}     // Turks and Caicos Islands  
        };  
  
        private string _outCode;  
        private string _inCode;  
  
        /// <summary>  
        /// Outer portion of the Postcode  
        /// </summary>  
        public string OutCode  
        {  
            get { return _outCode; }  
            private set { _outCode = value; }  
        }  
  
        /// <summary>  
        /// Inner portion of the Postcode  
        /// </summary>  
        public string InCode  
        {  
            get { return _inCode; }  
            private set { _inCode = value; }  
        }  
  
        /// <summary>  
        /// Parses a string as a Postcode.  
        /// </summary>  
        /// <param name="s">String to be parsed</param>  
        /// <returns>Postcode object</returns>  
        /// <exception cref="FormatException">  
        /// If the passed string cannot be parsed as a UK postcode  
        /// </exception>  
        /// <remarks>Using this overload, the inner code is not mandatory.</remarks>  
        public static Postcode Parse(string s)  
        {  
            return Parse(s, false);  
        }  
  
        /// <summary>  
        /// Parses a string as a Postcode.  
        /// </summary>  
        /// <param name="s">String to be parsed</param>  
        /// <param name="incodeMandatory">  
        /// Indicates that the string passed must include a valid inner code.  
        /// </param>  
        /// <returns>Postcode object</returns>  
        /// <exception cref="FormatException">  
        /// If the passed string cannot be parsed as a UK postcode  
        /// </exception>  
        public static Postcode Parse(string s, bool incodeMandatory)  
        {  
            Postcode p = new Postcode();  
            if (TryParse(s, out p, incodeMandatory))  
                return p;  
  
            throw new FormatException();  
        }  
  
        /// <summary>  
        /// Attempts to parse a string as a Postcode.  
        /// </summary>  
        /// <param name="s">String to be parsed</param>  
        /// <param name="result">Postcode object</param>  
        /// <returns>  
        /// Boolean indicating whether the string was successfully parsed as a UK Postcode  
        /// </returns>  
        /// <remarks>Using this overload, the inner code is not mandatory.</remarks>  
        public static bool TryParse(string s, out Postcode result)  
        {  
            return TryParse(s, out result, false);  
        }  
  
        /// <summary>  
        /// Attempts to parse a string as a Postcode.  
        /// </summary>  
        /// <param name="s">String to be parsed</param>  
        /// <param name="result">Postcode object</param>  
        /// <param name="incodeMandatory">  
        /// Indicates that the string passed must include a valid inner code.  
        /// </param>  
        /// <returns>  
        /// Boolean indicating whether the string was successfully parsed as a UK Postcode  
        /// </returns>  
        public static bool TryParse(string s, out Postcode result, bool incodeMandatory)  
        {  
            // Set output to new Postcode  
            result = new Postcode();  
  
            // Copy the input before messing with it  
            string input = s;  
  
            // Guard clause - check for null or empty string  
            if (string.IsNullOrEmpty(input)) return false;  
  
            // uppercase input and strip undesirable characters  
            input = Regex.Replace(input.ToUpperInvariant(), "[^A-Z0-9]", string.Empty);  
  
            // guard clause - input is more than seven characters  
            if (input.Length > 7) return false;  
 
            #region BS7666 Matching  
  
            // Try to match full standard postcode  
            Match fullMatch = Regex.Match(input, regexBS7666Full);  
            if (fullMatch.Success)  
            {  
                result.OutCode = fullMatch.Groups["outCode"].Value;  
                result.InCode = fullMatch.Groups["inCode"].Value;  
                return true;  
            }  
  
            // Try to match outer standard postcode only  
            Match outerMatch = Regex.Match(input, regexBS7666OuterStandAlone);  
            if (outerMatch.Success)  
            {  
                if (incodeMandatory) return false;  
  
                result.OutCode = outerMatch.Groups["outCode"].Value;  
                return true;  
            }  
 
            #endregion  
 
            #region BFPO Matching  
  
            // Try to match full BFPO postcode  
            Match bfpoFullMatch = Regex.Match(input, regexBfpoFull);  
            if (bfpoFullMatch.Success)  
            {  
                result.OutCode = bfpoFullMatch.Groups["outCode"].Value;  
                result.InCode = bfpoFullMatch.Groups["inCode"].Value;  
                return true;  
            }  
  
            // Try to match outer BFPO postcode  
            Match bfpoOuterMatch = Regex.Match(input, regexBfpoOuterStandalone);  
            if (bfpoOuterMatch.Success)  
            {  
                if (incodeMandatory) return false;  
  
                result.OutCode = bfpoOuterMatch.Groups["outCode"].Value;  
                return true;  
            }  
 
            #endregion  
 
            #region Exceptions to the rule matching  
  
            // Loop through exceptions to the rule  
            for (int i = 0; i < exceptionsToTheRule.GetLength(0); i++)  
            {  
                // Check for a full match  
                if (input == string.Concat(exceptionsToTheRule[i, 0], exceptionsToTheRule[i, 1]))  
                {  
                    result.OutCode = exceptionsToTheRule[i,0];  
                    result.InCode = exceptionsToTheRule[i,1];  
                    return true;  
                }  
  
                // Check for partial match only  
                if (input == exceptionsToTheRule[i,0])  
                {  
                    if (incodeMandatory) return false;  
  
                    result.OutCode = exceptionsToTheRule[i,0];  
                    return true;  
                }  
            }  
 
            #endregion  
  
            return false;  
        }  
  
        /// <summary>  
        /// Returns a string representation of this postcode  
        /// </summary>  
        /// <returns></returns>  
        public override string ToString()  
        {  
            if (string.IsNullOrEmpty(InCode))  
            {  
                return OutCode;  
            }  
            else  
            {  
                return string.Concat(OutCode, " ", InCode);  
            }  
        }  
    }  
}  
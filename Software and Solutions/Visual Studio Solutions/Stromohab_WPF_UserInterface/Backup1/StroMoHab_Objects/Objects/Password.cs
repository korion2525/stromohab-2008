using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Taken from http://www.aspheute.com/english/20040105.asp
    /// </summary>
    public class Password
    {
        private string _password;
        private int _salt;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strPassword"></param>
        /// <param name="nSalt"></param>
        public Password(string strPassword, int nSalt)
        {
            _password = strPassword;
            _salt = nSalt;
        }

        /// <summary>
        /// Generates a new random password
        /// </summary>
        /// <param name="PasswordLength"></param>
        /// <returns></returns>
        public static string CreateRandomPassword(int PasswordLength)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }
        /// <summary>
        /// Generates a random salt
        /// </summary>
        /// <returns></returns>
        public static int CreateRandomSalt()
        {
            Byte[] _saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
                (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }
        /// <summary>
        /// Calculates the salted hash
        /// </summary>
        /// <returns></returns>
        public string ComputeSaltedHash()
        {
            // Create Byte array of password string
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] _secretBytes = encoder.GetBytes(_password);

            // Create a new salt
            Byte[] _saltBytes = new Byte[4];
            _saltBytes[0] = (byte)(_salt >> 24);
            _saltBytes[1] = (byte)(_salt >> 16);
            _saltBytes[2] = (byte)(_salt >> 8);
            _saltBytes[3] = (byte)(_salt);

            // append the two arrays
            Byte[] toHash = new Byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);

            SHA1 sha1 = SHA1.Create();
            Byte[] computedHash = sha1.ComputeHash(toHash);

            return encoder.GetString(computedHash);
        }
    }
}

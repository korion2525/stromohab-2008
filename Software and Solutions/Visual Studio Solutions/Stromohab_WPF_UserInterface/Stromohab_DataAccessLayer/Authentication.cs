using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics.Contracts;


namespace Stromohab_DataAccessLayer
{
    public static class Authentication
    {
        public static bool AuthenticateUser(string userName, string password)
        {
            Contract.Requires(password!=null);
            Contract.Requires(userName!=null);
            string hashedPassword = Md5Hash(password);
            
            stromohabDevEntities db = new stromohabDevEntities();

            var authenticatedClinicians = from p in db.clinicians
                                          where p.cUserName == userName && p.cPassword == hashedPassword
                                          select p.cUserName;

            return authenticatedClinicians.Count() == 1;
        }

        private static string Md5Hash(string passwordToHash)
        {
            Contract.Requires(passwordToHash != null);
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(passwordToHash);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            md5.Dispose();
            return sb.ToString();
        }
    }
}

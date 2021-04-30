using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SistemaVentas.Helpers
{
    public class UserHelper
    {
        public static string EncriptarPassword(string pass)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var textSHA = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));
                var sb = new StringBuilder(textSHA.Length * 2);
                foreach (byte b in textSHA)
                {
                    sb.Append(b.ToString());
                }

                return sb.ToString();
            }
        }
    }
}
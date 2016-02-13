using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Helper
{
    public static class HashGenerator
    {
        public static string ComputeSHA(string str, string key)
        {
            byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(str);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(key);
            var crypt = new HMACSHA512(saltBytes);

            return BitConverter.ToString(crypt.ComputeHash(passwordBytes)).Replace("-", "");
        }

        public static bool VerifySHA(string str, string key, string hash)
        {
            return String.Equals(ComputeSHA(str, key), hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}

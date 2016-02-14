using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Text.Encoding;

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

        public static string HashMD5(string str)
        {
            MD5 md5 = MD5.Create(str);

            byte[] inputBytes = ASCII.GetBytes(str);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool VerifyMd5(string str, string hash)
        {
            return  string.Equals(HashMD5(str), hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}

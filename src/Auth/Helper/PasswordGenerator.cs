using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Helper
{
    public static  class PasswordServie
    {

        /// <summary>
        /// Хэшируем пароль
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password, string salt)
        {
            return HashGenerator.ComputeSHA(password, salt);
        }


        /// <summary>
        /// Функция сравнения пароль и хэша
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static bool ComparePassword(string password, string hash, string salt)
        {
            return !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(hash) && HashGenerator.VerifySHA(password, salt, hash);
        }


    }
}

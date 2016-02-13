using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Helper
{
    public static  class SaltGenerator
    {
        public static string GenerateSalt(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                // Генерируем число являющееся латинским символом в юникоде
                ch = RandomChar(random.NextDouble());
                // Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static char RandomChar(double value)
        {
            return Convert.ToChar(Convert.ToInt32(Math.Floor(26 * value + 65)));
        }
    }
}

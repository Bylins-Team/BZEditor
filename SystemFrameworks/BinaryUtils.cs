using System;

namespace SystemFrameworks
{
    /// <summary>
    /// Содержит вспомогательные методы для работы двоисным форматом
    /// </summary>
    public static class BinaryUtils
    {
        public static string NumberToBinary(int number, int bitsLength = 9)
        {
            string result = Convert.ToString(number, 2).PadLeft(bitsLength, '0');
            return result;
        }

        public static int BinaryToInt(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }
    }
}

using System;

namespace SystemFrameworks
{ 
    /// <summary>
    /// Содержит вспомогательные методы для работы со строками
    /// </summary>
    public static class StringUtils
    {
        public static string ReplaceFirstOccurrance(string original, string oldValue, string newValue)
        {
            if (String.IsNullOrEmpty(original))
                return String.Empty;
            if (String.IsNullOrEmpty(oldValue))
                return original;
            if (String.IsNullOrEmpty(newValue))
                newValue = String.Empty;
            int loc = original.IndexOf(oldValue, StringComparison.Ordinal);
            return original.Remove(loc, oldValue.Length).Insert(loc, newValue);
        }

        /// <summary>
        /// Проверка, является ли содержимое строки целым числом без знака "-"
        /// </summary>
        /// <param name="target">Проверяемая строка</param>
        public static bool IsUnsignedNumber(string target)
        {
            if (string.IsNullOrEmpty(target))
                return false;
            foreach (char t in target)
            {
                if (!char.IsNumber(t))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка, является ли содержимое строки целым числом без знака "-"
        /// </summary>
        /// <param name="target">Проверяемая строка</param>
        public static bool IsUnsignedDecimal(string target)
        {
            bool hasDecimal = false;
            foreach (char chr in target)
            {
                // Наличие дробной части
                if (chr == '.' || chr == ',')
                {
                    if (hasDecimal) // второй десятичный разделитель в строке
                        return false;

                    // найден первый десятичный разделитель
                    hasDecimal = true;
                    continue;
                }
                // если символ не цифра
                if (!char.IsNumber(chr))
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Преобразует строку в целое беззнаковое число
        /// </summary>
        /// <param name="value">Строковое представление целого числа</param>
        /// <returns>Целое число</returns>
        public static byte ToUnsignedByteFast(string value)
        {
            byte result = 0;
            foreach (char chr in value)
            {
                if (!char.IsNumber(chr))
                {
                    throw new InvalidCastException("Ошибка преобразования в byte");
                }
                result = (byte)(10 * result + (chr - 48));
            }
            return result;
        }

        /// <summary>
        /// Преобразует строку в целое беззнаковое число
        /// </summary>
        /// <param name="value">Строковое представление целого числа</param>
        /// <returns>Целое число</returns>
        public static short ToUnsignedShortFast(string value)
        {
            short result = 0;
            foreach (char chr in value)
            {
                if (!char.IsNumber(chr))
                {
                    throw new InvalidCastException("Ошибка преобразования в integer");
                }
                result = (short)(10 * result + (chr - 48));
            }
            return result;
        }

        /// <summary>
        /// Преобразует строку в целое беззнаковое число
        /// </summary>
        /// <param name="value">Строковое представление целого числа</param>
        /// <returns>Целое число</returns>
        public static int ToUnsignedIntFast(string value)
        {
            int result = 0;
            foreach (char chr in value)
            {
                if (!char.IsNumber(chr))
                {
                    throw new InvalidCastException("Ошибка преобразования в integer");
                }
                result = 10 * result + (chr - 48);
            }
            return result;
        }

        /// <summary>
        /// Преобразует строку в целое число
        /// </summary>
        /// <param name="value">Строковое представление целого числа</param>
        /// <returns>Целое число</returns>
        public static int ToIntFast(string value)
        {
            int result = 0;
            int sign = 1;
            for (var chr = 0; chr < value.Length; chr++)
            {
                if (!char.IsNumber(value[chr]))
                {
                    if (value[chr] == '-' && chr == 0)
                    {
                        sign = -1;
                        continue;
                    }
                    throw new InvalidCastException("Ошибка преобразования в integer");
                }

                result = 10 * result + (value[chr] - 48);
            }
            result *= sign;
            return result;
        }

        /// <summary>
        /// Преобразует строку в большое целое беззнаковое число
        /// </summary>
        /// <param name="value">Строковое представление целого числа</param>
        /// <returns>Большое целое число</returns>
        public static long ToUnsignedLongFast(string value)
        {
            long result = 0;
            foreach (char chr in value)
            {
                if (!char.IsNumber(chr))
                {
                    throw new InvalidCastException("Ошибка преобразования в integer");
                }
                result = 10 * result + (chr - 48);
            }
            return result;
        }
    }
}

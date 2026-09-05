using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtils.Resolvers
{
    public static class EncodingResolver
    {
        // Если у файла кодировка любая кроме UTF-8 или KOI-8 - будет кинут exception.
        public static Encoding GetFileEncoding(string filePath)
        {
            // Pass 'true' to detectEncodingFromByteOrderMarks
            using (var reader = new StreamReader(filePath, Encoding.Default, true))
            {
                reader.Peek();
                return reader.CurrentEncoding;
            }
        }
    }
}

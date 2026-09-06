using System;
using System.IO;
using System.Text;

namespace DataUtils
{
    /// <summary>
    /// Detects the encoding of a text file by content, without relying on a BOM
    /// (the world files never carry one).
    ///
    /// UTF-8 is the expected encoding: if the whole file decodes as valid UTF-8 it is
    /// reported as <see cref="Utf8NoBom"/>. Otherwise the file is assumed to be written
    /// by an older editor build and koi8-r is reported as a fallback.
    ///
    /// Pure ASCII files are valid UTF-8, so they resolve to UTF-8 - which is harmless,
    /// because ASCII is encoded identically in both encodings.
    /// </summary>
    public static class FileEncodingResolver
    {
        /// <summary>
        /// UTF-8 without a byte order mark - safe both for reading and for writing.
        /// </summary>
        public static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

        /// <summary>
        /// Encoding used for files written before the switch to UTF-8.
        /// </summary>
        public static readonly Encoding Fallback = Encoding.GetEncoding("koi8-r");

        private const int BufferSize = 64 * 1024;

        /// <summary>
        /// Returns the encoding of the given text file: UTF-8 when its bytes form a valid
        /// UTF-8 sequence, koi8-r otherwise.
        /// </summary>
        /// <exception cref="ArgumentException">File name is null or empty.</exception>
        /// <exception cref="IOException">File cannot be read.</exception>
        public static Encoding Resolve(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name must not be empty", "fileName");

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, BufferSize))
                return Resolve(stream);
        }

        /// <summary>
        /// Returns the encoding of the text in the given stream, reading it from the
        /// current position to the end. The stream position is not restored.
        /// </summary>
        public static Encoding Resolve(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var buffer = new byte[BufferSize];
            var validator = new Utf8Validator();
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (!validator.Feed(buffer, read))
                    return Fallback;
            }
            return validator.IsComplete ? Utf8NoBom : Fallback;
        }

        /// <summary>
        /// Returns the encoding of the given text bytes.
        /// </summary>
        public static Encoding Resolve(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            var validator = new Utf8Validator();
            return validator.Feed(bytes, bytes.Length) && validator.IsComplete ? Utf8NoBom : Fallback;
        }

        /// <summary>
        /// Reads the given text file and returns its content, decoding it with the
        /// encoding reported by <see cref="Resolve(string)"/>: UTF-8 when the bytes form
        /// a valid UTF-8 sequence, koi8-r otherwise.
        /// </summary>
        /// <exception cref="ArgumentException">File name is null or empty.</exception>
        /// <exception cref="IOException">File cannot be read.</exception>
        public static string ReadAllText(string fileName)
        {
            Encoding encoding;
            return ReadAllText(fileName, out encoding);
        }

        /// <summary>
        /// Same as <see cref="ReadAllText(string)"/>, additionally reporting the encoding
        /// the file was decoded with - for callers that have to write the file back.
        /// </summary>
        public static string ReadAllText(string fileName, out Encoding encoding)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name must not be empty", "fileName");

            var bytes = File.ReadAllBytes(fileName);
            encoding = Resolve(bytes);
            return StripBom(encoding.GetString(bytes));
        }

        /// <summary>
        /// The world files carry no BOM, but a file touched by an outside editor may:
        /// GetString keeps it as a leading U+FEFF, which would end up in the data.
        /// </summary>
        private static string StripBom(string text)
        {
            return text.Length > 0 && text[0] == '\uFEFF' ? text.Substring(1) : text;
        }

        /// <summary>
        /// Incremental UTF-8 well-formedness check. Keeps the state of a partially read
        /// multi-byte sequence, so it works across buffer boundaries. Overlong forms,
        /// surrogate code points (U+D800..U+DFFF) and values above U+10FFFF are rejected.
        /// </summary>
        private sealed class Utf8Validator
        {
            private int _pending;      // continuation bytes still expected
            private int _codePoint;    // code point accumulated so far
            private int _minCodePoint; // smallest value the started sequence may encode

            /// <summary>
            /// true when no multi-byte sequence is left half-read.
            /// </summary>
            public bool IsComplete
            {
                get { return _pending == 0; }
            }

            /// <summary>
            /// Feeds the first <paramref name="count"/> bytes of the buffer.
            /// Returns false as soon as the data cannot be UTF-8.
            /// </summary>
            public bool Feed(byte[] buffer, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    int b = buffer[i];

                    if (_pending > 0)
                    {
                        if ((b & 0xC0) != 0x80)
                            return false;
                        _codePoint = (_codePoint << 6) | (b & 0x3F);
                        if (--_pending > 0)
                            continue;
                        if (_codePoint < _minCodePoint || _codePoint > 0x10FFFF)
                            return false;
                        if (_codePoint >= 0xD800 && _codePoint <= 0xDFFF)
                            return false;
                        continue;
                    }

                    if (b < 0x80)
                        continue;

                    if ((b & 0xE0) == 0xC0)
                    {
                        _pending = 1;
                        _codePoint = b & 0x1F;
                        _minCodePoint = 0x80;
                    }
                    else if ((b & 0xF0) == 0xE0)
                    {
                        _pending = 2;
                        _codePoint = b & 0x0F;
                        _minCodePoint = 0x800;
                    }
                    else if ((b & 0xF8) == 0xF0)
                    {
                        _pending = 3;
                        _codePoint = b & 0x07;
                        _minCodePoint = 0x10000;
                    }
                    else
                    {
                        return false; // continuation byte out of sequence or 5/6-byte form
                    }
                }
                return true;
            }
        }
    }
}

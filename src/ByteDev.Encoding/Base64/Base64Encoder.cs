using System;
using System.Text;

namespace ByteDev.Encoding.Base64
{
    /// <summary>
    /// Represents a base 64 encoder/decoder.
    /// </summary>
    public class Base64Encoder : IEncoder
    {
        /// <summary>
        /// Encode a UTF8 encoded string to base 64.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>Base 64 string.</returns>
        public string Encode(string value)
        {
            return Encode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Encode a string to base 64.
        /// </summary>
        /// <param name="value">The string to encode./</param>
        /// <param name="encoding">The encoding of <paramref name="value" />.</param>
        /// <returns>Base 64 string.</returns>
        public string Encode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);

            return Encode(bytes);
        }

        /// <summary>
        /// Encode an array of bytes to base 64.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Base 64 string.</returns>
        public string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodes a string from base 64 to UTF-8
        /// </summary>
        /// <param name="value">The base 64 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value)
        {
            return Decode(value, new UTF8Encoding());
        }
        
        /// <summary>
        /// Decodes a string from base 64 to <paramref name="encoding" />.
        /// </summary>
        /// <param name="value">The base 64 string to decode.</param>
        /// <param name="encoding">The target encoding.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = DecodeToBytes(value);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Decodes a string from base 64 to byte array.
        /// </summary>
        /// <param name="value">The base 64 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public byte[] DecodeToBytes(string value)
        {
            return Convert.FromBase64String(value);
        }

        /// <summary>
        /// Calculates the base 64 size of content based on it's size in bytes. 
        /// </summary>
        /// <param name="originalSizeInBytes">The original size of the content in bytes.</param>
        /// <returns>The size of the content if encoded to base 64.</returns>
        public static long CalcBase64EncodedSize(long originalSizeInBytes)
        {
            return 4 * (int)Math.Ceiling(originalSizeInBytes / 3.0);
        }

        /// <summary>
        /// Calculates the original size in bytes of a base 64 string.
        /// </summary>
        /// <param name="base64">Base 64 string.</param>
        /// <returns>The size in bytes of the original string.</returns>
        public static long CalcOriginalSize(string base64)
        {
            if (base64 == null)
                return 0;

            return base64.RemoveBase64EndPadding().Length * 3 / 4;
        }
    }
}

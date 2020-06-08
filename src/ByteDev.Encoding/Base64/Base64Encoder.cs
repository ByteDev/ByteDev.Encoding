using System;
using System.Text;

namespace ByteDev.Encoding.Base64
{
    /// <summary>
    /// Represents a base64 encoder/decoder.
    /// </summary>
    public class Base64Encoder : IEncoder
    {
        /// <summary>
        /// Encode a UTF8 encoded string to base64.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>Base64 string.</returns>
        public string Encode(string value)
        {
            return Encode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Encode a string to base64.
        /// </summary>
        /// <param name="value">The string to encode./</param>
        /// <param name="encoding">The encoding of <paramref name="value" />.</param>
        /// <returns>Base64 string.</returns>
        public string Encode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);

            return Encode(bytes);
        }

        /// <summary>
        /// Encode an array of bytes to base64.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Base64 string.</returns>
        public string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodes a string from base64 to UTF-8
        /// </summary>
        /// <param name="value">The base64 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value)
        {
            return Decode(value, new UTF8Encoding());
        }
        
        /// <summary>
        /// Decodes a string from base64 to <paramref name="encoding" />.
        /// </summary>
        /// <param name="value">The base64 string to decode.</param>
        /// <param name="encoding">The target encoding.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = DecodeToBytes(value);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Decodes a string from base64 to byte array.
        /// </summary>
        /// <param name="value">The base64 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public byte[] DecodeToBytes(string value)
        {
            return Convert.FromBase64String(value);
        }

        /// <summary>
        /// Calculates the potential base64 size of content. 
        /// </summary>
        /// <param name="originalSizeInBytes">The original size of the content.</param>
        /// <returns>The size of the content if encoded to base64.</returns>
        public static long CalcBase64EncodedSize(long originalSizeInBytes)
        {
            return 4 * (int)Math.Ceiling(originalSizeInBytes / 3.0);
        }
    }
}

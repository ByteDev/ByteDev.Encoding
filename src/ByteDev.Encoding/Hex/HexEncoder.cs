using System;
using System.Globalization;
using System.Text;

namespace ByteDev.Encoding.Hex
{
    /// <summary>
    /// Represents a hexadecimal encoder/decoder.
    /// </summary>
    public class HexEncoder : IEncoder
    {
        /// <summary>
        /// Encode a UTF8 encoded string to hexadecimal.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>Hexadecimal string.</returns>
        public string Encode(string value)
        {
            return Encode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Encode a string to hexadecimal.
        /// </summary>
        /// <param name="value">The string to encode./</param>
        /// <param name="encoding">The encoding of <paramref name="value" />.</param>
        /// <returns>Hexadecimal string.</returns>
        public string Encode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);

            return Encode(bytes);
        }

        /// <summary>
        /// Encode an array of bytes to hexadecimal.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Hexadecimal string.</returns>
        public string Encode(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        /// <summary>
        /// Decodes a string from hexadecimal to UTF-8
        /// </summary>
        /// <param name="hex">The hexadecimal string to decode.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string hex)
        {
            return Decode(hex, new UTF8Encoding());
        }
        
        /// <summary>
        /// Decodes a string from hexadecimal to <paramref name="encoding" />.
        /// </summary>
        /// <param name="hex">The hexadecimal string to decode.</param>
        /// <param name="encoding">The target encoding.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string hex, System.Text.Encoding encoding)
        {
            byte[] bytes = DecodeToBytes(hex);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Decodes a string from hexadecimal to byte array.
        /// </summary>
        /// <param name="hex">The hexadecimal string to decode.</param>
        /// <returns>The decoded string.</returns>
        public byte[] DecodeToBytes(string hex)
        {
            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }

            return bytes;
        }
    }
}
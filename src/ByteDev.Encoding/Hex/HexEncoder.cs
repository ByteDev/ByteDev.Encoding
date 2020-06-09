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
        private readonly char? _delimiter;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Encoding.Hex.HexEncoder" /> class.
        /// </summary>
        public HexEncoder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Encoding.Hex.HexEncoder" /> class.
        /// </summary>
        /// <param name="delimiter">Delimiter between hex values.</param>
        public HexEncoder(char delimiter)
        {
            if (delimiter.IsHex())
            {
                throw new ArgumentException("Delimiter was a reserved hex character.");
            }

            _delimiter = delimiter;
        }

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="encoding" /> is null.</exception>
        public string Encode(string value, System.Text.Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            byte[] bytes = encoding.GetBytes(value);

            return Encode(bytes);
        }

        /// <summary>
        /// Encode an array of bytes to hexadecimal.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Hexadecimal string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="bytes" /> is null.</exception>
        public string Encode(byte[] bytes)
        {
            var hex = BitConverter.ToString(bytes);

            if (_delimiter.HasValue)
                return hex.Replace('-', _delimiter.Value);

            return hex.Replace("-", string.Empty);    
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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="encoding" /> is null.</exception>
        public string Decode(string hex, System.Text.Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            byte[] bytes = DecodeToBytes(hex);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Decodes a string from hexadecimal to byte array.
        /// </summary>
        /// <param name="hex">The hexadecimal string to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="hex" /> is null.</exception>
        public byte[] DecodeToBytes(string hex)
        {
            if (hex == null)
                throw new ArgumentNullException(nameof(hex));

            var bytes = CreateByteArrayFor(hex);

            var index = 0;

            for (var i = 0; i < hex.Length; i++)
            {
                if (_delimiter.HasValue)
                {
                    if ((i + 1).IsMultipleOf(3))
                        continue;
                }

                bytes[index] = byte.Parse(hex.Substring(i, 2), NumberStyles.HexNumber);
                index++;
                i++;
            }

            return bytes;
        }

        private byte[] CreateByteArrayFor(string hex)
        {
            if (_delimiter.HasValue)
            {
                var numberDelimiters = hex.Length / 3;

                return new byte[(hex.Length - numberDelimiters) / 2];
            }

            return new byte[hex.Length / 2];
        }
    }
}
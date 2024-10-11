using System;
using System.IO;
using System.Text;

namespace ByteDev.Encoding.Base85
{
    /// <summary>
    /// Represents a base 85 encoder/decoder.
    /// </summary>
    /// <remarks>
    /// Modified from code at: https://github.com/LogosBible/Logos.Utility/blob/master/src/Logos.Utility/Ascii85.cs
    /// </remarks>
    public class Base85Encoder : IEncoder
    {
        private static readonly uint[] PowersOf85 = { 85u * 85u * 85u * 85u, 85u * 85u * 85u, 85u * 85u, 85u, 1 };

        /// <summary>
        /// Encode a UTF8 encoded string to base 85.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>Base 85 string.</returns>
        public string Encode(string value)
        {
            return Encode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Encode a string to base 85.
        /// </summary>
        /// <param name="value">The string to encode./</param>
        /// <param name="encoding">The encoding of <paramref name="value" />.</param>
        /// <returns>Base 85 string.</returns>
        public string Encode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);

            return Encode(bytes);
        }

        /// <summary>
        /// Encode an array of bytes to base 85.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Base 85 string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="bytes" /> is null.</exception>
        public string Encode(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length == 0)
                return string.Empty;

            var sb = new StringBuilder(bytes.Length * 5 / 4);

            var count = 0;
            uint value = 0;

            foreach (byte b in bytes)
            {
                // Build a 32-bit value from the bytes
                value |= (uint)b << (24 - (count * 8));
                count++;

                // Every 32 bits, convert the previous 4 bytes into 5 Ascii85 characters
                if (count == 4)
                {
                    if (value == 0)
                        sb.Append('z');
                    else
                        sb.Append(EncodeValue(value, 0));

                    count = 0;
                    value = 0;
                }
            }

            // Encode any remaining bytes (that weren't a multiple of 4)
            if (count > 0)
                sb.Append(EncodeValue(value, 4 - count));

            return sb.ToString();
        }

        /// <summary>
        /// Decodes a string from base 85 to UTF-8
        /// </summary>
        /// <param name="value">The base 85 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value)
        {
            return Decode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Decodes a string from base 85 to <paramref name="encoding" />.
        /// </summary>
        /// <param name="value">The base 85 string to decode.</param>
        /// <param name="encoding">The target encoding.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = DecodeToBytes(value);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Decodes a string from base 85 to byte array.
        /// </summary>
        /// <param name="value">The base 85 string to decode.</param>
        /// <returns>The decoded string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="value" /> is null.</exception>
        /// <exception cref="T:System.FormatException">Value is not a valid base 85 encoding.</exception>
        public byte[] DecodeToBytes(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value == string.Empty)
                return Array.Empty<byte>();

            using (var stream = new MemoryStream(value.Length * 4 / 5))
            {
                var count = 0;
                uint val = 0;

                foreach (char ch in value)
                {
                    if (ch == 'z' && count == 0)
                    {
                        // Handle 'z' block specially
                        WriteDecodeValue(stream, val, 0);
                    }
                    else if (ch < Base85CharacterSet.FirstCharacter || ch > Base85CharacterSet.LastCharacter)
                    {
                        throw new FormatException($"Value is not a valid base 85 encoding. Invalid character '{ch}'.");
                    }
                    else
                    {
                        // build a 32-bit value from the input characters
                        try
                        {
                            checked
                            {
                                val += (uint)(PowersOf85[count] * (ch - Base85CharacterSet.FirstCharacter));
                            }
                        }
                        catch (OverflowException ex)
                        {
                            throw new FormatException("Value is not a valid base 85 encoding. Current group of chars decodes to a value greater than UInt32.MaxValue.", ex);
                        }

                        count++;

                        // Every five characters, convert the characters into the equivalent byte array
                        if (count == 5)
                        {
                            WriteDecodeValue(stream, val, 0);
                            count = 0;
                            val = 0;
                        }
                    }
                }

                if (count <= 1)
                    throw new FormatException("Value is not a valid base 85 encoding. The final block must contain more than one character.");

                // Decode any remaining characters
                for (var padding = count; padding < 5; padding++)
                {
                    try
                    {
                        checked
                        {
                            val += 84 * PowersOf85[padding];
                        }
                    }
                    catch (OverflowException ex)
                    {
                        throw new FormatException("Value is not a valid base 85 encoding. Current group of chars decodes to a value greater than UInt32.MaxValue.", ex);
                    }
                }

                WriteDecodeValue(stream, val, 5 - count);

                return stream.ToArray();
            }
        }

        // Get the base 85 characters for a 32-bit value
        private static char[] EncodeValue(uint value, int paddingBytes)
        {
            var encoded = new char[5];

            for (var index = 4; index >= 0; index--)
            {
                encoded[index] = (char)((value % 85) + Base85CharacterSet.FirstCharacter);
                value /= 85;
            }

            if (paddingBytes != 0)
                Array.Resize(ref encoded, 5 - paddingBytes);

            return encoded;
        }
        
        // Writes the bytes of a 32-bit value to a stream.
        private static void WriteDecodeValue(Stream stream, uint value, int paddingChars)
        {
            stream.WriteByte((byte)(value >> 24));

            if (paddingChars == 3)
                return;

            stream.WriteByte((byte)((value >> 16) & 0xFF));

            if (paddingChars == 2)
                return;

            stream.WriteByte((byte)((value >> 8) & 0xFF));

            if (paddingChars == 1)
                return;

            stream.WriteByte((byte)(value & 0xFF));
        }
    }
}
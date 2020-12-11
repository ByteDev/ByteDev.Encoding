using System;
using System.Text;

namespace ByteDev.Encoding.Base32
{
    /// <summary>
    /// Represents a base32 encoder/decoder.
    /// </summary>
    /// <remarks>
    /// Modified from answer at: https://stackoverflow.com/questions/641361/base32-decoding
    /// </remarks>
    public class Base32Encoder : IEncoder
    {
        /// <summary>
        /// Encode a UTF8 encoded string to base32.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>Base32 string.</returns>
        public string Encode(string value)
        {
            return Encode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Encode a string to base32.
        /// </summary>
        /// <param name="value">The string to encode./</param>
        /// <param name="encoding">The encoding of <paramref name="value" />.</param>
        /// <returns>Base32 string.</returns>
        public string Encode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value);

            return Encode(bytes);
        }

        /// <summary>
        /// Encode an array of bytes to base32.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Base32 string.</returns>
        public string Encode(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length == 0)
                return string.Empty;

            int charCount = (int)Math.Ceiling(bytes.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0;
            byte bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in bytes)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            // if we didn't end with a full char
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                while (arrayIndex != charCount) 
                    returnArray[arrayIndex++] = Base32CharacterSet.PaddingChar;
            }

            return new string(returnArray);
        }

        /// <summary>
        /// Decodes a string from base32 to UTF-8
        /// </summary>
        /// <param name="value">The base32 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value)
        {
            return Decode(value, new UTF8Encoding());
        }

        /// <summary>
        /// Decodes a string from base32 to <paramref name="encoding" />.
        /// </summary>
        /// <param name="value">The base32 string to decode.</param>
        /// <param name="encoding">The target encoding.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string value, System.Text.Encoding encoding)
        {
            byte[] bytes = DecodeToBytes(value);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Decodes a string from base32 to byte array.
        /// </summary>
        /// <param name="value">The base32 string to decode.</param>
        /// <returns>The decoded string.</returns>
        public byte[] DecodeToBytes(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value == string.Empty)
                return new byte[0];

            value = value.TrimEnd(Base32CharacterSet.PaddingChar);

            int byteCount = value.Length * 5 / 8; // this must be TRUNCATED

            byte[] returnArray = new byte[byteCount];

            byte curByte = 0;
            byte bitsRemaining = 8;
            int arrayIndex = 0;

            foreach (char ch in value)
            {
                int cValue = CharToValue(ch);

                int mask;

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            // if we didn't end with a full byte
            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }

            return returnArray;
        }

        private static int CharToValue(char ch)
        {
            // 65-90 == uppercase letters
            if (ch < 91 && ch > 64)
                return ch - 65;

            // 50-55 == numbers 2-7
            if (ch < 56 && ch > 49)
                return ch - 24;

            // 97-122 == lowercase letters
            if (ch < 123 && ch > 96)
                return ch - 97;

            throw new ArgumentException("Character is not a base32 character.", nameof(ch));
        }

        private static char ValueToChar(byte b)
        {
            if (b < 26)
                return (char)(b + 65);

            if (b < 32)
                return (char)(b + 24);

            throw new ArgumentException("Byte is not a value base32 value.", nameof(b));
        }
    }
}
using System.Collections.Generic;

namespace ByteDev.Encoding.Base32
{
    /// <summary>
    /// Represents the Base 32 character set.
    /// </summary>
    public static class Base32CharacterSet
    {
        /// <summary>
        /// Valid Base 32 characters.
        /// </summary>
        public static readonly HashSet<char> Base32Chars = new HashSet<char>
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '2', '3', '4', '5', '6', '7',
            '='
        };

        /// <summary>
        /// Valid Base 32 characters.
        /// </summary>
        public static readonly string Base32 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567=";

        internal const char PaddingChar = '=';
    }
}
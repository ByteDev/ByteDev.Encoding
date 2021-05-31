using System.Collections.Generic;

namespace ByteDev.Encoding.Hex
{
    /// <summary>
    /// Represents the hexadecimal character set.
    /// </summary>
    public static class HexCharacterSet
    {
        /// <summary>
        /// Valid hexadecimal characters (0-9 and A-Z).
        /// </summary>
        public static readonly HashSet<char> HexChars = new HashSet<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F'
        };

        /// <summary>
        /// Valid hexadecimal characters (0-9 and A-Z).
        /// </summary>
        public static readonly string Hex = "0123456789ABCDEF";
    }
}
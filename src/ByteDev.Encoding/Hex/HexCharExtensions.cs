namespace ByteDev.Encoding.Hex
{
    public static class HexCharExtensions
    {
        /// <summary>
        /// Indicates whether a character is a valid hexadecimal character
        /// (0-9 or A-Z or a-z).
        /// </summary>
        /// <param name="source">The char to perform this operation on.</param>
        /// <returns>True if char is a hexadecimal character; otherwise returns false.</returns>
        public static bool IsHex(this char source)
        {
            return source >= '0' && source <= '9' ||
                   source >= 'A' && source <= 'F' ||
                   source >= 'a' && source <= 'f';
        }
    }
}
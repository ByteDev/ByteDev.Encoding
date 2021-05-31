namespace ByteDev.Encoding.Hex
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class HexStringExtensions
    {
        /// <summary>
        /// Indicates whether the string contains only valid hexadecimal characters (0-9 or A-Z).
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if contains only hexadecimal characters; otherwise returns false.</returns>
        public static bool IsHex(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            foreach(char c in source)
            {
                if (!c.IsHex())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Indicates whether the string contains only valid hexadecimal characters (0-9 or A-Z).
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <param name="delimiter">Delimiter used between hexadecimal values.</param>
        /// <returns>True if contains only hexadecimal characters; otherwise returns false.</returns>
        public static bool IsHex(this string source, char delimiter)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            foreach(char c in source)
            {
                if (!c.IsHex() && c != delimiter) 
                    return false;
            }

            return true;
        }
    }
}
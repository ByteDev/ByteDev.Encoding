namespace ByteDev.Encoding.Base85
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class Base85StringExtensions
    {
        /// <summary>
        /// Indicates if the string is potentially base 85 encoded.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if string is base 85 encoded; otherwise returns false.</returns>
        public static bool IsBase85(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            foreach (char ch in source)
            {
                if (!ch.IsBase85())
                    return false;
            }

            return true;
        }
    }
}
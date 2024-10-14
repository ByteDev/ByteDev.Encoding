namespace ByteDev.Encoding.Base85
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Char" />.
    /// </summary>
    public static class Base85CharExtensions
    {
        /// <summary>
        /// Indicates whether a character is a valid base 85 character.
        /// </summary>
        /// <param name="source">The char to perform this operation on.</param>
        /// <returns>True if char is a valid base 85 character; otherwise returns false.</returns>
        public static bool IsBase85(this char source)
        {
            return Base85CharacterSet.Base85Chars.Contains(source);
        }
    }
}
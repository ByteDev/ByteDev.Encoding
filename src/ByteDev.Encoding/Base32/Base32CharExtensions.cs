namespace ByteDev.Encoding.Base32
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Char" />.
    /// </summary>
    public static class Base32CharExtensions
    {
        /// <summary>
        /// Indicates whether a character is a valid base32 character.
        /// </summary>
        /// <param name="source">The char to perform this operation on.</param>
        /// <returns>True if char is a valid base64 character; otherwise returns false.</returns>
        public static bool IsBase32(this char source)
        {
            return Base32CharacterSet.Base32Chars.Contains(source);
        }
    }
}
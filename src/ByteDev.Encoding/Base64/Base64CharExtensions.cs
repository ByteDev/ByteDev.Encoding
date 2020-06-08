namespace ByteDev.Encoding.Base64
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Char" />.
    /// </summary>
    public static class Base64CharExtensions
    {
        /// <summary>
        /// Indicates whether a character is a valid base64 character.
        /// </summary>
        /// <param name="source">The char to perform this operation on.</param>
        /// <returns>True if char is a valid base64 character; otherwise returns false.</returns>
        public static bool IsBase64(this char source)
        {
            return Base64.Base64CharacterSet.Base64Chars.Contains(source);
        }
    }
}
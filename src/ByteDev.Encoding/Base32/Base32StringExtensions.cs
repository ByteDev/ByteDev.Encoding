using System.Text.RegularExpressions;

namespace ByteDev.Encoding.Base32
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class Base32StringExtensions
    {
        /// <summary>
        /// Indicates if the string is potentially base 32 encoded.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if string is base 32 encoded; otherwise returns false.</returns>
        public static bool IsBase32(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            if (!source.Length.IsMultipleOf(8))
                return false;

            // Check every char is A-Z2-7 except for end padding which can be 0-6 '=' characters
            return Regex.IsMatch(source, "^(?:[A-Z2-7]{8})*(?:[A-Z2-7]{2}={6}|[A-Z2-7]{4}={4}|[A-Z2-7]{5}={3}|[A-Z2-7]{7}=)?$");
        }
    }
}
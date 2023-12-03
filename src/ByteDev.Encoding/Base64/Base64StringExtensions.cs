using System;
using System.Text.RegularExpressions;

namespace ByteDev.Encoding.Base64
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class Base64StringExtensions
    {
        /// <summary>
        /// Indicates if the string is potentially base64 encoded.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if string is base64 encoded; otherwise returns false.</returns>
        public static bool IsBase64(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            if (!source.Length.IsMultipleOf(4))
                return false;

            // Check every char is [A-Za-z0-9+/] except for end padding which can be 0-2 '=' characters
            return Regex.IsMatch(source, "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$");
        }

        /// <summary>
        /// Gets the base 64 end padding characters (if any) as a string. If no end padding exists
        /// then empty string is returned.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>Any base 64 end padding characters as a string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static string GetBase64EndPadding(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Length >= 2)
            {
                if (source[source.Length - 1] == '=')
                {
                    if (source[source.Length - 2] == '=')
                    {
                        return "==";
                    }

                    return "=";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Removes any end padding for a base 64 encoded string. If no padding exists the
        /// same string is returned.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>String with any base 64 end padding characters removed.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static string RemoveBase64EndPadding(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Length >= 2)
            {
                if (source[source.Length - 1] == '=')
                {
                    if (source[source.Length - 2] == '=')
                    {
                        return source.Substring(0, source.Length - 2);
                    }

                    return source.Substring(0, source.Length - 1);
                }
            }

            return source;
        }
    }
}
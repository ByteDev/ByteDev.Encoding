﻿using System;

namespace ByteDev.Encoding.Hex
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class HexStringExtensions
    {
        /// <summary>
        /// Indicates if the string is potentially hexadecimal encoded.
        /// </summary>
        /// <param name="source">The string to perform this operation on.</param>
        /// <returns>True if contains only hexadecimal characters; otherwise returns false.</returns>
        public static bool IsHex(this string source)
        {
            if (String.IsNullOrEmpty(source))
                return false;

            foreach(char c in source)
            {
                if(!c.IsHex())
                    return false;
            }

            return true;
        }
    }
}
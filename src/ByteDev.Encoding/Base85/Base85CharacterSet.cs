using System.Collections.Generic;

namespace ByteDev.Encoding.Base85
{
    /// <summary>
    /// Represents the base 85 character set.
    /// </summary>
    public class Base85CharacterSet
    {
        /// <summary>
        /// Valid base 85 characters (including 'z').
        /// </summary>
        public static readonly HashSet<char> Base85Chars = new HashSet<char>
        {
            // Base 85 uses ASCII chars 33 ('!') -> 117 ('u')
            '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', 
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
            ':', ';', '<', '=', '>', '?', '@', 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
            '[', '\\', ']', '^', '_', '`', 
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            // 'z' is used to represent a a block (4 char) of NUL (\0) chars
            'z'
        };

        /// <summary>
        /// Valid base 85 characters (including 'z').
        /// </summary>
        public static readonly string Base85 = @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuz";

        internal const char FirstCharacter = '!'; 
        internal const char LastCharacter = 'u';
    }
}
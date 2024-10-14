namespace ByteDev.Encoding
{
    /// <summary>
    /// Represents an encoding type.
    /// </summary>
    public enum EncodingType
    {
        /// <summary>
        /// Base 64.
        /// </summary>
        Base64 = 0,

        /// <summary>
        /// Hexadecimal (AKA Base 16).
        /// </summary>
        Hex = 1,

        /// <summary>
        /// Base 32.
        /// </summary>
        Base32 = 2,

        /// <summary>
        /// Base 85 (AKA ASCII85).
        /// </summary>
        Base85 = 3
    }
}
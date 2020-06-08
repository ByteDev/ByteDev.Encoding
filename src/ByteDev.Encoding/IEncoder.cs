namespace ByteDev.Encoding
{
    public interface IEncoder
    {
        /// <summary>
        /// Encode a UTF8 encoded string.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>Encoded string.</returns>
        string Encode(string value);

        /// <summary>
        /// Encode a string.
        /// </summary>
        /// <param name="value">The string to encode./</param>
        /// <param name="encoding">The encoding of <paramref name="value" />.</param>
        /// <returns>Encoded string.</returns>
        string Encode(string value, System.Text.Encoding encoding);

        /// <summary>
        /// Encode an array of bytes.
        /// </summary>
        /// <param name="bytes">The byte array to encode.</param>
        /// <returns>Encoded string.</returns>
        string Encode(byte[] bytes);

        /// <summary>
        /// Decodes a string.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <returns>The decoded string.</returns>
        string Decode(string value);

        /// <summary>
        /// Decodes a string to <paramref name="encoding" />.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <param name="encoding">The target encoding.</param>
        /// <returns>The decoded string.</returns>
        string Decode(string value, System.Text.Encoding encoding);

        /// <summary>
        /// Decodes a string to byte array.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <returns>The decoded string.</returns>
        byte[] DecodeToBytes(string value);
    }
}
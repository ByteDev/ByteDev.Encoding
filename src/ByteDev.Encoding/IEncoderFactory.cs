namespace ByteDev.Encoding
{
    /// <summary>
    /// Represents an interface forcreating different types of encoders.
    /// </summary>
    public interface IEncoderFactory
    {
        /// <summary>
        /// Create a new encoder instance based on the
        /// encoding type.
        /// </summary>
        /// <param name="encodingType">Encoding type of the encoder.</param>
        /// <returns>New encoder instance.</returns>
        IEncoder Create(EncodingType encodingType);
    }
}
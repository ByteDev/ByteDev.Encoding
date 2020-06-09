using System;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Hex;

namespace ByteDev.Encoding
{
    /// <summary>
    /// Represents a factory for creating different types
    /// of encoders.
    /// </summary>
    public class EncoderFactory : IEncoderFactory
    {
        /// <summary>
        /// Create a new encoder instance based on the
        /// encoding type.
        /// </summary>
        /// <param name="encodingType">Encoding type of the encoder.</param>
        /// <returns>New encoder instance.</returns>
        public IEncoder Create(EncodingType encodingType)
        {
            switch (encodingType)
            {
                case EncodingType.Base64:
                    return new Base64Encoder();

                case EncodingType.Hex:
                    return new HexEncoder();

                default:
                    throw new InvalidOperationException($"Unhandled {nameof(EncodingType)} value: '{encodingType}'.");
            }
        }
    }
}
using System;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Hex;

namespace ByteDev.Encoding
{
    public class EncoderFactory : IEncoderFactory
    {
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
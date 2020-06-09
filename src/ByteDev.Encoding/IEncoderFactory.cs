namespace ByteDev.Encoding
{
    public interface IEncoderFactory
    {
        IEncoder Create(EncodingType encodingType);
    }
}
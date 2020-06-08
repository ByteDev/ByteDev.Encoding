namespace ByteDev.Encoding
{
    public interface IEncoderFactory
    {
        IEncoder CreateFor(EncodingType encodingType);
    }
}
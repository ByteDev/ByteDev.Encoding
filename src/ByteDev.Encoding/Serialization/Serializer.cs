using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace ByteDev.Encoding.Serialization
{
    /// <summary>
    /// Represents a generic serializer for any encoder.
    /// </summary>
    public class Serializer : ISerializer
    {
        private readonly IEncoder _encoder;

        public Serializer(IEncoder encoder)
        {
            _encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
        }

        /// <summary>
        /// Serializes an object to an encoded string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized representation of <paramref name="obj" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
        public string Serialize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var stream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();

                // Convert object to a stream
                binaryFormatter.Serialize(stream, obj);

                var bytes = Compress(stream);

                return _encoder.Encode(bytes);
            }
        }

        /// <summary>
        /// Deserialize an encoded string to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="value">Serialized string to deserialize.</param>
        /// <returns>Deserialized type.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="value" /> is null or empty.</exception>
        public T Deserialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(value);

            byte[] compressedBytes = _encoder.DecodeToBytes(value);

            using (var stream = new MemoryStream())
            {
                Decompress(compressedBytes, stream);
                stream.Position = 0;
                
                var formatter = new BinaryFormatter();

                return (T)formatter.Deserialize(stream);
            }
        }
        
        private static byte[] Compress(Stream stream)
        {
            using (var resultStream = new MemoryStream())
            {
                using (var writeStream = new GZipStream(resultStream, CompressionMode.Compress, true))
                {
                    CopyBuffered(stream, writeStream);
                }

                return resultStream.ToArray();
            }
        }

        private static void Decompress(byte[] compressedBytes, Stream outputStream)
        {
            var memoryStream = new MemoryStream(compressedBytes);

            try
            {
                using (var readStream = new GZipStream(memoryStream, CompressionMode.Decompress, true))
                {
                    memoryStream = null;
                    CopyBuffered(readStream, outputStream);
                }
            }
            finally
            {
                memoryStream?.Dispose();
            }
        }

        private static void CopyBuffered(Stream readStream, Stream writeStream)
        {
            if (readStream.CanSeek)
                readStream.Position = 0;

            var bytes = new byte[4096];
            int byteCount;

            while ((byteCount = readStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                writeStream.Write(bytes, 0, byteCount);
            }
        }
    }
}

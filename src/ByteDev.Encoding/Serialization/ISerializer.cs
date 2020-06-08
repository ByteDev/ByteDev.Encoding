namespace ByteDev.Encoding.Serialization
{
    /// <summary>
    /// Provides a way to serialize and deserialize objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes an object to an encoded string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized representation of <paramref name="obj" />.</returns>
        string Serialize(object obj);

        /// <summary>
        /// Deserialize an encoded string to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="value">Serialized string to deserialize.</param>
        /// <returns>Deserialized type.</returns>
        T Deserialize<T>(string value);
    }
}
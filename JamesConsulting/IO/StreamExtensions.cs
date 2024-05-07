using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Metalama.Patterns.Contracts;

namespace JamesConsulting.IO
{
    /// <summary>
    ///     The stream extensions.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// The is executable.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsExecutable([NotNull] this Stream stream)
        {
            var firstBytes = new byte[2];
            stream.Position = 0;
            var read = stream.Read(firstBytes, 0, 2);
            return read == 2 && Encoding.UTF8.GetString(firstBytes) == "MZ";
        }


        /// <summary>
        /// Deserializes the content of the stream into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="stream">The stream containing the serialized object.</param>
        /// <returns>The deserialized object of type T.</returns>
        public static T? Deserialize<T>([NotNull] this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            using var sr = new StreamReader(stream);
            using JsonReader reader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<T>(reader);
        }
    }
}
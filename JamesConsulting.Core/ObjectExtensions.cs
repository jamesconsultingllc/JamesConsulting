//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObjectExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// The from byte array.
        /// </summary>
        /// <param name="byteArray">
        /// The byte array.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to create from the <paramref name="byteArray"/>
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The serializationStream supports seeking, but its length is 0.
        ///     -or-
        ///     The target type is a <see cref="System.Decimal"></see>, but the value is out of range of the
        ///     <see cref="System.Decimal"></see> type.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// The serializationStream is null.
        /// </exception>
        public static T FromByteArray<T>(this byte[] byteArray)
            where T : class
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException(nameof(byteArray));
            }

            if (byteArray.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(byteArray));
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(byteArray))
            {
                return binaryFormatter.Deserialize(memoryStream) as T;
            }
        }

        /// <summary>
        /// The get object type.
        /// </summary>
        /// <param name="obj">
        /// The object to get the type from
        /// </param>
        /// <typeparam name="T">
        /// The type of <paramref name="obj"/>
        /// </typeparam>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetObjectType<T>(this T obj)
        {
            return obj?.GetType() ?? typeof(T);
        }

        /// <summary>
        /// The mask.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="ignore">
        /// The ignore.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static object Mask(this object data, params string[] ignore)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (ignore == null)
            {
                throw new ArgumentNullException(nameof(ignore));
            }

            if (ignore.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(ignore));
            }

            var jo = (JObject)JToken.FromObject(data);
            var keys = jo.Properties().Where(x => ignore.Any(y => y.Equals(x.Name, StringComparison.OrdinalIgnoreCase))).Select(x => x.Name);

            foreach (var key in keys)
            {
                jo[key] = "*****";
            }

            return jo.ToObject<object>();
        }

        /// <summary>
        /// The serialize to JSON stream.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="obj"/> or <paramref name="stream"/> is null
        /// </exception>
        public static Stream SerializeToJsonStream(this object obj, Stream stream)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var writer = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(writer);
            var ser = new JsonSerializer();
            ser.Serialize(jsonWriter, obj);
            jsonWriter.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// The to byte array.
        /// </summary>
        /// <param name="obj">
        /// The object to get the type from
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// An error has occurred during serialization,
        ///     such as if an object in the graph parameter is not marked as serializable.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// The serializationStream is null.
        ///     -or-
        ///     The graph is null.
        /// </exception>
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (!obj.GetObjectType().IsSerializable)
            {
                throw new InvalidOperationException("This object is not serializable");
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// The to json.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJson(this object obj)
        {
            return ObjectExtensions.ToJsonInternal(obj, Formatting.Indented);
        }

        /// <summary>
        /// The to json compact.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJsonCompact(this object obj)
        {
            return ObjectExtensions.ToJsonInternal(obj, Formatting.None);
        }

        /// <summary>
        /// The to json internal.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="formatting">
        /// The formatting.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ToJsonInternal(object obj, Formatting formatting)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is string objString)
            {
                return objString;
            }

            return JsonConvert.SerializeObject(obj, formatting);
        }
    }
}
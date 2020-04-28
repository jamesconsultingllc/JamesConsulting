//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObjectExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JamesConsulting
{
    /// <summary>
    ///     The object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        private static readonly JTokenType[] NumericTokenTypes = {JTokenType.Float, JTokenType.Integer};

        /// <summary>
        ///     The from byte array.
        /// </summary>
        /// <param name="byteArray">
        ///     The byte array.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the object to create from the <paramref name="byteArray" />
        /// </typeparam>
        /// <returns>
        ///     An instance of <typeparamref name="T"/> deserialized from they <paramref name="byteArray"/>
        /// </returns>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     The serializationStream supports seeking, but its length is 0.
        ///     -or-
        ///     The target type is a <see cref="System.Decimal"></see>, but the value is out of range of the
        ///     <see cref="System.Decimal"></see> type.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The serializationStream is null.
        /// </exception>
        public static T? FromByteArray<T>(this byte[] byteArray)
            where T : class
        {
            if (byteArray == null) throw new ArgumentNullException(nameof(byteArray));

            if (byteArray.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(byteArray));

            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream(byteArray);
            return binaryFormatter.Deserialize(memoryStream) as T;
        }

        /// <summary>
        ///     The get object type.
        /// </summary>
        /// <param name="obj">
        ///     The object to get the type from
        /// </param>
        /// <typeparam name="T">
        ///     The type of <paramref name="obj" />
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Type" />.
        /// </returns>
        public static Type GetObjectType<T>(this T obj)
        {
            return obj?.GetType() ?? typeof(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The object whose properties will be masked</param>
        /// <param name="propertiesToMask">
        ///     List of properties that should be masked. Fields will be replaced with default values for their given types.
        ///     Use <a href="https://goessner.net/articles/JsonPath/">JPath</a> to find properties.
        /// </param>
        /// <typeparam name="T">The <see cref="Type"/> of the <paramref name="data"/> object</typeparam>
        /// <returns>A new copy of <paramref name="data"/> with properties in <paramref name="propertiesToMask"/> set to default values for their types</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="data"/> or <paramref name="propertiesToMask"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="propertiesToMask"/> is an empty array.
        /// </exception>
        public static T Mask<T>(this T data, params string[]? propertiesToMask)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            if (propertiesToMask == null) throw new ArgumentNullException(nameof(propertiesToMask));

            if (propertiesToMask.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(propertiesToMask));

            var jo = (JObject) JToken.FromObject(data);

            foreach (var propertyToMask in propertiesToMask)
            {
                var properties = jo.SelectTokens($"$.{propertyToMask}");

                foreach (var property in properties)
                {
                    var key = ((JValue) jo.SelectToken(property.Path));
                    if (key.Type == JTokenType.String)
                        key.Value = default(string);
                    else if (NumericTokenTypes.Contains(key.Type))
                        key.Value = default(int);
                    else
                        key.Value = key.Type switch
                        {
                            JTokenType.Date => default(DateTime),
                            JTokenType.TimeSpan => default(TimeSpan),
                            JTokenType.Array => null,
                            JTokenType.Object => null,
                            _ => key.Value
                        };
                }
            }

            return jo.ToObject<T>();
        }

        /// <summary>
        ///     The serialize to JSON stream.
        /// </summary>
        /// <param name="obj">
        ///     The object to be serialized to Json
        /// </param>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="obj" /> or <paramref name="stream" /> is null
        /// </exception>
        public static Stream SerializeToJsonStream(this object? obj, Stream? stream)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var writer = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(writer);
            var ser = new JsonSerializer();
            ser.Serialize(jsonWriter, obj);
            jsonWriter.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        ///     The to byte array.
        /// </summary>
        /// <param name="obj">
        ///     The object to get the type from
        /// </param>
        /// <returns>
        ///     The <see cref="T:byte[]" />.
        /// </returns>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     An error has occurred during serialization,
        ///     such as if an object in the graph parameter is not marked as serializable.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The serializationStream is null.
        ///     -or-
        ///     The graph is null.
        /// </exception>
        public static byte[] ToByteArray(this object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (!obj.GetObjectType().IsSerializable) throw new InvalidOperationException("This object is not serializable");

            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }

        /// <summary>
        ///     The to json.
        /// </summary>
        /// <param name="obj">
        ///     The obj.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string? ToJson(this object? obj)
        {
            return ToJsonInternal(obj, Formatting.Indented);
        }

        /// <summary>
        ///     The to json compact.
        /// </summary>
        /// <param name="obj">
        ///     The obj.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string? ToJsonCompact(this object? obj)
        {
            return ToJsonInternal(obj, Formatting.None);
        }

        /// <summary>
        ///     The to json internal.
        /// </summary>
        /// <param name="obj">
        ///     The obj.
        /// </param>
        /// <param name="formatting">
        ///     The formatting.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private static string? ToJsonInternal(object? obj, Formatting formatting)
        {
            return obj switch
            {
                null => null,
                string objString => objString,
                _ => JsonConvert.SerializeObject(obj, formatting)
            };
        }
    }
}
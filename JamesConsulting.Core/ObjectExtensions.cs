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
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using Newtonsoft.Json;

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
                return default(T);
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
                return new byte[0];
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Converts the given object to JSON
        /// </summary>
        /// <param name="obj">
        /// The object to convert to JSON
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJson(this object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        /// <summary>
        /// Converts the given object to json
        /// </summary>
        /// <param name="obj">
        /// The obj to convert to json
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static string ToJsonCompact(this object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj);
        }
    }
}
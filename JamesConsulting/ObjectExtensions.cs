//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObjectExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using PostSharp.Patterns.Contracts;
using Utf8Json;

namespace JamesConsulting
{
    /// <summary>
    ///     The object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        private static readonly JTokenType[] NumericTokenTypes = {JTokenType.Float, JTokenType.Integer};

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
        public static T Mask<T>([NotNull] this T data, [NotEmpty] params string[] propertiesToMask)
        {
            var jo = JObject.FromObject(data!);
            if (jo == null) return data;
            
            foreach (var propertyToMask in propertiesToMask)
            {
                var properties = jo.SelectTokens($"$.{propertyToMask}");

                foreach (var property in properties)
                {
                    if (!(jo.SelectToken(property.Path) is JValue key)) continue;

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

            return jo.ToObject<T>()!;
        }

        /// <summary>
        ///     The serialize to JSON stream.
        /// </summary>
        /// <param name="obj">
        ///     The obj.
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
        public static Stream SerializeToJsonStream([NotNull] this object obj, [NotNull] Stream stream)
        {
            JsonSerializer.Serialize(stream, obj);
            stream.Position = 0;
            return stream;
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
        public static string ToJson([NotNull] this object obj)
        {
            return obj switch
            {
                string objString => objString,
                _ => JsonSerializer.ToJsonString(obj)
            };
        }
    }
}
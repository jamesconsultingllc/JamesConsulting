//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StreamExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JamesConsulting.IO
{
    /// <summary>
    ///     The stream extensions.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        ///     The deserialize stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <typeparamref name="T"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static T DeserializeJson<T>(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var streamReader = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(streamReader) {CloseInput = true};

            try
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jsonTextReader);
            }
            finally
            {
                streamReader.Close();
            }
        }

        /// <summary>
        ///     The is executable.
        /// </summary>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsExecutable(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            var firstBytes = new byte[2];
            stream.Position = 0;
            stream.Read(firstBytes, 0, 2);
            return Encoding.UTF8.GetString(firstBytes) == "MZ";
        }
    }
}
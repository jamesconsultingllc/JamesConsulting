//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StreamExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using MessagePack;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.IO
{
    /// <summary>
    ///     The stream extensions.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Deserializes a stream into a list of <typeparam name="T"/>
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to deserialize</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <typeparam name="T">The object type to return</typeparam>
        /// <returns>An instance of IAsyncEnumerable{T}</returns>
        public static async IAsyncEnumerable<T> DeserializeListFromStreamAsync<T>([NotNull] this Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var streamReader = new MessagePackStreamReader(stream);
            
            while (await streamReader.ReadAsync(cancellationToken) is { } messagePack)
            {
                yield return MessagePackSerializer.Deserialize<T>(messagePack, cancellationToken: cancellationToken);
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
        public static bool IsExecutable([NotNull] this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            var firstBytes = new byte[2];
            stream.Position = 0;
            stream.Read(firstBytes, 0, 2);
            return Encoding.UTF8.GetString(firstBytes) == "MZ";
        }
    }
}
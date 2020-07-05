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
using System.IO;
using System.Text;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.IO
{
    /// <summary>
    ///     The stream extensions.
    /// </summary>
    public static class StreamExtensions
    {
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
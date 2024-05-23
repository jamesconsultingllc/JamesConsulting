// <copyright file="ByteArrayExtensions.cs" company="James Consulting LLC">
// Copyright © James Consulting LLC. All rights reserved.
// </copyright>

namespace JamesConsulting
{
    using System;
    using Metalama.Patterns.Contracts;

    /// <summary>
    /// Provides extension methods for byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts a byte array to a string.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <returns>A string representation of the byte array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the byte array is null.</exception>
        public static string GetString([NotNull] this byte[] bytes)
        {
            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            // Create a character array of the appropriate length.
            var chars = new char[bytes.Length / sizeof(char)];

            // Copy the byte array into the character array.
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);

            // Return a new string created from the character array.
            return new string(chars);
        }
    }
}
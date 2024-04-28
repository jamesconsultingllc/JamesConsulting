using System;
using Metalama.Patterns.Contracts;

namespace JamesConsulting
{
    /// <summary>
    ///     The byte array extensions.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="bytes"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="OverflowException">
        /// The array is multidimensional and contains more than
        ///     <see cref="System.Int32.MaxValue"></see> elements.
        /// </exception>
        public static string GetString([NotNull] this byte[] bytes)
        {
            if (bytes.Length == 0) return string.Empty;

            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Threading;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting
{
    /// <summary>
    ///     String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts a string to a byte array
        /// </summary>
        /// <param name="arg">
        ///     The string to be converted.
        /// </param>
        /// <returns>
        ///     The <see cref="T:byte[]">byte[]</see> representation of the given string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="arg" /> is <see langword="null" />
        /// </exception>
        /// <exception cref="OverflowException">
        ///     The array is multidimensional and contains more than
        ///     <see cref="System.Int32.MaxValue"></see> elements.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
#if NETSTANDARD2_1
        public static byte[] GetBytes([NotNull] this string arg)
#else
        public static byte[] GetBytes([NotNull] this string arg)
#endif

        {
            if (arg.Length == 0) return new byte[0];

            var bytes = new byte[arg.Length * sizeof(char)];
            Buffer.BlockCopy(arg.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        ///     Returns the given string in title case using the given text info object or the current UI thread culture text info.
        /// </summary>
        /// <param name="arg">
        ///     The string to be returned in title case
        /// </param>
        /// <param name="ci">
        ///     The <typeparamref cref="System.Globalization.CultureInfo" /> used to convert the given string to title
        ///     case. Defaults to null, optional.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="arg" /> is <see langword="null" />
        /// </exception>
        public static string ToTitleCase([NotNull] this string arg, CultureInfo? ci = null)
        {
            if (arg.Length == 0) return arg;

            return ci != null ? ci.TextInfo.ToTitleCase(arg) : Thread.CurrentThread.CurrentUICulture.TextInfo.ToTitleCase(arg);
        }

        /// <summary>
        ///     The truncate.
        /// </summary>
        /// <param name="argument">
        ///     The argument.
        /// </param>
        /// <param name="length">
        ///     The length.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument" /> is <see langword="null" />
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="length" /> is less than or equal to 0
        /// </exception>
#if NETSTANDARD2_1
        public static string Truncate([NotNull] this string argument, [StrictlyPositive] int length)
#else
        public static string Truncate([NotNull] this string argument, [StrictlyPositive] int length)
#endif
        {
            return argument.Length == 0 ? string.Empty : argument.Substring(0, length);
        }
    }
}
//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="IEnumerableExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The i enumerable extensions.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="arg1">
        /// The arg 1.
        /// </param>
        /// <param name="arg2">
        /// The arg 2.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Equals<T>(IEnumerable<T> arg1, IEnumerable<T> arg2, IEqualityComparer<T> comparer = null)
        {
            var isArg1Null = arg1 == null;
            return (isArg1Null && (arg2 == null)) || (!isArg1Null && arg1.SequenceEqual(arg2, comparer));
        }
    }
}
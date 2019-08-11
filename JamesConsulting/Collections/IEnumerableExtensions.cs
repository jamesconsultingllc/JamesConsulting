//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="IEnumerableExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace JamesConsulting.Collections
{
    /// <summary>
    /// The i enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
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
        public static bool IsEqualTo<T>(this IEnumerable<T> arg1, IEnumerable<T> arg2, IEqualityComparer<T> comparer = null)
        {
            if (arg1 == null) throw new ArgumentNullException(nameof(arg1));
            if (arg2 == null) return false;
            
            return arg1.SequenceEqual(arg2, comparer);
        }
    }
}
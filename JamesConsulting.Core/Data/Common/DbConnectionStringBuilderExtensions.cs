//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="DbConnectionStringBuilderExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Data.Common
{
    using System;
    using System.Data.Common;

    /// <summary>
    ///     <see cref="DbConnectionStringBuilder" /> extension methods.
    /// </summary>
    public static class DbConnectionStringBuilderExtensions
    {
        /// <summary>
        /// The remove keys.
        /// </summary>
        /// <param name="connectionStringBuilder">
        /// The <see cref="DbConnectionStringBuilder"/> to remove the keys.
        /// </param>
        /// <param name="keys">
        /// The keys to remove
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="connectionStringBuilder"/> or <paramref name="keys"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="keys"/> is an empty collection.
        /// </exception>
        public static void RemoveKeys(this DbConnectionStringBuilder connectionStringBuilder, params string[] keys)
        {
            if (connectionStringBuilder == null)
            {
                throw new ArgumentNullException(nameof(connectionStringBuilder));
            }

            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (keys.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(keys));
            }

            Array.ForEach(keys, key => connectionStringBuilder.Remove(key));
        }
    }
}
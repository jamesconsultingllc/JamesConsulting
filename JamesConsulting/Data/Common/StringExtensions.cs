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
using System.Data.Common;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Data.Common
{
    /// <summary>
    ///     The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     The strip password from connection string.
        /// </summary>
        /// <param name="connectionString">
        ///     The connection string.
        /// </param>
        /// <returns>
        ///     The connection string with the password stripped out
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="connectionString" /> is <see langword="null" />
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="connectionString" /> is an empty <see cref="string" /> or whitespace />
        /// </exception>
        public static string StripPasswordFromConnectionString([NotNull] this string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return connectionString;
            var db = new DbConnectionStringBuilder {ConnectionString = connectionString};
            db.RemoveKeys("Password", "password", "Pwd", "pwd");
            return db.ToString();
        }
    }
}
//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Common;

namespace JamesConsulting.Data.Common
{
    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The strip password from connection string.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The connection string with the password stripped out
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="connectionString"/> is <see langword="null"/>
        /// </exception>
        public static string StripPasswordFromConnectionString(this string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var db = new DbConnectionStringBuilder { ConnectionString = connectionString };
            db.RemoveKeys("Password", "password", "Pwd", "pwd");
            return db.ToString();
        }
    }
}
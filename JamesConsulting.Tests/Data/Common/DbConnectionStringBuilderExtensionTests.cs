//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="DbConnectionStringBuilderExtensionTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Tests.Data.Common
{
    using System;
    using System.Data.Common;

    using JamesConsulting.Data.Common;

    using Xunit;

    /// <summary>
    ///     The <see cref="DbConnectionStringBuilderExtensions" /> tests.
    /// </summary>
    public class DbConnectionStringBuilderExtensionTests
    {
        /// <summary>
        /// The remove keys throws <see cref="ArgumentException"/> when keys is empty.
        /// </summary>
        [Fact]
        public void RemoveKeysThrowsArgumentExceptionWhenKeysIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new DbConnectionStringBuilder().RemoveKeys());
        }

        /// <summary>
        /// The remove keys throws argument null exception when <see cref="DbConnectionStringBuilder"/> is null.
        /// </summary>
        [Fact]
        public void RemoveKeysThrowsArgumentNullExceptionWhenDbConnectionStringBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(DbConnectionStringBuilder).RemoveKeys());
        }

        /// <summary>
        /// The remove keys throws <see cref="ArgumentNullException"/> when keys is null.
        /// </summary>
        [Fact]
        public void RemoveKeysThrowsArgumentNullExceptionWhenKeysIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DbConnectionStringBuilder().RemoveKeys(null));
        }
    }
}
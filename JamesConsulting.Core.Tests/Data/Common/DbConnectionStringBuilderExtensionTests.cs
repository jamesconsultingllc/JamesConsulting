//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="DbConnectionStringBuilderExtensionTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Tests.Data.Common
{
    using System;
    using System.Data.Common;

    using JamesConsulting.Core.Data.Common;

    using Xunit;

    /// <summary>
    ///     The <see cref="DbConnectionStringBuilderExtensions" /> tests.
    /// </summary>
    public class DbConnectionStringBuilderExtensionTests
    {
        [Fact]
        public void RemoveKeysThrowsArgumentNullExceptionWhenDbConnectionStringBuilderIsNull()
        {
            DbConnectionStringBuilder dbConnectionStringBuilder = null;
            Assert.Throws<ArgumentNullException>(() => dbConnectionStringBuilder.RemoveKeys());
        }

        [Fact]
        public void RemoveKeysThrowsArgumentNullExceptionWhenKeysIsNull()
        {
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
            Assert.Throws<ArgumentNullException>(() => dbConnectionStringBuilder.RemoveKeys(null));
        }

        [Fact]
        public void RemoveKeysThrowsArgumentExceptionWhenKeysIsEmpty()
        {
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
            Assert.Throws<ArgumentException>(() => dbConnectionStringBuilder.RemoveKeys());
        }
    }
}
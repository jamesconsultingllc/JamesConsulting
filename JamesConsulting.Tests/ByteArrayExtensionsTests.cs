//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ByteArrayExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Tests
{
    using JamesConsulting;

    using System;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// The byte array extensions tests.
    /// </summary>
    public class ByteArrayExtensionsTests
    {
        /// <summary>
        /// The get string empty array returns empty string.
        /// </summary>
        [Fact]
        public void GetStringEmptyArrayReturnsEmptyString()
        {
            var bytes = new byte[0];
            bytes.GetString().Should().BeEmpty();
        }

        /// <summary>
        /// The get string null array throws argument null exception.
        /// </summary>
        [Fact]
        public void GetStringNullArrayThrowsArgumentNullException()
        {
            byte[] bytes = null;
            Assert.Throws<ArgumentNullException>(() => bytes.GetString());
        }
    }
}
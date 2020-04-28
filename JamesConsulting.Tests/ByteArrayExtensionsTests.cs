//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ByteArrayExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Xunit;

namespace JamesConsulting.Tests
{
    /// <summary>
    ///     The byte array extensions tests.
    /// </summary>
    public class ByteArrayExtensionsTests
    {
        /// <summary>
        ///     The get string empty array returns empty string.
        /// </summary>
        [Fact]
        public void GetStringEmptyArrayReturnsEmptyString()
        {
            var bytes = new byte[0];
            bytes.GetString().Should().BeEmpty();
        }

        /// <summary>
        ///     The get string null array throws argument null exception.
        /// </summary>
        [Fact]
        public void GetStringNullArrayThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(byte[])!.GetString());
        }

        [Fact]
        public void GetStringReturnsStringFromBytes()
        {
            "Test".GetBytes().GetString().Should().Be("Test");
        }
    }
}
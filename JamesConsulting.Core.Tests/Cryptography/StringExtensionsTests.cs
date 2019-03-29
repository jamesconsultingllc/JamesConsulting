//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Tests.Cryptography
{
    using System;

    using FluentAssertions;

    using JamesConsulting.Core.Cryptography;

    using Xunit;

    /// <summary>
    ///     The string extensions tests.
    /// </summary>
    public class StringExtensionsTests
    {
        /// <summary>
        ///     The base 64 decode empty string returns empty string.
        /// </summary>
        [Fact]
        public void Base64DecodeEmptyStringReturnsEmptyString()
        {
            string.Empty.Base64Decode().Should().BeEmpty();
        }

        /// <summary>
        ///     The base 64 decode invalid target throws argument null exception.
        /// </summary>
        [Fact]
        public void Base64DecodeNullThrowsArgumentNullException()
        {
            string test = null;
            Assert.Throws<ArgumentNullException>(() => test.Base64Decode());
        }

        /// <summary>
        ///     The base 64 encode empty string returns empty string.
        /// </summary>
        [Fact]
        public void Base64DecodeValidStringReturnsDecodedString()
        {
            "test".Base64Encode().Base64Decode().Should().Be("test");
        }

        /// <summary>
        ///     The base 64 encode empty string returns empty string.
        /// </summary>
        [Fact]
        public void Base64EncodeEmptyStringReturnsEmptyString()
        {
            string.Empty.Base64Encode().Should().BeEmpty();
        }

        /// <summary>
        ///     The base 64 encode invalid target throws argument null exception.
        /// </summary>
        [Fact]
        public void Base64EncodeNullThrowsArgumentNullException()
        {
            string test = null;
            Assert.Throws<ArgumentNullException>(() => test.Base64Encode());
        }

        /// <summary>
        ///     The base 64 decode empty string returns empty string.
        /// </summary>
        [Fact]
        public void Base64EncodeValidStringReturnsEncodedString()
        {
            "test".Base64Encode().Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        ///     The hash invalid number of rounds throws argument out of range exception.
        /// </summary>
        [Fact]
        public void HashInvalidNumberOfRoundsThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "test".Hash(null, -100));
        }

        /// <summary>
        /// The hash null argument.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashInvalidTargetThrowsArgumentException(string target)
        {
            Assert.Throws<ArgumentException>(() => target.Hash());
        }

        /// <summary>
        /// The hash with invalid target.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashWithInvalidTargetThrowsArgumentException(string target)
        {
            Assert.Throws<ArgumentException>(() => target.Hash(null));
        }

        /// <summary>
        ///     The hash with null salt.
        /// </summary>
        [Fact]
        public void HashWithNullSalt()
        {
            Assert.Throws<ArgumentNullException>(() => "test".Hash(null));
        }

        /// <summary>
        ///     The hash string.
        /// </summary>
        [Fact]
        public void StringsHashedWithSameSaltShouldBeEqual()
        {
            var test = "Rudy James";
            var salt = StringExtensions.GenerateSalt();
            var result = test.Hash(salt);
            var result2 = test.Hash(salt);
            result.Should().Be(result2);
        }
    }
}
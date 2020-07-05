//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using FluentAssertions;
using JamesConsulting.Cryptography;
using Xunit;

namespace JamesConsulting.Tests.Cryptography
{
    /// <summary>
    ///     The string extensions tests.
    /// </summary>
    public class StringExtensionsTests
    {
        /// <summary>
        ///     The hash null argument.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashInvalidTargetThrowsArgumentNullException(string target)
        {
            Assert.Throws<ArgumentNullException>(() => target.Hash());
        }

        /// <summary>
        ///     The hash with invalid target.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashWithInvalidTargetThrowsArgumentNullException(string target)
        {
            Assert.Throws<ArgumentNullException>(() => target.Hash(default!));
        }

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
            Assert.Throws<ArgumentNullException>(() => default(string)!.Base64Decode());
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
            Assert.Throws<ArgumentNullException>(() => default(string)!.Base64Encode());
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
            Assert.Throws<ArgumentOutOfRangeException>(() => "test".Hash(new byte[3], -100));
        }

        [Fact]
        public void HashReturnHashedStringWithSalt()
        {
            var (hashedString, salt) = "test".Hash();
            salt.Should().NotBeEmpty();
            hashedString.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        ///     The hash with null salt.
        /// </summary>
        [Fact]
        public void HashWithNullSalt()
        {
            Assert.Throws<ArgumentNullException>(() => "test".Hash(default(byte[])!));
        }

        /// <summary>
        ///     The hash string.
        /// </summary>
        [Fact]
        public void StringsHashedWithSameSaltShouldBeEqual()
        {
            const string test = "Rudy James";
            var salt = JamesConsulting.Cryptography.StringExtensions.GenerateSalt();
            var result = test.Hash(salt);
            var result2 = test.Hash(salt);
            result.Should().Be(result2);
        }
    }
}
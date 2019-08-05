﻿//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="TypeExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Tests.Reflection
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using JamesConsulting.Reflection;

    using Xunit;

    /// <summary>
    /// The type extensions tests.
    /// </summary>
    public class TypeExtensionsTests
    {
        /// <summary>
        /// The instance type.
        /// </summary>
        private readonly Type instanceType = typeof(MyInterface);

        /// <summary>
        /// The get method info from string null type throws argument null exception.
        /// </summary>
        [Fact]
        public void GetMethodInfoFromStringNullTypeThrowsArgumentNullException()
        {
            Type test = null;
            Assert.Throws<ArgumentNullException>(() => test.GetMethodInfoFromString(""));
        }

        /// <summary>
        /// The to method info returns generic method info from method name.
        /// </summary>
        [Fact]
        public void ToMethodInfoReturnsGenericMethodInfoFromMethodName()
        {
            var expected = this.instanceType.GetMethods().FirstOrDefault(x => (x.Name == "GetClassById") && x.IsGenericMethod && x.GetGenericArguments().Length == 1);
            var actual = this.instanceType.GetMethodInfoFromString(expected.ToString());
            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        /// The to method info returns method info from method name.
        /// </summary>
        [Fact]
        public void ToMethodInfoReturnsMethodInfoFromMethodName()
        {
            var expected = this.instanceType.GetMethods().FirstOrDefault(x => (x.Name == "GetClassById") && !x.IsGenericMethod);
            var actual = this.instanceType.GetMethodInfoFromString(expected.ToString());
            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        /// The to method info returns multiple generic method info from method name.
        /// </summary>
        [Fact]
        public void ToMethodInfoReturnsMultipleGenericMethodInfoFromMethodName()
        {
            var expected = this.instanceType.GetMethods().FirstOrDefault(x => (x.Name == "GetClassById") && x.IsGenericMethod && (x.GetGenericArguments().Length > 1));
            var actual = this.instanceType.GetMethodInfoFromString(expected.ToString());
            this.instanceType.GetMethodInfoFromString(expected.ToString());
            actual.Should().BeSameAs(expected);
        }

        /// <summary>
        /// The to method info throws argument exception invalid string.
        /// </summary>
        /// <param name="test">
        /// The test.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ToMethodInfoThrowsArgumentExceptionInvalidString(string test)
        {
            Assert.Throws<ArgumentException>(() => this.instanceType.GetMethodInfoFromString(test));
        }

        /// <summary>
        /// The to method info throws argument null exception null string.
        /// </summary>
        [Fact]
        public void ToMethodInfoThrowsArgumentNullExceptionNullString()
        {
            string test = null;
            Assert.Throws<ArgumentNullException>(() => this.instanceType.GetMethodInfoFromString(test));
        }
    }
}
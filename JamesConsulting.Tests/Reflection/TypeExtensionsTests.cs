//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="TypeExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using JamesConsulting.Reflection;
using Xunit;

namespace JamesConsulting.Tests.Reflection
{
    /// <summary>
    ///     The type extensions tests.
    /// </summary>
    public class TypeExtensionsTests
    {
        /// <summary>
        ///     The instance type.
        /// </summary>
        private static readonly Type _instanceType = typeof(MyInterface);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GetMethodInfoFromStringEmptyOrWhitespaceMethodThrowsArgumentException(string method)
        {
            Assert.Throws<ArgumentException>(() => typeof(string).GetMethodInfoFromString(method));
        }

        /// <summary>
        ///     The to method info throws argument exception invalid string.
        /// </summary>
        /// <param name="test">
        ///     The test.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ToMethodInfoThrowsArgumentExceptionInvalidString(string test)
        {
            Assert.Throws<ArgumentException>(() => _instanceType.GetMethodInfoFromString(test));
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("TestAsync")]
        public void IsAsyncWithResultReturnsFalseWhenMethodIsNotAsync(string method)
        {
            _instanceType.GetMethod(method)?.IsAsyncWithResult().Should().BeFalse();
        }

        /// <summary>
        ///     The get method info from string null type throws argument null exception.
        /// </summary>
        [Fact]
        public void GetMethodInfoFromStringNullTypeThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(Type)!.GetMethodInfoFromString(""));
        }

        [Fact]
        public void GetMethodInfoFromStringReturnsMethodInfo()
        {
            var expected = _instanceType.GetMethods().First(x => x.Name == "GetClassById");
            _instanceType!.GetMethodInfoFromString(expected.ToString()!)?.Should().BeSameAs(expected);
        }

        [Fact]
        public void GetMethodInfoFromStringReturnsNullIfMethodIsNotPartOfClass()
        {
            GetType().GetMethodInfoFromString("Rudy").Should().BeNull();
        }

        [Fact]
        public void HasReturnValueReturnsFalseWhenMethodHasReturnTypeTask()
        {
            _instanceType.GetMethod("TestAsync")?.HasReturnValue().Should().BeFalse();
        }

        [Fact]
        public void HasReturnValueReturnsFalseWhenMethodHasReturnTypeVoid()
        {
            _instanceType.GetMethod("Test")?.HasReturnValue().Should().BeFalse();
        }

        [Fact]
        public void HasReturnValueReturnsTrueWhenMethodHasReturnValue()
        {
            var expected = _instanceType.GetMethods().First(x => x.Name == "GetClassById");
            _instanceType.GetMethod(expected.Name)?.HasReturnValue().Should().BeTrue();
        }

        [Fact]
        public void HasReturnValueThrowsArgumentNullExceptionWhenMethodInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(MethodInfo)!.HasReturnValue());
        }

        [Fact]
        public void IsAsyncReturnsFalseWhenMethodIsNotAsync()
        {
            _instanceType.GetMethod("Test")?.IsAsync().Should().BeFalse();
        }

        [Fact]
        public void IsAsyncReturnsTrueWhenMethodIsAsync()
        {
            _instanceType.GetMethod("TestAsync")?.IsAsync().Should().BeTrue();
        }

        [Fact]
        public void IsAsyncThrowsArgumentNullExceptionWhenMethodInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(MethodInfo)!.IsAsync());
        }

        [Fact]
        public void IsAsyncWithResultReturnsTrueWhenMethodIsAsync()
        {
            _instanceType.GetMethod("GetClassById")?.IsAsyncWithResult().Should().BeTrue();
        }

        [Fact]
        public void IsAsyncWithResultThrowsArgumentNullExceptionWhenMethodInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(MethodInfo)!.IsAsyncWithResult());
        }

        [Fact]
        public void IsConcreteClassAbstractClassTypeReturnsFalse()
        {
            typeof(DbConnection).IsConcreteClass().Should().BeFalse();
        }

        [Fact]
        public void IsConcreteClassConcreteClassTypeReturnsTrue()
        {
            GetType().IsConcreteClass().Should().BeTrue();
        }

        [Fact]
        public void IsConcreteClassInterfaceTypeReturnsFalse()
        {
            typeof(ICloneable).IsConcreteClass().Should().BeFalse();
        }

        [Fact]
        public void IsConcreteClassThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(Type)!.IsConcreteClass());
        }

        /// <summary>
        ///     The to method info returns method info from method name.
        /// </summary>
        [Fact]
        public void ToMethodInfoReturnsMethodInfoFromMethodName()
        {
            var expected = _instanceType.GetMethods().First(x => x.Name == "GetClassById");
            var actual = _instanceType!.GetMethodInfoFromString(expected!.ToString()!);
            actual!.Should().BeSameAs(expected);
        }

        /// <summary>
        ///     The to method info throws argument null exception null string.
        /// </summary>
        [Fact]
        public void ToMethodInfoThrowsArgumentExceptionNullString()
        {
            Assert.Throws<ArgumentException>(() => _instanceType.GetMethodInfoFromString(default!));
        }
    }
}
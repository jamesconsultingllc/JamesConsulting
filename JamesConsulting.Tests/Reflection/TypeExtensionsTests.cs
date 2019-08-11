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
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GetMethodInfoFromStringEmptyOrWhitespaceMethodThrowsArgumentException(string method)
        {
            Assert.Throws<ArgumentException>(() => typeof(string).GetMethodInfoFromString(method));
        }

        [Fact]
        public void GetMethodInfoFromStringReturnsCachedMethodOnSubsequentRequests()
        {
            GetType().GetMethodInfoFromString(nameof(IsConcreteClassInterfaceTypeReturnsFalse));
            GetType().GetMethodInfoFromString(nameof(IsConcreteClassInterfaceTypeReturnsFalse));
        }

        [Fact]
        public void GetMethodInfoFromStringReturnsNullIfMethodIsNotPartOfClass()
        {
            GetType().GetMethodInfoFromString("Rudy").Should().BeNull();
        }
        
        [Fact]
        public void GetMethodInfoFromStringReturnsMethodInfo()
        {
            var expected = instanceType.GetMethods().FirstOrDefault(x => (x.Name == "GetClassById"));
            instanceType.GetMethodInfoFromString(expected.ToString()).Should().Equals(expected);
        }
        
        /// <summary>
        /// The to method info returns method info from method name.
        /// </summary>
        [Fact]
        public void ToMethodInfoReturnsMethodInfoFromMethodName()
        {
            var expected = instanceType.GetMethods().FirstOrDefault(x => (x.Name == "GetClassById"));
            var actual = instanceType.GetMethodInfoFromString(expected.ToString());
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
            Assert.Throws<ArgumentException>(() => instanceType.GetMethodInfoFromString(test));
        }

        /// <summary>
        /// The to method info throws argument null exception null string.
        /// </summary>
        [Fact]
        public void ToMethodInfoThrowsArgumentExceptionNullString()
        {
            Assert.Throws<ArgumentException>(() => instanceType.GetMethodInfoFromString(null));
        }

        [Fact]
        public void IsConcreteClassThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(Type).IsConcreteClass());
        }

        [Fact]
        public void IsConcreteClassInterfaceTypeReturnsFalse()
        {
            typeof(ICloneable).IsConcreteClass().Should().BeFalse();
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
        public void HasReturnValueThrowsArgumentNullExceptionWhenMethodInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(MethodInfo).HasReturnValue());
        }

        [Fact]
        public void HasReturnValueReturnsTrueWhenMethodHasReturnValue()
        {
            var expected = instanceType.GetMethods().FirstOrDefault(x => (x.Name == "GetClassById"));
            instanceType.GetMethod(expected.Name).HasReturnValue().Should().BeTrue();
        }

        [Fact]
        public void HasReturnValueReturnsFalseWhenMethodHasReturnTypeTask()
        {
            instanceType.GetMethod("TestAsync").HasReturnValue().Should().BeFalse();
        }
        
        [Fact]
        public void HasReturnValueReturnsFalseWhenMethodHasReturnTypeVoid()
        {
            instanceType.GetMethod("Test").HasReturnValue().Should().BeFalse();
        }

        [Fact]
        public void IsAsyncThrowsArgumentNullExceptionWhenMethodInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(MethodInfo).IsAsync());
        }
        
        [Fact]
        public void IsAsyncReturnsTrueWhenMethodIsAsync()
        {
            instanceType.GetMethod("TestAsync").IsAsync().Should().BeTrue();
        }
        
        [Fact]
        public void IsAsyncReturnsFalseWhenMethodIsNotAsync()
        {
            instanceType.GetMethod("Test").IsAsync().Should().BeFalse();
        }
        
        [Fact]
        public void IsAsyncWithResultThrowsArgumentNullExceptionWhenMethodInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(MethodInfo).IsAsyncWithResult());
        }
        
        [Fact]
        public void IsAsyncWithResultReturnsTrueWhenMethodIsAsync()
        {
            instanceType.GetMethod("GetClassById").IsAsyncWithResult().Should().BeTrue();
        }
        
        [Theory]
        [InlineData("Test")]
        [InlineData("TestAsync")]
        public void IsAsyncWithResultReturnsFalseWhenMethodIsNotAsync(string method)
        {
            instanceType.GetMethod(method).IsAsyncWithResult().Should().BeFalse();
        }
    }
}
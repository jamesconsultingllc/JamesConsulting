//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObjectExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace JamesConsulting.Tests
{
    /// <summary>
    ///     The object extensions tests.
    /// </summary>
    public class ObjectExtensionsTests
    {
        /// <summary>
        ///     The test.
        /// </summary>
        public class Test
        {
            /// <summary>
            ///     Gets or sets the value 1.
            /// </summary>
            public string? Value1 { get; set; }

            /// <summary>
            ///     Gets or sets the value 2.
            /// </summary>
            public int Value2 { get; set; }

            public DateTime Value3 { get; set; }
            public TimeSpan Value4 { get; set; }

            public Test2[]? Value5 { get; set; }
        }

        /// <summary>
        ///     The test.
        /// </summary>
        [Serializable]
        public class Test2
        {
            /// <summary>
            ///     Gets or sets the value 1.
            /// </summary>
            public string? Value1 { get; set; }

            /// <summary>
            ///     Gets or sets the value 2.
            /// </summary>
            public int Value2 { get; set; }

            /// <summary>
            ///     Gets or sets the value 2.
            /// </summary>
            public int Value3 { get; set; }
        }

        /// <summary>
        ///     The mask as static method null object throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskAsStaticMethodNullObjectThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(object)!.Mask());
        }

        /// <summary>
        ///     The mask empty ignore list throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskEmptyIgnoreListThrowsArgumentNullException()
        {
            object test = new Test();
            Assert.Throws<ArgumentNullException>(() => test.Mask());
        }

        /// <summary>
        ///     The mask masks given values.
        /// </summary>
        [Fact]
        public void MaskMasksGivenValues()
        {
            var test = new Test
            {
                Value1 = "MyPassword", Value2 = 32, Value3 = DateTime.Now, Value4 = TimeSpan.FromMinutes(3), Value5 = new[]
                {
                    new Test2 {Value1 = "test", Value2 = 2},
                    new Test2 {Value1 = "test", Value2 = 2},
                    new Test2 {Value1 = "test", Value2 = 2}
                }
            };

            var maskedTest = test.Mask("Value2", "Value3", "Value4", "Value5[*].Value1");
            maskedTest.Value1.Should().Be("MyPassword");
            maskedTest.Value2.Should().Be(default);
            maskedTest.Value3.Should().Be(default);
            maskedTest.Value4.Should().Be(default);

            if (maskedTest.Value5 == null) return;

            foreach (var value5 in maskedTest.Value5) value5.Value1.Should().Be(default);
        }

        /// <summary>
        ///     The mask null ignore list throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskNullIgnoreListThrowsArgumentNullException()
        {
            object test = new Test();
            Assert.Throws<ArgumentNullException>(() => test.Mask(default!));
        }

        /// <summary>
        ///     The mask null object throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskNullObjectThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(object)!.Mask());
        }

        [Fact]
        public void NullObjectToJsonThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(object)!.ToJson());
        }

        [Fact]
        public void ObjectToJsonShouldNotBeNull()
        {
            object obj = new Test();
            obj.ToJson().Should().NotBeNull();
        }

        /// <summary>
        ///     The serialize to json stream.
        /// </summary>
        [Fact]
        public void SerializeToJsonStream()
        {
            var test = new Test2 {Value1 = "test", Value2 = 2};
            var stream = test.SerializeToJsonStream(new MemoryStream());
            stream.Should().NotBeNull();
            stream.Length.Should().BeGreaterThan(0);
        }

        /// <summary>
        ///     The serialize to json stream null object throws argument null exception.
        /// </summary>
        [Fact]
        public void SerializeToJsonStreamNullObjectThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => default(object)!.SerializeToJsonStream(default!));
        }

        /// <summary>
        ///     The serialize to json stream null stream throws argument null exception.
        /// </summary>
        [Fact]
        public void SerializeToJsonStreamNullStreamThrowsArgumentNullException()
        {
            var test = new Test2 {Value1 = "test", Value2 = 2};
            Assert.Throws<ArgumentNullException>(() => test.SerializeToJsonStream(default!));
        }

        [Fact]
        public void StringToJsonShouldReturnItself()
        {
            object obj = "test";
            obj.ToJson().Should().BeEquivalentTo(obj.ToString());
        }
    }
}
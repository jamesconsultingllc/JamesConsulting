//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="ObjectExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
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
        /// The from byte array.
        /// </summary>
        [Fact]
        public void FromByteArray()
        {
            var test = new Test2 { Value1 = "test", Value2 = 2 };
            var bytes = test.ToByteArray();
            var newTest = bytes.FromByteArray<Test2>();
            newTest.Value1.Should().BeEquivalentTo(test.Value1);
            newTest.Value2.Should().Be(test.Value2);
            newTest.Value3.Should().Be(test.Value3);
        }

        /// <summary>
        /// The from byte array empty array throws argument exception.
        /// </summary>
        [Fact]
        public void FromByteArrayEmptyArrayThrowsArgumentException()
        {
            var bytes = new byte[0];
            Assert.Throws<ArgumentException>(() => bytes.FromByteArray<Test>());
        }

        /// <summary>
        /// The from byte array not serializable class throws.
        /// </summary>
        [Fact]
        public void ToByteArrayNotSerializableClassThrows()
        {
            var test = new Test { Value1 = "test", Value2 = 2 };
            Assert.Throws<InvalidOperationException>(() => test.ToByteArray());
        }

        
        [Fact]
        public void ToByteArrayNullObjectThrowsArgumentNullException()
        {
            byte[] bytes = null;
            Assert.Throws<ArgumentNullException>(() => bytes.ToByteArray());
        }

        /// <summary>
        /// The from byte array null array throws argument null exception.
        /// </summary>
        [Fact]
        public void FromByteArrayNullArrayThrowsArgumentNullException()
        {
            byte[] bytes = null;
            Assert.Throws<ArgumentNullException>(() => bytes.FromByteArray<Test>());
        }

        /// <summary>
        ///     The mask as static method null object throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskAsStaticMethodNullObjectThrowsArgumentNullException()
        {
            object test = null;
            Assert.Throws<ArgumentNullException>(() => test.Mask());
        }

        /// <summary>
        ///     The mask empty ignore list throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskEmptyIgnoreListThrowsArgumentNullException()
        {
            object test = new Test();
            Assert.Throws<ArgumentException>(() => test.Mask());
        }

        /// <summary>
        ///     The mask masks given values.
        /// </summary>
        [Fact]
        public void MaskMasksGivenValues()
        {
            var test = new Test { Value1 = "MyPassword", Value2 = 32 };
            dynamic maskedTest = test.Mask("Value2");
            ((string)maskedTest.Value2.Value).Should().BeEquivalentTo("*****");
        }

        /// <summary>
        ///     The mask null ignore list throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskNullIgnoreListThrowsArgumentNullException()
        {
            object test = new Test();
            Assert.Throws<ArgumentNullException>(() => test.Mask(null));
        }

        /// <summary>
        ///     The mask null object throws argument null exception.
        /// </summary>
        [Fact]
        public void MaskNullObjectThrowsArgumentNullException()
        {
            object test = null;
            Assert.Throws<ArgumentNullException>(() => test.Mask());
        }

        /// <summary>
        /// The serialize to json stream.
        /// </summary>
        [Fact]
        public void SerializeToJsonStream()
        {
            var test = new Test2 { Value1 = "test", Value2 = 2 };
            var stream = test.SerializeToJsonStream(new MemoryStream());
            stream.Should().NotBeNull();
            stream.Length.Should().BeGreaterThan(0);
        }

        /// <summary>
        /// The serialize to json stream null object throws argument null exception.
        /// </summary>
        [Fact]
        public void SerializeToJsonStreamNullObjectThrowsArgumentNullException()
        {
            object obj = null;
            Assert.Throws<ArgumentNullException>(() => obj.SerializeToJsonStream(null));
        }

        /// <summary>
        /// The serialize to json stream null stream throws argument null exception.
        /// </summary>
        [Fact]
        public void SerializeToJsonStreamNullStreamThrowsArgumentNullException()
        {
            var test = new Test2 { Value1 = "test", Value2 = 2 };
            Assert.Throws<ArgumentNullException>(() => test.SerializeToJsonStream(null));
        }

        [Fact]
        public void NullObjectToJsonShouldReturnNull()
        {
            object obj = null;
            obj.ToJson().Should().BeNull();
        }

        [Fact]
        public void NullObjectToJsonCompactShouldReturnNull()
        {
            object obj = null;
            obj.ToJsonCompact().Should().BeNull();
        }

        [Fact]
        public void StringToJsonShouldReturnItself()
        {
            object obj = "test";
            obj.ToJson().Should().BeEquivalentTo(obj.ToString());
        }

        [Fact]
        public void StringToJsonCompactShouldReturnItself()
        {
            object obj = "test";
            obj.ToJsonCompact().Should().BeEquivalentTo(obj.ToString());
        }

        [Fact]
        public void ObjectToJsonShouldNotBeNull()
        {
            object obj = new Test();
            obj.ToJson().Should().NotBeNull();
        }

        [Fact]
        public void ObjectToJsonCompactShouldNotBeNull()
        {
            object obj = new Test();
            obj.ToJsonCompact().Should().NotBeNull();
        }

        /// <summary>
        ///     The test.
        /// </summary>
        public class Test
        {
            /// <summary>
            ///     Gets or sets the value 1.
            /// </summary>
            public string Value1 { get; set; }

            /// <summary>
            ///     Gets or sets the value 2.
            /// </summary>
            public int Value2 { get; set; }
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
            public string Value1 { get; set; }

            /// <summary>
            ///     Gets or sets the value 2.
            /// </summary>
            public int Value2 { get; set; }

            /// <summary>
            ///     Gets or sets the value 2.
            /// </summary>
            public int Value3 { get; set; }
        }
    }
}
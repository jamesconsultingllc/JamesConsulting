using System;
using System.IO;
using System.Text;
using FluentAssertions;
using FluentAssertions.Common;
using JamesConsulting.IO;
using Utf8Json;
using Xunit;

namespace JamesConsulting.Tests.IO
{
    public class StreamExtensionsTests
    {
        [Serializable]
        public class MyClass
        {
            public string Property1 { get; set; } = string.Empty;
            public int Property2 { get; set; }

            public override bool Equals(object? obj)
            {
                if (!(obj is MyClass myClass))
                    return false;

                return myClass.Property1 == Property1 && myClass.Property2 == Property2;
            }

            public override int GetHashCode()
            {
#if NET461
                var hashcode = 35203352;
                var offset = -1521134295;
                hashcode *= offset + Property1.GetHashCode();
                hashcode *= offset + Property2.GetHashCode();
                return hashcode;
#else
                return HashCode.Combine(Property1, Property2);
#endif
            }
        }

        [Fact]
        public void DeserializeStreamRecreatesObject()
        {
            var test = new MyClass {Property1 = "Test", Property2 = 3};
            var ms = test.SerializeToJsonStream(new MemoryStream());
            var newTest = JsonSerializer.Deserialize<MyClass>(ms);
            newTest.Should().NotBeNull();
            newTest.IsSameOrEqualTo(test);
        }

        [Fact]
        public void IsExecutableExeStream()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream, Encoding.UTF8);
            writer.Write('M');
            writer.Write('Z');
            writer.Write("<Z1234239075032850jfddfjsldfjsdf");
            writer.Flush();
            stream.IsExecutable().Should().BeTrue();
        }

        [Fact]
        public void IsExecutableNonExeStream()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream, Encoding.UTF8);
            writer.Write('Z');
            writer.Write("<Z1234239075032850jfddfjsldfjsdf");
            writer.Flush();
            stream.IsExecutable().Should().BeFalse();
        }

        [Fact]
        public void IsExecutableThrowsArgumentNullExceptionWhenStreamIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(Stream)!.IsExecutable());
        }
    }
}
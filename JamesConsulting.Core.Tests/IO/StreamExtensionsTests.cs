namespace JamesConsulting.Core.Tests.IO
{
    using System;
    using System.IO;
    using System.Text;

    using FluentAssertions;
    using FluentAssertions.Common;

    using JamesConsulting.Core.IO;

    using Xunit;

    public class StreamExtensionsTests
    {
        [Fact]
        public void DeserializeThrowsArgumentNullExceptionWhenStreamIsNull()
        {
            Stream stream = null;
            Assert.Throws<ArgumentNullException>(() => stream.DeserializeJson<object>());
        }

        [Fact]
        public void DeserializeStreamRecreatesObject()
        {
            var test = new MyClass { Property1 = "Test", Property2 = 3 };
            var ms = test.SerializeToJsonStream(new MemoryStream());
            var newTest = ms.DeserializeJson<MyClass>();
            newTest.Should().NotBeNull();
            newTest.IsSameOrEqualTo(test);
        }

        [Fact]
        public void IsExecutableThrowsArgumentNullExceptionWhenStreamIsNull()
        {
            Stream stream = null;
            Assert.Throws<ArgumentNullException>(() => stream.IsExecutable());
        }

        [Fact]
        public void IsExecutableExeStream()
        {
            var stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
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
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
            writer.Write('Z');
            writer.Write("<Z1234239075032850jfddfjsldfjsdf");
            writer.Flush();
            stream.IsExecutable().Should().BeFalse();
        }

        [Serializable]
        private class MyClass
        {
            public string Property1 { get; set; }
            public int Property2 { get; set; }

            public override bool Equals(object obj)
            {

                var myClass = obj as MyClass;

                if(myClass == null)
                    return false;

                return myClass.Property1 == this.Property1 && myClass.Property2 == this.Property2;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Property1, Property2);
            }
        }
    }
}

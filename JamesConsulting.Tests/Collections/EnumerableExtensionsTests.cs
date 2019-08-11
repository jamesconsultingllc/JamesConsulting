using System;
using System.Collections.Generic;
using FluentAssertions;
using JamesConsulting.Collections;
using Xunit;

namespace JamesConsulting.Tests.Collections
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void EqualsThrowsArgumentNullExceptionWhenArg1IsNull()
        {
            Assert.Throws<ArgumentNullException>(() => default(IEnumerable<MyInterface>).IsEqualTo(null));
        }

        [Fact]
        public void EqualsReturnsFalseWhenArg2IsNull()
        {
            new[] {new MyClass()}.IsEqualTo(null).Should().BeFalse();
        }
    }
}
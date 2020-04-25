using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using JamesConsulting.Threading;
using Xunit;
using MethodInfoExtensions = JamesConsulting.Threading.MethodInfoExtensions;

namespace JamesConsulting.Tests.Threading
{
    public class MethodInfoExtensionsTests
    {
        private static readonly Type instanceType = typeof(MyInterface);
        
        [Fact]
        public void CreateTaskResultThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => MethodInfoExtensions.CreateTaskResult(default,null));
        }

        [Fact]
        public void CreateTaskResultReturnTypeNullThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => instanceType.GetMethod("Test").CreateTaskResult(null));
        }

        [Fact]
        public void CreateTaskResultReturnsTaskResult()
        {
            var result = instanceType.GetMethod("GetClassById").CreateTaskResult(new MyClass() { X = 1 });
            result.Should().BeOfType<Task<MyClass>>();
            (result as Task<MyClass>).Result.X.Should().Be(1);
        }
    }
}
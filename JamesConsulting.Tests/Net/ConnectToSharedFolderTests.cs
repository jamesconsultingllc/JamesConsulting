using System;
using System.Net;
using FluentAssertions;
using JamesConsulting.Net;
using Moq;
using Moq.Protected;
using Xunit;

namespace JamesConsulting.Tests.Net
{
    public class ConnectToSharedFolderTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default)]
        public void Constructor_ThrowsArgumentException_WhenNetworkCredentialsUsername_IsInvalid(string userName)
        {
            var exception = Assert.Throws<ArgumentException>(() => new ConnectToSharedFolder("Test", new NetworkCredential() { UserName = userName }));
            exception.ParamName.Should().Be("credentials");
            exception.Message.Should().StartWith("UserName specified cannot be null or whitespace.");
        }

        [Fact]
        public void Constructor_CallsConnect_WhenParametersAreValid()
        {
            var connectToSharedFolder =
                new Mock<ConnectToSharedFolder>("rjames", new NetworkCredential("Test", "Test"));
            connectToSharedFolder.Protected().Setup("Connect");
            _ = connectToSharedFolder.Object;
            connectToSharedFolder.Protected().Verify("Connect", Times.Once());
        }
        
        [Fact]
        public void Dispose_CallsDispose_WithTrue()
        {
            var connectToSharedFolder =
                new Mock<ConnectToSharedFolder>("rjames", new NetworkCredential("Test", "Test"));
            connectToSharedFolder.Protected().Setup("Connect");
            connectToSharedFolder.Object.Dispose();
            connectToSharedFolder.Protected().Verify("Dispose", Times.Once(), true, true);
        }
    }
}
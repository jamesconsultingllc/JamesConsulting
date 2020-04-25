using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using FluentAssertions.Common;
using JamesConsulting.Net.Http;
using Xunit;

namespace JamesConsulting.Tests.Net.Http
{
    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void SetHeadersResultHeaderListShouldMatchHeadersPassedIn()
        {
            var requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("Test", "Test");

            IDictionary<string, string> headers = new Dictionary<string, string> {{"Test2", "Test2 "}, {"Test3", "Test3 "}};
            requestMessage.SetHeaders(headers);

            requestMessage.Headers.Contains("Test").Should().BeFalse();
            requestMessage.Headers.Count().IsSameOrEqualTo(2);
        }

        [Fact]
        public void SetHeadersThrowsArugmentNullExceptionWhenHeadersIsNull()
        {
            var requestMessage = new HttpRequestMessage();
            Assert.Throws<ArgumentNullException>(() => requestMessage.SetHeaders(null));
        }

        [Fact]
        public void SetHeadersThrowsArugmentNullExceptionWhenRequestMessageIsNull()
        {
            HttpRequestMessage requestMessage = null;
            Assert.Throws<ArgumentNullException>(() => requestMessage.SetHeaders(null));
        }
    }
}
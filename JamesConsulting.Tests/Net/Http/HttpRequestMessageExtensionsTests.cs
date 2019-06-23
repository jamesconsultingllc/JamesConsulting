namespace JamesConsulting.Tests.Net.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    using FluentAssertions;
    using FluentAssertions.Common;

    using JamesConsulting.Net.Http;

    using Xunit;

    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void SetHeadersThrowsArugmentNullExceptionWhenRequestMessageIsNull()
        {
            HttpRequestMessage requestMessage = null;
            Assert.Throws<ArgumentNullException>(() => requestMessage.SetHeaders(null));
        }

        [Fact]
        public void SetHeadersThrowsArugmentNullExceptionWhenHeadersIsNull()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            Assert.Throws<ArgumentNullException>(() => requestMessage.SetHeaders(null));
        }

        [Fact]
        public void SetHeadersResultHeaderListShouldMatchHeadersPassedIn()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("Test", "Test");

            IDictionary<string, string> headers = new Dictionary<string, string> { { "Test2", "Test2 " }, { "Test3", "Test3 " } };
            requestMessage.SetHeaders(headers);

            requestMessage.Headers.Contains("Test").Should().BeFalse();
            requestMessage.Headers.Count().IsSameOrEqualTo(2);
        }
    }
}

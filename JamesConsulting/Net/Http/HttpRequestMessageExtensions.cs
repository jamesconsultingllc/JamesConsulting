using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Net.Http
{
    /// <summary>
    ///     The http request message extensions.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// The set headers.
        /// </summary>
        /// <param name="httpRequestMessage">
        /// The http request message.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <returns>
        /// The <see cref="HttpRequestMessage"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="httpRequestMessage"/> or <paramref name="headers"/> is null
        /// </exception>
        public static HttpRequestMessage SetHeaders(
            [NotNull] this HttpRequestMessage httpRequestMessage,
            [NotNull] IDictionary<string, string> headers)
        {
            if (httpRequestMessage.Headers.Any()) httpRequestMessage.Headers.Clear();

            foreach (var headerKey in headers.Keys)
                httpRequestMessage.Headers.Add(headerKey, headers[headerKey]);

            return httpRequestMessage;
        }
    }
}
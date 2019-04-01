// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtensions.cs" company="CBRE">
//   
// </copyright>
// <summary>
//   The http request message extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Net.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    ///     The http request message extensions.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Sets the headers on the HttpRequestMessage
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
        /// </exception>
        public static HttpRequestMessage SetHeaders(this HttpRequestMessage httpRequestMessage, IDictionary<string, string> headers)
        {
            if (httpRequestMessage == null) throw new ArgumentNullException(nameof(httpRequestMessage));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (httpRequestMessage.Headers.Any())
                httpRequestMessage.Headers.Clear();

            foreach (var headerKey in headers.Keys)
                if (headers[headerKey] != null)
                    httpRequestMessage.Headers.Add(headerKey, headers[headerKey]);

            return httpRequestMessage;
        }
    }
}
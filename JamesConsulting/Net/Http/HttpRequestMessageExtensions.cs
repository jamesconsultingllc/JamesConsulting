//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="HttpRequestMessageExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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
        public static HttpRequestMessage SetHeaders(this HttpRequestMessage httpRequestMessage, IDictionary<string, string> headers)
        {
            Validate(httpRequestMessage, headers);

            if (httpRequestMessage.Headers.Any())
            {
                httpRequestMessage.Headers.Clear();
            }

            foreach (var headerKey in headers.Keys)
            {
                if (headers[headerKey] != null)
                {
                    httpRequestMessage.Headers.Add(headerKey, headers[headerKey]);
                }
            }

            return httpRequestMessage;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="httpRequestMessage">
        /// The http request message.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="httpRequestMessage"/> or <paramref name="headers"/> is null
        /// </exception>
        private static void Validate(HttpRequestMessage httpRequestMessage, IDictionary<string, string> headers)
        {
            if (httpRequestMessage == null)
            {
                throw new ArgumentNullException(nameof(httpRequestMessage));
            }

            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
        }
    }
}
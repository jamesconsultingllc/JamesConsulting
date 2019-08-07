//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHostExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JamesConsulting.Hosting
{
    /// <summary>
    ///     The IHost extension methods.
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        public static void Initialize(this IHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetServices<IHostInitializer>();

                if (services != null)
                {
                    Parallel.ForEach(services, svc => svc.Initialize());
                }
            }
        }

        /// <summary>
        /// The initialize async.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static Task InitializeAsync(this IHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return host.InitializeInternalAsync();
        }

        /// <summary>
        /// The initialize internal async.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private static async Task InitializeInternalAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetServices<IHostInitializerAsync>();

                if (services != null)
                {
                    await Task.Run(() => Parallel.ForEach(services, svc => svc.InitializeAsync())).ConfigureAwait(false);
                }
            }
        }
    }
}
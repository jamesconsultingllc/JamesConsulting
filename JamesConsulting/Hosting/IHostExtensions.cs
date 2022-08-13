using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PostSharp.Patterns.Contracts;

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
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="host"/> is null
        /// </exception>
        public static void Initialize([NotNull] this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider.GetServices<IHostInitializer>();
            Parallel.ForEach(services, svc => svc.Initialize());
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
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="host"/> is null
        /// </exception>
        public static Task InitializeAsync([NotNull] this IHost host)
        {
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
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="host"/> is null
        /// </exception>
        private static async Task InitializeInternalAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider.GetServices<IHostInitializerAsync>();
            await Task.Run(() => Parallel.ForEach(services, svc => svc.InitializeAsync())).ConfigureAwait(false);
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHostInitializer.cs" company="CBRE">
//   
// </copyright>
// // <summary>
//   The HostInitializer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Hosting
{
    using System.Threading.Tasks;

    /// <summary>
    /// The HostInitializer interface.
    /// </summary>
    public interface IHostInitializerAsync
    {
        /// <summary>
        /// The initialize async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task InitializeAsync();
    }
}
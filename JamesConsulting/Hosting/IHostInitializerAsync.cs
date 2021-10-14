using System.Threading.Tasks;

namespace JamesConsulting.Hosting
{
    /// <summary>
    ///     The HostInitializer interface.
    /// </summary>
    public interface IHostInitializerAsync
    {
        /// <summary>
        ///     The initialize async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task InitializeAsync();
    }
}
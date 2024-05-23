using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.InteropServices;
using Metalama.Patterns.Contracts;

namespace JamesConsulting.Net;

/// <summary>
///     Provides functionality to allow to connection with given network credentials
/// </summary>
public sealed class ConnectToSharedFolder : IDisposable
{
    /// <summary>
    /// The credentials.
    /// </summary>
    private readonly NetworkCredential credentials;

    /// <summary>
    /// The network name.
    /// </summary>
    private readonly string networkName;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectToSharedFolder"/> class. 
    /// </summary>
    /// <param name="networkName">
    /// Name of the shared network folder
    /// </param>
    /// <param name="credentials">
    /// Credentials for the user to impersonate
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="networkName"/> the UserName of the
    ///     <paramref name="credentials"/> is null, empty or whitespace
    /// </exception>
    public ConnectToSharedFolder([Required] string networkName, [Metalama.Patterns.Contracts.NotNull] NetworkCredential credentials)
    {
            if (string.IsNullOrWhiteSpace(credentials.UserName))
                throw new ArgumentException("UserName specified cannot be null or whitespace.", nameof(credentials));

            this.networkName = networkName;
            this.credentials = credentials;
        }

    /// <summary>
    /// Finalizes an instance of the <see cref="ConnectToSharedFolder"/> class. 
    /// </summary>
    [ExcludeFromCodeCoverage]
    ~ConnectToSharedFolder()
    {
            WNetCancelConnection2(networkName, 0, true);
        }

    /// <summary>
    /// The resource scope.
    /// </summary>
    private enum ResourceScope
    {
        /// <summary>
        /// The global network.
        /// </summary>
        GlobalNetwork,
    }

    /// <summary>
    /// The resource type.
    /// </summary>
    private enum ResourceType
    {
        /// <summary>
        /// The disk.
        /// </summary>
        Disk = 1,
    }

    /// <summary>
    ///     Connects to the shared folder with the given credentials
    /// </summary>
    /// <exception cref="Win32Exception">Thrown when connection is not successful</exception>
    [ExcludeFromCodeCoverage]
    public void Connect()
    {
            var netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                               ? credentials.UserName
                               : $@"{credentials.Domain}\{credentials.UserName}";

            var result = WNetAddConnection2(netResource, credentials.Password, userName, 0);

            if (result != 0) throw new Win32Exception(result, "Error connecting to remote share");
        }

    /// <summary>
    /// The dispose.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public void Dispose()
    {
            WNetCancelConnection2(networkName, 0, true);
            GC.SuppressFinalize(this);
        }

    [DllImport("mpr.dll")]
    private static extern int WNetAddConnection2(
        NetResource netResource,
        string password,
        string username,
        int flags);

    [DllImport("mpr.dll")]
    private static extern int WNetCancelConnection2(string name, int flags, bool force);

    /// <summary>
    /// The net resource.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [ExcludeFromCodeCoverage]
    private sealed class NetResource
    {
        /// <summary>
        /// Gets or sets the scope
        /// </summary>
        public ResourceScope Scope { get; set; }

        /// <summary>
        /// Gets or sets the resource type
        /// </summary>
        public ResourceType ResourceType { get; set; }
    }
}
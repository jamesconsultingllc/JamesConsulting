using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Net
{
    /// <summary>
    ///     Provides functionality to allow to connection with given network credentials
    /// </summary>
    public class ConnectToSharedFolder : IDisposable
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
        public ConnectToSharedFolder([Required] string networkName, [NotNull] NetworkCredential credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.UserName))
                throw new ArgumentException("UserName specified cannot be null or whitespace.", nameof(credentials));

            this.networkName = networkName;
            this.credentials = credentials;
            
            Connect();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ConnectToSharedFolder"/> class. 
        /// </summary>
        ~ConnectToSharedFolder()
        {
            Dispose(false);
        }

        /// <summary>
        /// The resource display type.
        /// </summary>
        private enum ResourceDisplayType
        {
            /// <summary>
            /// The generic.
            /// </summary>
            Generic = 0x0,

            /// <summary>
            /// The domain.
            /// </summary>
            Domain = 0x01,

            /// <summary>
            /// The server.
            /// </summary>
            Server = 0x02,

            /// <summary>
            /// The share.
            /// </summary>
            Share = 0x03,

            /// <summary>
            /// The file.
            /// </summary>
            File = 0x04,

            /// <summary>
            /// The group.
            /// </summary>
            Group = 0x05,

            /// <summary>
            /// The network.
            /// </summary>
            Network = 0x06,

            /// <summary>
            /// The root.
            /// </summary>
            Root = 0x07,

            /// <summary>
            /// The share admin.
            /// </summary>
            ShareAdmin = 0x08,

            /// <summary>
            /// The directory.
            /// </summary>
            Directory = 0x09,

            /// <summary>
            /// The tree.
            /// </summary>
            Tree = 0x0a,

            /// <summary>
            /// The nds container.
            /// </summary>
            NdsContainer = 0x0b
        }

        /// <summary>
        /// The resource scope.
        /// </summary>
        private enum ResourceScope
        {
            /// <summary>
            /// The connected.
            /// </summary>
            Connected = 1,

            /// <summary>
            /// The global network.
            /// </summary>
            GlobalNetwork,

            /// <summary>
            /// The remembered.
            /// </summary>
            Remembered,

            /// <summary>
            /// The recent.
            /// </summary>
            Recent,

            /// <summary>
            /// The context.
            /// </summary>
            Context
        }

        /// <summary>
        /// The resource type.
        /// </summary>
        private enum ResourceType
        {
            /// <summary>
            /// The any.
            /// </summary>
            Any = 0,

            /// <summary>
            /// The disk.
            /// </summary>
            Disk = 1,

            /// <summary>
            /// The print.
            /// </summary>
            Print = 2,

            /// <summary>
            /// The reserved.
            /// </summary>
            Reserved = 8
        }

        /// <summary>
        ///     Connects to the shared folder
        /// </summary>
        /// <exception cref="Win32Exception">Thrown when connection is not successful</exception>
        protected virtual void Connect()
        {
            var netResource = new NetResource
                                  {
                                      Scope = ResourceScope.GlobalNetwork,
                                      ResourceType = ResourceType.Disk,
                                      DisplayType = ResourceDisplayType.Share,
                                      RemoteName = networkName
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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(networkName, 0, true);
        }

        /// <summary>
        /// The w net add connection 2.
        /// </summary>
        /// <param name="netResource">
        /// The net resource.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(
            NetResource netResource,
            string password,
            string username,
            int flags);

        /// <summary>
        /// The w net cancel connection 2.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <param name="force">
        /// The force.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);

        /// <summary>
        /// The net resource.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private sealed class NetResource
        {
            /// <summary>
            /// The comment.
            /// </summary>
            public string Comment;

            /// <summary>
            /// The display type.
            /// </summary>
            public ResourceDisplayType DisplayType;

            /// <summary>
            /// The local name.
            /// </summary>
            public string LocalName;

            /// <summary>
            /// The provider.
            /// </summary>
            public string Provider;

            /// <summary>
            /// The remote name.
            /// </summary>
            public string RemoteName;

            /// <summary>
            /// The resource type.
            /// </summary>
            public ResourceType ResourceType;

            /// <summary>
            /// The scope.
            /// </summary>
            public ResourceScope Scope;

            /// <summary>
            /// The usage.
            /// </summary>
            public int Usage;
        }
    }
}
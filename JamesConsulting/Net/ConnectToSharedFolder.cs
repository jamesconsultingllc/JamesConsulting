// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="ConnectToSharedFolder.cs" company="James Consulting LLC">
// //     Copyright (c) 2021. All rights reserved
// // </copyright>
// // <summary>
// //   Please reference https://www.c-sharpcorner.com/blogs/how-to-access-network-drive-using-c-sharp for more details.
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;

namespace JamesConsulting.Net
{
    public class ConnectToSharedFolder : IDisposable
    {
        readonly string networkName;

        public ConnectToSharedFolder(string networkName, NetworkCredential credentials)
        {
            this.networkName = networkName;

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

            var result = WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                throw new Win32Exception(result, "Error connecting to remote share");
            }
        }

        ~ConnectToSharedFolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplayType DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        public enum ResourceScope
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        }

        public enum ResourceType
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplayType
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            ShareAdmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            NdsContainer = 0x0b
        }
    }
}
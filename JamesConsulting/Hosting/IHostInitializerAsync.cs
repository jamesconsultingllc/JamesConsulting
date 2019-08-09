//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHostInitializerAsync.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

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
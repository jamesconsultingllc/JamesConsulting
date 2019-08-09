//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHostExtensionsTests.cs" company="James Consulting LLC">
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
using System.Threading.Tasks;
using JamesConsulting.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace JamesConsulting.Tests.Hosting
{
    /// <summary>
    /// The i host extensions tests.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class IHostExtensionsTests
    {
        /// <summary>
        /// The initialize async call initialize on host initalizers.
        /// </summary>
        [Fact]
        public async Task InitializeAsyncCallInitializeOnHostInitalizers()
        {
            var services = CreateInitializers<IHostInitializerAsync>(3);
            var serviceProvider = new Mock<IServiceProvider>();
            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.SetupGet(x => x.ServiceProvider).Returns(serviceProvider.Object);
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IEnumerable<IHostInitializerAsync>))).Returns(services.Select(x => x.Object));
            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);
            var host = new Mock<IHost>();
            host.SetupGet(x => x.Services).Returns(serviceProvider.Object);
            await host.Object.InitializeAsync().ConfigureAwait(false);
            services.ForEach(x => x.Verify(y => y.InitializeAsync(), Times.Once()));
        }

        /// <summary>
        /// The initialize async null host throws argument null exception.
        /// </summary>
        [Fact]
        public async Task InitializeAsyncNullHostThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => default(IHost).InitializeAsync()).ConfigureAwait(false);
        }

        /// <summary>
        /// The initialize call initialize on host initalizers.
        /// </summary>
        [Fact]
        public void InitializeCallInitializeOnHostInitalizers()
        {
            var services = CreateInitializers<IHostInitializer>(3);
            var serviceProvider = new Mock<IServiceProvider>();
            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.SetupGet(x => x.ServiceProvider).Returns(serviceProvider.Object);
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IEnumerable<IHostInitializer>))).Returns(services.Select(x => x.Object));
            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);
            var host = new Mock<IHost>();
            host.SetupGet(x => x.Services).Returns(serviceProvider.Object);
            host.Object.Initialize();
            services.ForEach(x => x.Verify(y => y.Initialize(), Times.Once()));
        }

        /// <summary>
        /// The initialize null host throws argument null exception.
        /// </summary>
        [Fact]
        public void InitializeNullHostThrowsArgumentNullException()
        {
            IHost host = null;
            Assert.Throws<ArgumentNullException>(() => host.Initialize());
        }

        /// <summary>
        /// The create initializers.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T:List{Mock{T}}"/>.
        /// </returns>
        private List<Mock<T>> CreateInitializers<T>(int count)
            where T : class
        {
            var list = new List<Mock<T>>();

            for (var i = 0; i < count; i++)
            {
                list.Add(new Mock<T>());
            }

            return list;
        }
    }
}
//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHostExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Tests.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JamesConsulting.Core.Hosting;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Moq;

    using Xunit;

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
        public void InitializeAsyncCallInitializeOnHostInitalizers()
        {
            var services = this.CreateInitializers<IHostInitializerAsync>(3);
            var serviceProvider = new Mock<IServiceProvider>();
            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.SetupGet(x => x.ServiceProvider).Returns(serviceProvider.Object);
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IEnumerable<IHostInitializerAsync>))).Returns(services.Select(x => x.Object));
            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);
            var host = new Mock<IHost>();
            host.SetupGet(x => x.Services).Returns(serviceProvider.Object);
            var task = host.Object.InitializeAsync().ContinueWith(z => { services.ForEach(x => x.Verify(y => y.InitializeAsync(), Times.Once())); });
            Task.WaitAll(task);
        }

        /// <summary>
        /// The initialize async null host throws argument null exception.
        /// </summary>
        [Fact]
        public void InitializeAsyncNullHostThrowsArgumentNullException()
        {
            IHost host = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => host.InitializeAsync());
        }

        /// <summary>
        /// The initialize call initialize on host initalizers.
        /// </summary>
        [Fact]
        public void InitializeCallInitializeOnHostInitalizers()
        {
            var services = this.CreateInitializers<IHostInitializer>(3);
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
        /// The <see cref="List"/>.
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
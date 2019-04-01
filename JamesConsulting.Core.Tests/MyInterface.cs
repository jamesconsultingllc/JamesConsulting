//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="MyInterface.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Tests
{
    using System;
    using System.Threading.Tasks;

    using Xunit;

    /// <summary>
    ///     The my interface.
    /// </summary>
    internal class MyInterface : IInterface
    {
        /// <inheritdoc />
        public async Task<MyClass> GetClassById(int id)
        {
            await Task.Delay(100).ConfigureAwait(false);
            return new MyClass { X = id, Y = id.ToString() };
        }

        /// <inheritdoc />
        public Task<T> GetClassById<T>(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get class by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="K">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task<K> GetClassById<T, K>(int id)
            where K : class where T : class
        {
            GetClassById<T>(id);
            return default(Task<K>);
        }

        /// <inheritdoc />
        public void Test(int x, string y, MyClass myClass)
        {
            Console.WriteLine("testing");
        }

        /// <inheritdoc />
        public async Task TestAsync(int x, string y, MyClass myClass)
        {
            await Task.Delay(100).ConfigureAwait(false);
        }
    }
}
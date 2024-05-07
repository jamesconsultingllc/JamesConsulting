//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="MyInterface.cs" company="James Consulting LLC">
//    Copyright © James Consulting LLC. All rights reserved.
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace JamesConsulting.Tests
{
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
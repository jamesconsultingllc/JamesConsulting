﻿//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="MyClass.cs" company="James Consulting LLC">
//    Copyright © James Consulting LLC. All rights reserved.
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Tests
{
    /// <summary>
    ///     The my class.
    /// </summary>
    internal class MyClass
    {
        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        public int? X { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        public string? Y { get; set; }

        /// <summary>
        ///     The to string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            return $"X - {X} : Y - {Y}";
        }
    }
}
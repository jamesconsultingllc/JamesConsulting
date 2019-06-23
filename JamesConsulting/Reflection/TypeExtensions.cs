//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="TypeExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The type extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// The methods.
        /// </summary>
        private static readonly Dictionary<string, MethodInfo> Methods = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// The get method info from string.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <returns>
        /// The <see cref="MethodInfo"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static MethodInfo GetMethodInfoFromString(this Type type, string method)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(method));
            }

            if (TypeExtensions.Methods.ContainsKey(method))
            {
                return TypeExtensions.Methods[method];
            }

            var result = type.GetMethods().FirstOrDefault(x => x.ToString().Equals(method));
            TypeExtensions.Methods[method] = result;
            return result;
        }
    }
}
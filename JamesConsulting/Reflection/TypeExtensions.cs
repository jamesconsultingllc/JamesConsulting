﻿//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="TypeExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Reflection
{
    /// <summary>
    ///     The type extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     The methods.
        /// </summary>
        private static readonly ConcurrentDictionary<string, MethodInfo> Methods = new ConcurrentDictionary<string, MethodInfo>();

        /// <summary>
        ///     The get method info from string.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="method">
        ///     The method.
        /// </param>
        /// <returns>
        ///     The <see cref="MethodInfo" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="type"/> or <paramref name="method"/> is null
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="method"/> is an empty or whitespace string
        /// </exception>
        public static MethodInfo? GetMethodInfoFromString([NotNull] this Type type, [Required] string method)
        {
            if (Methods.ContainsKey(method))
                return Methods[method];

            var result = type.GetMethods().FirstOrDefault(x => x.ToString().Equals(method));

            if (result != null)
                Methods[method] = result;

            return result;
        }

        /// <summary>
        ///     Gets whether or not the type is a concrete class.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Returns true if type is not an abstract class or interface, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type" /> is null</exception>
        public static bool IsConcreteClass([NotNull] this Type type)
        {
            return !type.IsAbstract && !type.IsInterface;
        }

        /// <summary>
        ///     The is void return type.
        /// </summary>
        /// <param name="methodInfo">
        ///     The method info.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool HasReturnValue([NotNull] this MethodInfo methodInfo)
        {
            return methodInfo.ReturnType != Constants.VoidType && methodInfo.ReturnType != Constants.TaskType;
        }

        /// <summary>
        ///     The is async.
        /// </summary>
        /// <param name="methodInfo">
        ///     The method info.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool IsAsync([NotNull] this MethodInfo methodInfo)
        {
            return Constants.TaskType.IsAssignableFrom(methodInfo.ReturnType);
        }

        /// <summary>
        ///     The is result task.
        /// </summary>
        /// <param name="methodInfo">
        ///     The methodInfo.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool IsAsyncWithResult([NotNull] this MethodInfo methodInfo)
        {
            return methodInfo.ReturnType != Constants.TaskType && Constants.TaskType.IsAssignableFrom(methodInfo.ReturnType);
        }
    }
}
//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="MethodInfoExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace JamesConsulting.Threading
{
    /// <summary>
    /// The method info extensions.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// The create task result.
        /// </summary>
        /// <param name="methodInfo">
        /// The method info.
        /// </param>
        /// <param name="results">
        /// The results.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static object CreateTaskResult(this MethodInfo methodInfo, dynamic results)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            if (methodInfo.ReturnType == TypeConstants.VoidType)
            {
                throw new ArgumentException($"{methodInfo} has a return type of void.");
            }

            var resultType = TypeConstants.TaskCompletionSourceType.MakeGenericType(methodInfo.ReturnType.GetGenericArguments());
            object taskSource = Activator.CreateInstance(resultType);
            resultType.InvokeMember("SetResult", BindingFlags.Instance | BindingFlags.Public, null, taskSource, new[] { results });
            return resultType.InvokeMember("Task", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, taskSource, new[] { results });
        }
    }
}
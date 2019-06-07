//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="Constants.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
// 
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    ///     The constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     The generic configured task awaitable.
        /// </summary>
        public static readonly Type GenericConfiguredTaskAwaitable = typeof(ConfiguredTaskAwaitable<>);

        /// <summary>
        ///     The generic task type.
        /// </summary>
        public static readonly Type GenericTaskType = typeof(Task<>);

        /// <summary>
        ///     The output only function type.
        /// </summary>
        public static readonly Type OutputOnlyFunctionType = typeof(Func<>);

        /// <summary>
        ///     The task completion source type.
        /// </summary>
        public static readonly Type TaskCompletionSourceType = typeof(TaskCompletionSource<>);

        /// <summary>
        ///     The task type.
        /// </summary>
        public static readonly Type TaskType = typeof(Task);

        /// <summary>
        /// The type methods.
        /// </summary>
        public static readonly Dictionary<Type, MethodInfo[]> TypeMethods = new Dictionary<Type, MethodInfo[]>();

        /// <summary>
        ///     The void type.
        /// </summary>
        public static readonly Type VoidType = typeof(void);

        /// <summary>
        ///     The method templates.
        /// </summary>
        public static readonly Dictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)> MethodTemplates = new Dictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)>();
    }
}
//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="TypeConstants.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JamesConsulting
{
    /// <summary>
    ///     The type constants.
    /// </summary>
    public static class TypeConstants
    {
        /// <summary>
        ///     The generic configured task that can be awaited.
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
        ///     The void type.
        /// </summary>
        public static readonly Type VoidType = typeof(void);
    }
}
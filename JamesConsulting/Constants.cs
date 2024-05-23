// <copyright file="Constants.cs" company="James Consulting LLC">
// Copyright © James Consulting LLC. All rights reserved.
// </copyright>

namespace JamesConsulting
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    ///     Constants class contains all the constant values and types used in the project.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents the type of generic configured task awaitable.
        /// </summary>
        public static readonly Type GenericConfiguredTaskAwaitable = typeof(ConfiguredTaskAwaitable<>);

        /// <summary>
        /// Represents the type of generic task.
        /// </summary>
        public static readonly Type GenericTaskType = typeof(Task<>);

        /// <summary>
        /// Represents the type of function that only has an output.
        /// </summary>
        public static readonly Type OutputOnlyFunctionType = typeof(Func<>);

        /// <summary>
        /// Represents the type of task completion source.
        /// </summary>
        public static readonly Type TaskCompletionSourceType = typeof(TaskCompletionSource<>);

        /// <summary>
        /// Represents the type of task.
        /// </summary>
        public static readonly Type TaskType = typeof(Task);

        /// <summary>
        /// Represents the type of void.
        /// </summary>
        public static readonly Type VoidType = typeof(void);

        /// <summary>
        /// Gets a dictionary that maps a Type to its MethodInfo array.
        /// </summary>
        public static ConcurrentDictionary<Type, MethodInfo[]> TypeMethods { get; } = new ConcurrentDictionary<Type, MethodInfo[]>();

        /// <summary>
        /// Gets a dictionary that maps a MethodInfo to its parameters and template.
        /// </summary>
        public static ConcurrentDictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)> MethodTemplates { get; } = new ConcurrentDictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)>();
    }
}
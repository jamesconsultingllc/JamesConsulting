using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JamesConsulting
{
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
        ///     The method templates.
        /// </summary>
        public static ConcurrentDictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)> MethodTemplates { get; private set; } = new();

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
        ///     The type methods.
        /// </summary>
        public static ConcurrentDictionary<Type, MethodInfo[]> TypeMethods { get; private set; } = new();

        /// <summary>
        ///     The void type.
        /// </summary>
        public static readonly Type VoidType = typeof(void);
    }
}
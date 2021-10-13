using System;
using System.Reflection;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Threading
{
    /// <summary>
    ///     The method info extensions.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// The set result.
        /// </summary>
        private const string SetResult = "SetResult";

        /// <summary>
        /// The task.
        /// </summary>
        private const string Task = "Task";

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
        public static object? CreateTaskResult([NotNull] this MethodInfo methodInfo, dynamic results)
        {
            if (methodInfo.ReturnType == Constants.VoidType)
                throw new ArgumentException($"{methodInfo} has a return type of void.");

            var resultType =
                Constants.TaskCompletionSourceType.MakeGenericType(methodInfo.ReturnType.GetGenericArguments());
            var taskSource = Activator.CreateInstance(resultType);
            var taskType = taskSource.GetObjectType();
            taskType.InvokeMember(
                SetResult,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod,
                null,
                taskSource,
                new[] {results});
            return taskType.InvokeMember(
                Task,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty,
                null,
                taskSource,
                null);
        }
    }
}
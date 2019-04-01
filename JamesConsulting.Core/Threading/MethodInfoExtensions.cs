using System;

namespace JamesConsulting.Core.Threading
{
    using System.Reflection;

    public static class MethodInfoExtensions
    {
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
            dynamic taskSource = Activator.CreateInstance(resultType);
            taskSource.SetResult(results);
            return taskSource.Task;
        }
    }
}

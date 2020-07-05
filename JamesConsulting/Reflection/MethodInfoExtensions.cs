//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="MethodInfoExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2020 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Reflection
{
    /// <summary>
    ///     MethodInfoExtension methods
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        ///     The method templates.
        /// </summary>
        private static readonly ConcurrentDictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)> MethodTemplates = new ConcurrentDictionary<MethodInfo, (ParameterInfo[] Parameters, string Template)>();

        /// <summary>
        ///     Returns a string representation of the MethodInfo with parameter names, types and values.
        /// </summary>
        /// <param name="methodInfo">
        ///     The <see cref="MethodInfo" /> to convert to string
        /// </param>
        /// <param name="parameterValues">
        ///     The parameter values corresponding to the method parameters
        /// </param>
        /// <returns>
        ///     Returns a <see cref="string" /> representation of the <see cref="MethodInfo" /> with parameter names, types
        ///     and values.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="methodInfo" /> is <see langword="null" />
        /// </exception>
        public static string ToInvocationString([NotNull] this MethodInfo methodInfo, params object[] parameterValues)
        {
            if (!MethodTemplates.ContainsKey(methodInfo))
                MethodTemplates[methodInfo] = methodInfo.GetMethodTemplate();

            var (parameters, template) = MethodTemplates[methodInfo];
            return BindTemplate(template, parameters, parameterValues);
        }

        /// <summary>
        ///     The bind template.
        /// </summary>
        /// <param name="template">
        ///     The template.
        /// </param>
        /// <param name="parameters">
        ///     The parameters.
        /// </param>
        /// <param name="parameterValues">
        ///     The parameter values.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private static string BindTemplate(string template, IEnumerable<ParameterInfo> parameters, IReadOnlyList<object> parameterValues)
        {
            return string.Format(template, parameters.Select((x, idx) => GetValue(x, parameterValues[idx])).ToArray());
        }

        /// <summary>
        ///     The get method template.
        /// </summary>
        /// <param name="methodInfo">
        ///     The method info.
        /// </param>
        /// <returns>
        ///     The <see cref="T:(ParameterInfo[] Parameters, string Template)" />.
        /// </returns>
        private static (ParameterInfo[] Parameters, string Template) GetMethodTemplate(this MethodBase methodInfo)
        {
            var stringBuilder = new StringBuilder($"{methodInfo.DeclaringType.FullName}.{methodInfo.Name}(");
            var parameterInfo = methodInfo.GetParameters();
            stringBuilder.Append(string.Join(", ", parameterInfo.Select(ToInvocationString)));
            stringBuilder.Append(")");
            return (parameterInfo, stringBuilder.ToString());
        }

        /// <summary>
        ///     The get value.
        /// </summary>
        /// <param name="parameterInfo">
        ///     The parameter info.
        /// </param>
        /// <param name="parameterValue">
        ///     The parameter value.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        private static object? GetValue(ParameterInfo parameterInfo, object parameterValue)
        {
            if (parameterValue == null || parameterInfo.ParameterType.IsPrimitive) return parameterValue;

            return parameterValue is string ? $"\"{parameterValue}\"" : parameterValue.ToJson();
        }

        /// <summary>
        ///     The to invocation string.
        /// </summary>
        /// <param name="parameterInfo">
        ///     The parameter info.
        /// </param>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private static string ToInvocationString(ParameterInfo parameterInfo, int index)
        {
            return $"{parameterInfo.ParameterType.FullName} {parameterInfo.Name} : {{{index}}}";
        }
    }
}
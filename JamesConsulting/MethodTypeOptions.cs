// <copyright file="MethodTypeOptions.cs" company="James Consulting LLC">
// Copyright © James Consulting LLC. All rights reserved.
// </copyright>

namespace JamesConsulting
{
    /// <summary>
    /// Enum MethodTypeOptions
    /// This enum represents the different types of methods that can be used in the application.
    /// It includes synchronous actions and functions, as well as asynchronous actions and functions.
    /// </summary>
    public enum MethodTypeOptions
    {
        /// <summary>
        /// Represents a synchronous action.
        /// This is a method that performs an operation and does not return a value.
        /// </summary>
        SyncAction,

        /// <summary>
        /// Represents a synchronous function.
        /// This is a method that performs an operation and returns a value.
        /// </summary>
        SyncFunction,

        /// <summary>
        /// Represents an asynchronous action.
        /// This is a method that performs an operation asynchronously and does not return a value.
        /// </summary>
        AsyncAction,

        /// <summary>
        /// Represents an asynchronous function.
        /// This is a method that performs an operation asynchronously and returns a value.
        /// </summary>
        AsyncFunction,
    }
}
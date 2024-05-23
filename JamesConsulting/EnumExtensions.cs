// <copyright file="EnumExtensions.cs" company="James Consulting LLC">
// Copyright © James Consulting LLC. All rights reserved.
// </copyright>

namespace JamesConsulting
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Provides extension methods for the Enum type.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the description of an enumeration value.
        /// </summary>
        /// <param name="enumValue">
        /// The enumeration value for which to retrieve the description.
        /// </param>
        /// <returns>
        /// The description of the enumeration value. If a description is not found, the name of the enumeration value is returned.
        /// if the enumeration value does not exist, null is returned.
        /// </returns>
        /// <exception cref="System.Reflection.AmbiguousMatchException">
        /// Thrown when more than one of the requested attributes is found.
        /// </exception>
        /// <exception cref="System.TypeLoadException">
        /// Thrown when a custom attribute type cannot be loaded.
        /// </exception>
        public static string? GetDescription(this Enum enumValue)
        {
            // Get the field information for the enumeration value.
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            // If the field information is not found, return null.
            if (fieldInfo == null)
            {
                return null;
            }

            // Get the DescriptionAttribute for the field, if it exists.
            // If the DescriptionAttribute does not exist, return the name of the enumeration value.
            // If the DescriptionAttribute exists, return its description.
            return Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is not DescriptionAttribute attribute
                       ? Enum.GetName(enumValue.GetType(), enumValue)
                       : attribute.Description;
        }
    }
}
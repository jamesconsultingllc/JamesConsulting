//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="EnumExtensions.cs" company="James Consulting LLC">
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
    using System.ComponentModel;

    /// <summary>
    ///     The <see cref="Enum" /> extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// The get description.
        /// </summary>
        /// <param name="enumValue">
        /// The enumeration value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="T:System.Reflection.AmbiguousMatchException">
        /// More than one of the requested attributes was found.
        /// </exception>
        /// <exception cref="T:System.TypeLoadException">
        /// A custom attribute type cannot be loaded.
        /// </exception>
        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? Enum.GetName(enumValue.GetType(), enumValue) : attribute.Description;
        }
    }
}
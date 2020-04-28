//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="EnumExtensionsTests.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using FluentAssertions;
using Xunit;

namespace JamesConsulting.Tests
{
    /// <summary>
    ///     The enum extensions tests.
    /// </summary>
    public class EnumExtensionsTests
    {
        /// <summary>
        ///     The my enum.
        /// </summary>
        private enum MyOptions
        {
            /// <summary>
            ///     The with.
            /// </summary>
            [Description("Testing")] With,

            /// <summary>
            ///     The without.
            /// </summary>
            Without
        }

        /// <summary>
        ///     The get description_ enum does not have description attribute.
        /// </summary>
        [Fact]
        public void GetDescription_EnumDoesNotHaveDescriptionAttribute()
        {
            var description = MyOptions.With.GetDescription();
            description.Should().BeEquivalentTo("Testing");
        }

        /// <summary>
        ///     The get description_ enum has description attribute.
        /// </summary>
        [Fact]
        public void GetDescription_EnumHasDescriptionAttribute()
        {
            var description = MyOptions.Without.GetDescription();
            description.Should().BeEquivalentTo("Without");
        }
    }
}
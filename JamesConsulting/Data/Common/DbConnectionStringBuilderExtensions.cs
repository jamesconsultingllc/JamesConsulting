using System;
using System.Data.Common;
using PostSharp.Patterns.Contracts;

namespace JamesConsulting.Data.Common
{
    /// <summary>
    ///     <see cref="DbConnectionStringBuilder" /> extension methods.
    /// </summary>
    public static class DbConnectionStringBuilderExtensions
    {
        /// <summary>
        /// The remove keys.
        /// </summary>
        /// <param name="connectionStringBuilder">
        /// The <see cref="DbConnectionStringBuilder"/> to remove the keys.
        /// </param>
        /// <param name="keys">
        /// The keys to remove
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="connectionStringBuilder"/> or <paramref name="keys"/> is null or an empty collection.
        /// </exception>
        public static void RemoveKeys(
            [NotNull] this DbConnectionStringBuilder connectionStringBuilder,
            [NotEmpty] params string[] keys)
        {
            Array.ForEach(
                keys,
                key =>
                    {
                        if (connectionStringBuilder.ContainsKey(key)) connectionStringBuilder.Remove(key);
                    });
        }
    }
}
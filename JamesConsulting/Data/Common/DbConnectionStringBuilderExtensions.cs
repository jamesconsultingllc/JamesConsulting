using System;
using System.Data.Common;
using Metalama.Patterns.Contracts;

namespace JamesConsulting.Data.Common;

/// <summary>
/// Provides extension methods for <see cref="DbConnectionStringBuilder"/>.
/// </summary>
public static class DbConnectionStringBuilderExtensions
{
    /// <summary>
    /// Removes the specified keys from the <see cref="DbConnectionStringBuilder"/>.
    /// </summary>
    /// <param name="connectionStringBuilder">
    /// The <see cref="DbConnectionStringBuilder"/> to remove the keys from.
    /// </param>
    /// <param name="keys">
    /// The keys to remove.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="connectionStringBuilder"/> or <paramref name="keys"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///Thrown when <paramref name="keys"/> is empty collection.
    /// </exception>
    public static void RemoveKeys(
        [NotNull] this DbConnectionStringBuilder connectionStringBuilder,
        [NotNull][NotEmpty] params string[] keys)
    {
            Array.ForEach(
                keys,
                key =>
                    {
                        if (connectionStringBuilder.ContainsKey(key)) connectionStringBuilder.Remove(key);
                    });
        }
}
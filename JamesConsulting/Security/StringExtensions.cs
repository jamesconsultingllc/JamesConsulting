using System.Security;

namespace JamesConsulting.Security;

/// <summary>
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="str">
    /// </param>
    /// <returns>
    /// The <see cref="SecureString"/>.
    /// </returns>
    public static unsafe SecureString ToSecureString(this string str)
    {
            if (string.IsNullOrEmpty(str)) return new SecureString();

            fixed (char* ptr = str)
            {
                return new SecureString(ptr, str.Length);
            }
        }
}
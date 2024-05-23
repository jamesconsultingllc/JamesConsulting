using System;
using System.Runtime.InteropServices;
using System.Security;

namespace JamesConsulting.Security;

/// <summary>
///     Extension methods for <see cref="SecureString" />
/// </summary>
public static class SecureStringExtensions
{
    /// <summary>
    /// Converts the <paramref name="secureString"/> to a <see cref="string"/>
    /// </summary>
    /// <param name="secureString">
    /// The <see cref="SecureString"/> to be converted
    /// </param>
    /// <returns>
    /// A decrypted <see cref="string"/>
    /// </returns>
    public static string ConvertToString(this SecureString secureString)
    {
            if (secureString.Length == 0) return string.Empty;

            var ptr = IntPtr.Zero;
            var result = string.Empty;

            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                result = Marshal.PtrToStringUni(ptr)!;
            }
            finally
            {
                if (ptr != IntPtr.Zero) Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }

            return result;
        }
}
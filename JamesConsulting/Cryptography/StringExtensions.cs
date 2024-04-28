using System;
using System.Security.Cryptography;
using System.Text;
using Metalama.Patterns.Contracts;

namespace JamesConsulting.Cryptography
{
    /// <summary>
    ///     The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The base 64 decode.
        /// </summary>
        /// <param name="encoded">
        /// The encoded.
        /// </param>
        /// <param name="encoding">
        /// The encoding used to decode the string. Uses <see cref="Encoding.Default"/> when null.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="encoded"/> is <see langword="null"/>
        /// </exception>
        public static string Base64Decode([NotNull] this string encoded, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(encoded)) return encoded;
            var bytes = Convert.FromBase64String(encoded);
            return (encoding ?? Encoding.Default).GetString(bytes);
        }

        /// <summary>
        /// The base 64 encode.
        /// </summary>
        /// <param name="decoded">
        /// The decoded.
        /// </param>
        /// <param name="encoding">
        /// The encoding used to encode the string. Uses <see cref="Encoding.Default"/> when null.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="decoded"/> is <see langword="null"/>
        /// </exception>
        public static string Base64Encode([NotNull] this string decoded, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(decoded)) return decoded;
            var bytes = (encoding ?? Encoding.Default).GetBytes(decoded);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///     The generate salt.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:byte[]" />.
        /// </returns>
        /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
        public static byte[] GenerateSalt()
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var randomNumber = new byte[32];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }

        /// <summary>
        /// Hashes the given string
        /// </summary>
        /// <param name="target">
        /// The text to be hashed
        /// </param>
        /// <param name="numberOfRounds">
        /// The number of rounds to hash
        /// </param>
        /// <returns>
        /// Returns the hashed version of the given string
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// salt is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The number of rounds cannot be less than 0.
        /// </exception>
        /// <exception cref="CryptographicException">
        /// The cryptographic service provider (CSP) cannot be acquired.
        /// </exception>
        public static (string hashedString, byte[] salt) Hash(
            [NotNull][NotEmpty] this string target,
            [StrictlyPositive] int numberOfRounds = 100)
        {
            var salt = GenerateSalt();
            var hashedString = target.Hash(salt, numberOfRounds);
            return (hashedString, salt);
        }

        /// <summary>
        /// The hash.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="salt">
        /// The salt.
        /// </param>
        /// <param name="numberOfRounds">
        /// The number of rounds.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="target"/> is <see langword="null"/> or empty
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="salt"/> is <see langword="null"/>
        /// </exception>
        public static string Hash(
            [NotNull][NotEmpty] this string target,
            [NotNull][NotEmpty] byte[] salt,
            [StrictlyPositive] int numberOfRounds = 100)
        {
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(target), salt, numberOfRounds, HashAlgorithmName.SHA256);
            var hash = rfc2898DeriveBytes.GetBytes(32);
            return Convert.ToBase64String(hash);
        }
    }
}
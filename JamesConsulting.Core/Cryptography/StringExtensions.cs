//  ----------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="James Consulting LLC">
//    Copyright (c) 2019 All Rights Reserved
//  </copyright>
//  <author>Rudy James</author>
//  <summary>
//  
//  </summary>
//  ----------------------------------------------------------------------------------------------------------------------

namespace JamesConsulting.Core.Cryptography
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

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
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="encoded"/> is <see langword="null"/>
        /// </exception>
        public static string Base64Decode(this string encoded)
        {
            if (!ValidForEncoding(encoded))
            {
                return encoded;
            }

            var bytes = Convert.FromBase64String(encoded);
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// The base 64 encode.
        /// </summary>
        /// <param name="decoded">
        /// The decoded.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="decoded"/> is <see langword="null"/>
        /// </exception>
        public static string Base64Encode(this string decoded)
        {
            if (!ValidForEncoding(decoded))
            {
                return decoded;
            }

            var bytes = Encoding.Default.GetBytes(decoded);
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
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
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
        /// <exception cref="ArgumentException">
        /// <paramref name="target"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// salt is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The number of rounds cannot be less than 0.
        /// </exception>
        /// <exception cref="CryptographicException">
        /// The cryptographic service provider (CSP) cannot be acquired.
        /// </exception>
        public static (string hashedString, byte[] salt) Hash(this string target, int numberOfRounds = 100)
        {
            ValidateHashArguments(target, numberOfRounds);

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
        public static string Hash(this string target, byte[] salt, int numberOfRounds = 100)
        {
            ValidateHashArguments(target, numberOfRounds);

            if (salt == null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(target), salt, numberOfRounds))
            {
                var hash = rfc2898DeriveBytes.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// The validate hash arguments.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="numberOfRounds">
        /// The number of rounds.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="numberOfRounds">number of rounds</paramref> is less
        ///     than 0
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="target"/> is <see langword="null"/> or empty
        /// </exception>
        private static void ValidateHashArguments(string target, int numberOfRounds)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentException(nameof(target));
            }

            if (numberOfRounds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfRounds));
            }
        }

        /// <summary>
        /// The validate for encoding.
        /// </summary>
        /// <param name="decoded">
        /// The decoded.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="decoded"/> is <see langword="null"/>
        /// </exception>
        private static bool ValidForEncoding(string decoded)
        {
            if (decoded == null)
            {
                throw new ArgumentNullException(nameof(decoded));
            }

            if (decoded.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
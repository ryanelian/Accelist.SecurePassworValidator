using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Linq;
using System.Collections.Immutable;

namespace Accelist.SecurePasswordValidator
{
    /// <summary>
    /// Contains methods for validating a secure password.
    /// </summary>
    public static class ValidatePassword
    {
        private static ImmutableHashSet<string> _ForbiddenPasswords;

        private static readonly object _ForbiddenPasswordsLock = new object();

        /// <summary>
        /// Returns a lazy-loaded hashed collection of most common passwords.
        /// </summary>
        public static ImmutableHashSet<string> ForbiddenPasswords
        {
            get
            {
                if (_ForbiddenPasswords == null)
                {
                    lock (_ForbiddenPasswordsLock)
                    {
                        if (_ForbiddenPasswords == null)
                        {
                            var passwords = UnzipPasswords(ZippedPasswords);
                            _ForbiddenPasswords = ImmutableHashSet.Create(passwords.ToArray());
                        }
                    }
                }

                return _ForbiddenPasswords;
            }
        }

        /// <summary>
        /// Gets a stream of zipped passwords.
        /// </summary>
        private static Stream ZippedPasswords
        {
            get
            {
                return typeof(ValidatePassword).GetTypeInfo().Assembly.GetManifestResourceStream("Accelist.SecurePasswordValidator.rockyou-3min8.zip");
            }
        }

        /// <summary>
        /// Checks whether password input string is of sufficient length and not blacklisted.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            if (ForbiddenPasswords.Contains(password))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Returns a list of string from a zipped text file that contains passwords separated by newlines.
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static IEnumerable<string> UnzipPasswords(Stream zip)
        {
            using (var archive = new ZipArchive(zip))
            {
                var stream = archive.Entries.First().Open();

                using (var reader = new StreamReader(stream))
                {
                    string s;
                    while ((s = reader.ReadLine()) != null)
                    {
                        yield return s;
                    }
                }
            }
        }
    }
}

using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Domain
{
    public class PasswordManager
    {
        internal static byte[] GetPasswordHashed(string password, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password) || salt is null)
            {
                return new byte[0];
            }

            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
            return hashed;
        }

        internal static byte[] CreateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

    }
}

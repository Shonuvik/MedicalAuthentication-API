using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace MedicalAuthenticationAPI.Helpers
{
    public static class HashPassword
    {
        public static string GenerateHash(string password, out string saltBase64)
        {
            // 1. Gerar salt
            var salt = new byte[16];
            RandomNumberGenerator.Fill(salt);
            saltBase64 = Convert.ToBase64String(salt);

            // 2. Criar o hash com Argon2
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                MemorySize = 65536,
                Iterations = 4
            };

            var hashBytes = argon2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string storedSaltBase64, string storedHashBase64)
        {
            var salt = Convert.FromBase64String(storedSaltBase64);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                MemorySize = 65536,
                Iterations = 4
            };

            var hashBytes = argon2.GetBytes(32);
            var computedHashBase64 = Convert.ToBase64String(hashBytes);

            // Comparação segura
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(computedHashBase64),
                Encoding.UTF8.GetBytes(storedHashBase64));
        }

    }
}


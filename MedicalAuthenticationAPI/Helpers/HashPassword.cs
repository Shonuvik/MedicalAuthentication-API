using System.Text;
using Isopoh.Cryptography.Argon2;

namespace MedicalAuthenticationAPI.Helpers
{
    public static class HashPassword
    {
        public static string GenerateHash(string password, string salt)
        {
            string hashedPassword = Argon2.Hash($"{password}{salt}");
            return hashedPassword;
        }

        public static string Salt()
        {
            var guid = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N"));
            var base64 = Convert.ToBase64String(guid);

            return base64;
        }
    }
}


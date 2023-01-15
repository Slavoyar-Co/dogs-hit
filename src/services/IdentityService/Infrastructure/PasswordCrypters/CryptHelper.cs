using BCrypt.Net;
using Domain.Entities;

namespace Infrastructure.PasswordCrypters
{
    public static class CryptHelper
    {
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

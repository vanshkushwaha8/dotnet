using Microsoft.AspNet.Identity;

namespace CrudApplication.Helpers
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher _hasher = new PasswordHasher();

        public static string HashPassword(string password)
        {
            return _hasher.HashPassword(password);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
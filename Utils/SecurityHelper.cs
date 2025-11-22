using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace UserManagement.Utils
{
    public static class SecurityHelper
    {
        // Method to hash password using SHA256
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty");

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Method to verify password against hash
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            return HashPassword(password) == hashedPassword;
        }

        // Method to generate a random salt (for enhanced security)
        public static string GenerateSalt()
        {
            var randomBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        // Enhanced password hashing with salt
        public static string HashPasswordWithSalt(string password, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
                throw new ArgumentException("Password and salt cannot be null or empty");

            var saltedPassword = password + salt;
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Verify password with salt
        public static bool VerifyPasswordWithSalt(string password, string salt, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(hashedPassword))
                return false;

            return HashPasswordWithSalt(password, salt) == hashedPassword;
        }

        // Method to validate password strength
        public static (bool isValid, string message) ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return (false, "Password cannot be empty");

            if (password.Length < 6)
                return (false, "Password must be at least 6 characters long");

            if (!password.Any(char.IsDigit))
                return (false, "Password must contain at least one digit");

            if (!password.Any(char.IsLetter))
                return (false, "Password must contain at least one letter");

            return (true, "Password is strong");
        }

        // Method to generate a random temporary password
        public static string GenerateTemporaryPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            var randomBytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[randomBytes[i] % validChars.Length];
            }

            return new string(chars);
        }

        // Method to sanitize input (basic XSS protection)
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Replace("<", "&lt;")
                       .Replace(">", "&gt;")
                       .Replace("\"", "&quot;")
                       .Replace("'", "&#x27;")
                       .Replace("/", "&#x2F;");
        }

        // Method to check if email is valid
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Method to check if phone number is valid (basic validation)
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            // Remove common separators and check if it's all digits
            var cleanNumber = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "");
            return cleanNumber.All(char.IsDigit) && cleanNumber.Length >= 10;
        }
    }
}
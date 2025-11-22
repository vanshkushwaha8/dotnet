using System;
using System.Linq;
using CrudApplication.Helpers;
using CrudApplication.Models;
using CrudApplication.Models.Entities;

namespace CrudApplication.Services
{
    public class AuthService : IAuthService, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public AuthService()
        {
            _context = new ApplicationDbContext();
        }

        public Admin ValidateAdmin(string email, string password)
        {
            var admin = _context.Admins
                .FirstOrDefault(a => a.Email == email && a.IsActive);

            if (admin == null)
                return null;

            bool isValid = PasswordHelper.VerifyPassword(admin.PasswordHash, password);

            if (isValid)
            {
                admin.LastLoginDate = DateTime.Now;
                _context.SaveChanges();
                return admin;
            }

            return null;
        }

        public User ValidateUser(string emailOrPhone, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u =>
                    (u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone)
                    && u.IsActive);

            if (user == null)
                return null;

            bool isValid = PasswordHelper.VerifyPassword(user.PasswordHash, password);

            return isValid ? user : null;
        }

        public User RegisterUser(User user, string password)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return null;

            user.PasswordHash = PasswordHelper.HashPassword(password);
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CrudApplication.Helpers;
using CrudApplication.Models;
using CrudApplication.Models.Entities;

namespace CrudApplication.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UserService()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users
                .Where(u => u.IsActive)
                .OrderByDescending(u => u.CreatedDate)
                .ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserId == id && u.IsActive);
        }

        public User CreateUser(User user, string password)
        {
            try
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
            catch
            {
                return null;
            }
        }

        public bool UpdateUser(User user, string newPassword)
        {
            try
            {
                var existingUser = _context.Users.Find(user.UserId);
                if (existingUser == null)
                    return false;

                if (existingUser.Email != user.Email)
                {
                    if (_context.Users.Any(u => u.Email == user.Email && u.UserId != user.UserId))
                        return false;
                }

                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Age = user.Age;
                existingUser.Gender = user.Gender;
                existingUser.Skills = user.Skills;
                existingUser.Education = user.Education;
                existingUser.Address = user.Address;
                existingUser.UpdatedDate = DateTime.Now;

                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    existingUser.PasswordHash = PasswordHelper.HashPassword(newPassword);
                }

                _context.Entry(existingUser).State = EntityState.Modified;
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    return false;

                user.IsActive = false;
                user.UpdatedDate = DateTime.Now;

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id && u.IsActive);
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

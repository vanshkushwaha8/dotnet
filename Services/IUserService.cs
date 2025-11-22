using System.Collections.Generic;
using CrudApplication.Models.Entities;

namespace CrudApplication.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User user, string password);
        bool UpdateUser(User user, string newPassword);
        bool DeleteUser(int id);
        bool UserExists(int id);
    }
}
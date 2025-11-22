using CrudApplication.Models.Entities;

namespace CrudApplication.Services
{
    public interface IAuthService
    {
        Admin ValidateAdmin(string email, string password);
        User ValidateUser(string emailOrPhone, string password);
        User RegisterUser(User user, string password);
    }
}

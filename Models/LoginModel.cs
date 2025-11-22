using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email or Phone Number is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
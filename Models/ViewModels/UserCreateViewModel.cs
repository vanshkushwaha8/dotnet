using System.ComponentModel.DataAnnotations;

namespace CrudApplication.Models.ViewModels
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(256)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Range(1, 120)]
        [Display(Name = "Age")]
        public int? Age { get; set; }

        [StringLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(500)]
        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [StringLength(200)]
        [Display(Name = "Education")]
        public string Education { get; set; }

        [StringLength(500)]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CC_Regist_System.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [MinLength(1, ErrorMessage = "Last Name Must Be At Least 1 Character")]
        [MaxLength(20, ErrorMessage = "Last Name Max 20 characters long")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fullname is required!")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Username must be max 50 characters long")]
        public string Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required!")]
        [RegularExpression(
            @"^\d{5,20}$",
            ErrorMessage = "Phone Number must be between 5 and 20 digits!"
        )]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(
            @"^(?=.[A-Z])(?=.[a-z])(?=.\d)(?=.[!@#$%^&*])$",
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number, and one special character (!@#$%^&*)"
        )]
        public string Password { get; set; } = string.Empty;
    }
}

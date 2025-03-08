using System.ComponentModel.DataAnnotations;
using CC_Regist_System.Enums;
using CC_Regist_System.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CC_Regist_System.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters!")]
        [MaxLength(100, ErrorMessage = "Username must not exceed 100 characters!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Username can only contain letters, number and spaces!"
        )]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required!")]
        [MinLength(3, ErrorMessage = "Full Name must be at least 3 characters!")]
        [MaxLength(100, ErrorMessage = "Full Name must not exceed 100 characters!")]
        [RegularExpression(
            @"^[a-zA-Z\s]+$",
            ErrorMessage = "Full Name can only contain letters and spaces!"
        )]
        public string Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        [MaxLength(255, ErrorMessage = "Email must not exceed 255 characters!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required!")]
        [RegularExpression(
            @"^\d{5,20}$",
            ErrorMessage = "Phone Number must be between 5 and 20 digits!"
        )]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase, one lowercase, one number, and one special character (!@#$%^&*)"
        )]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must agree to the terms and conditions")]
        public bool AgreeToTerms { get; set; }
    }
}

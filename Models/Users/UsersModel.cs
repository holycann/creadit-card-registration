using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CC_Regist_System.Models.Cards;

namespace CC_Regist_System.Models.Users
{
    public class UsersModel
    {
        [Key]
        public int UserID { get; set; }

        public CardsModel? Card { get; set; }
        public FilesModel? File { get; set; }

        public UserDetailsModel? UserDetail { get; set; }

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
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*])",
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number, and one special character (!@#$%^&*)"
        )]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
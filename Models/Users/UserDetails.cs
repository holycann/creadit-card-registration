using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CC_Regist_System.Models.Address;

namespace CC_Regist_System.Models.Users
{
    public class UserDetailsModel
    {
        [Key]
        public int UserDetailID { get; set; }

        [Required(ErrorMessage = "ID Number is required!")]
        [RegularExpression(
            @"^\d{5,20}$",
            ErrorMessage = "ID Number must be between 5 and 20 digits!"
        )]
        public string IDNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender Status is required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Gender can only contain letters, number and spaces!"
        )]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Marital Status is required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Marital Status can only contain letters, number and spaces!"
        )]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Date of Birth is required!")]
        [CustomValidation(typeof(UsersModel), nameof(ValidateAge))]
        public DateOnly DateOfBirthDay { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }
        public UsersModel? Users { get; set; }

        public AddressModel? Address { get; set; }

        [Required(ErrorMessage = "Company is required!")]
        [MaxLength(255, ErrorMessage = "Company must not exceed 100 characters!")]
        [MinLength(3, ErrorMessage = "Company must be at least 3 characters!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Company must contain only letters, numbers and spaces!"
        )]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Position is required!")]
        [MaxLength(100, ErrorMessage = "Position must not exceed 100 characters!")]
        [MinLength(3, ErrorMessage = "Position must be at least 3 characters!")]
        [RegularExpression(
            @"^[a-zA-Z\s]+$",
            ErrorMessage = "Position must contain only letters and spaces!"
        )]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Monthly Income is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "Monthly Income must be a positive number!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Monthly Income must contain only numbers!")]
        public double MonthlyIncome { get; set; }

        [Required(ErrorMessage = "Bank Account Number is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "Bank Account Number must be a positive number!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Bank Account Number must contain only numbers!")]
        public string BankAccountNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age))
                age--;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Must be at least 18 years old!");
        }
    }
}

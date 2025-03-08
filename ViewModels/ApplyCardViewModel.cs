using System.ComponentModel.DataAnnotations;
using CC_Regist_System.Enums;
using CC_Regist_System.Models.Address;
using CC_Regist_System.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CC_Regist_System.ViewModels
{
    public class ApplyCardViewModel
    {
        public UsersModel? User { get; set; }

        // Personal Details
        [Required(ErrorMessage = "ID Card Number is required!")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "ID Card Number must be 16 digits!")]
        public string IDCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required!")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirthDay { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        public string? Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Marital Status is required!")]
        public string? MaritalStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Province is required!")]
        public string Province { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required!")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Name is required!")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Position is required!")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Income/Month is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "Income must be a positive number!")]
        public double Income { get; set; }

        [Required(ErrorMessage = "Bank Account Number is required!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Bank Account Number must be numeric!")]
        public string BankAccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Scan ID is required!")]
        public IFormFile ScanID { get; set; }

        [Required(ErrorMessage = "Card Name is required!")]
        public string? CardName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card Type is required!")]
        public string? CardType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card Tier is required!")]
        public string CardTier { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.Date.AddYears(5);

        [Required(ErrorMessage = "Credit Limit is required!")]
        [Range(20000, 1000000, ErrorMessage = "Credit Limit must range RM 20.000 - RM 1.000.000!")]
        public double CreditLimit { get; set; }

        [Required(ErrorMessage = "Annual Fee is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "Annual Fee must be a positive number!")]
        public double AnnualFee { get; set; }

        [Required(ErrorMessage = "Interest Rate is required!")]
        [Range(10, 15, ErrorMessage = "Interest Rate must range 10% - 20%!")]
        public int InterestRate { get; set; }

        public List<SelectListItem> CardNameOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> GenderOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MaritalStatusOptions { get; set; } = new List<SelectListItem>();
    }
}

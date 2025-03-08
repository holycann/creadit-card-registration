using System.ComponentModel.DataAnnotations;

namespace CC_Regist_System.Models.Address
{
    public class CountryModel
    {
        [Key]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "State Name Is Required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "State Name can only contain letters, number and spaces!"
        )]
        public string CountryName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel.DataAnnotations;

namespace CC_Regist_System.Models.Address
{
    public class CityModel
    {
        [Key]
        public int CityID { get; set; }

        [Required(ErrorMessage = "City Field Is Required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "City can only contain letters, number and spaces!"
        )]
        public string CityName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CC_Regist_System.Models.Users;

namespace CC_Regist_System.Models.Address
{
    public class AddressModel
    {
        [Key]
        public int AddressID { get; set; }

        [ForeignKey("UserDetails")]
        public int UserDetailID { get; set; }
        public UserDetailsModel? UserDetails { get; set; }

        [ForeignKey("City")]
        [Required(ErrorMessage = "City ID Field Is Required!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "City ID can only contain number!")]
        public int CityID { get; set; }
        public CityModel? City { get; set; }

        [ForeignKey("Province")]
        [Required(ErrorMessage = "Province ID Field Is Required!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Province ID can only contain number!")]
        public int ProvinceID { get; set; }
        public ProvinceModel? Province { get; set; }

        [ForeignKey("Country")]
        [Required(ErrorMessage = "Country ID Field Is Required!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Country ID can only contain number!")]
        public int CountryID { get; set; }
        public CountryModel? Country { get; set; }

        [Required(ErrorMessage = "Detail? Address Field Is Required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Detail? Address can only contain letters, numbers, and spaces!"
        )]
        public string DetailAddress { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

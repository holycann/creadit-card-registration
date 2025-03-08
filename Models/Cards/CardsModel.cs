using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CC_Regist_System.Database;
using CC_Regist_System.Models.Users;

namespace CC_Regist_System.Models.Cards
{
    public class CardsModel
    {
        [Key]
        public int CardID { get; set; }

        [Required(ErrorMessage = "Card Name is required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Card Name can only contain letters, numbers, and spaces!"
        )]
        public string CardName { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }
        public UsersModel User { get; set; }

        [ForeignKey("CardDetails")]
        public int? CardDetailID { get; set; }
        public CardDetailsModel? CardDetail { get; set; }

        [Required(ErrorMessage = "Card Number is required!")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card Type is required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Card Type can only contain letters, number and spaces!"
        )]
        public string CardType { get; set; }

        [Required(ErrorMessage = "Card Tier is required!")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Card Tier can only contain letters, number and spaces!"
        )]
        public string CardTier { get; set; }

        [Required(ErrorMessage = "Expiration Date is required!")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddYears(3);

        [Required(ErrorMessage = "CVV Number is required!")]
        [Range(100, 999, ErrorMessage = "CVV must be a 3-digit number!")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be exactly 3 digits!")]
        public int CVV { get; set; }

        [Required(ErrorMessage = "Pin Number is required!")]
        public string PinNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
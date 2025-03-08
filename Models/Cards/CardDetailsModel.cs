using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC_Regist_System.Models.Cards
{
    public class CardDetailsModel
    {
        [Key]
        public int CardDetailID { get; set; }

        [ForeignKey("Cards")]
        public int CardID { get; set; }
        public CardsModel Cards { get; set; } = null!;

        [Required(ErrorMessage = "Annual Fee is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "Annual Fee must be a positive number!")]
        public double AnnualFee { get; set; }

        [Required(ErrorMessage = "Credit Limit is required!")]
        [Range(20000, 1000000, ErrorMessage = "Credit Limit must range RM 10.000 - RM 1.000.000!")]
        public double CreditLimit { get; set; }

        [Required(ErrorMessage = "Interest Rate is required!")]
        [Range(10, 15, ErrorMessage = "Interest Rate must range 10% - 15%!")]
        public int InterestRate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

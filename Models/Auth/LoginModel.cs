using System.ComponentModel.DataAnnotations;

namespace CC_Regist_System.Models.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Is Required !")]
        [MinLength(3, ErrorMessage = "Username Must Be At Least 3 Characters")]
        [MaxLength(30, ErrorMessage = "Username Max 30 Characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required !")]
        public string Password { get; set; } = string.Empty;
    }
}

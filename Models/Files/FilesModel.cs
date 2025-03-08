using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CC_Regist_System.Models.Users;

namespace CC_Regist_System.Models
{
    public class FilesModel
    {
        [Key]
        public int FileID { get; set; }

        [ForeignKey("Users")]
        [Required(ErrorMessage = "User ID is required!")]
        public int UserID { get; set; }
        public UsersModel? User { get; set; }

        [Required(ErrorMessage = "File Name is required!")]
        [MaxLength(255, ErrorMessage = "File Name must not exceed 255 characters!")]
        [RegularExpression(
            @"^[a-zA-Z0-9_\- ]+\.[a-zA-Z0-9]+$",
            ErrorMessage = "Invalid File Name format!"
        )]
        public string FileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "File Path is required!")]
        [MaxLength(500, ErrorMessage = "File Path must not exceed 500 characters!")]
        public string FilePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "File Type is required!")]
        public string FileType { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Models.UserModels
{
    public class EditUserViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(0,130)]
        public int Age { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}

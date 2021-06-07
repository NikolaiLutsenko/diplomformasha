using System.ComponentModel.DataAnnotations;

namespace DiplomaWork.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

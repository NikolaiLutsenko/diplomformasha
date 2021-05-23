using System.ComponentModel.DataAnnotations;

namespace DiplomaWork1.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Поле Email обязательно")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Имя обязательно")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(RegisterUserModel.Password), ErrorMessage = "Пароль не совпадает")]
        public string ConfirmPassword { get; set; }
    }
}

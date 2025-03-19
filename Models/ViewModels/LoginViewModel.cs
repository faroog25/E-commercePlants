using System.ComponentModel.DataAnnotations;

namespace E_commercePlants.Models.ViewModels;

public class LoginViewModel
{
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required and must be at least 2 characters long"),MinLength(2)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required and must be at least 5 characters long"),MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace OurProject.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}

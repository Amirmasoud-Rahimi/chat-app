using System.ComponentModel.DataAnnotations;

namespace ChatApp.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
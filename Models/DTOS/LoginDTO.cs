using System.ComponentModel.DataAnnotations;

namespace Models.DTOS
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Password { get; set; } = null!;
    }
}
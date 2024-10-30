using Models.Entities;
using Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOS
{
    public class UserDTO
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(155)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(155)]
        public string LastName { get; set; } = null!;

        public string? Image { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(250)]
        public string Email { get; set; } = null!;

        public bool Status { get; set; }
        public UserType UserType { get; set; }
        public List<Building>? Buildings { get; set; }
        public int BuildingsCountr => Buildings == null ? 0 : Buildings.Count;
    }
}
using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.DTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "You must specify password minimum length 12 characters !")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.DTO
{
    public class UserForRgisterDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 12,ErrorMessage = "You must specify password minimum length 12 characters !")]
        public string Password { get; set; }
    }
}
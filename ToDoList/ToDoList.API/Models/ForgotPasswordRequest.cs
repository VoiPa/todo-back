using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
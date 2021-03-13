using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Models
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
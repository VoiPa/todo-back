using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
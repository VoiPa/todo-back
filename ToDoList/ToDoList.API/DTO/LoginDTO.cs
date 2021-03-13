using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.DTO
{
    public class LoginDTO
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
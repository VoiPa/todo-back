using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.DTO
{
    public class ToDoItemDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        // [RegularExpression(@"^(?:[a-zA-Z0-9_\-]*,?){0,3}$", ErrorMessage = "Maximum 3 comma separated tags!")]
        public string TaskDescription { get; set; }
    }
}
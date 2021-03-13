using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.DTO
{
    public class ToDoDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string TaskDescription { get; set; }
        
        public bool? Done { get; set; }

        public DateTime ValidFromDateTime { get; set; }

        [RegularExpression(@"^(?:[a-zA-Z0-9_\-]*,?){0,3}$", ErrorMessage = "Maximum 3 comma separated tags!")]
        public string Tags { get; set; }
    }
}
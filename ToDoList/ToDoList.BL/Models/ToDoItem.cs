using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.BL.Models
{
    [Table("ToDoItems")]
    public class ToDoItem : BaseEntity
    {
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        
        [Required(ErrorMessage = "Description is mandatory!!!")]
        [StringLength(100, ErrorMessage = "The description cannot be longer than 100 characters")]
        public string TaskDescription { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public bool IsDone { get; set; }
    }
}
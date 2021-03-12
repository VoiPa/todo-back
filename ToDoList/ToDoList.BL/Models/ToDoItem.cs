using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ToDoList.BL.Models
{
    [Table("ToDoItems")]
    public class ToDoItem
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        
        // prop for cascade delete relation
        public AppUser AppUser { get; set; }

        [Required(ErrorMessage = "Description is mandatory!!!")]
        [StringLength(500, ErrorMessage = "The description cannot be longer than 500 characters")]
        public string TaskDescription { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public bool IsDone { get; set; }
    }
}
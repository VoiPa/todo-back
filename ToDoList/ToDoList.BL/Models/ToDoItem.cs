using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.BL.Models
{
    [Table("ToDoItems")]
    public class ToDoItem
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        
        public DateTimeOffset? CreatedAt { get; set; }
        
        public bool IsDone { get; set; }
    }
}
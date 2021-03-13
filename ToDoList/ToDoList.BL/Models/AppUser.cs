using System;
using System.Collections.Generic;

namespace ToDoList.BL.Models
{
    public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Role = "role2";
        }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<ToDoItem> ToDoItems{ get; set; }
        
    }
}
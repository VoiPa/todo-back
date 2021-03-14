using System;
using System.Collections.Generic;
using ToDoList.API.Models.Common;

namespace ToDoList.API.Entities
{
    public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Role = "role2";
            CreateDate = DateTime.Now;
            UpdateDate =DateTime.Now;
            
        }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }

        public IEnumerable<ToDoItem> ToDoItems{ get; set; }
        
    }
}
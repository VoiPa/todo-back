using System;

namespace ToDoList.BL.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Role = "role2";
        }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        private string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
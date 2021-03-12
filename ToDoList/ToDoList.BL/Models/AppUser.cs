using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.BL.Models
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<ToDoItem> ToDoItems { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
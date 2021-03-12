using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.BL.Models
{
    public class AppRole: IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
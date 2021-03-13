using System.Collections.Generic;
using ToDoList.API.Models.Common;

namespace ToDoList.API.Models
{
    public class AppUserRole : BaseEntity
    {
        public string Name { get; set; }
        
        public int AppUserId { get; set; }
       
    }
}
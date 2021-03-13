using System.Collections.Generic;

namespace ToDoList.BL.Models
{
    public class AppUserRole : BaseEntity
    {
        public string Name { get; set; }
        
        public int AppUserId { get; set; }
       
    }
}
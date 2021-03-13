using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BL.Models;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.DAL.Services.Implementations
{
    public class ToDoItemsRepository : IToDoItemsRepository
    {
        public Task<IEnumerable<ToDoItem>> AllItemsAsync()
        {
            
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(AppUser currentUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ToDoItem>> GetCompleteItemsAsync(AppUser currentUser)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExists(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
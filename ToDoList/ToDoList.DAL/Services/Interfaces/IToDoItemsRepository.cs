using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BL.Models;

namespace ToDoList.DAL.Services.Interfaces
{
    public interface IToDoItemsRepository
    {
        Task<IEnumerable<ToDoItem>> AllItemsAsync();
        Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(AppUser currentUser);
        Task<IEnumerable<ToDoItem>> GetCompleteItemsAsync(AppUser currentUser);
        bool Exists(int id);
        Task<bool> UserExists(string email);
        Task<bool> DeleteTodoAsync(int id);
       
    }
}
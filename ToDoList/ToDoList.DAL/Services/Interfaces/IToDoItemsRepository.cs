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
        Task<bool> DeleteTodoAsync(int id);
        Task<ToDoItem> GetItemAsync(int id);
        Task<bool> AddItemAsync(ToDoItem todo, AppUser currentUser);
        Task<bool> UpdateTodoAsync(ToDoItem todo, AppUser currentUser);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.BL.Models;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.DAL.Services.Implementations
{
    public class ToDoItemsRepository : IToDoItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //if user is admin
        public async Task<IEnumerable<ToDoItem>> AllItemsAsync()
        {
            return await _context.ToDo.ToArrayAsync();
        }
        
        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.ToDo.FindAsync(id);
            _context.ToDo.Remove(todo);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<ToDoItem>> GetIncompleteItemsAsync(AppUser user)
        {
            return await _context.ToDo
                .Where(t => !t.IsDone && t.AppUserId == user.Id)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetCompleteItemsAsync(AppUser user)
        {
            return await _context.ToDo
                .Where(t => t.IsDone && t.AppUser == user)
                .ToArrayAsync();
        }

        public bool Exists(int id)
        {
            return _context.ToDo
                .Any(t => t.Id == id);
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            }

            return false;
        }
    }
}
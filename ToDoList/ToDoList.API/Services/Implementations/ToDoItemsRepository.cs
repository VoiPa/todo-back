using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Entities;
using ToDoList.API.Helpers.Data;
using ToDoList.API.Models;
using ToDoList.API.Services.Interfaces;

namespace ToDoList.API.Services.Implementations
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
            var test = await _context.ToDo
                .Where(t => t.IsDone && t.AppUserId == user.Id)
                .ToArrayAsync();
            return test;
        }

        public async Task<ToDoItem> GetItemAsync(int id)
        {
            return await _context.ToDo
                .Where(t => t.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> AddItemAsync(ToDoItem todo, AppUser user)
        {
            todo.IsDone = false;
            todo.CreatedAt = DateTime.Now;
            todo.AppUserId = user.Id;
            _context.ToDo.Add(todo);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<AppUser> ValidateUserAsync(string email)
        {
            return await _context.Users.SingleAsync(a => a.Email == email);
        }

        public async Task<bool> UpdateTodoAsync(ToDoItem editedTodo, AppUser user)
        {
            var todo = await _context.ToDo
                .Where(t => t.Id == editedTodo.Id && t.AppUserId == user.Id)
                .SingleOrDefaultAsync();

            if (todo == null)
            {
                return false;
            }

            todo.TaskDescription = editedTodo.TaskDescription;
            todo.IsDone = editedTodo.IsDone;
            var saved = await _context.SaveChangesAsync();
            return saved == 1;
        }
    }
}
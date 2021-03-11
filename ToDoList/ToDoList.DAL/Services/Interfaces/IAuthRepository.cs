using System.Threading.Tasks;
using ToDoList.BL.Models;

namespace ToDoList.DAL.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<User> Login(string email, string password);
    }
}
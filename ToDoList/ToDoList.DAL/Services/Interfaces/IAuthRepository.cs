using System.Threading.Tasks;
using ToDoList.BL.Models;

namespace ToDoList.DAL.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser> Register(AppUser appUser, string password);
        Task<bool> UserExists(string email);
        Task<AppUser> Login(string email, string password);
    }
}
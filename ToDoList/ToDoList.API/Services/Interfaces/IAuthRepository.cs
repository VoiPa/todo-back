using System.Threading.Tasks;
using ToDoList.API.Entities;
using ToDoList.API.Models;

namespace ToDoList.API.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser> Register(AppUser appUser, string password);
        Task<bool> UserExists(string email);
        Task<AppUser> Login(string email, string password);
        void ForgotPassword(ForgotPasswordRequest model, string origin);
        void ResetPassword(ResetPasswordRequest model);
    }
}
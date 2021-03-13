using ToDoList.API.Models;

namespace ToDoList.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
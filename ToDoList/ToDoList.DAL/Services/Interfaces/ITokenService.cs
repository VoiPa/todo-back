using ToDoList.BL.Models;

namespace ToDoList.DAL.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
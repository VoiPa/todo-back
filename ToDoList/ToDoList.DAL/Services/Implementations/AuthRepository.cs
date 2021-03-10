using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.BL.Models;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.DAL.Services.Implementations
{
    public class AuthRepository:IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email==email))
            {
                return true;
            }

            return false;
        }
    }
}
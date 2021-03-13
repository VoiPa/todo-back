using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoList.API.Entities;
using ToDoList.API.Models;

namespace ToDoList.API.Helpers.Data
{
    public class Seed
    {
        public static async Task SeedUsers(ApplicationDbContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }
            
            var userData = await File.ReadAllTextAsync("Helpers/Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
            var roles = new List<AppUserRole>
            {
                new AppUserRole {Name = "role1"},
                new AppUserRole {Name = "role2"},
            };
            foreach (var user in users)
            {
                CreatePasswordHash("stringstringstring", out var passwordhash, out var passwordsalt);
                user.Role = roles[1].Name;
                user.PasswordHash = passwordhash;
                user.PasswordSalt = passwordsalt;
                context.Add(user);
            }
            
            var admin = new AppUser
            {
                Email = "admin@superadmin.lt",
                Role = roles[0].Name
                
            };
            CreatePasswordHash("Mypa77w0rdSlaptasLabai", out var passwordhashAdmin, out var passwordsaltAdmin);
            admin.PasswordHash = passwordhashAdmin;
            admin.PasswordSalt = passwordsaltAdmin;
            context.Add(admin);

            await context.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
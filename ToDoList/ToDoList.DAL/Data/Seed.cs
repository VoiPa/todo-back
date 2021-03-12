using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoList.BL.Models;

namespace ToDoList.DAL.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var userData = await File.ReadAllTextAsync("../ToDoList.DAL/Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
            var roles = new List<AppRole>
            {
                new AppRole {Name = "role1"},
                new AppRole {Name = "role2"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa77wordPa77wordPa77word");
                await userManager.AddToRoleAsync(user, "role2");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@superadmin.lt"
            };

            await userManager.CreateAsync(admin, "LabasVakars123456");
            await userManager.AddToRolesAsync(admin, new[] {"role1"});
        }
    }
}
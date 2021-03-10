using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ToDoList.BL.Models;

namespace ToDoList.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){ }
        public virtual DbSet<User> Users { get; set; } 
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> 
    { 
        public ApplicationDbContext CreateDbContext(string[] args) 
        { 
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../ToDoList.API/appsettings.json").Build(); 
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>(); 
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            builder.UseMySql(connectionString); 
            return new ApplicationDbContext(builder.Options); 
        } 
    }
}
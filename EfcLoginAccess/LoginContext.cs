using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcLoginAccess;

public class LoginContext : DbContext
{
    public DbSet<UserLogin> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcLoginAccess/Login.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // # Primary Key
        modelBuilder.Entity<UserLogin>().HasKey(user => user.UserName);
    }
}
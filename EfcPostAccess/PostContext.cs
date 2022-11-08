using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcPostAccess;

public class PostContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcPostAccess/Post.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // # Primary Key
        modelBuilder.Entity<Post>().HasKey(post => post.PostId);
        modelBuilder.Entity<User>().HasKey(user => user.UserID);
    }
}
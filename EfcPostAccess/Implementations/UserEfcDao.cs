using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcPostAccess.Implementations;

public class UserEfcDao : IUserDao
{
    // # Fields
    private readonly PostContext _context;
    
    // # Constructor
    public UserEfcDao(PostContext context)
    {
        _context = context;
    }
    
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        User? existing = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
        return existing;
    }

    public async Task<User?> GetByUserIdAsync(int userId)
    {
        User? existing = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
        return existing;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IQueryable<User> usersQuery = _context.Users.AsQueryable();
        if (searchParameters.UserNameContains != null)
        {
            usersQuery = usersQuery.Where(u =>
                u.UserName.ToLower().Contains(searchParameters.UserNameContains.ToLower()));
        }

        IEnumerable<User> result = await usersQuery.ToListAsync();
        return result;
    }
}
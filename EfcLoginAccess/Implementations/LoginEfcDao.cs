using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DAOInterface;
using Shared.Models;

namespace EfcLoginAccess.Implementations;

public class LoginEfcDao : IUserLoginDao
{
    private readonly LoginContext _context;
    
    // # Constructor
    public LoginEfcDao(LoginContext context)
    {
        _context = context;
    }
    public async Task<UserLogin> CreateAsync(UserLogin user)
    {
        EntityEntry<UserLogin> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<UserLogin?> GetByUserNameAsync(string userName)
    {
        UserLogin? existing = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));
        return existing;
    }
}
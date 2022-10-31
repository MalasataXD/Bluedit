using Shared.DAOInterface;
using Shared.Models;

namespace FileData.DAOs;

public class LoginFileDao : IUserLoginDao
{
    // # Fields
    private readonly LoginContext _context;

    // ¤ Constructor
    public LoginFileDao(LoginContext context)
    {
        _context = context;
    }
    // ¤ Create method
    public Task<UserLogin> CreateAsync(UserLogin user)
    {
        // # Add user to list of users.
        _context.Users.Add(user);
        // # Save changes in database.
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    // ¤ Get method (Username)
    public Task<UserLogin?> GetByUserNameAsync(string userName)
    {
        UserLogin? existing = _context.Users.FirstOrDefault(u 
            => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        
        return Task.FromResult(existing);
    }
}
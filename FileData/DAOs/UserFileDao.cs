using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    // # Fields
    private readonly FileContext _context;

    // ¤ Constructor
    public UserFileDao(FileContext context)
    {
        this._context = context;
    }
    // ¤ Create method
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        // # Check if other users exists and count them.
        if (_context.Users.Any())
        {
            userId = _context.Users.Max(u => u.UserID);
            userId++;
        }

        user.UserID = userId;
        
        // # Add user to list of users.
        _context.Users.Add(user);
        // # Save changes in database.
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    // ¤ Get method (Username)
    public Task<User?> GetByUserNameAsync(string userName)
    {
        User? existing = _context.Users.FirstOrDefault(u 
            => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        
        return Task.FromResult(existing);
    }
    // ¤ Get method (UserId)
    public Task<User?> GetByUserIdAsync(int userId)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.UserID == userId);
        return Task.FromResult(existing);
    }
    // ¤ Get method (Search Parameters)
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        // # Get the list of users
        IEnumerable<User> users = _context.Users.AsEnumerable();
        // # Check if any users match search parameters
        if (searchParameters.UserNameContains != null)
        {
            users = _context.Users.Where(u =>
                u.UserName.Contains(searchParameters.UserNameContains, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(users);
    }
}
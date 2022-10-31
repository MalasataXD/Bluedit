using Application.DAOInterfaces;
using Shared.DAOInterface;
using Shared.DTOs;
using Shared.Models;

namespace BlueditAPI.Service;

public class AuthService : IAuthService
{
    private readonly IUserLoginDao _userLoginDao;

    public AuthService(IUserLoginDao userLoginDao)
    {
        _userLoginDao = userLoginDao;
    }
    
    
    public async Task<UserLogin> GetUser(string username, string password)
    {
        UserLogin? user = await _userLoginDao.GetByUserNameAsync(username);
        if (user == null)
        {
            throw new Exception("Couldn't find a user with that username!");
        }
        
        if (!user.PassWord.Equals(password, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Password mismatch...");
        }

        return user;
    }
    
    public async Task<UserLogin> GetUserByName(string username)
    {
        UserLogin? user = await _userLoginDao.GetByUserNameAsync(username);
        if (user == null)
        {
            throw new Exception("Couldn't find a user with that username!");
        }
        return user;
    }

    public async Task RegisterUser(UserLoginCreationDto user)
    {
        UserLogin? existing = await _userLoginDao.GetByUserNameAsync(user.UserName);
        if (existing != null)
        {
            throw new Exception("Username already taken!");
        }

        UserLogin toCreate = new UserLogin
        {
            UserName = user.UserName,
            PassWord = user.PassWord
        };

        if (!string.IsNullOrEmpty(user.Role))
        {
            toCreate.Role = user.Role;
        }

        UserLogin created = await _userLoginDao.CreateAsync(toCreate);
    }
    
    public async Task<UserLogin> ValidateUser(string username, string password)
    {
        UserLogin? existingUser = await _userLoginDao.GetByUserNameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.PassWord.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
}
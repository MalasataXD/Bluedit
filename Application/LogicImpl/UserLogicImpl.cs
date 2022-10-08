using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicImpl;

public class UserLogicImpl : IUserLogic
{
    // # Fields
    private readonly IUserDao userDao;
    
    // ¤ Constructor
    public UserLogicImpl(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    // ! Create Method
    public async Task<User> CreateAsync(UserCreationDto userToCreate)
    {
        // # Check if username already exists in database
        User? existing = await userDao.GetByUserNameAsync(userToCreate.UserName);
        if (existing != null)
        {
            throw new Exception("Username already taken!");
        }

        // # Validate the DTO
        ValidateData(userToCreate);
        
        // # Create new user
        User toCreate = new User
        {
            UserName = userToCreate.UserName,
        };
        User created = await userDao.CreateAsync(toCreate);

        return created;
    }
    

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
    
    
    // ¤ Validation of DTO
    private static void ValidateData(UserCreationDto userToCreate)
    {
        // # Extract the username
        string userName = userToCreate.UserName;
        // # Check if username is within the parameters
        if (userName.Length < 3)
        {
            throw new Exception("Username must be at least 3 characters!");
        }
        else if (userName.Length > 15)
        {
            throw new Exception("Username must be less than 15 characters!");
        }
    }
    
    
    
}
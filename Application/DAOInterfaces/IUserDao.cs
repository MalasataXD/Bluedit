using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByUserIdAsync(int userId);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}
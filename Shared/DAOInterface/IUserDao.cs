using Shared.Models;

namespace Application.DAOInterfaces;

public interface IUserLoginDao
{
    Task<UserLogin> CreateAsync(UserLogin user);
    Task<UserLogin?> GetByUserNameAsync(string userName);
}
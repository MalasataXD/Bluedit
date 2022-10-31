using Shared.Models;

namespace Shared.DAOInterface;

public interface IUserLoginDao
{
    Task<UserLogin> CreateAsync(UserLogin user);
    Task<UserLogin?> GetByUserNameAsync(string userName);
}
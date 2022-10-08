using Shared.DTOs;
using Shared.Models;

namespace BlueditAPI.Service;

public interface IAuthService
{
    Task<UserLogin> GetUser(string username, string password);
    Task RegisterUser(UserLoginCreationDto user);
    Task<UserLogin> ValidateUser(string username, string password);
}
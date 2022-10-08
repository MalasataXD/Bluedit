using Domain.DTOs;
using Domain.Models;

namespace HttpClients.Interfaces;

public interface IUserService
{
    // # Methods
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsers();
}
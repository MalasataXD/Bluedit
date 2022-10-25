using Domain.DTOs;
using Domain.Models;

namespace HttpClients.Interfaces;

public interface IPostService
{
    // # Methods
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(string? userName,string? title);
    Task UpdateAsync(PostUpdateDto dto);
    Task DeleteAsync(int id);
}
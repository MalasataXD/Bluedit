using Domain.DTOs;
using Domain.Models;

namespace HttpClients.Interfaces;

public interface IPostService
{
    // # Methods
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(string? userName,int? id, string? title);
    Task<Post> GetByIdAsync(int id);
    Task UpdateAsync(PostUpdateDto dto);
    Task DeleteAsync(int id);
}
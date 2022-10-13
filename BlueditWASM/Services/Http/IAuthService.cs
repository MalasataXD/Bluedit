using System.Security.Claims;
using Shared.DTOs;
using Shared.Models;

namespace BlueditWASM.Services.Http;
public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task RegisterAsync(UserLoginCreationDto user);
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
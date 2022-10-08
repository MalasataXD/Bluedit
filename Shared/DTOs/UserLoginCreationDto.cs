
namespace Shared.DTOs;

public class UserLoginCreationDto
{
    public string UserName { get; init; }
    public string PassWord { get; init; }
    public string Role { get; set; } = "user";
}
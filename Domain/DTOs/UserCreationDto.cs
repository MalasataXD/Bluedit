namespace Domain.DTOs;

public class UserCreationDto
{
    // # Fields
    public string UserName { get; }
    
    // ¤ Constructor
    public UserCreationDto(string userName)
    {
        UserName = userName;
    }
    
}
namespace Domain.DTOs;

public class SearchUserParametersDto
{
    // # Fields
    public string? UserNameContains { get; set; }
    
    // ¤ Constructor
    public SearchUserParametersDto(string? userNameContains)
    {
        UserNameContains = userNameContains;
    }
    
}
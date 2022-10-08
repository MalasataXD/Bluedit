namespace Domain.DTOs;

public class SearchPostParametersDto
{
    // # Fields
    public string? UserName { get; set; }
    public int? UserId { get; set; }
    public string? TitleContains { get; set; }

    // ¤ Constructor
    public SearchPostParametersDto(string? userName, int? userId, string? titleContains)
    {
        UserName = userName;
        UserId = userId;
        TitleContains = titleContains;
    }
}
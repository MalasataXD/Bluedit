namespace Domain.DTOs;

public class PostCreationDto
{
    // # Fields
    public string OwnerName { get; }
    public string Title { get; }
    public string Description { get;}
    
    // ¤ Constructor
    public PostCreationDto(string ownerName, string title, string description)
    {
        OwnerName = ownerName;
        Title = title;
        Description = description;
    }



}
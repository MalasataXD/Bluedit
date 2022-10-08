namespace Domain.DTOs;

public class PostCreationDto
{
    // # Fields
    public int OwnerId { get; }
    public string Title { get; }
    public string Description { get;}
    
    // ¤ Construcotr
    public PostCreationDto(int ownerId, string title, string description)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
    }



}
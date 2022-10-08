namespace Domain.DTOs;

public class PostUpdateDto
{
    // # Fields
    public int PostId { get; }
    public int? OwnerId { get; set; }
    public string? Tilte { get; set; }
    public string? Description { get; set; }

    // ¤ Constructor
    public PostUpdateDto(int postId)
    {
        PostId = postId;
    }
}
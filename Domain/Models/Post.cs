namespace Domain.Models;

public class Post
{
    //# Fields
    public int PostId { get; set; }
    public User Owner { get; private set; }
    public string Title { get; private set; }
    public string Description { get; set; }
    public string DateTime { get; set; }
   
    
    // ¤ Constructor
    public Post(User owner, string title, string description)
    {
        Owner = owner;
        Title = title;
        Description = description;
    }
    
    private Post(){}
}
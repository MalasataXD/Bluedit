namespace Domain.Models;

public class Post
{
    //# Fields
    public int PostId { get; set; }
    public User Owner { get;}
    public string Title { get; set; }
    public string Description { get; set; }
    public string DateTime { get; set; }
   
    
    // ¤ Constructor
    public Post(User owner, string title, string description)
    {
        Owner = owner;
        Title = title;
        Description = description;
    }
}
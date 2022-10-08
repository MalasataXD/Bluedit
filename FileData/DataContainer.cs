using Domain.Models;

namespace FileData;

public class DataContainer
{
    // # Fields
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }
}
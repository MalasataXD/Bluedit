using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    // # Fields
    public int UserID { get; set; }
    public string UserName { get; set; }
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }
}
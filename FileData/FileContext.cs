using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    // ! TO BE LATER REPLACED BY DATABASE
    // # Fields
    private const string FilePath = "data.json";
    private DataContainer? DataContainer;
    
    // ¤ Load data
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return DataContainer!.Posts;
        }
    }
    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return DataContainer!.Users;
        }
    }

    // ¤ Extract data form file
    private void LoadData()
    {
        // # Check if data has already been loaded
        if (DataContainer != null)
        {
            return; 
            // ! DO NOTHING.
        }
        
        // # Check if File is at the filepath, then create DataContainer
        if (!File.Exists(FilePath))
        {
            DataContainer = new()
            {
                Posts = new List<Post>(), Users = new List<User>()
            };
            return;
        }
        
        // ¤ Get content from file
        string content = File.ReadAllText(FilePath);
        DataContainer = JsonSerializer.Deserialize<DataContainer>(content);



    }

    // ¤ Save data into file
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(DataContainer);
        File.WriteAllText(FilePath,serialized);
        DataContainer = null; // ! RESET DATACONTAINER
    }
}
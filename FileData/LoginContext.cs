using System.Text.Json;
using Shared.Models;

namespace FileData;

public class LoginContext
{
    // ! TO BE LATER REPLACED BY DATABASE
    // # Fields
    private const string FilePath = "login.json";
    private LoginContainer? loginContainer;
    
    // ¤ Load data
    
    public ICollection<UserLogin> Users
    {
        get
        {
            LoadData();
            return loginContainer!.Users;
        }
    }

    // ¤ Extract data form file
    private void LoadData()
    {
        // # Check if data has already been loaded
        if (loginContainer != null)
        {
            return; 
            // ! DO NOTHING.
        }
        
        // # Check if File is at the filepath, then create DataContainer
        if (!File.Exists(FilePath))
        {
            loginContainer = new()
            {
                 Users = new List<UserLogin>()
            };
            return;
        }
        
        // ¤ Get content from file
        string content = File.ReadAllText(FilePath);
        loginContainer = JsonSerializer.Deserialize<LoginContainer>(content);



    }

    // ¤ Save data into file
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(loginContainer,new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath,serialized);
        loginContainer = null; // ! RESET DATACONTAINER
    }
}
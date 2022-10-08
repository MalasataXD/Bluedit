using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.Interfaces;

namespace HttpClients.Services;

public class UserHttpClient : IUserService
{
    // # Fields
    private readonly HttpClient _client;
    
    // ¤ Constructor
    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }

    
    // ¤ Create new User
    public async Task<User> Create(UserCreationDto dto)
    {
        // # Send the UserCreationDTO to WebAPI
        HttpResponseMessage response = await _client.PostAsJsonAsync("/user", dto);
        string result = await response.Content.ReadAsStringAsync();
        
        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Creation of user went wrong!");
        }
        
        // # If user was created, return user.
        User user = JsonSerializer.Deserialize<User>(result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return user;
    }

    // ¤ Get the complete list of users
    public async Task<IEnumerable<User>> GetUsers()
    {
        // # Request the list of users.
        const string uri = "/user";
        HttpResponseMessage response = await _client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        
        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Getting the list of user went wrong!");
        }

        // # If list was received, return list.
        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        return users;
    }
}
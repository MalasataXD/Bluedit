using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.Interfaces;

namespace HttpClients.Services;

public class PostHttpClient : IPostService
{
    // # Fields
    private readonly HttpClient _client;
    

    // ¤ Constructor
    public PostHttpClient(HttpClient client)
    {
        _client = client;
    }

    
    // # Create new Post
    public async Task CreateAsync(PostCreationDto dto)
    {
        // # Send the PostCreationDto to WebAPI
        HttpResponseMessage response = await _client.PostAsJsonAsync("/post", dto);

        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Creation of Post went wrong!");
        }
    }

    // # Get Post (Query)
    public async Task<ICollection<Post>> GetAsync(string? userName, string? titleContains)
    {
        // # Send Request of Post/Posts
        string query = ConstructQuery(userName, titleContains);
        HttpResponseMessage response = await _client.GetAsync("/post" + query);
        string content = await response.Content.ReadAsStringAsync();
        
        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not find a Post with specific details");
        }

        // # If list was received, return list.
        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return posts;
        
    }

    // # Update an existing Post
    public async Task UpdateAsync(PostUpdateDto dto)
    {
        // # Send PostUpdateDto to WebAPI
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PatchAsync("/post", body);
        
        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Update of Post went wrong!");
        }
    }

    // # Delete an existing Post
    public async Task DeleteAsync(int id)
    {
        // # Send a DELETE to WebAPI
        HttpResponseMessage response = await _client.DeleteAsync($"post/{id}");
        
        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Deletion of Post went wrong!");
        }
    }
    
    // ¤ Create Query by using SearchParameters
    private static string ConstructQuery(string? userName, string? titleContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }
        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"title={titleContains}";
        }
        
        return query;
    }
}
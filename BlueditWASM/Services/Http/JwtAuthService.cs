using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Shared.DTOs;
using Shared.Models;

namespace BlueditWASM.Services.Http;

public class JwtAuthService : IAuthService
{
    // # Fields
    private readonly HttpClient _client = new();
    // ¤ Variable for simpel caching
    public static string? Jwt { get; private set; } = "";
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
    // ! Below methods stolen from https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    // ¤ Creating the ClaimsPrincipal!
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    
    // ¤ Log out on the Website
    public async Task LoginAsync(string username, string password)
    {
        // # Send Login request to WebAPI
        UserLoginDto userLoginDto = new()
        {
            UserName = username,
            PassWord = password
        };
        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("https://localhost:7157/auth/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        // # Set Token.
        string token = responseContent;
        Jwt = token;
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        OnAuthStateChanged.Invoke(principal);
    }

    // ¤ Log out on the Website
    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
            return Task.CompletedTask;
    }

    // ¤ Register a new user on the Website
    public async Task RegisterAsync(UserLoginCreationDto user)
    {
        string UserCreationDtoAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(UserCreationDtoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("https://localhost:7157/auth/register", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        // ! Did it work?
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Something is FUCKED!");
        }
    }

    // ¤ Get Authentication on the Website
    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }


}
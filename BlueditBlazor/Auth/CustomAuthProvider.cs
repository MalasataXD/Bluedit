using System.Security.Claims;
using Http;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlueditBlazor.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    // # Fields
    private readonly IAuthService authService;

    // ¤ Constructor
    public CustomAuthProvider(IAuthService authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    // ¤ Get the current Auth state.
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();

        return new AuthenticationState(principal);
    }

    // ¤ Auth State changed (Notify when state changed!)
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }

}
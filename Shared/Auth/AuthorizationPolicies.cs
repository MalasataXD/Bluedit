using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public class AuthorizationPolicies
{
    // ! Add policies here!
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeUser",a => a.RequireAuthenticatedUser().RequireClaim("Role","user","admin"));
            options.AddPolicy("MustBeAdmin",a=> a.RequireAuthenticatedUser().RequireClaim("Role","admin"));
        });
    }
}
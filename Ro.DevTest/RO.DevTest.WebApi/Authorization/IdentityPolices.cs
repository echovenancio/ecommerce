using Microsoft.AspNetCore.Authorization;

namespace RO.DevTest.WebApi.Authorization;

public static class IdentityPolicies {
    public const string AdminPolicy = "RequireAdminRole";
    public const string UserPolicy = "RequireUserRole";

    public static void AddIdentityPolicies(this AuthorizationOptions options) {
        options.AddPolicy(AdminPolicy, policy =>
            policy.RequireRole("Admin"));

        options.AddPolicy(UserPolicy, policy =>
            policy.RequireRole("User", "Admin")); // admin can do user stuff too
    }
}

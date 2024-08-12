using Microsoft.AspNetCore.Authorization;

namespace ApiServfyNotifications.Middleware
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> Roles { get; set; }

        public RoleRequirement(IEnumerable<string> roles)
        {
            Roles = roles.ToList() ?? new List<string>();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiServfyNotifications.Middleware
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        public const string HeaderName = "Authorization";

        private readonly IConfiguration _configuration;


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            SucceedRequirementIfRoleIsPresent(context, requirement);
            return Task.CompletedTask;
        }

        private void SucceedRequirementIfRoleIsPresent(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                var token = httpContext.Request.Headers[HeaderName].FirstOrDefault()?.Replace("Bearer ", "");

                var isToken = new JwtSecurityTokenHandler().CanReadToken(token);
                if (isToken)
                {
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    var scopes = jwt.Claims.First(f => f.Type == "scope");
                    var claimRoles = GetRoles(scopes);

                    if (claimRoles.Any(f => requirement.Roles.Contains(f)))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
        }

        public string[] GetRoles(Claim claim)
        {
            List<Claim> claims = new List<Claim>();
            var roles = claim.Value.Split(' ');

            return roles;
        }
    }
}

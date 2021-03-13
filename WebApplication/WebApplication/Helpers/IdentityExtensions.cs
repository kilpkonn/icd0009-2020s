using System;
using System.Linq;
using System.Security.Claims;

namespace WebApplication.Helpers
{
    public static class IdentityExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? Guid.Parse(userId) : null;
        }
    }
}
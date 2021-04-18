using System;
using System.Linq;
using System.Security.Claims;

namespace WebApplication.Helpers
{
    /// <summary>
    /// Identity extension helper
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Get user id from claims
        /// </summary>
        /// <param name="user">User claims</param>
        /// <returns></returns>
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? Guid.Parse(userId) : null;
        }
    }
}
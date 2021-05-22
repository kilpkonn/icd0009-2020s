using System;
using Microsoft.AspNetCore.Identity;

namespace PublicApi.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
    }
}
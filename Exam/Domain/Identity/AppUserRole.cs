using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public Guid Id { get; set; }

        public AppUser? AppUser { get; set; }
        public AppRole? AppRole { get; set; }
    }
}
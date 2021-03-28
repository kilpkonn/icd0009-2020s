using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(64), MinLength(1)]
        public string DisplayName { get; set; } = default!;

        public ICollection<PublicApi.DTO.v1.CarAccess>? CarAccesses { get; set; }
        public ICollection<PublicApi.DTO.v1.GasRefill>? GasRefills { get; set; }
        public ICollection<PublicApi.DTO.v1.Track>? Tracks { get; set; }
    }
}
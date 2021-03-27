using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(64), MinLength(1)]
        public string DisplayName { get; set; } = default!;

        public ICollection<Domain.App.CarAccess>? CarAccesses { get; set; }
        public ICollection<Domain.App.GasRefill>? GasRefills { get; set; }
        public ICollection<Domain.App.Track>? Tracks { get; set; }
    }
}
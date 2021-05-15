using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Car.DAL.Base.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(64), MinLength(1)]
        public string DisplayName { get; set; } = default!;

        public ICollection<CarAccess>? CarAccesses { get; set; }
        public ICollection<GasRefill>? GasRefills { get; set; }
        public ICollection<Track>? Tracks { get; set; }
    }
}
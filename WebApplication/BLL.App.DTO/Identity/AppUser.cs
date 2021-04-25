using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Resource.Base;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(64, MinimumLength = 1, ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax",
            ErrorMessageResourceType = typeof(Common))]
        public string DisplayName { get; set; } = default!;

        public ICollection<CarAccess>? CarAccesses { get; set; }
        public ICollection<GasRefill>? GasRefills { get; set; }
        public ICollection<Track>? Tracks { get; set; }
    }
}
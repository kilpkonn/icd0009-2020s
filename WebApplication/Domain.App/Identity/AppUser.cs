using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Resource.Base;
using Resource.Base.Areas.Identity.Pages.Account;
using Resource.Base.Base.Domain.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required(ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_Required")]
        [Display(Name = nameof(DisplayName), ResourceType = typeof(Register))]
        public string DisplayName { get; set; } = default!;

        [Display(Name = nameof(UserName), ResourceType = typeof(BaseUser))]
        public override string? UserName { get; set; }

        [Display(Name = nameof(NormalizedUserName), ResourceType = typeof(BaseUser))]
        public override string? NormalizedUserName { get; set; }

        [Display(Name = nameof(Email), ResourceType = typeof(BaseUser))]
        public override string? Email { get; set; }

        [Display(Name = nameof(NormalizedEmail), ResourceType = typeof(BaseUser))]
        public override string? NormalizedEmail { get; set; } 

        [Display(Name = "EmailConfirmed", ResourceType = typeof(BaseUser))]
        public override bool EmailConfirmed { get; set; } 

        [Display(Name = nameof(PasswordHash), ResourceType = typeof(BaseUser))]
        public override string? PasswordHash { get; set; } 

        [Display(Name = nameof(SecurityStamp), ResourceType = typeof(BaseUser))]
        public override string? SecurityStamp { get; set; }

        [Display(Name = nameof(ConcurrencyStamp), ResourceType = typeof(BaseUser))]
        public override string? ConcurrencyStamp { get; set; } 

        [Display(Name = nameof(PhoneNumber), ResourceType = typeof(BaseUser))]
        public override string? PhoneNumber { get; set; }

        [Display(Name = nameof(PhoneNumberConfirmed), ResourceType = typeof(BaseUser))]
        public override bool PhoneNumberConfirmed { get; set; }
        [Display(Name = nameof(TwoFactorEnabled), ResourceType = typeof(BaseUser))]
        public override bool TwoFactorEnabled { get; set; }
        [Display(Name = nameof(LockoutEnd), ResourceType = typeof(BaseUser))]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [Display(Name = nameof(LockoutEnabled), ResourceType = typeof(BaseUser))]
        public override bool LockoutEnabled { get; set; }
        [Display(Name = nameof(AccessFailedCount), ResourceType = typeof(BaseUser))]
        public override int AccessFailedCount { get; set; }

        public ICollection<CarAccess>? CarAccesses { get; set; }
        public ICollection<GasRefill>? GasRefills { get; set; }
        public ICollection<Track>? Tracks { get; set; }
    }
}
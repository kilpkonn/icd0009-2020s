using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Resource.Base.Base.Domain.Identity;

namespace Domain.App.Identity
{
    public class AppRole: IdentityRole<Guid>
    {
        [Display(Name = nameof(Name), ResourceType = typeof(BaseRole))]
        public override string? Name { get; set; }
        [Display(Name = nameof(NormalizedName), ResourceType = typeof(BaseRole))]
        public override string? NormalizedName { get; set; }
        [Display(Name = nameof(ConcurrencyStamp), ResourceType = typeof(BaseRole))]
        public override string? ConcurrencyStamp { get; set; }
    }
}
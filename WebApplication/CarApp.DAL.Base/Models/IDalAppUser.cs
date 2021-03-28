using System;
using Microsoft.AspNetCore.Identity;

namespace Car.DAL.Base.Models
{
    public interface IDalAppUser<TAppUser> : IDalAppUser<Guid, TAppUser>
        where TAppUser : IdentityUser<Guid>
    {
    }

    public interface IDalAppUser<TKey, TAppUser> : IDalAppUserId<TKey>
        where TKey : IEquatable<TKey>
        where TAppUser : IdentityUser<TKey>
    {
        TAppUser? AppUser { get; set; }
    }
}
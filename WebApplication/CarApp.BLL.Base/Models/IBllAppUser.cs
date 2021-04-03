using System;
using Car.DAL.Base.Models;
using Microsoft.AspNetCore.Identity;

namespace CarApp.BLL.Base.Models
{
    public interface IBllAppUser<TAppUser> : IBllAppUser<Guid, TAppUser>
        where TAppUser : IdentityUser<Guid>
    {
    }

    public interface IBllAppUser<TKey, TAppUser> : IBllAppUserId<TKey>
        where TKey : struct, IEquatable<TKey>
        where TAppUser : IdentityUser<TKey>
    {
        TAppUser? AppUser { get; set; }
    }
}
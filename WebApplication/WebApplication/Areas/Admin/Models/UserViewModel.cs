using System.Collections.Generic;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Areas.Admin.Models
{
    public class UserViewModel
    {
        public AppUser? User { get; set; }
        public ICollection<string> SelectedRoles { get; set; } = new List<string>();
        public ICollection<AppRole>? Roles { get; set; }
    }
}
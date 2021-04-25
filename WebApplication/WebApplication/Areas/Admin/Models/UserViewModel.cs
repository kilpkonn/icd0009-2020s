using System.Collections.Generic;
using Domain.App.Identity;

namespace WebApplication.Areas.Admin.Models
{
    /// <summary>
    /// User view model
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// User
        /// </summary>
        public AppUser? User { get; set; }
        /// <summary>
        /// Selected roles
        /// </summary>
        public ICollection<string> SelectedRoles { get; set; } = new List<string>();
        /// <summary>
        /// Possible roles
        /// </summary>
        public ICollection<AppRole>? Roles { get; set; }
    }
}
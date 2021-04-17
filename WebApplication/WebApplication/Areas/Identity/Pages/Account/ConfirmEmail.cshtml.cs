using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApplication.Areas.Identity.Pages.Account
{
    /// <inheritdoc />
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        /// <inheritdoc />
        public ConfirmEmailModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Status message
        /// </summary>
        [TempData]
        public string? StatusMessage { get; set; }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="code">Email code</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(string? userId, string? code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    /// <inheritdoc />
    public class ResetAuthenticatorModel : PageModel
    {
        UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        ILogger<ResetAuthenticatorModel> _logger;

        /// <inheritdoc />
        public ResetAuthenticatorModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<ResetAuthenticatorModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Status message
        /// </summary>
        [TempData]
        public string? StatusMessage { get; set; }

        /// <summary>
        /// Get reset auth
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        /// <summary>
        /// Post reset auth
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key", user.Id);
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.";

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}
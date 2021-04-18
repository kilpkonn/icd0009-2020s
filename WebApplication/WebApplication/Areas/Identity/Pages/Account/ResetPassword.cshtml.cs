using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        /// <inheritdoc />
        public ResetPasswordModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Reset password input
        /// </summary>
        [BindProperty]
        public InputModel? Input { get; set; }

        /// <summary>
        /// Reset password input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Email
            /// </summary>
            [Required]
            [EmailAddress]
            public string? Email { get; set; }

            /// <summary>
            /// Password
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            /// <summary>
            /// Confirm password
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string? ConfirmPassword { get; set; }

            /// <summary>
            /// Code
            /// </summary>
            public string? Code { get; set; }
        }

        /// <summary>
        /// Get reset password
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult OnGet(string? code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        /// <summary>
        /// Post reset password
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input!.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}

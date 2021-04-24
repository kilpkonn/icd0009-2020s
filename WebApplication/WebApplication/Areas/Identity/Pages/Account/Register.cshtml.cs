using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Resource.Base.Areas.Identity.Pages.Account;

namespace WebApplication.Areas.Identity.Pages.Account
{
    /// <inheritdoc />
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        /// <inheritdoc />
        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Register input
        /// </summary>
        [BindProperty]
        public InputModel? Input { get; set; }

        /// <summary>
        /// Return url
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// External logins
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        /// <summary>
        /// REgister input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Email
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = nameof(Email), ResourceType = typeof(Register))]
            public string? Email { get; set; }
            
            /// <summary>
            /// Display name
            /// </summary>
            [Required]
            [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            [Display(Name = nameof(DisplayName), ResourceType = typeof(Register))]
            public string? DisplayName { get; set; }

            /// <summary>
            /// Password
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = nameof(Password), ResourceType = typeof(Register))]
            public string? Password { get; set; }

            /// <summary>
            /// Confirm password
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Register))]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string? ConfirmPassword { get; set; }
        }

        /// <summary>
        /// Get register
        /// </summary>
        /// <param name="returnUrl"></param>
        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        /// <summary>
        /// Post register
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = Input!.Email, Email = Input.Email, DisplayName = Input.DisplayName!};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

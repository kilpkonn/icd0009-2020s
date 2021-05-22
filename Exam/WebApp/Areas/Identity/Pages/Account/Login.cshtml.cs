using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Resource.Base;
using Resource.Base.Areas.Identity.Pages.Account;

namespace WebApp.Areas.Identity.Pages.Account
{
    /// <inheritdoc />
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        /// <inheritdoc />
        public LoginModel(SignInManager<AppUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Input model for login
        /// </summary>
        [BindProperty]
        public InputModel? Input { get; set; }

        /// <summary>
        /// External logins
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        /// <summary>
        /// Return url
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [TempData]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Login input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Email
            /// </summary>
            [Required(ErrorMessageResourceType = typeof(Common),
                ErrorMessageResourceName = "ErrorMessage_Required")]
            [EmailAddress(ErrorMessageResourceType = typeof(Common),
                ErrorMessageResourceName = "ErrorMessage_Email")]
            [Display(Name = nameof(Email), ResourceType = typeof(Login))]
            public string? Email { get; set; }

            /// <summary>
            /// Password
            /// </summary>
            [Required(ErrorMessageResourceType = typeof(Common),
                ErrorMessageResourceName = "ErrorMessage_Required")]
            [StringLength(100, ErrorMessageResourceType = typeof(Common),
                ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = nameof(Password), ResourceType = typeof(Login))]
            public string? Password { get; set; }

            /// <summary>
            /// Remember me
            /// </summary>
            [Display(Name = nameof(RememberMe), ResourceType = typeof(Login))]

            public bool RememberMe { get; set; }
        }

        /// <summary>
        /// Get login
        /// </summary>
        /// <param name="returnUrl">Return url</param>
        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl!;
        }

        /// <summary>
        /// Post login
        /// </summary>
        /// <param name="returnUrl">Return url</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input!.Email, Input.Password, Input.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in");
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new {ReturnUrl = returnUrl, RememberMe = Input.RememberMe});
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
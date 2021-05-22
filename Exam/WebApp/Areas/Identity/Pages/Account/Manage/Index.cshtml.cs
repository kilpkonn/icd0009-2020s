using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resource.Base;
using Index = Resource.Base.Areas.Identity.Pages.Account.Manage.Index;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    /// <inheritdoc />
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        /// <inheritdoc />
        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Username
        /// </summary>
        [Display(Name = nameof(Username), ResourceType = typeof(Index))]

        public string? Username { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        [TempData]
        public string? StatusMessage { get; set; }

        /// <summary>
        /// Index input model
        /// </summary>
        [BindProperty]
        public InputModel? Input { get; set; }

        /// <summary>
        /// Index input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Phone number
            /// </summary>
            [Phone(ErrorMessageResourceName = "ErrorMessage_NotValidPhone", ErrorMessageResourceType = typeof(Common))]
            [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Index))]

            public string? PhoneNumber { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        /// <summary>
        /// Get index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(Index.Unable_to_load_user_with_ID,_userManager.GetUserId(User)));
            }

            await LoadAsync(user);
            return Page();
        }

        /// <summary>
        /// Post index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(Index.Unable_to_load_user_with_ID,_userManager.GetUserId(User)));
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input!.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = Index.Unexpected_error_when_trying_to_set_phone_number;
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = Index.Your_profile_has_been_updated;
            return RedirectToPage();
        }
    }
}

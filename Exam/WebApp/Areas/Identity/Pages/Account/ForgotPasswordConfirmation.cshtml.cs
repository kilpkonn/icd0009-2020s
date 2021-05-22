using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Identity.Pages.Account
{
    /// <inheritdoc />
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        /// <summary>
        /// Empty get
        /// </summary>
        public void OnGet()
        {
        }
    }
}

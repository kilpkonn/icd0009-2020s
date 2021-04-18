using System;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    /// <inheritdoc />
    public class HomeController: Controller
    {
        
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public HomeController(IAppBll bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Set language and culture
        /// </summary>
        /// <param name="culture">Culture</param>
        /// <param name="returnUrl">Return url</param>
        /// <returns></returns>
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return LocalRedirect(returnUrl);
        }

    }
}
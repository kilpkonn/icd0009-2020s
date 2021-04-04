using System;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using CarApp.BLL.App.Services;
using Extensions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AppUser = Domain.App.Identity.AppUser;

namespace BLL.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IConfiguration _configuration;

        public AccountService(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<AccountService> logger,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<JwtResponse?> Register(Register register)
        {
            var appUser = await _userManager.FindByEmailAsync(register.Email);
            if (appUser != null)
            {
                _logger.LogWarning(" User {User} already registered", register.Email);
                return null;
            }

            appUser = new AppUser()
            {
                Email = register.Email,
                UserName = register.Email,
                DisplayName = register.DisplayName,
            };
            var result = await _userManager.CreateAsync(appUser, register.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", register.Email);
                    return new JwtResponse()
                    {
                        Token = jwt,
                    };
                }
                else
                {
                    _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                    return null;
                }
            }

            return null;
        }

        public async Task<JwtResponse?> Login(Login login)
        {
            var appUser = await _userManager.FindByEmailAsync(login.Email);
            // TODO: wait a random time here to fool timing attacks
            if (appUser == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found", login.Email);
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, login.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                _logger.LogInformation("WebApi login. User {User}", login.Email);
                return new JwtResponse()
                {
                    Token = jwt,
                };
            }

            _logger.LogWarning("WebApi login failed. User {User} - bad password", login.Email);
            return null;
        }
    }
}
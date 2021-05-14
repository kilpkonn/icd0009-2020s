using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Error;
using PublicApi.DTO.v1.Identity;
using PublicApi.DTO.v1.Mappers;

namespace WebApplication.ApiControllers.Identity
{
    /// <inheritdoc />
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly LoginMapper _loginMapper;
        private readonly RegisterMapper _registerMapper;

        /// <inheritdoc />
        public AccountController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _loginMapper = new LoginMapper(mapper);
            _registerMapper = new RegisterMapper(mapper);
        }

        /// <summary>
        /// Login to account
        /// </summary>
        /// <param name="dto">Email and Password dto</param>
        /// <returns>JWT or errors</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login dto)
        {
            var res = await _bll.Accounts.Login(_loginMapper.Map(dto)!);

            if (res != null)
            {
                return Ok(new JwtResponse {Token = res.Token});
            }

            return NotFound(new Error("User/Password problem!"));
        }


        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="dto">Register dto</param>
        /// <returns>Generated JWT or errors</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            var res = await _bll.Accounts.Register(_registerMapper.Map(dto)!);

            if (res != null)
            {
                return Ok(new JwtResponse {Token = res.Token});
            }

            return BadRequest(new Error("Error registering! Try some other email"));
        }
    }
}
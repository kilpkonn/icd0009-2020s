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
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly LoginMapper _loginMapper;
        private readonly RegisterMapper _registerMapper;
        
        public AccountController(IAppBll bll, IMapper mapper) 
        {
            _bll = bll;
            _loginMapper = new LoginMapper(mapper);
            _registerMapper = new RegisterMapper(mapper);
        }

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
                return Ok(res);
            }
            
            return NotFound(new Error("User/Password problem!"));
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            var res = await _bll.Accounts.Register(_registerMapper.Map(dto)!);

            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(new Error("Error registering! Try some other email"));
        }
    }
}
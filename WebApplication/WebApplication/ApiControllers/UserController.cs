using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Identity;
using PublicApi.DTO.v1.Mappers;

namespace WebApplication.ApiControllers
{
    /// <summary>
    /// Car Access controller for managing car accesses
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserMapper _mapper;

        /// <summary>
        /// Car Access Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public UserController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserMapper(mapper);
        }

        // GET: api/CarAccess
        /// <summary>
        /// Get All car accesses for user 
        /// </summary>
        /// <returns>Car accesses</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<IEnumerable<AppUser>>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetCarAccesses()
        {
            return (await _bll.Users.GetAllAsync())
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

    }
}
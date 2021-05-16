using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PublicApi.DTO.v1;

namespace WebApplication.ApiControllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LangController : ControllerBase
    {
        private readonly ILogger<LangController> _logger;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        /// <inheritdoc />
        public LangController(ILogger<LangController> logger, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _logger = logger;
            _localizationOptions = localizationOptions;
        }

        /// <summary>
        /// Get All supported languages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public  ActionResult<IEnumerable<SupportedLanguage>> GetSupportedLanguages()
        {
            var res = _localizationOptions.Value.SupportedUICultures.Select(c => new SupportedLanguage()
            {
                Name = c.Name,
                NativeName = c.NativeName,
            });
            return Ok(res);
        }
        
        /// <summary>
        /// Get language resource
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult<LangResources> GetLangResources()
        {
            return Ok(JsonConvert.SerializeObject(new LangResources()));
        }
    }

}
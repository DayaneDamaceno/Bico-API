using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTools_NTS.Util;

namespace Bico.Api.v1.Controllers
{
    [ApiVersion(1.0)]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AcessoController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AcessoTokenDto>> Login([FromQuery] AcessoDto acesso)
        {
            return new AcessoTokenDto() {
                Email = acesso.email,
                Token = await _authenticateService.Authenticate(acesso.email, acesso.senha)
            };
        }
    }
}

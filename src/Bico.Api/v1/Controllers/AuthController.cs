using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult GerarToken(GerarTokenRequestDto usuarioDto)
    {
        var usuario = usuarioDto.ToEntity();
        var token = _authService.GenerateToken(usuario);

        return Ok(new { id = usuario.Id, token });
    }

}


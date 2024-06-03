using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/autenticacao")]
public class AutenticacaoController : ControllerBase
{
    private readonly IAuthenticateService _authenticateService;

    public AutenticacaoController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UsuarioDto usuario)
    {
        var (id, token) = await _authenticateService.Authenticate(usuario.Email, usuario.Senha);

        if(string.IsNullOrEmpty(token))
            return Unauthorized();
        
        return Ok( new { id, token });
    }
}

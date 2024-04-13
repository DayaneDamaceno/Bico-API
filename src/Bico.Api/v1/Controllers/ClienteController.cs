using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/clientes")]
public class ClienteController : ControllerBase
{
    private readonly IPrestadorService _prestadorService;

    public ClienteController(IPrestadorService prestadorService)
    {
        _prestadorService = prestadorService;
    }

    [HttpGet("{clienteId}/prestadores/proximos")]
    public async Task<ActionResult> ObterPrestadoresMaisProximos(int clienteId, [FromQuery] int habilidade, [FromQuery] int pagina)
    {
        var prestadores = await _prestadorService.ObterPrestadoresMaisProximosAsync(clienteId, habilidade, pagina);
        var prestadoresDto = prestadores.Select(p => new PrestadorDto(p)).ToList();

        return Ok(prestadoresDto);
    }

    


}

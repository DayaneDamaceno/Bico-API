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
    private readonly IPrestadorRepository _prestadorRepository;

    public ClienteController(IPrestadorRepository prestadorRepository)
    {
        _prestadorRepository = prestadorRepository;
    }

    [HttpGet("{clienteId}/prestadores/proximos")]
    public async Task<ActionResult> ObterPrestadoresMaisProximos(int clienteId, [FromQuery]int habilidade)
    {
        var prestadores = await _prestadorRepository.ObterPrestadoresMaisProximosAsync(clienteId, habilidade);
        var prestadoresDto = prestadores.Select(p => new PrestadorDto(p)).ToList();

        return Ok(prestadoresDto);
    }


}

using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Domain.Interfaces;
using Bico.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/prestadores")]
public class PrestadorController : ControllerBase
{
    private readonly IPrestadorService _prestadorRepository;

    public PrestadorController(IPrestadorService prestadorRepository)
    {
        _prestadorRepository = prestadorRepository;
    }      

    [HttpGet]
    public async Task<ActionResult> GetBuscaPrestador([FromQuery] int numero)
    {
        var prestadores = await _prestadorRepository.ObterPrestador(numero);
        var prestadoresDto = prestadores.Select(p => new PrestadorDto(p)).ToList();

        return Ok(prestadoresDto);
    }
}



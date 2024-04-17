using Asp.Versioning;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/habilidades")]
public class HabilidadeController : ControllerBase
{
    private readonly IHabilidadeRepository _habilidadeRepository;

    public HabilidadeController(IHabilidadeRepository habilidadeRepository)
    {
        _habilidadeRepository = habilidadeRepository;
    }

    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult> Get(int categoriaId)
    {
        var habilidades = await _habilidadeRepository.ListarHabilidades(categoriaId);

        return Ok(habilidades);
    }

    [HttpGet]
    public async Task<ActionResult> GetBuscaHabilidade([FromQuery]string texto)
    {
        var habilidades = await _habilidadeRepository.ObterHabilidadesBusca(texto);

        return Ok(habilidades);
    }

}



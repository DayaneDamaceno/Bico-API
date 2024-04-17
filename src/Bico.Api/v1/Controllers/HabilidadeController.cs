using Asp.Versioning;
using Bico.Api.v1.Models;
using Bico.Domain.Interfaces;
using Bico.Infra.Repositories;
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

    [HttpGet("{categoriaId}")]
    public async Task<ActionResult> Get(int categoriaId)
    {
        var habilidades = await _habilidadeRepository.ListarHabilidades(categoriaId);

        return Ok(habilidades);
    }

}



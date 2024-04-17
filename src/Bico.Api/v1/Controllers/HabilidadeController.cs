using Bico.Api.v1.Models;
using Bico.Domain.Interfaces;
using Bico.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiController]
[Route("[controller]")]
public class HabilidadeController : ControllerBase
{
    private readonly IHabilidadeRepository _habilidadeRepository;

    public HabilidadeController(IHabilidadeRepository habilidadeRepository)
    {
        _habilidadeRepository = habilidadeRepository;
    }

    [HttpGet(Name = "idCategoria")]
    public async Task<ActionResult> Get(int idCategoria)
    {
        var habilidades = await _habilidadeRepository.ListarHabilidades(idCategoria);

        return Ok(habilidades);
    }

}



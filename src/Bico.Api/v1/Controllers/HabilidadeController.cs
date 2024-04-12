using Bico.Api.v1.Models;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

    [HttpGet("")]
    public async Task<ActionResult> Get()
    {
        var habilidades = await _habilidadeRepository.ObterHabilidades();       
       
        return Ok(habilidades);
    }

     [HttpGet("Buscar/{texto}")]
    public async Task<ActionResult> GetBuscaHabilidade(string texto)
     {
        var habilidades = await _habilidadeRepository.ObterHabilidadesBusca(texto);

        return Ok(habilidades);
     }
    /*
    [HttpPost(Name = "")]
     public ActionResult CriarHabilidade(Habilidade habilidade)
     {
          return Ok(habilidade);
     }*/

}



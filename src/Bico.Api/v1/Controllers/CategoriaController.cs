using Bico.Api.v1.Models;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bico.Api.v1.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet("")]
    public async Task<ActionResult> Get()
    {
        var categorias = await _categoriaRepository.ObterCategorias();

        return Ok(categorias);
    }
  
    [HttpGet("Buscar/{texto}")]
    public async Task<ActionResult> GetBuscaCategoria(string texto)
    {
        var categorias = await _categoriaRepository.ObterCategoriasBusca(texto);

        return Ok(categorias);
    }

    /*[HttpPost("")]
    public ActionResult CriarCategoria(Categoria categoria)
    {
        return Ok(categoria);
    }*/
}



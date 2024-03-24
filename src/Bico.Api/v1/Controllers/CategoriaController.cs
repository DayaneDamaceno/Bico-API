using Bico.Api.v1.Models;
using Bico.Domain.Interfaces;
using Bico.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet(Name = "")]
    public async Task<ActionResult> Get()
    {
        var categorias = await _categoriaRepository.ObterCategorias();

        return Ok(categorias);
    }

    [HttpPost(Name = "")]
    public ActionResult CriarCategoria(Categoria categoria)
    {

        return Ok(categoria);
    }

}



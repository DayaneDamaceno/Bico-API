using Asp.Versioning;
using Bico.Api.v1.Models;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/categorias")]
[Authorize]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var categorias = await _categoriaRepository.ObterCategorias();

        return Ok(categorias);
    }

    [HttpPost]
    public ActionResult CriarCategoria(CategoriaDto categoria)
    {

        return Ok(categoria);
    }

}



using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Api.v1.Models;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/usuarios")]
public class usuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public usuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    [HttpGet]
    public async Task<ActionResult> GetUsuario([FromQuery] int id)
    {
        var retorno = await _usuarioRepository.ObterUsuario(id);
        //var usuarioDto = retorno.Select(p => new UsuarioDto(p)).ToList();
        var retornoLast = retorno.LastOrDefault();
        var usuarioDto = new UsuarioAlteraDto(retornoLast.Id, retornoLast.Nome, retornoLast.AvatarUrl, retornoLast.Cpf, retornoLast.Email, retornoLast.Senha, retornoLast.Localizacao.ToString());

        return Ok(usuarioDto);
    }


    [HttpPost("altera")]
    public async Task<ActionResult> AlteraUsuario(UsuarioAlteraDto usuarioDto)
    {
        var usuario = new Usuario();
        usuario.Id = usuarioDto.Id;
        usuario.Nome = usuarioDto.Nome;
        usuario.AvatarUrl = usuarioDto.AvatarUrl;
        usuario.Cpf = usuarioDto.Cpf;
        usuario.Email = usuarioDto.Email;
        usuario.Senha = usuarioDto.Senha;
        //usuario.Localizacao = usuarioDto.Localizacao;
        var retorno = await _usuarioRepository.AtualizaUsuario(usuario);
        return Ok(retorno);
    }

}

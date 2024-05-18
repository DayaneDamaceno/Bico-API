using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/chat")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IChatRepository _chatRepository;

    public ChatController(IChatService chatService, IChatRepository chatRepository)
    {
        _chatService = chatService;
        _chatRepository = chatRepository;
    }

    [HttpPost("mensagem")]
    //[Authorize]
    public async Task<ActionResult> EnviarMensagem(MensagemDto mensagem)
    {
        var mensagemEntity = mensagem.ToEntity();
        await _chatService.EnviaMensagemParaFila(mensagemEntity);

        return Ok("Mensagem enviada com sucesso");
    }

    [HttpGet("conversa/{usuarioId1}/{usuarioId2}")]
    public async Task<ActionResult> ObterConversaEntreUsuarios(int usuarioId1, int usuarioId2)
    {
        var conversa = await _chatRepository.ObterConversaEntreUsuariosAsync(usuarioId1, usuarioId2);

        return Ok(conversa);
    }

    [HttpGet("conversas/recentes/{usuarioId}")]
    public async Task<ActionResult> ObterConversasRecentes(int usuarioId)
    {

        var mensagens = await _chatService.ObterConversasRecentes(usuarioId);

        var conversas = mensagens.Select(m => new ConversaRecenteDto(m, usuarioId)).ToList();

        return Ok(conversas);

    }
}

using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Api.v1.Hubs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/chat")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IChatService chatService, IChatRepository chatRepository, IHubContext<ChatHub> hubContext)
    {
        _chatService = chatService;
        _chatRepository = chatRepository;
        _hubContext = hubContext;
    }

    [HttpPost("mensagem")]
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
        var conversas = await _chatService.ObterConversasRecentes(usuarioId);

        return Ok(conversas);
    }

    [HttpPatch("mensagens/{mensagemId}/ler")]
    public async Task<ActionResult> MarcarMensagemComoLida(int mensagemId)
    {
        var mensagem = await _chatRepository.MarcarMensagemComoLidaAsync(mensagemId);

        var connections = ChatHub.GetConnections(mensagem.RemetenteId.ToString());
        foreach (var connectionId in connections)
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveReadingUpdate", mensagemId);

     
        return Ok();
    }

    [HttpPatch("mensagens/ler")]
    public async Task<ActionResult> MarcarMensagemComoLida(MarcarMensagemComoLidaRequestDto request)
    {
        var mensagens = await _chatRepository.MarcarMensagensComoLidaAsync(request.MensagemIds);
        var remetenteId = mensagens.First().RemetenteId;
        var connections = ChatHub.GetConnections(remetenteId.ToString());

        foreach (var connectionId in connections)
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveReadingUpdate", request.MensagemIds);


        return Ok();
    }
}

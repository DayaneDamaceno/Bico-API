using Asp.Versioning;
using Bico.Api.v1.DTOs;
using Bico.Api.v1.Hubs;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Bico.Api.v1.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("v{version:apiVersion}/acordos")]
[Authorize]
public class AcordoController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IAcordoService _acordoService;

    public AcordoController(IHubContext<ChatHub> hubContext, IAcordoService acordoService)
    {
        _hubContext = hubContext;
        _acordoService = acordoService;
    }

    [HttpPost]
    public async Task<ActionResult> CriarAcordo(AcordoRequestDto request)
    {
        var acordo = request.ToEntity();
        var mensagem = await _acordoService.CriarAcordoAsync(acordo, request.DestinatarioId, request.RemetenteId);

        var connections = ChatHub.GetConnections(mensagem.DestinatarioId.ToString());
        foreach (var connectionId in connections)
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", mensagem);
        return Ok();
    }

    [HttpPatch("{id:int}/{aceito:bool}")]
    public async Task<ActionResult> ResponderAcordo(int id, bool aceito)
    {
        var (acordo, mensagemResposta) = await _acordoService.AlterarAcordoAsync(id, aceito);

        var connections = ChatHub.GetConnections(acordo.Mensagem.RemetenteId.ToString());
        foreach (var connectionId in connections)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("UpdateAcordo", acordo);
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", mensagemResposta);
        }
        return Ok();
    }
}

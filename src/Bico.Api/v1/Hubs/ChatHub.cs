using Bico.Api.v1.DTOs;
using Bico.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Bico.Api.v1.Hubs;

public class ChatHub : Hub
{
    private static readonly ConnectionDto<string> _connections = new();

    public async Task SendMessageToUser(string receiverUserName, Mensagem message)
    {
        foreach (var connectionId in _connections.GetConnections(receiverUserName))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }

    public async Task EnviarAtualizacaoDeLeitura(string receiverUserName, int mensagemId)
    {
        foreach (var connectionId in _connections.GetConnections(receiverUserName))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveReadingUpdate", mensagemId);
        }
    }

    public override Task OnConnectedAsync()
    {
        string userId = Context.User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Task.CompletedTask;

        _connections.Add(userId, Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        string userId = Context.User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Task.CompletedTask;

        _connections.Remove(userId, Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public static IEnumerable<string> GetConnections(string userId)
    {
        return _connections.GetConnections(userId);
    }
}
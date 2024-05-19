using Azure.Messaging.ServiceBus;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Bico.Chat.Functions;

public class Functions
{
    private readonly ILogger<Functions> _logger;
    private readonly IChatService _chatService;
    private readonly HubConnection _connectionHub;

    public Functions(ILogger<Functions> logger, IChatService chatService, IAuthService authService)
    {
        _logger = logger;
        _chatService = chatService;
        var token = authService.GenerateToken(new Usuario() { Id = 0, Nome = "Azure Function" });
        _connectionHub = new HubConnectionBuilder()
                                //.WithUrl("https://bico-api-hml.azurewebsites.net/hub/chat", x => x.AccessTokenProvider = () => Task.FromResult(token))
                                .WithUrl("http://192.168.0.8:5283/hub/chat", x => x.AccessTokenProvider = () => Task.FromResult(token))
                                .Build();
    }

    [Function("ProcessarMensagens")]
    public async Task Run(
        [ServiceBusTrigger("chatmessagesqueue", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {Id}", message.MessageId);
        _logger.LogInformation("Message Body: {Body}", message.Body);
        _logger.LogInformation("Message Content-Type: {ContentType}", message.ContentType);

        //se eu ja salvei a msg nao posso salvar dnv
        var mensagem = await _chatService.SalvarMensagem(message.Body);


        //verifico se o usuario final esta online
        await _connectionHub.StartAsync();
        await _connectionHub.InvokeAsync("SendMessageToUser", mensagem.DestinatarioId.ToString(), mensagem);


        await messageActions.CompleteMessageAsync(message);
    }
}

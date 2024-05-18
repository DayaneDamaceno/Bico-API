using Azure.Messaging.ServiceBus;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bico.Infra.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly BicoContext _context;
    private readonly ILogger<ChatRepository> _logger;

    public ChatRepository(ServiceBusClient serviceBusClient, BicoContext context, ILogger<ChatRepository> logger)
    {
        _serviceBusClient = serviceBusClient;
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Mensagem>> ObterConversaEntreUsuariosAsync(int usuarioId1, int usuarioId2)
    {
        var conversa = await _context.Mensagens.AsNoTracking()
                                        .Where(m => (m.RemetenteId == usuarioId1 && m.DestinatarioId == usuarioId2)
                                                    || (m.RemetenteId == usuarioId2 && m.DestinatarioId == usuarioId1))
                                        .OrderBy(m => m.EnviadoEm)
                                        .ToListAsync();

        return conversa;
    }

    public async Task<IEnumerable<Mensagem>> ObterConversasRecentesAsync(int usuarioId)
    {
        var conversa = await _context.Mensagens.AsNoTracking()
                                        .Include(x => x.Destinatario)
                                        .Include(x => x.Remetente)
                                        .Where(m => m.RemetenteId == usuarioId || m.DestinatarioId == usuarioId)
                                        .OrderByDescending(m => m.EnviadoEm)
                                        .ToListAsync();

        return conversa;
    }

    public async Task SalvarMensagemAsync(Mensagem mensagem)
    {
        await _context.Mensagens.AddAsync(mensagem);
        await _context.SaveChangesAsync();
    }

    public async Task SendMessageAsync(string messageBody)
    {
        var queueName = "chatmessagesqueue";

        var sender = _serviceBusClient.CreateSender(queueName);


        var message = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(message);

        _logger.LogInformation("Sent a single message to the queue: {queueName}", queueName);
    }
}

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

    public async Task<IEnumerable<Mensagem>> ObterConversaEntreUsuariosAsync(int usuarioIdA, int usuarioIdB)
    {
        var conversa = await _context.Mensagens.AsNoTracking()
                                        .Include(m => m.Acordo)
                                        .Where(m => (m.RemetenteId == usuarioIdA && m.DestinatarioId == usuarioIdB)
                                                    || (m.RemetenteId == usuarioIdB && m.DestinatarioId == usuarioIdA))
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
                                        .OrderBy(m => m.MensagemLida)
                                        .ThenByDescending(m => m.EnviadoEm)
                                        .ToListAsync();

        return conversa;
    }

    public async Task<Dictionary<int, int>> ObterContagemMensagensNaoLidasAsync(int usuarioId)
    {
        var contagem = await _context.Mensagens
                                     .Where(m => m.DestinatarioId == usuarioId && !m.MensagemLida)
                                     .GroupBy(m => m.RemetenteId)
                                     .Select(g => new { RemetenteId = g.Key, Contagem = g.Count() })
                                     .ToDictionaryAsync(g => g.RemetenteId, g => g.Contagem);

        return contagem;
    }

    public async Task<Mensagem> MarcarMensagemComoLidaAsync(int mensagemId)
    {
        var mensagem = await _context.Mensagens.FirstOrDefaultAsync(m => m.Id == mensagemId);

        if (mensagem is not null && !mensagem.MensagemLida)
        {
            mensagem.MensagemLida = true;
            await _context.SaveChangesAsync();
        }

        return mensagem;
    }

    public async Task<IEnumerable<Mensagem>> MarcarMensagensComoLidaAsync(IEnumerable<int> ids)
    {
        var mensagens = await _context.Mensagens
                                       .Where(m => ids.Contains(m.Id))
                                       .ToListAsync();

        foreach (var mensagem in mensagens)
        {
            mensagem.MensagemLida = true;
        }

        await _context.SaveChangesAsync();
        return mensagens;
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

        _logger.LogInformation("Sent a single message to the queue: {QueueName}", queueName);
    }

}

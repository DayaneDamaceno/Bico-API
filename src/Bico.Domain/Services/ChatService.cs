using Bico.Domain.DTOs;
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.ValueObjects;
using System.Text.Json;

namespace Bico.Domain.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IAcordoRepository _acordoRepository;
    private readonly IAvatarRepository _avatarRepository;

    public ChatService(IChatRepository chatRepository, IAcordoRepository acordoRepository, IAvatarRepository avatarRepository)
    {
        _chatRepository = chatRepository;
        _acordoRepository = acordoRepository;
        _avatarRepository = avatarRepository;
    }

    public async Task EnviaMensagemParaFila(Mensagem mensagem)
    {
        string mensagemJson = JsonSerializer.Serialize(mensagem);
        await _chatRepository.SendMessageAsync(mensagemJson);
    }

    public async Task<Mensagem> SalvarMensagem(BinaryData mensagemJson)
    {
        var mensagem = JsonSerializer.Deserialize<Mensagem>(mensagemJson);

        //TO DO: if (mensagem is null) tratar retorno;

        await _chatRepository.SalvarMensagemAsync(mensagem);

        return mensagem;
    }

    public async Task<IEnumerable<ConversaRecenteDto>> ObterConversasRecentes(int usuarioId)
    {
        var mensagens = await _chatRepository.ObterConversasRecentesAsync(usuarioId);
        var contagemNaoLidas = await _chatRepository.ObterContagemMensagensNaoLidasAsync(usuarioId);


        var conversasAgrupadas = mensagens
                                   .GroupBy(m => new { MinId = Math.Min(m.RemetenteId, m.DestinatarioId), MaxId = Math.Max(m.RemetenteId, m.DestinatarioId) })
                                   .Select(g => g.First())
                                   .ToList();
       
        var conversas = conversasAgrupadas.Select(m =>
        {
            var avatarFileName = m.RemetenteId == usuarioId ? m.Destinatario.AvatarFileName : m.Remetente.AvatarFileName;
            var dto = new ConversaRecenteDto(m, usuarioId)
            {
                AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(avatarFileName)
            };
            dto.QuantidadeMensagensNaoLidas = contagemNaoLidas.TryGetValue(dto.Id, out int value) ? value : 0;
            return dto;
        });

        return conversas;
    }

    
}

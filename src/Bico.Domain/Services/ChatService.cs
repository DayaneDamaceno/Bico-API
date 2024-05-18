using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using System.Text.Json;

namespace Bico.Domain.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IAvatarRepository _avatarRepository;

    public ChatService(IChatRepository chatRepository, IAvatarRepository avatarRepository)
    {
        _chatRepository = chatRepository;
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

    public async Task<IEnumerable<Mensagem>> ObterConversasRecentes(int usuarioId)
    {
        var mensagens = await _chatRepository.ObterConversasRecentesAsync(usuarioId);

        var conversasAgrupadas = mensagens
                                   .GroupBy(m => new { MinId = Math.Min(m.RemetenteId, m.DestinatarioId), MaxId = Math.Max(m.RemetenteId, m.DestinatarioId) })
                                   .Select(g => g.First())
                                   .ToList();

        conversasAgrupadas.ForEach(mensagem =>
        {
            mensagem.Remetente.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(mensagem.Remetente.AvatarFileName);
            mensagem.Destinatario.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(mensagem.Destinatario.AvatarFileName);
        });

        return conversasAgrupadas;
    }
}

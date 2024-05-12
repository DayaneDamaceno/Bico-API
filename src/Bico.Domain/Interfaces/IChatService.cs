using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IChatService
{
    Task EnviaMensagemParaFila(Mensagem mensagem);
    Task<Mensagem> SalvarMensagem(BinaryData mensagemJson);
    Task<IEnumerable<Mensagem>> ObterConversasRecentes(int usuarioId);
}

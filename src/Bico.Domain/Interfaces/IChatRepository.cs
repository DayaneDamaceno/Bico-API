using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IChatRepository
{
    Task SendMessageAsync(string messageBody);
    Task SalvarMensagemAsync(Mensagem mensagem);
    Task<IEnumerable<Mensagem>> ObterConversaEntreUsuariosAsync(int usuarioIdA, int usuarioIdB);
    Task<IEnumerable<Mensagem>> ObterConversasRecentesAsync(int usuarioId);
    Task<Mensagem> MarcarMensagemComoLidaAsync(int mensagemId);
    Task<IEnumerable<Mensagem>> MarcarMensagensComoLidaAsync(IEnumerable<int> ids);
    Task<Dictionary<int, int>> ObterContagemMensagensNaoLidasAsync(int usuarioId);

}

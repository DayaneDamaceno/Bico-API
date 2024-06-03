using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces
{
    public interface IAcordoService
    {
        Task<Mensagem> CriarAcordoAsync(Acordo acordo, int destinarioId, int remetenteId);
        Task<(Acordo, Mensagem)> AlterarAcordoAsync(int id, bool aceito);
    }
}
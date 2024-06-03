

using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces
{
    public interface IAcordoRepository
    {
        Task SalvarAcordoAsync(Acordo acordo);
        Task<Acordo> AtualizarAcordoAsync(int id, bool aceito);
    }
}

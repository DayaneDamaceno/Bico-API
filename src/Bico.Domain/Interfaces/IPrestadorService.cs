using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IPrestadorService
{
    Task<IEnumerable<Prestador>> ObterPrestadoresMaisProximosAsync(int clientId, int habilidadeId, int pagina);
    Task<List<Prestador>> ObterPrestador(int prestadorId);

}

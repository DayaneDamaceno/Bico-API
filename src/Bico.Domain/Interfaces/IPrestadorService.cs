using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IPrestadorService
{
    Task<List<Prestador>> ObterPrestadoresMaisProximosAsync(int clientId, int habilidadeId, int pagina);
}

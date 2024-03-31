using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IPrestadorRepository
{
    Task<List<Prestador>> ObterPrestadoresMaisProximosAsync(int clienteId, int habilidadeId);
}

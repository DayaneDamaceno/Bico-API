using Bico.Domain.Entities;
using Bico.Domain.ValueObjects;

namespace Bico.Domain.Interfaces;

public interface IPrestadorRepository
{
    Task<List<Prestador>> ObterPrestadoresMaisProximosAsync(int clienteId, int habilidadeId, Paginacao paginacao);
    Task<List<Prestador>> ObterPrestador(int prestadorId);

}

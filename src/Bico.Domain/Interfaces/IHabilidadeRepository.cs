using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IHabilidadeRepository
{
    Task<List<Habilidade>> ListarHabilidades(int categoriaId);
}

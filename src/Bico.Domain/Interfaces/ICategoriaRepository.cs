using Bico.Domain.Entities;


namespace Bico.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObterCategorias();
    }
}

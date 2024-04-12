using Bico.Domain.Entities;


namespace Bico.Domain.Interfaces
{
    public interface IHabilidadeRepository
    {
        Task<List<Habilidade>> ObterHabilidades();
        Task<List<Habilidade>> ObterHabilidadesBusca(string textoPesquisa);
    }
}

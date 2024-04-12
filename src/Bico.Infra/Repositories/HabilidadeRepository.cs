using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Repositories;

public class HabilidadeRepository : IHabilidadeRepository
{
    private readonly BicoContext _context;


    public HabilidadeRepository(BicoContext bicoContext)
    {
        _context = bicoContext;
    }

    public async Task<List<Habilidade>> ObterHabilidades()
    {
        return await _context.Habilidades.ToListAsync();         
    }

    public async Task<List<Habilidade>> ObterHabilidadesBusca(string textoPesquisa)
    {
        return await _context.Habilidades.Where(x => x.Nome.Contains(textoPesquisa)).ToListAsync();

    }
}

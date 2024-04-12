using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly BicoContext _context;


    public CategoriaRepository(BicoContext bicoContext)
    {
        _context = bicoContext;
    }

    public async Task<List<Categoria>> ObterCategorias()
    {
        var categorias = await _context.Categorias.ToListAsync();

        return categorias;
    }

    public async Task<List<Categoria>> ObterCategoriasBusca(string textoPesquisa)
    {
        return await _context.Categorias.Where(x => x.Nome.Contains(textoPesquisa)).ToListAsync();
    }
}

using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Bico.Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Repositories;

public class PrestadorRepository : IPrestadorRepository
{
    private readonly BicoContext _context;

    public PrestadorRepository(BicoContext bicoContext)
    {
        _context = bicoContext;
    }

    public async Task<List<Prestador>> ObterPrestadoresMaisProximosAsync(int clienteId, int habilidadeId)
    {
        var localizacaoDoCliente = await _context.Clientes
                             .Where(c => c.Id == clienteId)
                             .Select(c => c.Localizacao)
                             .FirstOrDefaultAsync();

        var prestadoresProximos = _context.Prestadores
            .Where(p => p.Habilidades.Any(h => h.Id == habilidadeId))
            .Where(p => p.Localizacao.StDWithin(localizacaoDoCliente, p.RaioDeAlcance, true))
            .OrderBy(p => p.Localizacao.StDistance(localizacaoDoCliente, true))
            .ToList();      


        return prestadoresProximos;
    }
}

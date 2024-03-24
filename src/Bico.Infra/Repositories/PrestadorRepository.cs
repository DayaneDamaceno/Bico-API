using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;

namespace Bico.Infra.Repositories;

public class PrestadorRepository : IPrestadorRepository
{
    private readonly BicoContext _context;

    public PrestadorRepository(BicoContext bicoContext)
    {
        _context = bicoContext;
    }
}

using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Repositories;

public class AcordoRepository : IAcordoRepository
{
    private readonly BicoContext _context;

    public AcordoRepository(BicoContext context)
    {
        _context = context;
    }

    public async Task SalvarAcordoAsync(Acordo acordo)
    {
        await _context.Acordos.AddAsync(acordo);
        await _context.SaveChangesAsync();
    }

    public async Task<Acordo> AtualizarAcordoAsync(int id, bool aceito)
    {
        var acordo = await _context.Acordos.Include(a => a.Mensagem)
                                           .FirstOrDefaultAsync(a => a.Id == id);

        if (acordo is not null)
        {
            acordo.Resposta = aceito;
            acordo.RespondidoEm = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return acordo;
    }
}

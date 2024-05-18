using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BicoContext _context;

    public UsuarioRepository(BicoContext bicoContext)
    {
        _context = bicoContext;
    }

    public async Task<Usuario> ObterUsuarioPorEmail(string email)
    {
        var usuario = await _context.Usuarios
            .Where(a => a.Email == email)
            .FirstOrDefaultAsync();

        return usuario;
    }
}
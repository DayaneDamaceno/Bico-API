using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Infra.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Bico.Infra.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BicoContext _context;
    private readonly IAvatarRepository _avatarRepository;

    public UsuarioRepository(BicoContext bicoContext, IAvatarRepository avatarRepository)
    {
        _context = bicoContext;
        _avatarRepository = avatarRepository;

    }

    public async Task<Usuario> ObterUsuarioPorEmail(string email)
    {
        var usuario = await _context.Usuarios
            .Where(a => a.Email == email)
            .FirstOrDefaultAsync();

        return usuario;
    }
    public async Task<List<Usuario>> ObterUsuario(int Id)
    {
        var usuario = await _context.Usuarios.Where(x => x.Id.Equals(Id)).ToListAsync();
        usuario.ForEach(u =>
        {
            u.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(u.AvatarFileName, "avatar");
        });

        return usuario;
    }

    public async Task<bool> AtualizaUsuario(Usuario usuarioAtualizado)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioAtualizado.Id);

        if (usuario != null)
        {
            usuario.Cpf = usuarioAtualizado.Cpf;
            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Senha = usuarioAtualizado.Senha;
            usuario.Email = usuarioAtualizado.Email;

            await _context.SaveChangesAsync();

            return true;
        }

        return true; // Retorna false se o usuário não for encontrado
    }
}
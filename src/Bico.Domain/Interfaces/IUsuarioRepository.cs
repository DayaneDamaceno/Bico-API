using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> ObterUsuarioPorEmail(string email);
    Task<List<Usuario>> ObterUsuario(int Id);

    Task<bool> AtualizaUsuario(Usuario usuarioAtualizado);

}

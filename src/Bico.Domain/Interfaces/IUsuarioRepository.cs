using Bico.Domain.Entities;


namespace Bico.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObterUsuario(int Id);

        Task<bool> AtualizaUsuario(Usuario usuarioAtualizado);
    }
}

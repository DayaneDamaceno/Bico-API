using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> ObterUsuarioPorEmail(string email);
}

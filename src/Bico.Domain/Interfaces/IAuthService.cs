using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IAuthService
{
    string GenerateToken(Usuario usuario);
}

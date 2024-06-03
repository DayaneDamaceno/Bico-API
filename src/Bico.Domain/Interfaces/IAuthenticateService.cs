using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IAuthenticateService
{
    Task<(int id, string token)> Authenticate(string email, string senha);
    public string GerarToken(Usuario usuario);
}

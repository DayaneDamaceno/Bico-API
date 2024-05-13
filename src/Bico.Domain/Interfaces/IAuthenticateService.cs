using Bico.Domain.Entities;

namespace Bico.Domain.Interfaces;

public interface IAuthenticateService
{
    Task<string> Authenticate(string email, string senha);
    public string GerarToken(Usuario usuario);
}

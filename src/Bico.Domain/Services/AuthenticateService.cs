using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bico.Domain.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUsuarioRepository _context;
    private readonly IConfiguration _configuration;

    public AuthenticateService(IUsuarioRepository context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<(int id, string token)> Authenticate(string email, string senha)
    {
        var usuario = await _context.ObterUsuarioPorEmail(email);
        var senhaEhValida = ValidarSenha(usuario, senha);

        if (usuario != null && senhaEhValida)
            return (usuario.Id, GerarToken(usuario));
        
        return (0, string.Empty);
    }

    public string GerarToken(Usuario usuario)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"]));
        var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["EncryptionKey"]));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Sid, usuario.Id.ToString()),
                new(ClaimTypes.Email, usuario.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
            EncryptingCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256)
        };

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public string TransformarSenhaEmHash(Usuario usuario, string senha)
    {
        var hasher = new PasswordHasher<Usuario>();
        return hasher.HashPassword(usuario, senha);
    }

    public bool ValidarSenha(Usuario usuario, string senha)
    {
        var hasher = new PasswordHasher<Usuario>();

        var resultado = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);

        return resultado == PasswordVerificationResult.Success;
    }

}

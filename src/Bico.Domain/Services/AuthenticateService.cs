using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Bico.Domain.Services
{

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAcessoRepository _context;

        public AuthenticateService(IAcessoRepository context, IConfiguration configuration)
        {
            _context = context;
        }

        public async Task<string> Authenticate(string email, string senha)
        {
            var usuario = await _context.ObterUsuarioPorEmail(email);

            if (usuario != null && usuario.Senha == senha) {

                return GenerateToken(usuario);
            }

            return string.Empty;
        }

        //public string GenerateToken(int id, string email)
        //{
        //    var claims = new[]
        //    {
        //        new Claim("id", id.ToString()),
        //        new Claim("email", email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("crossfit"));

        //    var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        //    JwtSecurityToken token = new JwtSecurityToken(issuer: "bicoApp", 
        //        audience: "Bicoapp.dayduguiwes", 
        //        claims: claims, 
        //        expires: DateTime.UtcNow.AddHours(1),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public string GenerateToken(Acesso acesso)
        {
            try { 
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!jENA7_qLbR'@C/S?{3td?3{3]1i-+>/HBx_2{w6`B6YKkIqM@"));
            var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MpH(~j065o{*`2!d"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new(ClaimTypes.Sid, acesso.Id.ToString()),
            new(ClaimTypes.Email, acesso.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            }catch (Exception ex) { return string.Empty;}
        }

    }
}

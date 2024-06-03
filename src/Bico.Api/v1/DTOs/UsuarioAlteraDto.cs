using Bico.Domain.Entities;

namespace Bico.Api.v1.Models;

public class UsuarioAlteraDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Localizacao { get; set; }

    public UsuarioAlteraDto(int id, string nome, string avatarUrl, string cpf, string email, string senha, string localizacao)
    {
        Id = id;
        Nome = nome;
        AvatarUrl = avatarUrl;
        Cpf = cpf;
        Email = email;
        Senha = senha;
        Localizacao = "Teste";
    }

}

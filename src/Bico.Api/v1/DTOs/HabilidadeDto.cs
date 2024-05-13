using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs;

public class HabilidadeDto
{
    public HabilidadeDto(string nome, int id)
    {
        Id = id;
        Nome = nome;
    }

    public int Id { get; set; }
    public string Nome { get; set; }

    public HabilidadeDto(Habilidade habilidade)
    {
        Id = habilidade.Id;
        Nome = habilidade.Nome;
    }
}

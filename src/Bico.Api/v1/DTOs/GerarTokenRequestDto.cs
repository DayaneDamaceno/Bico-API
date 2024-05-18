using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs;

public class GerarTokenRequestDto
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public Usuario ToEntity()
    {
        return new Usuario()
        {
            Id = Id,
            Nome = Nome
        };
    }
}

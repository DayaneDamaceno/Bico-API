using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs;

public class PrestadorDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public double MediaEstrelas { get; set; }


    public PrestadorDto(Prestador prestador)
    {
        Id = prestador.Id;
        Nome = prestador.Nome;
        AvatarUrl = prestador.AvatarUrl;
        MediaEstrelas = prestador.MediaEstrelas;
    }
}

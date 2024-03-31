using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs;

public class PrestadorDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    //public IEnumerable<HabilidadeDto> Habilidades { get; set; }

    public PrestadorDto(Prestador prestador)
    {
        Id = prestador.Id;
        Nome = prestador.Nome;
        Latitude = prestador.Localizacao?.Coordinate.Y ?? 0;
        Longitude = prestador.Localizacao?.Coordinate.X ?? 0;
        AvatarUrl = prestador.AvatarUrl;
        //Habilidades = prestador.Habilidades.Select(x => new HabilidadeDto(x));
    }
}

using Bico.Api.v1.Models;
using Bico.Domain.Entities;
using System.Linq;

namespace Bico.Api.v1.DTOs;

public class PrestadorDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public double MediaEstrelas { get; set; }
    public string Sobre { get; set; }
    public List<HabilidadeDto> Habilidades { get; set; }
    public int RaioDeAlcance { get; set; }
    public List<FotoServico> FotosServico { get; set; }
    public List<AvaliacaoDto> Avaliacoes { get; set; }


    public PrestadorDto(Prestador prestador)
    {
        Id = prestador.Id;
        Nome = prestador.Nome;
        AvatarUrl = prestador.AvatarUrl;
        MediaEstrelas = prestador.MediaEstrelas;
        Sobre = prestador.Sobre;
        RaioDeAlcance = prestador.RaioDeAlcance;
        if (prestador.Habilidades is not null)
            Habilidades = prestador.Habilidades.Select(p => new HabilidadeDto(p)).ToList();
        if(prestador.Fotos is not null)
            FotosServico = [.. prestador.Fotos];
        if (prestador.Avaliacoes is not null)
            Avaliacoes = prestador.Avaliacoes.Select(p => new AvaliacaoDto(p)).ToList();
    }
}

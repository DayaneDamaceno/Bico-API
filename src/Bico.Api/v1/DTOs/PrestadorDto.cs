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
    public List<Avaliacao> Avaliacoes { get; set; }


    public PrestadorDto(Prestador prestador)
    {
        Id = prestador.Id;
        Nome = prestador.Nome;
        AvatarUrl = prestador.AvatarUrl;
        MediaEstrelas = prestador.MediaEstrelas;
        Sobre = prestador.Sobre;
        if (prestador.Habilidades != null)
            Habilidades = prestador.Habilidades.Select(p => new HabilidadeDto(p)).ToList();
        RaioDeAlcance = prestador.RaioDeAlcance;
        FotosServico = prestador.Fotos.ToList();
        Avaliacoes = prestador.Avaliacoes.ToList();
    }
}

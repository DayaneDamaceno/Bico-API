using Bico.Api.v1.Models;
using Bico.Domain.Entities;
using System.Linq;

namespace Bico.Api.v1.DTOs;

public class AvaliacaoDto
{
    public int Id { get; set; }
    public string Conteudo { get; set; }
    public double QuantidadeEstrelas { get; set; }
    public string ClienteNome { get; set; }
    public string AvatarUrl { get; set; }



    public AvaliacaoDto(Avaliacao? avaliacao)
    {
        Id = avaliacao.Id;
        Conteudo = avaliacao.Conteudo;
        QuantidadeEstrelas = avaliacao.QuantidadeEstrelas;
        if(avaliacao.Cliente is not null)
        {
            AvatarUrl = avaliacao.Cliente.AvatarUrl;
            ClienteNome = avaliacao.Cliente.Nome;
        }
    }
}

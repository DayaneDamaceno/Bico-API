using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs;

public class AcordoRequestDto
{
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public int RemetenteId { get; set; }
    public int DestinatarioId { get; set; }


    public Acordo ToEntity()
    {
        return new Acordo()
        {
            Descricao = Descricao,
            Valor = Valor
        };
    }
}

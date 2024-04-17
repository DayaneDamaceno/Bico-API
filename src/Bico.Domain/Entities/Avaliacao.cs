namespace Bico.Domain.Entities;

public class Avaliacao
{
    public int Id { get; set; }
    public int PrestadorId { get; set; }
    public int ClienteId { get; set; }
    public string Conteudo { get; set; }
    public int QuantidadeEstrelas { get; set; }

    public virtual Prestador Prestador { get; set; }
    public virtual Cliente Cliente { get; set; }
}


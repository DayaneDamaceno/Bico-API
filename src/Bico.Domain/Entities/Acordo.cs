

using System.Text.Json.Serialization;

namespace Bico.Domain.Entities;

public class Acordo
{
    public int Id { get; set; }
    public int MensagemId { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public bool? Resposta { get; set; }
    public DateTime? RespondidoEm { get; set; }

    [JsonIgnore]
    public virtual Mensagem Mensagem { get; set; }
}

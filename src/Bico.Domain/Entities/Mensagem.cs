using Bico.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Bico.Domain.Entities;

public class Mensagem
{
    public int Id { get; set; }

    public int RemetenteId { get; set; }

    public int DestinatarioId { get; set; }

    public string Conteudo { get; set; }

    public TipoMensagem Tipo { get; set; }

    public DateTime EnviadoEm { get; set; }

    public bool MensagemLida { get; set; }

    [JsonIgnore]
    public Usuario Remetente { get; set; }

    [JsonIgnore]
    public Usuario Destinatario { get; set; }

    public virtual Acordo Acordo { get; set; }
}

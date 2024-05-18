using Bico.Domain.Entities;

namespace Bico.Api.v1.DTOs;

public class MensagemDto
{
    public int RemetenteId { get; set; }
    public int DestinatarioId { get; set; }
    public string Conteudo { get; set; }
    public DateTime EnviadoEm => DateTime.UtcNow;

    public Mensagem ToEntity()
    {
        return new Mensagem()
        {
            Conteudo = Conteudo,
            DestinatarioId = DestinatarioId,
            EnviadoEm = EnviadoEm,
            RemetenteId = RemetenteId
        };
    }
}

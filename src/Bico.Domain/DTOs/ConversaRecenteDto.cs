using Bico.Domain.Entities;

namespace Bico.Domain.DTOs;

public class ConversaRecenteDto
{
    public ConversaRecenteDto(Mensagem mensagem, int usuarioId)
    {
        Id = mensagem.RemetenteId == usuarioId ? mensagem.DestinatarioId : mensagem.RemetenteId;
        Nome = mensagem.RemetenteId == usuarioId ? mensagem.Destinatario.Nome : mensagem.Remetente.Nome;
        AvatarUrl = mensagem.RemetenteId == usuarioId ? mensagem.Destinatario.AvatarUrl : mensagem.Remetente.AvatarUrl;
        UltimaMensagem = mensagem.Conteudo;
        DataUltimaMensagem = mensagem.EnviadoEm;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string AvatarUrl { get; set; }
    public string UltimaMensagem { get; set; }
    public DateTime DataUltimaMensagem { get; set; }
    public int QuantidadeMensagensNaoLidas { get; set; }

}

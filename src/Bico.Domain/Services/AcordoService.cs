
using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.ValueObjects;
using System.Globalization;

namespace Bico.Domain.Services;

public class AcordoService : IAcordoService
{
    private readonly IAcordoRepository _acordoRepository;
    private readonly IChatRepository _chatRepository;

    public AcordoService(IAcordoRepository acordoRepository, IChatRepository chatRepository)
    {
        _acordoRepository = acordoRepository;
        _chatRepository = chatRepository;
    }

    public async Task<(Acordo, Mensagem)> AlterarAcordoAsync(int id, bool aceito)
    {
        var acordoAlterado = await _acordoRepository.AtualizarAcordoAsync(id, aceito);
       
        var valor = acordoAlterado.Valor.ToString("C", new CultureInfo("pt-BR"));
        var conteudoResposta = aceito
            ? $"Aceito o acordo acima com o valor de: {valor}"
            : $"Recuso o acordo acima com o valor de: {valor}";

        var mensagemResposta = new Mensagem()
        {
            Tipo = TipoMensagem.Texto,
            Conteudo = conteudoResposta,
            DestinatarioId = acordoAlterado.Mensagem.RemetenteId,
            RemetenteId = acordoAlterado.Mensagem.DestinatarioId,
            EnviadoEm = DateTime.UtcNow
        };

        await _chatRepository.SalvarMensagemAsync(mensagemResposta);

        return (acordoAlterado, mensagemResposta);
    }

    public async Task<Mensagem> CriarAcordoAsync(Acordo acordo, int destinarioId, int remetenteId)
    {
        var mensagem = new Mensagem()
        {
            Tipo = TipoMensagem.Acordo,
            Conteudo = acordo.Descricao,
            DestinatarioId = destinarioId,
            RemetenteId = remetenteId,
            EnviadoEm = DateTime.UtcNow
        };

        await _chatRepository.SalvarMensagemAsync(mensagem);

        acordo.MensagemId = mensagem.Id;

        await _acordoRepository.SalvarAcordoAsync(acordo);
        return mensagem;
    }
}

using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bico.Domain.Services;

public class PrestadorService : IPrestadorService
{
    private readonly IPrestadorRepository _prestadorRepository;
    private readonly IAvatarRepository _avatarRepository;

    public PrestadorService(IPrestadorRepository prestadorRepository, IAvatarRepository avatarRepository)
    {
        _prestadorRepository = prestadorRepository;
        _avatarRepository = avatarRepository;
    }

    public async Task<List<Prestador>> ObterPrestador(int prestadorId)
    {
        var prestadores = await _prestadorRepository.ObterPrestador(prestadorId);

        if (prestadores is null)
            return prestadores;

        prestadores.ForEach(prestador =>
        {
            prestador.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(prestador.AvatarFileName, "avatar");
            prestador.FotosServico.ForEach(prestador =>
            {
                prestador.Foto = _avatarRepository.GerarAvatarUrlSegura(prestador.Foto, "servicos");
            });
        });


        return prestadores;
    }

    public async Task<IEnumerable<Prestador>> ObterPrestadoresMaisProximosAsync(int clientId, int habilidadeId, int pagina)
    {
        var paginacao = new Paginacao(pagina);

        var prestadores = await _prestadorRepository.ObterPrestadoresMaisProximosAsync(clientId, habilidadeId, paginacao);

        if (prestadores is null)
            return Enumerable.Empty<Prestador>();

        prestadores.ForEach(prestador =>
        {
            prestador.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(prestador.AvatarFileName, "avatar");
        });

        return prestadores;
    }

}

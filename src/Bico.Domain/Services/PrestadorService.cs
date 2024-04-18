using Bico.Domain.Entities;
using Bico.Domain.Interfaces;
using Bico.Domain.ValueObjects;

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

  

    public async Task<IEnumerable<Prestador>> ObterPrestadoresMaisProximosAsync(int clientId, int habilidadeId, int pagina)
    {
        var paginacao = new Paginacao(pagina);


        var prestadores = await _prestadorRepository.ObterPrestadoresMaisProximosAsync(clientId, habilidadeId, paginacao);

        if (prestadores is null)
            return Enumerable.Empty<Prestador>();

        prestadores.ForEach(prestador =>
        {
            prestador.AvatarUrl = _avatarRepository.GerarAvatarUrlSegura(prestador.AvatarFileName);
        });

        return prestadores;
    }

}
